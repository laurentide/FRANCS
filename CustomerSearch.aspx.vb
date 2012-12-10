Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.DBNull
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class CustomerSearch
    Inherits System.Web.UI.Page
    Protected WithEvents Customer As System.Web.UI.WebControls.TextBox
    Protected WithEvents City As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents SearchResult As System.Web.UI.WebControls.Table
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected WithEvents CustomerNo As System.Web.UI.WebControls.TextBox


    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page Contact is loaded                                                                     |
    '|----------------------------------------------------------------------------------------------------------|
    Sub LoadPage()
        'Put user code to initialize the page here
        
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  showCustomer                                                                                            |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub showCustomers(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim strReq as string 
        Dim dtTable as new DataTable
        Dim cmdTable as  OleDbDataAdapter

        If Session("LG") = "FR" Then
            strReq = "Select CustomerNo AS [#], CUSTOMERNAME as [Client], City as [Ville] from QRY_customers where " & Where()
        Else
            strReq = "Select CustomerNo AS [#], CUSTOMERNAME as [Customer], City from QRY_customers where " & Where()
        End If

        cmdTable = new OleDbDataAdapter(strReq,  dbConnSqlServer)
        cmdTable.fill(dtTable)
        createTable(dtTable)   
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Where                                                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function Where() as string
        Dim strWhere as string = "1=1"

        strWhere += IIF(CustomerNo.Text <> Nothing, "And CustomerNo = " & CustomerNo.Text, "")
        strWhere += "And CustomerName like '%" & Customer.Text & "%' "
        strWhere += "And City like '%" & City.Text & "%' "

        Where = strWhere
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
        Dim maxLength() As Integer = {-1, 35, 20}

        Dim dtrow As DataRow
        Dim i As Integer

        row = New TableRow
        row.CssClass = "RechercheTitre taille10"

        cell = New TableCell
        cell.Text = ""
        row.Cells.Add(cell)

        'Column names (table's header)
        For i = 0 To dtTable.Columns.Count - 1
            cell = New TableCell
            cell.Text = dtTable.Columns(i).ColumnName
            row.Cells.Add(cell)
        Next

        Results.Rows.Add(row)

        'Results
        For Each dtrow In dtTable.Rows
            row = New TableRow

            'add radiobutton first: so we can select the wanted NCF 
            rb = New RadioButton
            rb.GroupName = "RBL_CustomerNo"
            rb.ID = dtrow(0)
            'enableReviewSearchButtons: enable action buttons depending if NCF is unassigned, assigned or closed
          '  rb.Attributes.Add("onClick", "javacript:enableSearchButtons(" & dtrow(6) & ");")

            cell = New TableCell
            cell.Controls.Add(rb)
            row.Cells.Add(cell)

            'show results from query
            For i = 0 To dtTable.Columns.Count - 1
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

    '   
    '|----------------------------------------------------------------------------------------------|
    '| SelectCustomer												                                |
    '|----------------------------------------------------------------------------------------------|
    Public Sub SelectCustomer(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strMsg as string
        strMsg = "<script language=javascript>"
                
                
        If Request.Form("RBL_CustomerNo") = Nothing then
            'Show alert
            strMsg += "alert("

            If Session("LG") = "FR" Then
                strMsg += "'Vous devez sélectionner un Client'"
            Else
                strMsg += "'You must select a Customer'"
            End If
        
            strMsg += ");"
        Else
            Session("CustomerSearch") = Request.Form("RBL_CustomerNo")
            strMsg += "opener.document.forms[0].submit();"
            strMsg += "self.close();"
        End If
            
        strMsg += "</script>"
        Response.write(strMsg)
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

    Private Sub InitializeComponent()

    End Sub
End Class
