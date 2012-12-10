<%@ Page Language="vb" CodeBehind="ReviewSearch.aspx.vb" Inherits="FRANCS.ReviewSearch" %>
<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<!doctype html Private "-//w3c//dtd html 4.0 transitional//en">
<HTML>
	<HEAD>
		<title>Non Confirmité des Produits & Services / Opportunité de Perfectionnement</title>
		<script runat="server">
			sub Page_Load
				Session("LG") = "FR"
				LoadPage()
			end sub
		</script>
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
			<script language="JavaScript" src="COMMUN/NCR.js"></script>
			<script language="JavaScript" src="CALENDAR/CalendarPopup.js"></script>
			<link rel="stylesheet" type="text/css" href="CALENDAR/Calendar.css">
	</HEAD>
	<body>
		<form id="NCS" runat="server" method="post">
			<!-- #include file ="COMMUN/CanevasHaut.htm" -->
			Recherche des Rétroactions
			<br>
			Modifier / Consulter 
			<!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
			<br>
			<table class="liste" width="97%">
				<tr>
					<td class="TitreFormulaire"># NCF</td>
					<td class="TitreFormulaire">Client</td>
					<td class="TitreFormulaire">Date du constat</td>
					<td colspan="2"></td>
					<td class="TitreFormulaire">Statut</td>
					<td class="TitreFormulaire">Assigné à</td>
				</tr>
				<tr>
					<td><asp:textbox id="NcfNo" maxlength="5" size="5" runat="server" />
					</td>
					<td><cc:combobox id="Customer" runat="server" width="230" cssclass="taille10"></cc:combobox></td>
					<td>
						<asp:textbox id="StartDate" columns="10" runat="server" />
						<a href="#" id="anchor1" onclick="cal1xx.select(document.forms[0].StartDate,'anchor1','MM/dd/yyyy'); return false;">
							<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21"> </a>
					</td>
					<td>À:</td>
					<td>
						<asp:textbox id="EndDate" columns="10" runat="server" />
						<a href="#" id="anchor2" onclick="cal1xx.select(document.forms[0].EndDate,'anchor2','MM/dd/yyyy'); return false;">
							<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21"> </a>
					</td>
					<td>
						<asp:dropdownlist runat="server" id="Status" />
					</td>
					<TD><cc:combobox id="AssignedTo" runat="server" width="130" cssclass="taille10"></cc:combobox>
					</TD>
				</tr>
			</table>
			<table class="liste" width="97%">
				<tr valign="bottom">
					<td class="TitreFormulaire">Employé</td>
					<td class="Taille10">
						<span class="TitreFormulaire">Mots clé</span><br>
						&nbsp;&nbsp;&nbsp;Description
					</td>
					<td class="Taille10">Action Immédiate:</td>
					<td class="Taille10">Cause:</td>
					<td class="Taille10">Suggestions:</td>
				</tr>
				<tr>
					<td><cc:combobox id="Employee" runat="server" width="150" cssclass="taille10"></cc:combobox></td>
					<td class="spaces">&nbsp;&nbsp;&nbsp;<asp:textbox id="Description" runat="server" /></td>
					<td class="spaces"><asp:textbox id="ImmediateAction" runat="server" /></td>
					<td class="spaces"><asp:textbox id="Cause" runat="server" /></td>
					<td class="spaces"><asp:textbox id="Suggestions" runat="server" /></td>
				</tr>
				<tr heigth="50px" valign="bottom">
					<td></td>
					<td></td>
					<td></td>
					<td></td>
					<td align="right">
						<asp:button text="Rechercher" width="150px" onclick="searchButton" runat="server" id="searchB" />&nbsp;&nbsp;
					</td>
				</tr>
			</table>
			<hr>
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 55%">
				<!-- TABLE!!!!! -->
				<asp:table id="SearchResult" class="searchMarge" runat="server" />
			</div>
			<br>
			&nbsp;&nbsp;&nbsp;
			<asp:button text="Modifier" onclick="RedirectEdit" enabled="False" runat="server" id="Edit" />
			<asp:button text="Consulter" onclick="RedirectView" enabled="False" runat="server" id="View" />
			<asp:button text="Supprimer" onclick="DeleteLine" enabled="False" runat="server" id="Delete" />
			<asp:button id="Followup" onclick="SetFollowUpFlag" runat="server" text=" Follow Up" enabled="True"></asp:button>
			<asp:button text="Envoyer Email" onclick="SendIncompleteEmail" enabled="False" runat="server"
				id="SendIncomplete" />
			<asp:dropdownlist runat="server" id="StatusChange" Enabled="False" autopostback="True" OnSelectedIndexChanged="ChangeStatus">
				<asp:listitem value="2" text="Ouvert-Non Assigné" />
				<asp:listitem value="3" text="Ouvert-Non Assigné" />
				<asp:listitem value="5" text="Ouvert-TEC" />
			</asp:dropdownlist>
			<asp:button text="Imprimer le Rapport" onclick="PrintReport" enabled="False" runat="server"
				id="Print" />
			<input type="button" value="Retour au Menu Principal" onclick="javascript:location.replace('Default_FR.aspx');">
			<!-- #include file ="COMMUN/CanevasBas.htm" -->
			<!-- Division for calendar -->
			<div id="Calendrier" style="VISIBILITY:hidden;POSITION:absolute"></div>
		</form>
	</body>
</HTML>
