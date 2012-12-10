Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.UI.WebControls
Imports System.DBNull
Imports System.Web.Mail

Public Class FormSaved
    Inherits System.Web.UI.Page

    '|----------------------------------------------------------------------------------------------------------|
    '|  Happens when page Suggestions is loaded                                                                 |
    '|----------------------------------------------------------------------------------------------------------|
    Sub LoadPage()
        'Put user code to initialize the page here
        dim strComm as string

        If Session("NCF") <> Nothing Then
            strComm = "<script language=Javascript>"
            strComm += "window.open("
            strComm += "'Report_" & Session("LG") & ".aspx?state=0'"
            strComm += ", 'new'"
            strComm += ", 'height=600,width=800,scrollbars=yes,toolbar=no'"
            strComm += ");" 
            strComm += "</script>"  
            
            Response.Write(strComm) 
        Else
            Response.Redirect("COMMUN/denied.html")
        End If
    End Sub
End Class
