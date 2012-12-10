Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.UI.WebControls
Imports System.DBNull

Public Class SectionA
    Inherits System.Web.UI.Page

    Public Button1 As System.Web.UI.WebControls.Button
    Public FormType1 As System.Web.UI.WebControls.TextBox
    Public Desc As System.Web.UI.WebControls.TextBox
    Public Button2 As System.Web.UI.WebControls.Button

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page GeneralInfo is loaded                                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Sub LoadPage()
        'Put user code to initialize the page here
        If Session("NCF") <> Nothing Then
            If Not Page.IsPostBack Then
                Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
                showInformation(Desc, "Deficiency", dbConnSqlServer)
            End If

            If Session("Edit") = False Then
                Desc.Enabled = False
            End If
        Else
            Response.Redirect("COMMUN/denied.html")
        End If

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Shows description defictiency                                                                           |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub showInformation(ByVal text As TextBox, ByVal field As String, ByVal dbConn As OleDbConnection)
        Dim strReq As String
        Dim dtTable As New DataTable
        Dim cmdtable As OleDbDataAdapter

        'gets the information from the database
        strReq = "select " & field & " from NCF_General where NCFNo=" & Session("NCF")
        cmdtable = New OleDbDataAdapter(strReq, dbConn)
        cmdtable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            'If something has already been entered in the database, show information
            If Not IsDBNull(dtTable.Rows(0)(0)) Then
                text.Text = dtTable.Rows(0)(0)
            End If
        End If


    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when "Next" Button is clicked                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub NextPage(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Session("edit") = True Then
            'Edit mode (valid, save and redirects)
            ValidAndRedirect("ImmediateAction_" & Session("LG") & ".aspx")
        Else
            'if user is in view mode, move on to the next page
            Response.Redirect("ImmediateAction_" & Session("LG") & ".aspx")
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when "Previous" Button is clicked                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub PreviousPage(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Session("edit") = True Then
            'Edit mode (valid, save and redirects)
            ValidAndRedirect("GeneralInfo_" & Session("LG") & ".aspx")
        Else
            'if user is in view mode, move back to the previous page
            Response.Redirect("GeneralInfo_" & Session("LG") & ".aspx")
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Valid page: if page is valid, save information and move on to the next or previous page                 |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub ValidAndRedirect(ByVal redirectionPage As String)
        Dim IsError As String

        'page is valid if Description isn't blank
        If Desc.Text <> "" Then
            'save in database
            SaveDescriptionDeficiency()
            Response.Redirect(redirectionPage)
        Else
            'Description is blank
            IsError = "<script language=javascript>alert('"
            If Session("LG") = "FR" then 'message en francais
                IsError += "Vous devez entrer la description du probl�me"
            Else
                IsError += "You must enter the deficiency\'s description"
            End If
            IsError += "');</script>" 
            
            Response.Write(IsError)
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Unpdates the Table y inserting Description Deficiency informations                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SaveDescriptionDeficiency()
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim cmdComm As New OleDbCommand

        dbConnSqlServer.Open()

        cmdComm.Connection = dbConnSqlServer
        cmdComm.CommandText = updateQry(Desc, "Deficiency")
        'Executes update Query
        cmdComm.ExecuteNonQuery()

        dbConnSqlServer.Close()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Builds the update query                                                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function updateQry(ByVal text As TextBox, ByVal field As String) As String
        Dim strReq As String

        strReq = "Update NCF_General set "
        strReq += field & "='" & Replace(text.Text, "'", "''") & "'"
        strReq += "where NCFNo=" & Session("NCF")

        updateQry = strReq

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
