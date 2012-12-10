<%@ Page Language="vb" codeBehind="Review.aspx.vb" Inherits="FRANCS.Review" %>
<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Rétroactions</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<script runat="server">
		sub Page_Load
			Session("LG") = "FR"
			LoadPage()
		end sub
		</script>
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
			<script language="javascript" src="COMMUN/NCR.js"></script>
			<script language="JavaScript" src="CALENDAR/CalendarPopup.js"></script>
			<link rel="stylesheet" type="text/css" href="CALENDAR/Calendar.css">
		<!-- Affiche le bon onglet du haut en subrillance -->
	</HEAD>
	<body class="" bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
		<!-- <% response.write("id=""AD_" & Division.value & """>") %> -->
		<form id="Review" method="post" runat="server">
			<!-- Get Current Div -->
			<input type="hidden" id="Division" value="GI" name="Division" runat="server"> 
			<!-- #include file ="COMMUN/CanevasHaut.htm" --> 
			Suivi des Rétroactions 
			<!-- #include file ="COMMUN/AD_CanevasApresTitre.htm" -->
			<table width="100%" cellpadding="0" cellspacing="0" class="pasbordure" height="100%">
				<tr>
					<td colspan="2"><!-- #include file ="Ad_Menu_FR.htm" --></td>
				</tr>
				<tr height="100%" valign="top">
					<td td width="2"></td>
					<td>
						<!-- #include file ="Ad_GeneralInfo_FR.aspx" -->
						<!-- #include file ="Ad_FurtherClarifications_FR.aspx" -->
						<!-- #include file ="Ad_FurtherActions_FR.aspx" -->
						<!-- #include file ="Ad_RootCause_FR.aspx" -->
						<!-- #include file ="Ad_CorrectiveAction_FR.aspx" -->
						<!-- #include file ="Ad_CloseOut_FR.aspx" -->
					</td>
				</tr>
				<tr>
					<td></td>
					<td colspan="2" align="right">
						<asp:CheckBox id="chkSendMail" text="Send Email?" checked=True runat="server"></asp:CheckBox>
						<asp:button width="100px" text="Enregistrer" onClick="Save" id="btnSave" runat="server" />
						<%if Session("Edit") = true Then %>
						<input type="button" value="Retour au Menu Principal" onclick="javascript:mainPage('FR');">
						<%if Session("Assignments") <> Nothing Then %>
						<asp:Button type="button" Text="Prochaine Assignation" onclick="NextAssigned" runat="server"
							id="Button1" />
						<% Else %>
						<input type="button" value="Retour à la liste" onclick="javascript:ReviewSearch('FR');">
						<% End If %>
						<% Else %>
						<input type="button" value="Retour au Menu Principal" onclick="javascript:location.replace('Default_FR.aspx');">
						<input type="button" value="Retour à la liste" onclick="javascript:location.replace('ReviewSearch_FR.aspx');">
						<% End If %>
						&nbsp;&nbsp;&nbsp;&nbsp;<br>
						<br>
					</td>
				</tr>
				<tr>
					<td colspan="2" class="formulaireLigne1"></td>
				</tr>
			</table>
			<div id="Calendrier" style="VISIBILITY:hidden;POSITION:absolute"></div>
			<!-- #include file ="COMMUN/AD_CanevasBas.htm" -->
			<!-- Afficher la bonne section !-->
			<%response.write("<script>ShowDiv(""" & Division.value & """);</script>")%>
		</form>
	</body>
</HTML>
