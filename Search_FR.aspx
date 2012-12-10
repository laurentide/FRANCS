<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<%@ Page Language="vb" Codebehind="Search.aspx.vb" Inherits="FRANCS.Search" %>
<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Non Confirmité des Produits & Services / Opportunité de Perfectionnement</title>
		
		<script runat="server">
			sub Page_Load
				Session("LG") = "FR"
				LoadPage()
			end sub
		</script>
    
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css" >
		<script language="JavaScript" src="COMMUN/NCR.js"></script>
		<script language="JavaScript" src="CALENDAR/CalendarPopup.js"></script>
		<link rel="stylesheet" type="text/css" href="CALENDAR/Calendar.css">
  </head>
	<body>
		<!-- #include file ="COMMUN/CanevasHaut.htm" -->
		<form id="NCS" runat="server" method="post">
				Recherche NCF <br >
				Modifier / Supprimer / Consulter
			<!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
		
			<br >
			<table class="liste" width="97%" >
				<tr>
					<td class="TitreFormulaire"># NCF</td>
					<td class="TitreFormulaire">Client</td>
					<td class="TitreFormulaire">Date du constat</td>
					<td colspan="2"></td>
					<td class="TitreFormulaire">Statut</td>
				</tr>
				<tr>
					<td><asp:textbox id="NcfNo" maxlength="5" size="5" runat="server" /> </td>
					<td><cc:combobox id="Customer" runat="server" width="330" cssclass="taille10"></cc:combobox></td>
					<td>
						<asp:textbox id="StartDate" columns="10" runat="server" />
						<a href="#" id="anchor1" onclick="cal1xx.select(document.forms[0].StartDate,'anchor1','MM/dd/yyyy'); return false;">
							<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21" >
						</a>
					</td>
					<td>À:</td>
					<td> 
						<asp:textbox id="EndDate" columns="10" runat="server" />
						<a href="#" id="anchor2" onclick="cal1xx.select(document.forms[0].EndDate,'anchor2','MM/dd/yyyy'); return false;">
							<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21" >
						</a>
					</td>
					<td>
						<asp:dropdownlist runat="server" id="Status" />
					</td>
				</tr>
			</table>
			<table class="liste">
				<tr valign="bottom">
					<td class="Taille10">
						<span class="TitreFormulaire">Mots clé:</span><br >
						Description:
					</td>
					<td class="Taille10">Action Immédiate:</td>
					<td class="Taille10">Cause:</td>
					<td class="Taille10">Suggestions:</td>
				</tr>
				<tr>
					<td class="spaces"><asp:textbox id="Description" runat="server" /></td>
					<td class="spaces"><asp:textbox id="ImmediateAction" runat="server" /></td>
					<td class="spaces"><asp:textbox id="Cause" runat="server" /></td>
					<td class="spaces"><asp:textbox id="Suggestions" runat="server" /></td>
				</tr>
				<tr>
					<td></td><td></td><td></td><td></td><td></td>
					<td align="right">
						<asp:button text="Rechercher" width="150px" onclick="searchButton" id="SearchCRF" runat="server" />
					</td>
				</tr>
			</table>
			<hr >
			
			<div style="OVERFLOW: auto; WIDTH: 95%; HEIGHT: 55%">
				<!-- TABLE!!!!! -->
				<asp:table id="SearchResult" class="searchMarge" runat="server" />
			</div>
			<br >&nbsp;&nbsp;&nbsp;
			<asp:button text="Supprimer" id="Delete" onclick="DeleteNCF" runat="server" enabled="False" />
			<asp:button text="Modifier" id="Edit" onclick="RedirectEdit" enabled="False" runat="server" />
			<asp:button text="Consulter" id="View" onclick="RedirectView" runat="server" enabled="False" />	
			<asp:button text="Consulter le message de fermeture" onclick="OpenCloseOutMsg" id="ViewCloseOut" runat="server" enabled="False" />	
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<input type="button" value="Retour au Menu Principal" onclick="javascript:location.replace('Default_FR.aspx');" >
			
		</form>
		<!-- Division for calendar -->
		<div id="Calendrier" style="VISIBILITY:hidden;POSITION:absolute"></div>
		<!-- #include file ="COMMUN/CanevasBas.htm" -->
	</body>
</html>
