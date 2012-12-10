<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<%@ Page Language="vb" CodeBehind="ReviewSearch.aspx.vb" Inherits="FRANCS.ReviewSearch" %>
<!doctype html Private "-//w3c//dtd html 4.0 transitional//en">
<HTML>
	<HEAD>
		<title>Non Confirming Products & Services / Opportunity for Improvement</title>
		<script runat="server">
			sub Page_Load
				Session("LG") = "EN"
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
			Review Search
			<br>
			Edit / View 
			<!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
			<br>
			<table class="liste" width="97%">
				<tr>
					<td class="TitreFormulaire" height="20">NCF #</td>
					<td class="TitreFormulaire" height="20">Customer</td>
					<td class="TitreFormulaire" height="20">Issue Date</td>
					<td colspan="2" height="20"></td>
					<td class="TitreFormulaire" height="20">Status</td>
					<TD class="TitreFormulaire" height="20">Assigned to</TD>
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
					<td>To:</td>
					<td>
						<asp:textbox id="EndDate" columns="10" runat="server" />
						<a href="#" id="anchor2" onclick="cal1xx.select(document.forms[0].EndDate,'anchor2','MM/dd/yyyy'); return false;">
							<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21"> </a>
					</td>
					<td>
						<asp:dropdownlist runat="server" id="Status">
							<asp:listitem value="0" text="All" selected="true" />
							<asp:listitem value="1" text="Incomplete" />
							<asp:listitem value="2" text="Open - Unassigned" />
							<asp:listitem value="3" text="Open - Assigned" />
							<asp:listitem value="4" text="Closed" />
						</asp:dropdownlist>
					</td>
					<TD><cc:combobox id="AssignedTo" runat="server" width="130" cssclass="taille10"></cc:combobox>
					</TD>
				</tr>
			</table>
			<table class="liste" width="97%">
				<tr valign="bottom">
					<td class="TitreFormulaire">Employee</td>
					<td class="Taille10">
						<span class="TitreFormulaire">Keywords</span><br>
						&nbsp;&nbsp;&nbsp;Description
					</td>
					<td class="Taille10">Immediate Action:</td>
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
						<asp:button text="Search" width="150px" onclick="searchButton" runat="server" id="searchB" />&nbsp;&nbsp;
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
			<asp:button text="Edit" onclick="RedirectEdit" enabled="False" runat="server" id="Edit" />
			<asp:button text="View" onclick="RedirectView" enabled="False" runat="server" id="View" />
			<asp:button text="Delete" onclick="DeleteLine" enabled="False" runat="server" id="Delete" />
			<asp:button id="Followup" onclick="SetFollowUpFlag" runat="server" text="Follow Up" enabled="True"></asp:button>
			<asp:button text="Send Email" onclick="SendIncompleteEmail" enabled="False" runat="server" id="SendIncomplete" />
			<asp:dropdownlist runat="server" id="StatusChange" Enabled="False" autopostback="True" OnSelectedIndexChanged="ChangeStatus">
				<asp:listitem value="2" text="Open-Unassigned" />
				<asp:listitem value="3" text="Open-Assigned" />
				<asp:listitem value="5" text="Open-WIP" />
			</asp:dropdownlist>
			<asp:button text="Print Report" onclick="PrintReport" enabled="False" runat="server" id="Print" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT onclick="javascript:location.replace('Default_EN.aspx');" type="button" value="Back to Main Menu">
			<!-- #include file ="COMMUN/CanevasBas.htm" -->
			<!-- Division for calendar -->
			<div id="Calendrier" style="VISIBILITY:hidden;POSITION:absolute"></div>
		</form>
	</body>
</HTML>
