Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.UI.WebControls
Imports System.DBNull
Imports System.Web.Mail

Public Class Report
    Inherits System.Web.UI.Page
    Protected WithEvents Internal As System.Web.UI.WebControls.CheckBox
    Protected WithEvents External As System.Web.UI.WebControls.CheckBox
    Protected WithEvents NCFNo As System.Web.UI.WebControls.Label
    Protected WithEvents NonConformance As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Opportunity As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Customer As System.Web.UI.WebControls.Label
    Protected WithEvents IssuedBy As System.Web.UI.WebControls.Label
    Protected WithEvents IssueDate As System.Web.UI.WebControls.Label
    Protected WithEvents EMPTel As System.Web.UI.WebControls.Label
    Protected WithEvents Contact As System.Web.UI.WebControls.Label
    Protected WithEvents ContactTel As System.Web.UI.WebControls.Label
    Protected WithEvents OrderNo As System.Web.UI.WebControls.Label
    Protected WithEvents CategoryType As System.Web.UI.WebControls.Label
    Protected WithEvents Category As System.Web.UI.WebControls.Label
    Protected WithEvents Production As System.Web.UI.WebControls.Label
    Protected WithEvents Deficiency As System.Web.UI.WebControls.TextBox
    Protected WithEvents ActionT As System.Web.UI.WebControls.TextBox
    Protected WithEvents Cause As System.Web.UI.WebControls.TextBox
    Protected WithEvents Vendor_Name As System.Web.UI.WebControls.Label
    Protected WithEvents PONo As System.Web.UI.WebControls.Label
    Protected WithEvents Suggestions As System.Web.UI.WebControls.TextBox

    Sub LoadPage()
        
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()

        If Session("NCF") <> Nothing Then
            showInformations(dbConnSqlServer)
        Else
            Response.Redirect("Commun\Denied.html")
        End If

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  fill all information entered in the database                                                            |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub showInformations(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String
        Dim dtTable As New DataTable
        Dim cmdtable As OleDbDataAdapter

        strReq = "Select * from QRY_UserReport_" & Session("LG") & " where NCFNo=" & Session("NCF")
        cmdtable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdtable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            AddnformationInFields(dtTable)
        End If

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  every column in the query is insert to the correct place in the form                                    |
    '|  Important: query fields and controls must match names                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub AddnformationInFields(ByVal dtTable As DataTable)
        Dim i As Integer
        Dim nomControle As String

        'for all columns in the query
        For i = 0 To dtTable.Columns.Count - 1
            'the name of the control is the same as the column
            nomControle = dtTable.Columns(i).ToString
            'If something is entered for this data
            If Not IsDBNull(dtTable.Rows(0)(i)) Then
                System.Diagnostics.Debug.WriteLine(dtTable.Columns(i).ToString & ": " & dtTable.Rows(0)(i).ToString)
                Select Case FindControl(nomControle).GetType.ToString
                    Case "System.Web.UI.WebControls.CheckBox"
                        CType(FindControl(nomControle), CheckBox).Checked = IIf(dtTable.Rows(0)(i) = 0, True, False)
                    Case "System.Web.UI.WebControls.Label"
                        'If controlType = Label, enter the data in the label
                        CType(FindControl(nomControle), Label).Text = dtTable.Rows(0)(i)
                    Case "System.Web.UI.WebControls.TextBox"
                        'If controlType = Textbox, enter the data in the Textbox
                        CType(FindControl(nomControle), TextBox).Text = dtTable.Rows(0)(i)
                End Select
            End If
        Next
    End Sub

    '
    '|----------------------------------------------------------------------------------------------|
    '| EtablitConnexionSQLServer												                    |
    '|----------------------------------------------------------------------------------------------|
    Private Function EtablitConnexionSQLServer() As OleDbConnection
        Dim strConn As String = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=NCF;" & _
                                "User ID=NCF;Password=NCF"
        EtablitConnexionSQLServer = New OleDbConnection(strConn)
    End Function

    Private Sub InitializeComponent()

    End Sub
End Class