Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.DBNull
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports UserControls.CC_Controls


Public Class GeneralInfo
    Inherits System.Web.UI.Page
    Protected WithEvents IssueDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents IssuedBy As System.Web.UI.WebControls.TextBox
    Protected WithEvents Tel As System.Web.UI.WebControls.TextBox
    Protected WithEvents ContactTel As System.Web.UI.WebControls.TextBox
    Protected WithEvents OrderNo As System.Web.UI.WebControls.TextBox


    Public Customer As ComboBox
    Public CategoryTypes As ComboBox
    Public ContactName As ComboBox
    Public Categories As ComboBox
    Public Vendor As ComboBox
    Protected WithEvents FormType1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents FormType2 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents FeedbackType1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents FeedbackType2 As System.Web.UI.WebControls.RadioButton

    Protected WithEvents AffectProd1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents AffectProd2 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Shared1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Shared2 As System.Web.UI.WebControls.RadioButton


    Private TailleDomaine As Integer = "LCLMTL\".Length
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents PONo As System.Web.UI.WebControls.TextBox
    Private strEMPName As String = User.Identity.Name.Substring(TailleDomaine)

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page GeneralInfo is loaded                                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Sub LoadPage()
        'Put user code to initialize the page here
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim userValid As Boolean = True

        If Request.QueryString("NCFNo") <> Nothing Then
            userValid = ValidUser(dbConnSqlServer)
        End If

        System.Diagnostics.Debug.Write(Session("NCF"))

        If userValid = True Then   'Session("Error")
            If Not Page.IsPostBack Then
                Session("Customer") = ""
                Session("Category") = ""
                Session("Contact") = ""
                Session("CategoryTypes") = ""

                'Fill independent lists
                FillCustomersList(dbConnSqlServer)
                FillTypeList(dbConnSqlServer)
                FillVendorList(dbConnSqlServer)
                If Session("NCF") = Nothing Then
                    Session("Edit") = True
                    'New NCF
                    IssueDate.Text = Format(Now, "MM/dd/yyyy")
                    FillEmployeeInfo(dbConnSqlServer)
                    Try
                        'DDN: Customer defaulted to LCL32820 and contact list for LCL house updated
                        Customer.SelectedValue = 32820
                        FillContactList(dbConnSqlServer)
                    Catch e As Exception
                        'Customer LCL (House) not found
                    End Try
                Else
                    'Edit or view: Show information from database
                    ShowInformation(dbConnSqlServer)
                End If
            Else
                'fill dependant lists
                'Select customer if returning from search
                '(contact depends on customer)
                'category depends on Type
                SelectCustomer()
                FillContactList(dbConnSqlServer)
                FillCategoryList(dbConnSqlServer)
                'Fill contact fields (tel #)
                FillContactInfo(dbConnSqlServer)
                'FillVendorList(dbConnSqlServer)
            End If

            If Session("Edit") = False Then
                'If user is in view mode, controls must be desactivated
                disableControls(Page)
            End If
        Else
            Response.Redirect("COMMUN/denied.html")
        End If


    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Valids if user has the right to access this page, comming from an email                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function ValidUser(ByVal dbConnSqlServer As OleDbConnection) As String
        Session("NCF") = Nothing
        Session("Edit") = True
        Dim userValid As Boolean = False

        Dim dtTable As New DataTable
        Dim cmdTable As OleDbDataAdapter
        Dim strReq As String

        strReq = "Select EmployeeWN, Status from NCF_General where NCFNo=" & Request.QueryString("NCFNo")
        cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)
        If dtTable.Rows.Count > 0 Then
            If Trim(dtTable.Rows(0)(0)) = strEMPName Then
                Session("NCF") = Request.QueryString("NCFNo")
                If dtTable.Rows(0)("Status") = 4 Then
                    Session("Edit") = False
                End If
                userValid = True
            End If
        End If

        ValidUser = userValid
    End Function


    '|----------------------------------------------------------------------------------------------------------|
    '|  Selects Customer                                                                                        |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SelectCustomer()
        If Session("CustomerSearch") <> Nothing Then
            Customer.SelectedValue = Session("CustomerSearch")
            Session("CustomerSearch") = Nothing
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the customer Combobox in Customer name order                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillCustomersList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String

        strReq = "Select (CUSTOMERNAME + ' --- ' + City + ' --- ' + Cast(CUSTOMERNO as Varchar)), CustomerNo from QRY_Customers order by CustomerName"
        AddItemsInComboBox(Customer, strReq, dbConnSqlServer)
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the type Combobox in Type order                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillTypeList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String

        Dim Type As String = "Type_" & Session("LG")

        strReq = "Select " & Type & ", " & Type & " from Category group by " & Type & " order by " & Type & ""
        AddItemsInComboBox(CategoryTypes, strReq, dbConnSqlServer)
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the contact Combobox in Contact name order                                                        |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillContactList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String

        If Customer.SelectedValue <> Session("Customer") Or Session("EditContact") <> Nothing Then
            If Session("CustomerChanged") <> Nothing Then
                Customer.SelectedValue = Session("CustomerChanged")
                Session("CustomerChanged") = Nothing
            End If
            Session("Customer") = Customer.SelectedValue

            strReq = "select ContactName, ContactNo from Contact where CustomerNo=" & Customer.SelectedValue & _
                    " order by contactName"
            AddItemsInComboBox(ContactName, strReq, dbConnSqlServer)
            If Session("EditContact") <> Nothing Then
                ContactName.SelectedValue = Session("EditContact")
            End If

            Session("EditContact") = Nothing
            Session("Contact") = Nothing
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the Category Combobox in Category order                                                           |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillCategoryList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String
        Dim Type As String = "Type_" & Session("LG")
        Dim Category As String = "Category_" & Session("LG")

        If CategoryTypes.Value <> Session("CategoryTypes") Then
            Session("CategoryTypes") = CategoryTypes.Value
            strReq = "Select " & Category & ", CategoryNo from category where " & Type & "='" & Trim(CategoryTypes.Value) & "' order by " & Category
            AddItemsInComboBox(Categories, strReq, dbConnSqlServer)
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the Employee informations (name and tel #) from the current identity he is logged in              |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillEmployeeInfo(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String

        strReq = "select displayName from AD_Employee where sAMAccountName='" & strEMPName & "'"
        AddInfoInTextBox(IssuedBy, strReq, dbConnSqlServer)
        strReq = "select telephonenumber from AD_Employee where sAMAccountName='" & strEMPName & "'"
        AddInfoInTextBox(Tel, strReq, dbConnSqlServer)

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the Contact informations (tel #)                                                                  |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillContactInfo(ByVal dbConnSqlServer As OleDbConnection)
        ContactTel.Text = ""

        Dim strReq As String
        If ContactName.Value <> Session("Contact") And ContactName.Value <> "" Then
            Session("Contact") = ContactName.Value
            strReq = "select ContactTel from Contact where ContactNo=" & ContactName.Value
            AddInfoInTextBox(ContactTel, strReq, dbConnSqlServer)
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the Vendor List
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillVendorList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String

        strReq = "Select (Vendor_Name + ' --- ' + Cast(Vendor_Number as Varchar)), Vendor_Number from QRY_Vendors order by Vendor_Name"
        AddItemsInComboBox(Vendor, strReq, dbConnSqlServer)
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
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Adds items from a query in a Textbox                                                                    |
    '|  In: query and Textbox                                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub AddInfoInTextBox(ByVal field As TextBox, ByVal strReq As String, ByVal dbConnSqlServer As OleDbConnection)
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)
        If dtTable.Rows.Count > 0 Then
            If Not IsDBNull(dtTable.Rows(0)(0)) Then
                field.Text = dtTable.Rows(0)(0)
            End If
        Else
            field.Text = ""
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fill NCF informations from the database                                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub ShowInformation(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String = "Select * from qry_GeneralInfo where NCFNo=" & Session("NCF")
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            If Not IsDBNull(dtTable.Rows(0)("FormType")) Then
                ConvertBitToRadioButton("FormType", dtTable.Rows(0)("FormType"))        'Form Type
            End If

            If Not IsDBNull(dtTable.Rows(0)("FeedBackType")) Then
                ConvertBitToRadioButton("FeedbackType", dtTable.Rows(0)("FeedBackType")) 'FeedBack Type
            End If

            If Not IsDBNull(dtTable.Rows(0)("Production")) Then
                ConvertBitToRadioButton("AffectProd", dtTable.Rows(0)("Production"))    'Production
            End If

            If Not IsDBNull(dtTable.Rows(0)("Shared")) Then
                ConvertBitToRadioButton("Shared", dtTable.Rows(0)("Shared"))            'Sharing
            End If

            Customer.SelectedValue = Trim(dtTable.Rows(0)("CustomerNo"))                      'Selects good Customer
            CategoryTypes.SelectedValue = Trim(dtTable.Rows(0)("Type_" & Session("LG")))      'Selects good Category Type

            FillContactList(dbConnSqlServer)            'Fills contact Combobox depending on the Customer
            FillCategoryList(dbConnSqlServer)           'Fills Caregory Combobox depending on the Category Type

            ContactName.SelectedValue = dtTable.Rows(0)("ContactNo")                    'Selects good Contact        
            Categories.SelectedValue = Trim(dtTable.Rows(0)("CategoryNo"))              'Selects good Category

            FillTextBox(IssueDate, dtTable.Rows(0), "IssueDate")                        'Issue date
            FillTextBox(OrderNo, dtTable.Rows(0), "OrderNo")                            'Order #
            FillTextBox(ContactTel, dtTable.Rows(0), "ContactTel")                      'Contact phone #
            FillTextBox(IssuedBy, dtTable.Rows(0), "displayName")                       'Employee Name
            FillTextBox(Tel, dtTable.Rows(0), "telephoneNumber")                        'Employe phone #
        End If

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fill textboxes from a database field (makes sure it is not null                                         |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillTextBox(ByVal textField As TextBox, ByVal Row As DataRow, ByVal ColumnField As String)
        If Not IsDBNull(Row(ColumnField)) Then
            textField.Text = Row(ColumnField)
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Desactivates controles (recursive)                                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub disableControls(ByVal Parent As Control)
        Dim c As Control
        'Goes as deep as it can and finds all the controls
        'Enables textboxes, RadioButtons and comboboxes
        For Each c In Parent.Controls
            If c.GetType().ToString() = "System.Web.UI.WebControls.TextBox" Then
                CType(c, TextBox).ReadOnly = True
                CType(c, TextBox).Enabled = False
            End If

            If c.GetType().ToString() = "System.Web.UI.WebControls.RadioButton" Then
                CType(c, RadioButton).Enabled = False
            End If

            If c.GetType().ToString() = "ASP.Combobox_ascx" Then
                CType(c, ComboBox).Enabled = False
            End If

            If c.Controls.Count > 0 Then
                disableControls(c)
            End If
        Next
    End Sub

    ' 
    '|----------------------------------------------------------------------------------------------|
    '| ConvertBitToRadioButton                                                                      |
    '| Takes a bit form the DB and selects the right radioButton	                                |
    '| Since bit is 0 or 1, and radio is 1 or 2, the bit value will be incremented by 1         	|
    '|----------------------------------------------------------------------------------------------|
    Private Sub ConvertBitToRadioButton(ByVal field As String, ByVal bitValue As Boolean)
        Dim radio As RadioButton
        If bitValue = False Then
            radio = FindControl(field & "1")
        Else
            radio = FindControl(field & "2")
        End If
        radio.Checked = True
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when "Next" Button is clicked                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub NextPage(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim valide As Boolean = True
        Dim isError As String

        'if user is in insert or edit mode, save informations in db
        If Session("Edit") = True Then
            valide = validePage()

            'If page is valid then save information
            If valide Then
                'Sauver dans la bd
                SaveGeneralInformation()
                Response.Redirect("DescriptionDeficiency_" & Session("LG") & ".aspx")
            Else
                'If page is not valid, show error message
                isError = "<script language=javascript>alert('"

                If Session("LG") = "FR" Then
                    isError += "Certains champs sont manquants ou incorrects.\n"
                    isError += "Veuillez consulter les champs en rouge\n"
                Else
                    isError += "Some fields are missing or incorrect.\n"
                    isError += "Please check red fields\n"
                End If

                isError += "');</script>"
                Response.Write(isError)
            End If

        Else
            'if user is in view mode, move on to the next page
            Response.Redirect("DescriptionDeficiency_" & Session("LG") & ".aspx")
        End If

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Valids General informations: Makes sure every necessaray is entered, and that data type are correct     |
    '|  (alors marks invalid fields in red                                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function validePage() As Boolean
        Dim valide As Boolean = True

        'puts all fields back to normal (white)
        initialiserCouleurs()

        'Missing Form Type
        If Request.Form("FormType") = Nothing Then
            valide = False
            FormType1.BackColor = Color.Red
            FormType2.BackColor = Color.Red
        End If

        'Missing FeedBack Type
        If Request.Form("FeedbackType") = Nothing Then
            valide = False
            FeedbackType1.BackColor = Color.Red
            FeedbackType2.BackColor = Color.Red
        End If

        'Missing Production
        If Request.Form("AffectProd") = Nothing Then
            valide = False
            AffectProd1.BackColor = Color.Red
            AffectProd2.BackColor = Color.Red
        End If

        'Missing Sharing
        If Request.Form("Shared") = Nothing Then
            valide = False
            Shared1.BackColor = Color.Red
            Shared2.BackColor = Color.Red
        End If

        'Missing Customer
        If Customer.Text = "" Then
            valide = False
            Customer.bgColor = Color.Red
        End If

        'Missing Contact
        If ContactName.Text = "" Then
            valide = False
            ContactName.bgColor = Color.Red
        End If

        'Missing Category Type
        If CategoryTypes.Value = "" Then
            valide = False
            CategoryTypes.bgColor = Color.Red
        End If

        'Missing Category
        If Categories.Value = "" Then
            valide = False
            Categories.bgColor = Color.Red
        End If

        'Invalid Issue date 
        If IssueDate.Text = "" Or Not IsDate(IssueDate.Text) Then
            valide = False
            IssueDate.BackColor = Color.Red
        End If

        validePage = valide
    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Makes all the fields back to normal                                                                     |
    '|  comboboxes et textboxes in white, RadioButton transparent                                               |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub initialiserCouleurs()
        Customer.bgColor = Color.White
        ContactName.bgColor = Color.White
        CategoryTypes.bgColor = Color.White
        Categories.bgColor = Color.White
        IssueDate.BackColor = Color.White
        FormType1.BackColor = Nothing
        FormType2.BackColor = Nothing
        FeedbackType1.BackColor = Nothing
        FeedbackType2.BackColor = Nothing        
        AffectProd1.BackColor = Nothing
        AffectProd2.BackColor = Nothing
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Saves Information in the database                                                                       |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SaveGeneralInformation()
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim cmdComm As New OleDbCommand
        Dim strReq As String
        Dim strReq1 As String

        dbConnSqlServer.Open()

        If Session("NCF") = Nothing Then
            'New -> insert
            strReq = InsertNCF(dbConnSqlServer)
            strReq1 = InsertAdminNCF(dbConnSqlServer)
        Else
            'already exists, update
            strReq = UpdateNCF(dbConnSqlServer)
        End If

        cmdComm.Connection = dbConnSqlServer
        cmdComm.CommandText = strReq
        cmdComm.ExecuteNonQuery()

        If strReq1 <> "" Then
            cmdComm.CommandText = strReq1
            cmdComm.ExecuteNonQuery()
        End If

        dbConnSqlServer.Close()

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Insert General information Query                                                                        |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function InsertNCF(ByVal dbConnSqlServer As OleDbConnection) As String
        Dim dtTable As New DataTable
        Dim cmdTable As OleDbDataAdapter
        Dim strReq As String

        strReq = "select * from QRY_NCFNo"
        cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        'Get NCF New #
        If dtTable.Rows.Count > 0 Then
            Session("NCF") = dtTable.Rows(0)(0) + 1
        Else
            'If first feedback
            Session("NCF") = 1
        End If

        'Insert Query
        strReq = "Insert into NCF_General values("

        strReq += Session("NCF") & ", "
        strReq += ConvertRadioButtonToBit("FormType") & ", "
        strReq += ConvertRadioButtonToBit("FeedbackType") & ", "
        strReq += ConvertRadioButtonToBit("AffectProd") & ", "
        strReq += ConvertRadioButtonToBit("Shared") & ", "
        strReq += ContactName.Value & ", "
        strReq += "'" & strEMPName & "', "
        strReq += "'" & IssueDate.Text & "', "
        strReq += "'" & OrderNo.Text & "', "
        strReq += "'" & Categories.Value & "', "

        strReq += "null, "                            'Description of deficiency -> entered in next forms
        strReq += "null, "                            'Action -> entered in next forms
        strReq += "null, "                            'Cause -> entered in next forms
        strReq += "null, "                            'Suggestions -> entered in next forms

        strReq += "1, "                               'Status: 1=incomplete
        'Ajout DDN: Vendor + POBox
        strReq += "'" & Trim(Vendor.Value) & "', "
        strReq += "'" & PONo.Text & "'"
        strReq += ")"

        InsertNCF = strReq
    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|   New Admin record - Insert Query                                                                        |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function InsertAdminNCF(ByVal dbConnSqlServer As OleDbConnection) As String
        Dim strReq As String

        'Insert Query
        strReq = "Insert into NCF_Admin values("

        strReq += Session("NCF") & ", "
        strReq += "null, "
        strReq += "null, "
        strReq += "null, "
        strReq += "null, "
        strReq += "null, "
        strReq += "null, "
        strReq += "null, "
        strReq += "null, "
        strReq += "null, "
        strReq += "null, "
        strReq += "0, "
        strReq += "0 "

        strReq += ")"

        InsertAdminNCF = strReq
    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Updates General information Query                                                                       |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function UpdateNCF(ByVal dbConnSqlServer As OleDbConnection) As String
        Dim strReq As String

        'Update Query
        strReq = "Update NCF_General set "

        strReq += "FormType =" & ConvertRadioButtonToBit("FormType") & ", "
        strReq += "FeedBackType =" & ConvertRadioButtonToBit("FeedbackType") & ", "
        strReq += "Production =" & ConvertRadioButtonToBit("AffectProd") & ", "
        strReq += "ContactNo =" & ContactName.Value & ", "
        strReq += "IssueDate =" & "'" & IssueDate.Text & "', "
        strReq += "OrderNo =" & "'" & OrderNo.Text & "', "
        strReq += "CategoryNo =" & "'" & Categories.Value & "', "
        'Ajout DDN: Vendor + POBox
        strReq += "VendorNo = '" & Trim(Vendor.Value) & "', "
        strReq += "PONo = '" & PONo.Text & "'"
        strReq += "where NCFNo=" & Session("NCF")

        UpdateNCF = strReq
    End Function

    ' 
    '|----------------------------------------------------------------------------------------------|
    '| ConvertRadioButtonToBit                                                                      |
    '| Takes a value form the radioButton and transforms it into bit                                |
    '|----------------------------------------------------------------------------------------------|
    Private Function ConvertRadioButtonToBit(ByVal field As String) As Integer
        Dim bin As Integer = 0
        System.Diagnostics.Debug.Write(Request.Form(field))
        System.Diagnostics.Debug.Write(field & "2")
        If Request.Form(field) = field & "2" Then
            bin = 1
        End If

        ConvertRadioButtonToBit = bin
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
