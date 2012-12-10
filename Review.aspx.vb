Imports System
Imports System.Web.Mail
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.DBNull
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports UserControls.CC_Controls

Public Class Review
    Inherits System.Web.UI.Page

    '
    '|----------------------------------------------------------------------------------------------|
    '| Important: Reviews deals with all of the 6 sections :                                        |             
    '| (AD_GeneralInfo, Ad_FurtherActions, Ad_FurtherActions, Ad_RootCause, Ad_CorrectiveAction     |
    '| AND Ad_CloseOut)		                                                                        |
    '|----------------------------------------------------------------------------------------------|

    'Tab links
    Public A_GI As System.Web.UI.WebControls.LinkButton
    Public A_SA As System.Web.UI.WebControls.LinkButton
    Public A_SB As System.Web.UI.WebControls.LinkButton
    Public A_SC As System.Web.UI.WebControls.LinkButton
    Public A_SD As System.Web.UI.WebControls.LinkButton
    Public A_CO As System.Web.UI.WebControls.LinkButton

'----------------------------------------------- Declarations -----------------------------------------------'
    'General Info
    Public AssignedTo As System.Web.UI.WebControls.DropDownList
    Public FollowUpDate As System.Web.UI.WebControls.TextBox
    Public TargetCloseOutDate As System.Web.UI.WebControls.TextBox
    Public DelegatedTo As System.Web.UI.WebControls.TextBox

    'MultiTexboxes
    Public FurtherClarifications As System.Web.UI.WebControls.TextBox
    Public FurtherActions As System.Web.UI.WebControls.TextBox
    Public CorrectiveAction As System.Web.UI.WebControls.TextBox
    Public RootCause As System.Web.UI.WebControls.TextBox

    'Comboboxes
    Public CategoryTypes as ComboBox
    Public Categories as ComboBox

    'DropDownLists
    Public FeedbackType as DropDownList
    Public Production as DropDownList

    'Close Out
    Public CloseOutDate As System.Web.UI.WebControls.TextBox
    Public CloseOutMsg As System.Web.UI.WebControls.TextBox
    Public HasProducedChange As System.Web.UI.WebControls.CheckBox
    Public FollowUp As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Division As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents chkSendMail As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button

    'Save Button

   
    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page Review is loaded                                                                     |
    '|----------------------------------------------------------------------------------------------------------|
    Sub LoadPage()
        'Put user code to initialize the page here
        ' Indique la division à afficher
        A_GI.Attributes.Add("onClick", "javacript:document.Review.Division.value = ""GI"";")
        A_SA.Attributes.Add("onClick", "javacript:document.Review.Division.value = ""SA"";")
        A_SB.Attributes.Add("onClick", "javacript:document.Review.Division.value = ""SB"";")
        A_SC.Attributes.Add("onClick", "javacript:document.Review.Division.value = ""SC"";")
        A_SD.Attributes.Add("onClick", "javacript:document.Review.Division.value = ""SD"";")
        A_CO.Attributes.Add("onClick", "javacript:document.Review.Division.value = ""CO"";")

        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()

        If Request.QueryString("NCFNo") <> Nothing Then
            ValidNCF(dbConnSqlServer)    'if not valid, Session("NCF") = Nothing
        End If

        'user must be in feedback comitee or IT_Projects to access this page
        If (User.IsInRole("LCLMTL\LCL_Feedback_Committee") = True Or _
            User.IsInRole("LCLMTL\LCL_IT_Projects") = True Or User.Identity.Name = "LCLMTL\d2dadmin") And Session("NCF") <> Nothing Then
            If Not Page.IsPostBack Then
                'fill all information entered in the database
                Session("CategoryTypes") = ""
                FillFeedBackType()
                FillProduction()
                FillTypeList(dbConnSqlServer)
                showInformations(dbConnSqlServer)
            Else
                FillCategoryList(dbConnSqlServer)
            End If

            'If user is in view mode, save button and other controls are disabled
            If Session("Edit") = False Then
                btnSave.Enabled = False
                disableControls(Page)
            End If


        Else
            Response.Redirect("COMMUN/denied.html")
        End If

    End Sub

