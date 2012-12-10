Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.DBNull
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports UserControls.CC_Controls

Public Class Search
    Inherits System.Web.UI.Page

    Public NcfNo As System.Web.UI.WebControls.TextBox
    Public Customer As ComboBox
    Public StartDate As System.Web.UI.WebControls.TextBox
    Public EndDate As System.Web.UI.WebControls.TextBox
    Public Status As System.Web.UI.WebControls.DropDownList
    Public Description As System.Web.UI.WebControls.TextBox
    Public ImmediateAction As System.Web.UI.WebControls.TextBox
    Public Cause As System.Web.UI.WebControls.TextBox
    Public Action As System.Web.UI.WebControls.TextBox
    Public SearchCRF As System.Web.UI.WebControls.Button
    Public SearchResult As System.Web.UI.WebControls.Table
    Public Button1 As System.Web.UI.WebControls.Button
    Public Button2 As System.Web.UI.WebControls.Button
    Public Button3 As System.Web.UI.WebControls.Button
    Public Suggestions As System.Web.UI.WebControls.TextBox
    Public Edit As System.Web.UI.WebControls.Button
    Public Delete As System.Web.UI.WebControls.Button
    Public View As System.Web.UI.WebControls.Button
    Public ViewCloseOut As System.Web.UI.WebControls.Button

    Private TailleDomaine As Integer = "LCLMTL\".Length
    Private strEMPName As String = User.Identity.Name.Substring(TailleDomaine)

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page Contact is loaded                                                                     |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub LoadPage()
        'Put user code to initialize the page here
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Session("NCF") = Nothing

        If Not Page.IsPostBack Then
            'adds javascript confirmtion before deleting an item
            If Session("LG") = "FR" Then
                Me.Delete.Attributes.Add("onclick", _
                   "return confirm('Désirez-vous vraiment supprimer cet élément?');")
            Else
                Me.Delete.Attributes.Add("onclick", _
                   "return confirm('Are you sure you want to delete this item?');")
            End If

            'Fills status list
            FillStatusList(dbConnSqlServer)

            'Fills the Customer List
            FillCustomersList(dbConnSqlServer)

            'Search (All - Unassigned()
            Search(dbConnSqlServer)
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the customer Combobox in Customer name order                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillCustomersList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String

        strReq = "Select (CUSTOMERNAME + ' - ' + Cast(CUSTOMERNO as Varchar)) AS Customer, CustomerNo from QRY_Customers"
        strReq += " order by CUSTOMERNAME"
        AddItemsInComboBox(Customer, strReq, dbConnSqlServer)
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

        cmdTable = new OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.fill(dtTable)

        strReq = "Select status_" & Session("LG") & " as Status, StatusNo from Status order by StatusNo"
        cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.fill(dtTable)

        Status.DataTextField = "Status"
        Status.DataValueField = "StatusNo"
        Status.DataSource = dtTable
        Status.Databind()
    End Sub 

    '|----------------------------------------------------------------------------------------------------------|
    '|  Adds items from a query in a Combobox                                                                   |
    '|  In: query and Combobox                                                                                  |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub AddItemsInComboBox(ByVal ComboB As ComboBox, ByVal strReq As String, ByVal dbConnSqlServer As OleDbConnection)
        'query results + 'All'
        Dim strAll as string

        'Affiche All - Tous selon la langue
        If Session("LG") = "FR" Then
            strAll = "'Tous'"
        Else
            strAll = "'All'"
        End If

        Dim strReq1 = "Select " & strAll & " AS Customer, -1 AS CustomerNo"
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
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Search(dbConnSqlServer)
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
           
            If Session("LG") = "FR" then 'message en francais
                 strError += "Date Invalide.\n"
            Else
                 strError += "Invalid date.\n"
            End If

            strError += "');</script>"
            Response.Write(strError)
        ElseIf NcfNo.Text <> "" And Not IsNumeric(NcfNo.Text) Then
            'Invalid NCF #
            strError = "<script language=javascript>alert('"
            
            If Session("LG") = "FR" then 'message en francais
                 strError += "Le # NCF doit être numérique.\n"
            Else
                strError += "NCF # must be numeric.\n"
            End If

            strError += "');</script>"
            Response.Write(strError)
        Else
            'Valid
            strReq = "Select * from QRY_search_" & Session("LG") & " where " & strWhere()
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
        'Employee name = Employee logged in
        where += " AND EmployeeWN='" & strEMPName & "'"

        'NCF #
        If NcfNo.Text <> "" Then
            where += " AND [ID #]=" & NcfNo.Text
        End If

        'Customer
        If Customer.Text <> "" And Customer.SelectedValue <> -1 Then
            where += " AND CustomerNo=" & Customer.Value
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

    '|----------------------------------------------------------------------------------------------------------|
    '|  create the shown table in the form from the query results                                               |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub createTable(ByVal dtTable As DataTable)
        Dim row As TableRow
        Dim cell As TableCell
        Dim Results As Table = FindControl("SearchResult")
        Dim rb As RadioButton

        'if there is a maximum length, else -1 (maximum lenght is for fields that might get big)
        'this is to prevent the table for not getting bigger than the screen
        Dim maxLength() As Integer = {-1, -1, 20, 35, 30, -1}

        Dim dtrow As DataRow
        Dim i As Integer

        row = New TableRow
        row.CssClass = "RechercheTitre taille10"

        cell = New TableCell
        cell.Text = ""
        row.Cells.Add(cell)

        'Column names (table's header)
        For i = 0 To dtTable.Columns.Count - 8
            cell = New TableCell
            cell.Text = dtTable.Columns(i).ColumnName
            cell.CssClass = ""
            row.Cells.Add(cell)
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
            rb.Attributes.Add("onClick", "javacript:enableSearchButtons(" & dtrow(6) & ");")

            cell = New TableCell
            cell.Controls.Add(rb)
            row.Cells.Add(cell)

            'show results from query
            For i = 0 To dtTable.Columns.Count - 8
                cell = New TableCell
                cell.CssClass = "spaces taille8"
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
                row.Cells.Add(cell)
            Next
            Results.Rows.Add(row)
        Next

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Redirect in Edit mode                                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub RedirectEdit(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Session("NCF") = Request.Form("RBL_NcfNo")
        Session("Edit") = True
        System.Diagnostics.Debug.Write(Session("NCF"))
        redirect()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Redirect in View mode                                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub RedirectView(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Session("NCF") = Request.Form("RBL_NcfNo")
        Session("Edit") = False
        redirect()
    End Sub

    Public Sub OpenCloseOutMsg(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()

        Dim NcfNo As String = Request.Form("RBL_NcfNo")
        Dim strComm As String = "<script language=Javascript>"
        strComm += "window.open('CloseOutCommunications_" & Session("LG") & ".aspx?NCFNo=" & NcfNo & "' ,"
        strComm += " 'new', 'toolbar=no,scrollbar=no'); "
        strComm += "</script>"

        Response.Write(strComm)
        Search(dbConnSqlServer)

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '| Selects the good NCF # and redirects the page                                                            |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub redirect()
        Response.Redirect("GeneralInfo_" & Session("LG") & ".aspx")
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '| Deletes NCF                                                                                              |
    '| The program enters this proc only if user entered Ok when asked for confirmation                         |
    '| See(Me.Delete.Attributes.Add) in page load proc for more details                                         |                 |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub DeleteNCF(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim cmdConn As New OleDbCommand
        Dim strComm As String = "Delete from NCF_General where NCFNo=" & Request.Form("RBL_NcfNo")

        dbConnSqlServer.Open()
        cmdConn.Connection = dbConnSqlServer
        cmdConn.CommandText = strComm
        cmdConn.ExecuteNonQuery()

        strComm = "Delete from NCF_Admin where NCFNo=" & Request.Form("RBL_NcfNo")
        cmdConn.CommandText = strComm
        'Delete NCF
        cmdConn.ExecuteNonQuery()

        dbConnSqlServer.Close()

        Search(dbConnSqlServer)
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

End Class
