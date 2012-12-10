Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.DBNull
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports UserControls.CC_Controls

Public Class CloseOutCommunications
    Inherits System.Web.UI.Page

    Public CloseOutMsg As System.Web.UI.WebControls.TextBox

    Private TailleDomaine As Integer = "LCLMTL\".Length
    Private strEMPName As String = User.Identity.Name.Substring(TailleDomaine)

    Sub LoadPage() 
        'Put user code to initialize the page here
        Dim valide As Boolean = True
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()

        If ValidEmployee(dbConnSqlServer) Then
            CloseOutMsg.Text = GetCloseOutMsg(dbConnSqlServer)
        Else
            Response.Redirect("COMMUN/denied.html")
        End If
    End Sub

    Private Function ValidEmployee(ByVal dbConnSqlServer As OleDbConnection) As Boolean
        Dim valide As Boolean = True
        Dim dtTable As New DataTable
        Dim cmdTable As OleDbDataAdapter
        Dim strReq As String

        If Request.QueryString("NCFNo") <> Nothing Then
            strReq = "Select EmployeeWN from NCF_General where status = 4 and  NCFNo=" & Request.QueryString("NCFNo")
            cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
            cmdTable.Fill(dtTable)
            If dtTable.Rows.Count > 0 Then
                If Trim(dtTable.Rows(0)(0)) <> strEMPName Then
                    valide = False
                End If
            Else
                    valide = False
            End If
        Else
            valide = False
        End If

        ValidEmployee = valide

    End Function

    Private Function GetCloseOutMsg(ByVal dbConnSqlServer As OleDbConnection) As String
        Dim strReq As String = "Select CloseOutMsg from NCF_Admin where NCFNo=" & Request.QueryString("NCFNo")
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        Dim CloseOutMsg As String = ""
        cmdTable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            If Not IsDBNull(dtTable.Rows(0)(0)) Then
                CloseOutMsg = dtTable.Rows(0)(0)
            End If
        End If

        GetCloseOutMsg = CloseOutMsg
    End Function
    '
    '|----------------------------------------------------------------------------------------------|
    '| EtablitConnexionSQLServer												                    |
    '|----------------------------------------------------------------------------------------------|
    Function EtablitConnexionSQLServer() As OleDbConnection
        Dim strConn As String = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=NCF;" & _
                                "User ID=NCF;Password=NCF"
        EtablitConnexionSQLServer = New OleDbConnection(strConn)
    End Function

End Class
