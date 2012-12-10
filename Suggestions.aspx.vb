Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.UI.WebControls
Imports System.DBNull
Imports System.Web.Mail

Public Class SectionD
    Inherits System.Web.UI.Page

    Public NextP As System.Web.UI.WebControls.Button
    Public FormType1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Suggestions As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button3 As System.Web.UI.WebControls.Button
    Protected WithEvents Previous As System.Web.UI.WebControls.Button
    Protected WithEvents Print As System.Web.UI.WebControls.Button
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button


    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page Suggestions is loaded                                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Sub LoadPage()
        'Put user code to initialize the page here

        If Session("NCF") <> Nothing Then
            If Not Page.IsPostBack Then
                Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
                showInformation(Suggestions, "Suggestions", dbConnSqlServer)
            End If

            If Session("Edit") = False Then
                Suggestions.Enabled = False
            End If
        Else
            Response.Redirect("COMMUN/denied.html")
        End If

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Shows Suggestions                                                                                       |
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
    '|  Happens when "Finish" Button is clicked                                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub Finish(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Session("edit") = True Then
            'Edit mode (valid, save and redirects)
            ValidAndRedirect("FormSaved_" & Session("LG") & ".aspx")
        Else
            'if user is in view mode, move on to the next page
            Response.Redirect("FormSaved_" & Session("LG") & ".aspx")
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Creates the report - when "print report is clicked                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub PrintReport(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strComm As String

        strComm = "<script language=Javascript>"
        strComm += "window.open("
        strComm += "'Report_" & Session("LG") & ".aspx?state=0'"
        strComm += ", 'new'"
        strComm += ", 'height=600,width=800,scrollbars=yes,toolbar=no'"
        strComm += ");"
        strComm += "</script>"

        Response.Write(strComm)
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when "Previous" Button is clicked                                                               |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub PreviousPage(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Session("edit") = True Then
            'Edit mode (valid, save and redirects)
            ValidAndRedirect("SuspectedCause_" & Session("LG") & ".aspx")
        Else
            'if user is in view mode, move back to the previous page
            Response.Redirect("SuspectedCause_" & Session("LG") & ".aspx")
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Valid page: if page is valid, save information and move on to the next or previous page                 |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub ValidAndRedirect(ByVal redirectionPage As String)
        Dim IsError As String

        'page is valid if Suggestions isn't blank
        If Suggestions.Text <> "" Then
            'save in database
            SaveSuggestions()
            Response.Redirect(redirectionPage)
        Else
            'Suggestions is blank
            IsError = "<script language javascript>alert('"

            If Session("LG") = "FR" Then 'message en francais
                IsError += "Vous devez entrer des Suggestions"
            Else
                IsError += "You must enter Suggestions"
            End If

            IsError += "');</script>"
            Response.Write(IsError)
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Unpdates the Table y inserting Suggestions informations                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SaveSuggestions()
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim cmdComm As New OleDbCommand
        Dim strReq As String

        dbConnSqlServer.Open()

        'Save suggestions
        strReq = updateQry(Suggestions, "Suggestions")
        cmdComm.Connection = dbConnSqlServer
        cmdComm.CommandText = strReq
        'Executes update Query
        cmdComm.ExecuteNonQuery()

        'update status:
        strReq = updateStatus(dbConnSqlServer)
        If strReq <> "" Then
            cmdComm.CommandText = strReq
            cmdComm.ExecuteNonQuery()
        End If

        dbConnSqlServer.Close()

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Builds the update query                                                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function updateQry(ByVal text As TextBox, ByVal field As String) As String
        Dim strReq As String

        strReq = "Update NCF_General set "
        strReq += field & "='" & Replace(text.Text, "'", "''") & "' "
        strReq += "where NCFNo=" & Session("NCF")

        updateQry = strReq

    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Updates status                                                                                          |
    '|  if not assigned -> status = 2                                                                           |
    '|  Else send email to assigned person                                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function updateStatus(ByVal dbConnSqlServer As OleDbConnection) As String
        'get assigned person
        Dim strReq As String = "Select Email from QRY_NCFAssignedEmail where NCFNo=" & Session("NCF")
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        If dtTable.Rows.Count = 0 Then
            'Not assigned - Status = 2
            strReq = "Update NCF_General set status=2"
            strReq += "where NCFNo=" & Session("NCF")
        Else
            'Assigned - Send Email
            strReq = ""
            If Not IsDBNull(dtTable.Rows(0)("Email")) Then
                'Send Email
                SendMail(dtTable.Rows(0)("Email"))
            End If
        End If

        updateStatus = strReq

    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  If NCF is assigned and changes have been made, send email to assigned person                            |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SendMail(ByVal strTo As String)
        Dim mail As New MailMessage
        Dim bodyFr As String
        Dim bodyEn As String
        Try
            'Creating message
            mail.From = "Comitee.CFF@laurentidecontrols.com"
            mail.To = strTo
            mail.Subject = "La rétroaction #" & Session("NCF") & " a été modifiée par son créateur"

            bodyFr = "Si vous désirez modifier les informations sur le suivi ou consulter les informations de la rétroaction" & _
                    ", veuillez suivre le lien ci-dessous: <br />"

            bodyEn = "<br /><br /><br />"
            bodyEn += "If you would like to edit any information on the review or consult feedback's information" & _
                    ", please follow this link: <br />"

            mail.BodyFormat = MailFormat.Html
            mail.Body = bodyFr & " " & AslinkChanges("FR", "Rétroaction #") & bodyEn & " " & AslinkChanges("EN", "Feedback #")
            'mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtauthenticate", 2)
            'mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "lcl-exc")
            SmtpMail.SmtpServer = "lcl-exc"
            SmtpMail.Send(mail)
        Catch ex As Exception
            System.Diagnostics.Debug.Write(ex)
        End Try
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Creates the changes link in the assigned body depending on the language                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Function AslinkChanges(ByVal strLang As String, ByVal strMsgLink As String) As String
        Dim link As String

        link += "<a href="
        link += """http://lcl-faxs/FRANCS"
        link += "/Review_" & strLang & ".aspx?NCFNo=" & Session("NCF") & " "" "
        link += ">" & strMsgLink & Session("NCF") & "</a>"

        AslinkChanges = link

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

    Private Sub InitializeComponent()

    End Sub
End Class