'------------------------------------------------ Filling lists ------------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the feedback type DropDownList                                                                    |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillFeedBackType()

        If Session("LG") = "FR"
            FeedbackType.Items.Add(New ListItem("Interne", False))
            FeedbackType.Items.Add(New ListItem("Externe", True))
        Else
            FeedbackType.Items.Add(New ListItem("Internal", False))
            FeedbackType.Items.Add(New ListItem("External", True))
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the feedback type DropDownList                                                                    |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillProduction()
        If Session("LG") = "FR"
            Production.Items.Add(New ListItem("Oui", False))
            Production.Items.Add(New ListItem("Non", True))
        Else
            Production.Items.Add(New ListItem("Yes", False))
            Production.Items.Add(New ListItem("No", True))
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the type Combobox in Type order                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillTypeList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String

        Dim Type as string = "Type_" & Session("LG")

        strReq = "Select " &  Type & ", " & Type & " from Category group by " & Type & " order by " & Type & ""
        AddItemsInComboBox(CategoryTypes, strReq, dbConnSqlServer)
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Fills the Category Combobox in Category order                                                           |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillCategoryList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String
        Dim Type as string = "Type_" & Session("LG")
        Dim Category as string = "Category_" & Session("LG")

        If CategoryTypes.Value <> Session("CategoryTypes") Then
            Session("CategoryTypes") = CategoryTypes.Value
            strReq = "Select " & Category & ", CategoryNo from category where " & Type & "='" & Trim(CategoryTypes.Value) & "' order by " & Category
            AddItemsInComboBox(Categories, strReq, dbConnSqlServer)
        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  fill assignement list from the assignements table                                                       |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub FillAssignementsList(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String = "Select '' AS EmployeeName, '-1' AS EmployeeNo"
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)
        strReq = "Select EmployeeName, EmployeeNo from Assignments"
        cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        AssignedTo.DataTextField = "EmployeeName"
        AssignedTo.DataValueField = "EmployeeNo"
        AssignedTo.DataSource = dtTable
        AssignedTo.DataBind()
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Adds items from a query in a Combobox                                                                   |
    '|  In: query and Combobox                                                                                  |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub AddItemsInComboBox(ByVal ComboB As Combobox, ByVal strReq As String, ByVal dbConnSqlServer As OleDbConnection)
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)

        cmdTable.Fill(dtTable)
        ComboB.AddItems(dtTable)
    End Sub


'------------------------------------------------- Validation --------------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  Valids if user has the right to access this page                                                        |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub ValidNCF(ByVal dbConnSqlServer As OleDbConnection)
        Session("NCF") = Nothing

        Dim dtTable As New DataTable
        Dim cmdTable As OleDbDataAdapter
        Dim strReq As String

        strReq = "Select * from NCF_General where status in (3,4) AND NCFNo=" & Request.QueryString("NCFNo")
        cmdTable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)
        If dtTable.Rows.Count > 0 Then
            Session("NCF") = Request.QueryString("NCFNo")
            If dtTable.Rows(0)("Status") = 3 Then
                Session("Edit") = True
            Else
                Session("Edit") = False
            End If

        End If
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Valids review : if valid, saves it                                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub Save(ByVal sender As Object, ByVal e As EventArgs)
        ValidReview()
    End Sub

    Private Sub ValidReview()
        Dim strError As String = ""
        Dim StrMsg As String = ""

        'invalid Categories
        If Categories.Text = "" Or Categories.Text = Nothing Then
             If Session("LG") = "FR" then 'message en francais
                StrMsg += "Vous devez entrer une catégorie.\n"
            Else
                StrMsg += "You must enter a category.\n"
            End If
        
        'invalid Dates
        ElseIf FollowUpDate.Text <> "" And Not IsDate(FollowUpDate.Text) Then
             If Session("LG") = "FR" then 'message en francais
                StrMsg += "Date du Suivi Invalide.\n"
            Else
                StrMsg += "Invalid Follow Up Date.\n"
            End If
        ElseIf TargetCloseOutDate.Text <> "" And Not IsDate(TargetCloseOutDate.Text) Then
            If Session("LG") = "FR" then 'message en francais
                StrMsg += "Date Prévue de Fermeture Invalide.\n"
            Else
                StrMsg += "Invalid Target Close Out Date.\n"
            End If
        ElseIf CloseOutDate.Text <> "" And Not IsDate(CloseOutDate.Text) Then
            If Session("LG") = "FR" then 'message en francais
                StrMsg += "Date de Fermeture Invalide.\n"
            Else
                StrMsg += "Invalid Close Out Date.\n"
            End If
            'ElseIf AssignedTo.SelectedValue = -1 Then
            '    'Unassigned
            '    If Session("LG") = "FR" then 'message en francais
            '        StrMsg += "Rétroaction non-assignée.\n"
            '    Else  
            '        StrMsg += "FeedBack unassigned.\n"
            '    End If
        Else
            'Valid
            SaveFeedbackInformation()
            SaveReviewInformation()
        End If

        'If there is an error message, show it
        If StrMsg <> "" Then
            strError = "<script language=javascript>alert('"
            strError += StrMsg
            strError += "');</script>"
            Response.Write(strError)
        End If
    End Sub

