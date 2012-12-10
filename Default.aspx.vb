Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.DBNull
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports UserControls.CC_Controls

Public Class _Default
    Inherits System.Web.UI.Page
    Protected WithEvents FormType1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents FormType2 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents FeedbackType1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents FeedbackType2 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents ContactTel As System.Web.UI.WebControls.TextBox
    Protected WithEvents IssuedBy As System.Web.UI.WebControls.TextBox
    Protected WithEvents IssueDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents Tel As System.Web.UI.WebControls.TextBox
    Protected WithEvents OrderNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents AffectProd1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents AffectProd2 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Shared1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Shared2 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents Image2 As System.Web.UI.WebControls.Image
    Protected WithEvents Image3 As System.Web.UI.WebControls.Image
    Protected WithEvents cmdIT As System.Web.UI.WebControls.Button
    Protected WithEvents cmdOMCI As System.Web.UI.WebControls.Button
    Protected WithEvents cmdConcierge As System.Web.UI.WebControls.Button
    Protected WithEvents cmdSugg As System.Web.UI.WebControls.Button
    Protected WithEvents cmdEnglish As System.Web.UI.WebControls.Button
    Protected WithEvents cmdFrench As System.Web.UI.WebControls.Button
    Protected WithEvents Link As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents LinkButton1 As System.Web.UI.WebControls.LinkButton


    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page Assignments is clicked                                                                |
    '|----------------------------------------------------------------------------------------------------------|
    Public Sub Assignments(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Put user code to initialize the page here
        Dim dbConnSqlServer As OleDbConnection = EtablitConnexionSQLServer()
        Dim strComm As String
        
        
        Dim strReq as string = "Select * from QRY_NextUnassigned"
        Dim dtTable as new DataTable
        Dim cmdTable as new OleDbDataAdapter(strReq, dbConnSqlServer)
        cmdTable.fill(dtTable)

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

    Private Sub cmdIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIT.Click
        Response.Redirect("mailto:support@laurentide.com")
    End Sub

    Private Sub cmdOMCI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOMCI.Click
        Response.Redirect("https://spreadsheets0.google.com/viewform?hl=en&formkey=dGhrSU9NVEc4Qmw0R1V0MmJtN2RPMXc6MQ#gid=0")
    End Sub

    Private Sub cmdConcierge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConcierge.Click
        Response.Redirect("mailto:Kirkland.Concierge@laurentidecontrols.com")
    End Sub

    Private Sub cmdSugg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSugg.Click
        Response.Redirect("mailto:Committee.CFF@laurentidecontrols.com")
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.cmdConcierge.Text = "Commentaire sur la batîsse\nBuilding Concern"
        'Me.cmdIT.Text = "Requête IT\nIT Request"
        'Me.cmdOMCI.Text = "Entrée de commandes\nOrder Management Continous Improvement"
        'Me.cmdSugg.Text = "Commentaire\nSuggestion pour le comité feedback"
    End Sub

    Private Sub cmdEnglish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnglish.Click
        Session("LG") = "EN"
        Response.Redirect("Default_EN.aspx")
    End Sub

    Private Sub cmdFrench_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFrench.Click
        Session("LG") = "FR"
        Response.Redirect("Default_FR.aspx")
    End Sub
End Class
