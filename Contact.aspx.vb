Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.DBNull
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports UserControls.CC_Controls

Public Class newContact
    Inherits System.Web.UI.Page

    Public Contact As System.Web.UI.WebControls.TextBox
    Public ContactTel As System.Web.UI.WebControls.TextBox
    Public Button1 As System.Web.UI.WebControls.Button
    Public Customer As ComboBox

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page Contact is loaded                                                                     |
    '|----------------------------------------------------------------------------------------------------------|
    Sub LoadPage()
        'Put user code to initialize the page here
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()

        If Not Page.IsPostBack Then
            'if first time we enter the page
            Session("EditContact") = Nothing
            'Fill the customers' list
            FillCustomersList(dbConnSqlServer)

            If Request.QueryString("contact") <> Nothing Then
                'If editing existing contact
                Session("EditContact") = Request.QueryString("contact")
                ShowContactInfo(dbConnSqlServer)
                Customer.Enabled = False
            End If
        End If


    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills customerList ordered by customer Name                                                             |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillCustomersList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String

        strReq = "Select (CUSTOMERNAME + ' - ' + Cast(CUSTOMERNO as Varchar)) AS Customer, CustomerNo from QRY_Customers order by CustomerName"
        AddItemsInComboBox(Customer, strReq, dbConnSqlServer)
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Adds items from a query in a Combobox                                                                   |
    '|  In: query and Combobox                                                                                  |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub AddItemsInComboBox(ByVal ComboB As ComboBox, ByVal strReq As String, ByVal dbConnSqlServer As OleDbConnection)
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)
        ComboB.AddItems(dtTable)

        If Session("Customer") <> Nothing Then
            ComboB.SelectedValue = Session("Customer")
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Shows contact information                                                                               |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub ShowContactInfo(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String = "Select ContactName, ContactTel From contact where ContactNo=" & Session("EditContact")
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            If Not IsDBNull(dtTable.Rows(0)(0)) Then
                Contact.Text = dtTable.Rows(0)(0)
            End If
            If Not IsDBNull(dtTable.Rows(0)(1)) Then
                ContactTel.Text = dtTable.Rows(0)(1)
            End If
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Save Contact (insert if new, update if existing)                                                        |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub saveContact(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strReq As String
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim cmdConn As New OleDbCommand
        Dim cmdTable As OleDbDataAdapter
        Dim dtTable As New DataTable
        Dim strComm As String


        'If page is valid (customer chosen and contact entered)
        If (Customer.Value <> Nothing And Customer.Value <> "" And Contact.Text <> "") Then
            Session("CustomerChanged") = Customer.SelectedValue
            If Session("EditContact") = Nothing Then
                'New contact
                strReq = "Select * from QRY_ContactNo"
                cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
                cmdTable.Fill(dtTable)

                If dtTable.Rows.Count > 0 Then
                    Session("EditContact") = dtTable.Rows(0)(0) + 1
                Else
                    Session("EditContact") = 1
                End If

                strReq = "Insert into contact values("
                strReq += Session("EditContact") & ", "
                strReq += Customer.Value & ", "
                strReq += "'" & Replace(Contact.Text, "'", "''") & "', "
                strReq += "'" & Replace(ContactTel.Text, "'", "''") & "')"

            Else
                'Existing contact
                strReq = "Update Contact set "
                strReq += "ContactName = '" & Replace(Contact.Text, "'", "''") & "', "
                strReq += "ContactTel = '" & Replace(ContactTel.Text, "'", "''") & "' "
                strReq += " where ContactNo=" & Session("EditContact")
            End If

            dbConnSqlServer.Open()
            cmdConn.Connection = dbConnSqlServer
            cmdConn.CommandText = strReq
            cmdConn.ExecuteNonQuery()
            dbConnSqlServer.Close()

            'Close window
            strComm = "<script language=Javascript>"
            strComm += "opener.document.forms[0].submit();"
            strComm += "self.close();"
            strComm += "</script>"
        Else
            'If page is invalid, show error message
            strComm = "<script language=javascript>alert('"

            If Session("LG") = "FR" then 'message en francais
                strComm += "Vous devez choisir un client et entrer le nom du contact"
            Else
                strComm += "Please choose a customer and enter a contact name"
            End If
            
            strComm += "');</script>"
        End If

        Response.Write(strComm)


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