'--------------------------------------------------- Display ---------------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  fill all information entered in the database                                                            |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub showInformations(ByVal dbConnSqlServer As OleDbConnection)
        Dim strReq As String
        Dim dtTable As New DataTable
        Dim cmdtable As OleDbDataAdapter

        FillAssignementsList(dbConnSqlServer)

        strReq = "Select * from qry_Review_" & Session("LG") & " where NCFNo=" & Session("NCF")
        cmdtable = New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdtable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            AddnformationInFields(dtTable, dbConnSqlServer)
        End If

    End Sub


    '|----------------------------------------------------------------------------------------------------------|
    '|  every column in the query is insert to the correct place in the form                                    |
    '|  Important: query fields and controls must match names                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub AddnformationInFields(ByVal dtTable As DataTable, ByVal dbConnSqlServer As OleDbConnection)
        Dim i As Integer
        Dim nomControle As String

        'for all columns in the query
        For i = 0 To dtTable.Columns.Count - 1
            'the name of the control is the same as the column
            nomControle = dtTable.Columns(i).ToString
            System.Diagnostics.Debug.WriteLine(dtTable.Columns(i).ToString & " : " & dtTable.Rows(0)(i))
            'If something is entered for this data
            If Not IsDBNull(dtTable.Rows(0)(i)) Then
                Select Case FindControl(nomControle).GetType.ToString
                    Case "System.Web.UI.WebControls.DropDownList"
                        'If controlType = DropDownList, select good value
                        CType(FindControl(nomControle), DropDownList).SelectedIndex = dtTable.Rows(0)(i)
                    Case "System.Web.UI.WebControls.Label"
                        'If controlType = Label, enter the data in the label
                        CType(FindControl(nomControle), Label).Text = dtTable.Rows(0)(i)
                    Case "System.Web.UI.WebControls.TextBox"
                        'If controlType = Textbox, enter the data in the Textbox
                        CType(FindControl(nomControle), TextBox).Text = dtTable.Rows(0)(i)
                    Case "ASP.Combobox_ascx"
                        'If controlType = Textbox, enter the data in the Textbox
                        CType(FindControl(nomControle), ComboBox).SelectedValue = dtTable.Rows(0)(i)
                        If nomControle = "CategoryTypes" Then
                            FillCategoryList(dbConnSqlServer)
                        End If
                    Case "System.Web.UI.WebControls.CheckBox"
                        'If controlType = Checkbox, check if true, vice versa
                        CType(FindControl(nomControle), CheckBox).Checked = dtTable.Rows(0)(i)
                End Select
            End If
        Next
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Desactivates controles (recursive)                                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub disableControls(ByVal Parent As Control)
        Dim c As Control
        For Each c In Parent.Controls
            If c.GetType().ToString() = "System.Web.UI.WebControls.TextBox" Then
                CType(c, TextBox).ReadOnly = True
                CType(c, TextBox).Enabled = False
            End If

            If c.GetType().ToString() = "System.Web.UI.WebControls.DropDownList" Then
                CType(c, DropDownList).Enabled = False
            End If

            If c.GetType().ToString() = "ASP.Combobox_ascx" Then
                CType(c, ComboBox).Enabled = False
            End If

            If c.Controls.Count > 0 Then
                disableControls(c)
            End If
        Next
    End Sub



    '------------------------------------------ Database - Table updates -------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  Administration can change some information from the feedback                                            |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SaveFeedbackInformation()
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim strReq As String
        Dim Comm As New OleDbCommand

        dbConnSqlServer.Open()
        Comm.Connection = dbConnSqlServer

        'Save general information
        strReq = SaveGeneralInformation()
        Comm.CommandText = strReq
        Comm.ExecuteNonQuery()

        dbConnSqlServer.Close()

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Saves review information : in both tables                                                               |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SaveReviewInformation()
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim strReq As String
        Dim Comm As New OleDbCommand

        dbConnSqlServer.Open()
        Comm.Connection = dbConnSqlServer

        'Change Status and send Email
        strReq = SaveStatus(dbConnSqlServer)
        If (strReq <> "") Then
            Comm.CommandText = strReq
            Comm.ExecuteNonQuery()
        End If

        'Save admin section
        strReq = SaveAdminSection()
        Comm.CommandText = strReq
        Comm.ExecuteNonQuery()

        dbConnSqlServer.Close()

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Builds the update query for changed general information                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function SaveGeneralInformation() As String
        Dim strReq As String

        strReq = "Update NCF_General set "

        strReq += "FeedBackType= " & IIf(FeedbackType.SelectedValue, 1, 0) & ", "
        strReq += "Production= " & IIf(Production.SelectedValue, 1, 0) & ", "
        strReq += "CategoryNo= '" & Categories.SelectedValue & "' "

        strReq += " WHERE NCFNO = " & Session("NCF")

        SaveGeneralInformation = strReq

    End Function


    '|----------------------------------------------------------------------------------------------------------|
    '|  Builds the update query for NCF_Admin Table                                                             |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function SaveAdminSection() As String
        Dim strReq As String

        strReq = "Update NCF_Admin set "

        'Assigned To
        If AssignedTo.SelectedValue <> -1 Then
            strReq += "AssignedTo= " & AssignedTo.SelectedValue & ", "
        Else
            strReq += "AssignedTo = null, "
        End If

        'Delegated To
        strReq += "DelegatedTo= '" & DelegatedTo.Text & "', "

        'Follow Up Date
        If FollowUpDate.Text <> "" Then
            strReq += "FollowUpDate= '" & FollowUpDate.Text & "', "
        End If

        'Target Close Out Date
        If TargetCloseOutDate.Text <> "" Then
            strReq += "TargetCloseOutDate= '" & TargetCloseOutDate.Text & "', "
        End If

        'Further Clarifications
        If FurtherClarifications.Text <> "" Then
            strReq += "FurtherClarifications= '" & Replace(FurtherClarifications.Text, "'", "''") & "', "
        End If

        'Further Actions
        If FurtherActions.Text <> "" Then
            strReq += "FurtherActions= '" & Replace(FurtherActions.Text, "'", "''") & "', "
        End If

        'Root Cause
        If RootCause.Text <> "" Then
            strReq += "RootCause= '" & Replace(RootCause.Text, "'", "''") & "', "
        End If

        'Corrective Action
        If CorrectiveAction.Text <> "" Then
            strReq += "CorrectiveAction= '" & Replace(CorrectiveAction.Text, "'", "''") & "', "
        End If

        'HasProducedChange
        If HasProducedChange.Checked Then
            strReq += "HasProducedChange = 1, "
        Else
            strReq += "HasProducedChange = 0, "
        End If

        'FollowUp
        If FollowUp.Checked Then
            strReq += "FollowUp = 1, "
        Else
            strReq += "FollowUp = 0, "
        End If

        'Close-out Date
        If CloseOutDate.Text <> "" Then
            strReq += "CloseOutDate= '" & CloseOutDate.Text & "', "
        End If

        'Close-out Message
        If CloseOutMsg.Text <> "" Then
            strReq += "CloseOutMsg= '" & Replace(CloseOutMsg.Text, "'", "''") & "', "
        End If



        'Delete last comma ','
        strReq = strReq.Substring(0, strReq.Length - 2)

        strReq += " WHERE NCFNO = " & Session("NCF")

        SaveAdminSection = strReq

    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Builds the update query for NCF_General Table (updates status)                                          |
    '|  If form is assigned or closed, sends appropriate email                                                  |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function SaveStatus(ByVal dbConnSqlServer As OleDbConnection) As String
        Dim strReq As String = ""

        strReq = "Update NCF_General set "
        If CloseOutDate.Text <> "" Then
            'Status = 4: Closed
            strReq += "Status=4 WHERE NCFNO = " & Session("NCF")
            'Send email on close out
            SendCloseOutMail(dbConnSqlServer)

            'now not possible to edit
            Session("edit") = False
        ElseIf AssignedTo.SelectedValue <> -1 Then
            'Status = 3: Assigned
            strReq += "Status=3 WHERE NCFNO = " & Session("NCF")
            'send mail on assignment
            SendAssignedMail(dbConnSqlServer)
        Else
            strReq += "Status=2 WHERE NCFNO = " & Session("NCF")
        End If

        SaveStatus = strReq
    End Function

    '---------------------------------------------- Email Functions ------------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  Sends an Email                                                                                          |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SendMail(ByVal strTo As String, ByVal strSubject As String, ByVal strBody As String)
        Dim mail As New MailMessage
        Try
            If chkSendMail.Checked = True Then
                If strTo <> "" Then
                    mail.From = "Comitee.CFF@laurentidecontrols.com"
                    mail.To = strTo
                    mail.Subject = strSubject
                    mail.BodyFormat = MailFormat.Html
                    mail.Body = strBody
                    'mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtauthenticate", 2)
                    'mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "lcl-exc")
                    SmtpMail.SmtpServer = "lcl-exc"
                    SmtpMail.Send(mail)
                End If
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.Write(ex)
        End Try
    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Sends an email to inform the employee to whom the NCF has been assigned to                              |
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SendAssignedMail(ByVal dbConnSqlServer As OleDbConnection)
        Dim strToOldAssigned As String = ""
        Dim strToNewAssigned As String = ""
        Dim strToOriginator As String = ""

        Dim strSubject As String
        Dim assigned As String = "" 'wether feedback is assigned or reassigned
        Dim i As Integer = 0

        'Checks wether NCF is assigned for the first time or not
        Dim strReq As String = "Select AssignedTo from NCF_Admin where NCFNO=" & Session("NCF")
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            If IsDBNull(dtTable.Rows(0)("AssignedTo")) Then
                'Assigned
                assigned = "assignée à "
            ElseIf dtTable.Rows(0)("AssignedTo").ToString <> AssignedTo.SelectedValue Then
                'Reassigned
                assigned = "ré-assignée à "

                'Send Email to current assigned employee
                strToOldAssigned = Reassigned(dbConnSqlServer, dtTable.Rows(0)("AssignedTo"))
            End If

            If assigned <> "" Then
                'Send Email to new assigned employee
                strToNewAssigned = NewAssigned(dbConnSqlServer)

                'Send Email to originator
                strToOriginator = Originator(dbConnSqlServer)

                'Builind Message
                strSubject = "Rétroaction #" & Session("NCF") & " " & assigned & _
                                AssignedTo.SelectedItem.ToString

                SendMail(strToOldAssigned, strSubject, "")
                SendMail(strToNewAssigned, strSubject, AsAssignedBody)
                SendMail(strToOriginator, strSubject, OrAssignedBody)
            End If
        End If

    End Sub

    '------------------------------------------------ Email Bodies -------------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  Return Assigned body written in new assigned employee 's mail                                           |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function AsAssignedBody() As String
        Dim bodyFr As String
        Dim bodyEn As String

        bodyFr = "Si vous désirez consulter les informations sur le suivi de cette rétroation, veuillez suivre le lien ci-dessous: <br />"

        bodyEn = "<br /><br /><br />"
        bodyEn += "If you would like to edit any information of the feedback, please follow this link: <br />"

        AsAssignedBody = bodyFr & " " & AslinkAssigned("FR", "Rétroaction #") & bodyEn & " " & AslinkAssigned("EN", "Feedback #")

    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Return Assigned body written in originator 's mail                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function OrAssignedBody() As String
        Dim bodyFr As String
        Dim bodyEn As String

        bodyFr = "Si vous désirez modifier des informations sur votre rétroactions, veuillez suivre le lien ci-dessous: <br />"

        bodyEn = "<br /><br /><br />"
        bodyEn += "If you would like to edit any information of your feedback, please follow this link: <br />"


        OrAssignedBody = bodyFr & " " & OrlinkAssigned("FR", "Rétroaction #") & bodyEn & " " & OrlinkAssigned("EN", "Feedback #")

    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Sends an email to inform the employee that the NCF is now closed (also sends hime the Close-out message)|
    '|----------------------------------------------------------------------------------------------------------|
    Private Sub SendCloseOutMail(ByVal dbConnSqlServer As OleDbConnection)
        'Builgind Message
        Dim strFrom As String = "Comitee.CFF@laurentidecontrols.com"
        Dim strSubject As String

        strSubject += "Rétroaction #" & Session("NCF") & " fermée le " & _
                                    CloseOutDate.Text & " par " & AssignedTo.SelectedItem.ToString

        Dim strToOriginator As String
        Dim strToAssigned As String

        strToOriginator = Originator(dbConnSqlServer)
        strToAssigned = NewAssigned(dbConnSqlServer)

        SendMail(strToOriginator, strSubject, OrCloseBody)
        SendMail(strToAssigned, strSubject, AsCloseBody)

    End Sub

    '|----------------------------------------------------------------------------------------------------------|
    '|  Return Close-Out body written in originator 's mail                                                     |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function OrCloseBody() As String
        Dim bodyFr As String
        Dim bodyEN As String

        bodyFr = "Si vous désirez consulter les informations sur cette fermeture, veuillez suivre le lien ci-dessous: <br />"


        bodyEN = "<br /><br /><br />"
        bodyEN += "If you would like to view Close-Out information, please follow this link: <br />"

        OrCloseBody = bodyFr & " " & ORlinkClose("FR", "Fermeture #") & bodyEN & ORlinkClose("EN", "Closing #") & _
                      "<br /><br />Message de fermeture<br /> " & _
                      "--------------------<br />" & _
                      CloseOutMsg.Text


    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Return Close-Out body written in assigned employee's mail                                               |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function AsCloseBody() As String
        Dim bodyFr As String
        Dim bodyEN As String

        bodyFr = "Si vous désirez consulter le suivi de cette rétroaction, veuillez suivre le lien ci-dessous: <br />"

        bodyEN = "<br /><br /><br />"
        bodyEN += "If you would like to review the closed feedback, please follow this link: <br />"


        AsCloseBody = bodyFr & " " & ASlinkClose("FR", "Fermeture #") & bodyEN & ASlinkClose("EN", "Closing #") & _
                      "<br /><br />Message de fermeture<br /> " & _
                      "--------------------<br />" & _
                      CloseOutMsg.Text

    End Function


    '------------------------------------------------- Email Links -------------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  Creates the assignment link in the assigned body depending on the language                              |
    '|----------------------------------------------------------------------------------------------------------|
    Function AslinkAssigned(ByVal strLang As String, ByVal strMsgLink As String) As String
        Dim link As String

        link += "<a href="
        link += """http://lcl-faxs/FRANCS"
        link += "/Review_" & strLang & ".aspx?NCFNo=" & Session("NCF") & " "" "
        link += ">" & strMsgLink & Session("NCF") & "</a>"

        AslinkAssigned = link

    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Creates the assignment link in the originator body depending on the language                            |
    '|----------------------------------------------------------------------------------------------------------|
    Function OrlinkAssigned(ByVal strLang As String, ByVal strMsgLink As String) As String
        Dim link As String

        link += "<a href="
        link += """http://lcl-faxs/FRANCS"
        link += "/GeneralInfo_" & strLang & ".aspx?NCFNo=" & Session("NCF") & " "" "
        link += ">" & strMsgLink & Session("NCF") & "</a>"

        OrlinkAssigned = link

    End Function


    '|----------------------------------------------------------------------------------------------------------|
    '|  Creates the closing link in the originator body depending on the language                               |
    '|----------------------------------------------------------------------------------------------------------|
    Function ORlinkClose(ByVal strLang As String, ByVal strMsgLink As String) As String
        Dim link As String

        link += "<a href="
        link += """http://lcl-faxs/FRANCS"
        link += "/CloseOutCommunications_" & strLang & ".aspx?NCFNo=" & Session("NCF") & " "" "
        link += ">" & strMsgLink & Session("NCF") & "</a>"

        ORlinkClose = link

    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Creates the closing link in the assigned body depending on the language                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Function ASlinkClose(ByVal strLang As String, ByVal strMsgLink As String) As String
        Dim link As String

        link += "<a href="
        link += """http://lcl-faxs/FRANCS"
        link += "/Review_" & strLang & ".aspx?NCFNo=" & Session("NCF") & " "" "
        link += ">" & strMsgLink & Session("NCF") & "</a>"

        ASlinkClose = link

    End Function

    '----------------------------------------------- Email Adresses ------------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  Returns the originator's mail address                                                                   |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function Originator(ByVal dbConnSqlServer As OleDbConnection) As String
        'Get employee Email
        Dim strTo As String = ""
        Dim strReq = "Select mail from QRY_EmployeeEmail where NCFNO=" & Session("NCF")
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

    '|----------------------------------------------------------------------------------------------------------|
    '|  Returns the new assigned employee's mail address                                                        |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function NewAssigned(ByVal dbConnSqlServer As OleDbConnection) As String
        'Get employee Email
        Dim strTo As String = ""
        Dim strReq = "Select Email from Assignments where EmployeeNo=" & AssignedTo.SelectedValue
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            If Not IsDBNull(dtTable.Rows(0)("Email")) Then
                strTo = dtTable.Rows(0)("Email")
            End If
        End If

        Return strTo
    End Function

    '|----------------------------------------------------------------------------------------------------------|
    '|  Returns the old assigned employee's address                                                             |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function Reassigned(ByVal dbConnSqlServer As OleDbConnection, ByVal OldAssigned As Integer) As String
        'Get employee Email
        Dim strTo As String = ""
        Dim strReq = "Select Email from Assignments where EmployeeNo=" & OldAssigned
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        If dtTable.Rows.Count > 0 Then
            If Not IsDBNull(dtTable.Rows(0)("Email")) Then
                strTo = dtTable.Rows(0)("Email")
            End If
        End If

        Return strTo
    End Function


    '------------------------------------------------- Assignments -------------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  Goes to the next Assignment                                                                             |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub NextAssigned(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Put user code to initialize the page here
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim strComm As String

        'Valid and saves information
        ValidReview()

        Dim strReq As String = "Select * from QRY_NextUnassigned"
        Dim dtTable As New DataTable
        Dim cmdTable As New OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.Fill(dtTable)

        Session("Assignments") = True
        Session("Edit") = True

        If dtTable.Rows.Count = 0 Then
            'Show message
            strComm = "<script Language=Javascript>"
            strComm += "alert('"
            If Session("LG") = "EN" Then
                strComm += "All feedbacks are assigned"
            Else
                strComm += "Toutes les rétroactions sont assignées"
            End If
            strComm += "');"
            strComm += "</script>"

            Response.Write(strComm)
        Else
            Session("NCF") = dtTable.Rows(0)(0)
            Response.Redirect("Review_" & Session("LG") & ".aspx")
        End If

    End Sub

    '------------------------------------------------- Connexions -------------------------------------------------'

    '|----------------------------------------------------------------------------------------------------------|
    '|  Establishes connexion to SQLServer                                                                      |
    '|----------------------------------------------------------------------------------------------------------|
    Private Function EtablitConnexionSQLServer() As OleDbConnection
        Dim strConn As String = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=NCF;" & _
                                "User ID=NCF;Password=NCF"
        EtablitConnexionSQLServer = New OleDbConnection(strConn)
    End Function

    Private Sub InitializeComponent()

    End Sub
End Class
