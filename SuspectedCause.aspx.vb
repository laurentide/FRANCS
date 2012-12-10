Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.UI.WebControls
Imports System.DBNull

Public Class SectionC
    Inherits System.Web.UI.Page

    Public NextP As Button
    Public FormType1 As TextBox
    Public Button1 As Button
    Public Textbox1 As TextBox
    Public cause As TextBox
    Public Button2 As Button

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page Suspected Cause is loaded                                                             |
    '|----------------------------------------------------------------------------------------------------------|
    Sub LoadPage()
        'Put user code to initialize the page here

        If Session("NCF") <> Nothing Then
            If Not Page.IsPostBack Then
                Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
                showInformation(cause, "Cause", dbConnSqlServer)
            End If

            If Session("Edit") = False Then
                cause.Enabled = False
            End If
        Else
            Response.Redirect("COMMUN/denied.html")
        End If

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Shows Suspected Cause of deficiency                                                                     |
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
            ValidAndRedirect("Suggestions_" & Session("LG") & ".aspx")
        Else
            'if user is in view mode, move on to the next page
            Response.Redirect("Suggestions_" & Session("LG") & ".aspx")
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when "Previous" Button is clicked                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub PreviousPage(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Session("edit") = True Then
            'Edit mode (valid, save and redirects)
            ValidAndRedirect("ImmediateAction_" & Session("LG") & ".aspx")
        Else
            'if user is in view mode, move back to the previous page
            Response.Redirect("ImmediateAction_" & Session("LG") & ".aspx")
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Valid page: if page is valid, save information and move on to the next or previous page                 |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub ValidAndRedirect(ByVal redirectionPage As String)
        Dim IsError As String

        'page is valid if Suspected Cause isn't blank
        If cause.Text <> "" Then
            'save in database
            SaveSuspectedCause()
            Response.Redirect(redirectionPage)
        Else
            'Suspected Cause is blank
            IsError = "<script language=javascript>alert('"
            
            If Session("LG") = "FR" then 'message en francais
                IsError += "Vous devez entrer Cause du problème soupçonnée"
            Else
                IsError += "You must enter the Suspected Cause of Deficiency"
            End If

            IsError += "');</script>"
            Response.Write(IsError)
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Unpdates the Table y inserting Suspected Cause informations                                             |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SaveSuspectedCause()
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim cmdComm As New OleDbCommand

        dbConnSqlServer.Open()

        cmdComm.Connection = dbConnSqlServer
        cmdComm.CommandText = updateQry(cause, "Cause")
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
