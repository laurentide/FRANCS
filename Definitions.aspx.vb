Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.OleDb

Public Class Def
    Inherits System.Web.UI.Page

    Public Title As Label
    Public NomDef As Label
    Public Def As Label

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page Suspected Cause is loaded                                                             |
    '|----------------------------------------------------------------------------------------------------------|
    Sub LoadPage()
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim dtTable As New DataTable
        Dim strReq As String = "Select Name_" & Session("LG") & " As Name, Definition_" & Session("LG") & _
                            " As Defition from Definitions where NoDef=" & Request.Querystring("NoDef")

        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)

        Title.text = IIF(Session("LG") = "FR", "Définitions", "Definitions")

        cmdTable.fill(dtTable)

        If (dtTable.Rows.Count() > 0) Then
            If Not isdbnull(dtTable.rows(0)("Name")) Then
                NomDef.Text = dtTable.rows(0)("Name") & ": "
            End If

            If Not isdbnull(dtTable.rows(0)("Defition")) Then
                Def.Text = dtTable.rows(0)("Defition")
            End If
        End If

    End Sub


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
