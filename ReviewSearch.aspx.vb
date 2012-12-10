Imports Microsoft.VisualBasic
Imports System.Web.Mail
Imports System.Data
Imports System.Data.OleDb
Imports System.DBNull
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports UserControls.CC_Controls


Public Class ReviewSearch
    Inherits System.Web.UI.Page

    Public Textbox2 As System.Web.UI.WebControls.TextBox
    Public Dropdownlist1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents StartDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents EndDate As System.Web.UI.WebControls.TextBox
    Public Textbox3 As System.Web.UI.WebControls.TextBox
    Public Textbox4 As System.Web.UI.WebControls.TextBox
    Public Textbox5 As System.Web.UI.WebControls.TextBox
    Public Textbox6 As System.Web.UI.WebControls.TextBox
    Public SearchCRF As System.Web.UI.WebControls.Button
    Public Button1 As System.Web.UI.WebControls.Button
    Public Button3 As System.Web.UI.WebControls.Button
    Public Dropdownlist2 As System.Web.UI.WebControls.DropDownList
    Public Dropdownlist3 As System.Web.UI.WebControls.DropDownList
    Public ddlEmployees As System.Web.UI.WebControls.DropDownList
    Public Customer As ComboBox
    Public Employee As ComboBox
    Public AssignedTo As ComboBox
    Protected WithEvents NcfNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents Description As System.Web.UI.WebControls.TextBox
    Protected WithEvents ImmediateAction As System.Web.UI.WebControls.TextBox
    Protected WithEvents Cause As System.Web.UI.WebControls.TextBox
    Protected WithEvents Suggestions As System.Web.UI.WebControls.TextBox
    Protected WithEvents searchB As System.Web.UI.WebControls.Button
    Protected WithEvents SearchResult As System.Web.UI.WebControls.Table
    Protected WithEvents Edit As System.Web.UI.WebControls.Button
    Protected WithEvents Print As System.Web.UI.WebControls.Button
    Protected WithEvents View As System.Web.UI.WebControls.Button
    Protected WithEvents StatusChange As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Delete As System.Web.UI.WebControls.Button
    Protected WithEvents FollowUp As System.Web.UI.WebControls.Button
    Protected WithEvents SendIncomplete As System.Web.UI.WebControls.Button
    Protected WithEvents Status As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Button_1 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_2 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_3 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_4 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_5 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_6 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_7 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_8 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_9 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_10 As System.Web.UI.WebControls.Button
    Protected WithEvents Button_11 As System.Web.UI.WebControls.Button

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page GeneralInfo is loaded                                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub LoadPage()
        'Put user code to initialize the page here
        Session("NCF") = Nothing

        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Session("NCF") = Nothing

        If User.IsInRole("LCLMTL\LCL_Feedback_Committee") = True Or _
            User.IsInRole("LCLMTL\LCL_IT_Projects") = True Or _
            User.Identity.Name = "LCLMTL\d2dadmin" Then
            If Not Page.IsPostBack Then
                'Fills status list
                FillStatusList(dbConnSqlServer)

                'Fills the Customer List
                FillCustomersList(dbConnSqlServer)

                'Fills the Employee List
                FillEmployeeList(dbConnSqlServer)

                'Fills the AssignedTo List
                FillAssignedToList(dbConnSqlServer)

            End If
        Else
            Response.Redirect("COMMUN/denied.html")
        End If
        Search(dbConnSqlServer)
        dbConnSqlServer.Close()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the customer Combobox in Customer name order                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillCustomersList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String
        Dim strAll As String

        strReq = "Select (CUSTOMERNAME + ' - ' + Cast(CUSTOMERNO as Varchar)) AS FieldText, CustomerNo AS FieldValue from QRY_Customers"
        strReq += " order by CUSTOMERNAME"

        'Affiche All - Tous selon la langue
        If Session("LG") = "FR" Then
            strAll = "'Tous'"
        Else
            strAll = "'All'"
        End If

        AddItemsInComboBox(Customer, strReq, dbConnSqlServer, strAll, "-1")
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the Employee Combobox in Employee name order                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillEmployeeList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String
        Dim strAll As String

        strReq = "Select displayName AS FieldText, samAccountName AS FieldValue from AD_Employee order by displayName"

        'Affiche All - Tous selon la langue
        If Session("LG") = "FR" Then
            strAll = "'Tous'"
        Else
            strAll = "'All'"
        End If

        AddItemsInComboBox(Employee, strReq, dbConnSqlServer, strAll, "'All'")
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the AssignedTo Combobox in Employee name order                                                    |
    '|  Created 2008/08/20 by Duc Duy Nguyen
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillAssignedToList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String
        Dim strAll As String

        strReq = "Select EmployeeName AS FieldText, EmployeeNo AS FieldValue from Assignments order by EmployeeName"

        'Affiche All - Tous selon la langue
        If Session("LG") = "FR" Then
            strAll = "'Tous'"
        Else
            strAll = "'All'"
        End If

        AddItemsInComboBox(AssignedTo, strReq, dbConnSqlServer, strAll, "'All'")
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the status DropdownList in StatusNo Order                                                         |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillStatusList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String
        Dim dtTable As New DataTable
        Dim cmdTable As OleDbDataAdapter

        If Session("LG") = "FR" Then
            strReq = "Select 'Tous'  as Status, 0 as StatusNo"
        Else
            strReq = "Select 'All'  as Status, 0 as StatusNo"
        End If

        cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        strReq = "Select status_" & Session("LG") & " as Status, StatusNo from Status order by StatusNo"
        cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        Status.DataTextField = "Status"
        Status.DataValueField = "StatusNo"
        Status.DataSource = dtTable
        Status.DataBind()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Adds items from a query in a Combobox                                                                   |
    '|  In: query and Combobox                                                                                  |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub AddItemsInComboBox(ByVal ComboB As ComboBox, ByVal strReq As String, _
                                    ByVal dbConnSqlServer As OleDbConnection, _
                                    ByVal firstSelectedText As String, ByVal firstSelectedValue As String)
        'query results + 'All'
        Dim strReq1 = "Select " & firstSelectedText & " AS FieldText, " & firstSelectedValue & " AS FieldValue"
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq1, dbConnSqlServer)
        cmdTable.Fill(dtTable)
        cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)
        ComboB.AddItems(dtTable)
        ComboB.SelectedIndex = 0
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page search button is clicked                                                              |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub searchButton(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  If page is valid, return all the results corresponding to the user's demand                             |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub Search(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String
        Dim dtTable As New DataTable
        Dim cmdTable As OleDbDataAdapter
        Dim strError As String

        If (StartDate.Text <> "" And Not IsDate(StartDate.Text)) Or (EndDate.Text <> "" And Not IsDate(EndDate.Text)) Then
            'invalid dates
            strError = "<script language=javascript>alert('"

            If Session("LG") = "FR" Then 'message en francais
                strError += "Date Invalide.\n"
            Else
                strError += "Invalid date.\n"
            End If

            strError += "');</script>"
            Response.Write(strError)
        ElseIf NcfNo.Text <> "" And Not IsNumeric(NcfNo.Text) Then
            'Invalid NCF #
            strError = "<script language=javascript>alert('"

            If Session("LG") = "FR" Then 'message en francais
                strError += "Le # NCF doit être numérique.\n"
            Else
                strError += "NCF # must be numeric.\n"
            End If

            strError += "');</script>"
            Response.Write(strError)
        Else
            'Valid
            strReq = "Select * from QRY_SearchReview_" & Session("LG") & " where " & strWhere() & strOrderBy()
            cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
            'exectute query
            cmdTable.Fill(dtTable)
            'Create the table containing the results
            createTable(dtTable)
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  builds the where part of the query depending of the informations the user entered                       |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function strWhere() As String
        Dim where As String = "1=1 "

        'NCF #
        If NcfNo.Text <> "" Then
            If Session("LG") = "EN" Then
                where += " AND [ID #]=" & NcfNo.Text
            Else
                where += " AND [#ID]=" & NcfNo.Text
            End If
        End If

        'Customer
        If Customer.Text <> "" And Customer.SelectedValue <> -1 Then
            where += " AND CustomerNo=" & Customer.Value
        End If

        'Employee
        If Employee.Text <> "" And Employee.SelectedValue <> "All" Then
            where += " AND EmployeeWN='" & Employee.SelectedValue & "'"
        End If

        'Start Date And End Date
        If StartDate.Text <> "" And EndDate.Text <> "" Then
            where += " AND IssueDate Between '" & StartDate.Text & "' AND '" & EndDate.Text & "'"
        ElseIf StartDate.Text <> "" Then
            where += " AND IssueDate >= '" & StartDate.Text & "'"
        ElseIf EndDate.Text <> "" Then
            where += " AND IssueDate <= '" & EndDate.Text & "'"
        End If

        'Status
        If Status.SelectedValue <> 0 Then
            where += " AND StatusNo= " & Status.SelectedValue
        End If

        'AssignedTo
        If AssignedTo.Text <> "" And AssignedTo.SelectedValue <> "All" Then
            where += " AND [EmployeeNo] ='" & AssignedTo.SelectedValue & "'"
        End If

        'Description
        If Description.Text <> "" Then
            where += " AND Deficiency like '%" & Replace(Description.Text, "'", "''") & "%'"
        End If

        'Immediate Action
        If ImmediateAction.Text <> "" Then
            where += " AND ActionT like '%" & Replace(ImmediateAction.Text, "'", "''") & "%'"
        End If

        'Cause
        If Cause.Text <> "" Then
            where += " AND Cause like '%" & Replace(Cause.Text, "'", "''") & "%'"
        End If

        'Suggestions
        If Suggestions.Text <> "" Then
            where += " AND Suggestions like '%" & Replace(Suggestions.Text, "'", "''") & "%'"
        End If

        strWhere = where
    End Function

    Public Function strOrderBy() As String
        Dim orderby As String
        If Session("Sort") = Nothing Then
            orderby = ""
        Else
            orderby = Session("Sort")
        End If
        strOrderBy = orderby
    End Function


    '|----------------------------------------------------------------------------------------------------------|
    '|  create the shown table in the form from the query results                                               |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub createTable(ByVal dtTable As DataTable)
        Dim row As TableRow
        Dim cell As TableCell
        Dim Results As Table = FindControl("SearchResult")
        Dim rb As RadioButton
        Dim but As Button
        'Reset the table

        Results.Rows.Clear()

        'if there is a maximum length, else -1 (maximum lenght is for fields that might get big)
        'this is to prevent the table for not getting bigger than the screen
        Dim maxLength() As Integer = {-1, -1, 10, 11, 35, 30, 20, -1, 20, -1, -1}

        Dim dtrow As DataRow
        Dim i As Integer

        row = New TableRow
        row.CssClass = "RechercheTitre taille10"

        cell = New TableCell
        cell.Text = ""
        row.Cells.Add(cell)

        'Column names (table's header)
        For i = 0 To dtTable.Columns.Count - 9
            cell = New TableCell
            Select Case i
                Case 0
                    Button_1 = New Button
                    With Button_1
                        .CssClass = "bouttonRecherche"
                        .Text = dtTable.Columns(i).ColumnName
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_1)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 1
                    Button_2 = New Button
                    With Button_2
                        .CssClass = "bouttonRecherche"
                        .Text = dtTable.Columns(i).ColumnName
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_2)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 2
                    Button_3 = New Button
                    With Button_3
                        .CssClass = "bouttonRecherche"
                        .Text = dtTable.Columns(i).ColumnName
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_3)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 3
                    Button_4 = New Button
                    With Button_4
                        .CssClass = "bouttonRecherche"
                        .Text = dtTable.Columns(i).ColumnName
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_4)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 4
                    Button_5 = New Button
                    With Button_5
                        .CssClass = "bouttonRecherche"
                        .Text = dtTable.Columns(i).ColumnName
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_5)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 5
                    Button_6 = New Button
                    With Button_6
                        .CssClass = "bouttonRecherche"
                        .Text = dtTable.Columns(i).ColumnName
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_6)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 6
                    Button_7 = New Button
                    With Button_7
                        .CssClass = "bouttonRecherche"
                        .Text = dtTable.Columns(i).ColumnName
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_7)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 7
                    Button_8 = New Button
                    With Button_8
                        .CssClass = "bouttonRecherche"
                        .Text = dtTable.Columns(i).ColumnName
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_8)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 8
                    Button_9 = New Button
                    With Button_9
                        .CssClass = "bouttonRecherche"
                        .Text = dtTable.Columns(i).ColumnName
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_9)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 9
                    Button_10 = New Button
                    With Button_10
                        .CssClass = "bouttonRecherche"
                        .Text = "Change"
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_10)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
                Case 10
                    Button_11 = New Button
                    With Button_11
                        .CssClass = "bouttonRecherche"
                        .Text = "Follow Up"
                        .ID = "Button_" & (i + 1)
                    End With
                    cell.Controls.Add(Button_11)
                    row.Cells.Add(cell)
                    cell.Width = Unit.Pixel(0)
            End Select
        Next

        Results.Rows.Add(row)

        'Results
        For Each dtrow In dtTable.Rows
            row = New TableRow

            'add radiobutton first: so we can select the wanted NCF 
            rb = New RadioButton
            rb.GroupName = "RBL_NcfNo"
            rb.ID = dtrow(0)
            'enableReviewSearchButtons: enable action buttons depending if NCF is unassigned, assigned or closed
            rb.Attributes.Add("onClick", "javacript:enableReviewSearchButtons(" & dtrow(11) & ");")

            cell = New TableCell
            cell.Controls.Add(rb)
            row.Cells.Add(cell)

            'show results from query
            For i = 0 To dtTable.Columns.Count - 9
                cell = New TableCell
                cell.CssClass = "smallspaces taille8"
                cell.Width = Unit.Pixel(0)
                If Not IsDBNull(dtrow(i)) Then
                    'controls maxlength
                    If maxLength(i) <> -1 Then
                        If dtrow(i).length > maxLength(i) Then
                            cell.Text = Trim(dtrow(i).Substring(0, maxLength(i)))
                        Else
                            cell.Text = Trim(dtrow(i))
                        End If
                    Else
                        cell.Text = Trim(dtrow(i))
                    End If
                End If

                row.Cells.Add(cell)
            Next
            Results.Rows.Add(row)
        Next

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Button_Clicked                                                                                          |
    '|  Par: Duc Duy Nguyen                                                                                     |
    '|  But: Quand le user click sur le header de la table, permet de sort by cette colonne. S'il click deux    |
    '|       fois, changer la direction du sort.                                                                |        
    '|  Quand: 2007/01/29                                                                                       |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub Button_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_1.Click, Button_2.Click, _
                                                                                                   Button_3.Click, Button_4.Click, _
                                                                                                   Button_5.Click, Button_6.Click, _
                                                                                                   Button_7.Click, Button_8.Click, _
                                                                                                   Button_9.Click, Button_10.Click
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim button_clicked As Button
        Dim strTextBoxID As String
        Dim strOrderby As String

        button_clicked = CType(sender, System.Web.UI.WebControls.Button)
        strTextBoxID = button_clicked.Text

        System.Diagnostics.Debug.Write(strTextBoxID)
        strOrderby = "order by [" & strTextBoxID & "]"
        If Session("Sort") = strOrderby Then
            Session("Sort") = "order by [" & strTextBoxID & "] DESC"
        Else
            Session("Sort") = strOrderby
        End If
        Search(dbConnSqlServer)
        dbConnSqlServer.Close()
    End Sub
    '|----------------------------------------------------------------------------------------------------------|
    '|  Redirect in Edit mode                                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub RedirectEdit(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Session("Edit") = True
        redirect()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Redirect in View mode                                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub RedirectView(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Session("Edit") = False
        redirect()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Delete                                                                                                  |
    '|  Par: Duc Duy Nguyen                                                                                     |
    '|  But: Permettre d'enlever les "incomplete forms"                                                         |
    '|  Quand: 2007/01/28                                                                                       |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub DeleteLine(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strReq As String
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim cmdComm As New OleDbCommand
        Try
            'Ne devrait rouler que si le NCF est incomplet
            strReq = "DELETE FROM NCF_General where NCFNo = " & Request.Form("RBL_NcfNo") & ""
            dbConnSqlServer.Open()

            cmdComm.Connection = dbConnSqlServer
            cmdComm.CommandText = strReq
            'System.Diagnostics.Debug.Write(strReq)
            cmdComm.ExecuteNonQuery()
            Response.Redirect("ReviewSearch_" & Session("LG") & ".aspx")
        Catch ex As Exception
            'Erreur dans le Delete
        End Try
        dbConnSqlServer.Close()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  SetFollowUpFlag                                                                                                  |
    '|  Par: Duc Duy Nguyen                                                                                     |
    '|  But: Change le followup a true ou false                                                        |
    '|  Quand: 2007/01/28                                                                                       |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub SetFollowUpFlag(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strReq As String
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim cmdComm As New OleDbCommand
        Try
            'Ne devrait rouler que si le NCF est incomplet
            strReq = "update NCF_admin set followup = case when followup = 0 then 1 else 0 end where NCFNo = " & Request.Form("RBL_NcfNo") & ""
            dbConnSqlServer.Open()

            cmdComm.Connection = dbConnSqlServer
            cmdComm.CommandText = strReq
            'System.Diagnostics.Debug.Write(strReq)
            cmdComm.ExecuteNonQuery()
            Response.Redirect("ReviewSearch_" & Session("LG") & ".aspx")
        Catch ex As Exception
            'Erreur dans le Delete
        End Try
        dbConnSqlServer.Close()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Prints de report with user information                                                                  |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub PrintReport(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Session("NCF") = Request.Form("RBL_NcfNo")

        Dim strComm As String
        strComm = "<script language=Javascript>"
        strComm += "window.open("
        strComm += "'Report_" & Session("LG") & ".aspx'"
        strComm += ", 'new'"
        strComm += ", 'height=600,width=800,scrollbars=yes,toolbar=no'"
        strComm += ");"
        strComm += "</script>"

        Response.Write(strComm)
    End Sub
    '|----------------------------------------------------------------------------------------------------------|
    '|  ChangeStatus                                                                                            |
    '|  Par: Duc Duy Nguyen                                                                                     |
    '|  But: Permettre de changer le status d'un NCF de Open-Unassigned à Open-WIP                              |
    '|  Quand: 2007/01/28                                                                                       |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub ChangeStatus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strReq As String
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim cmdComm As New OleDbCommand
        'Si le statut desiré est égal à OPEN-WIP changer le statut du NCF
        If (StatusChange.SelectedValue) = 5 Then
            Try
                strReq = "UPDATE NCF_General SET status = 5 where NCFNo = " & Request.Form("RBL_NcfNo") & ""
                dbConnSqlServer.Open()

                cmdComm.Connection = dbConnSqlServer
                cmdComm.CommandText = strReq
                'System.Diagnostics.Debug.Write(strReq)
                cmdComm.ExecuteNonQuery()
                Response.Redirect("ReviewSearch_" & Session("LG") & ".aspx")
            Catch ex As Exception
                System.Diagnostics.Debug.Write(ex.ToString)
            End Try
        End If
        dbConnSqlServer.Close()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '| Sends an email to the user of the NCF telling them that it is incomplete                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub sendIncompleteEmail(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim mail As New MailMessage
        Try
            mail.From = "Comitee.CFF@laurentidecontrols.com"
            mail.To = findEmail()
            mail.Subject = "Incomplete ARC No. " & Request.Form("RBL_NcfNo")
            mail.BodyFormat = MailFormat.Html
            mail.Body = "The NCF " & Request.Form("RBL_NcfNo") & " is incomplete. Please go to the ARC system to complete it or send an email to cdaigneault@laurentide.com to cancel it."
            'mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtauthenticate", 2)
            'mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "lcl-exc")
            SmtpMail.SmtpServer = "lcl-exc"
            SmtpMail.Send(mail)
        Catch ex As Exception
            System.Diagnostics.Debug.Write(ex)
        End Try
    End Sub
    '|----------------------------------------------------------------------------------------------------------|
    '| Selects the good NCF # and redirects the page                                                            |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub redirect()
        Session("NCF") = Request.Form("RBL_NcfNo")
        Response.Redirect("Review_" & Session("LG") & ".aspx")
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

    Private Function findEmail()
        'Get employee Email
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim strTo As String = ""
        Dim strReq = "Select mail from QRY_EmployeeEmail where NCFNO=" & Request.Form("RBL_NcfNo")
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)

        cmdTable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            If Not IsDBNull(dtTable.Rows(0)("mail")) Then
                strTo = dtTable.Rows(0)("mail")
            End If
        End If

        Return strTo
    End Function

End Class
