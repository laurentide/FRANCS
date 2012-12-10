<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<%@ Page Language="vb" Codebehind="Search.aspx.vb" Inherits="FRANCS.Search" %>
<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Non Confirming Products & Services / Opportunity for Improvement</title>
		
		<script runat="server">
			sub Page_Load
				Session("LG") = "EN"
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
				NCF Search <br >
				Edit / Delete / View
			<!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
		
			<br >
			<table class="liste" width="97%" >
				<tr>
					<td class="TitreFormulaire">NCF #</td>
					<td class="TitreFormulaire">Customer</td>
					<td class="TitreFormulaire">Issue Date</td>
					<td colspan="2"></td>
					<td class="TitreFormulaire">Status</td>
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
					<td>To:</td>
					<td> 
						<asp:textbox id="EndDate" columns="10" runat="server" />
						<a href="#" id="anchor2" onclick="cal1xx.select(document.forms[0].EndDate,'anchor2','MM/dd/yyyy'); return false;">
							<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21" >
						</a>
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
				</tr>
			</table>
			<table class="liste">
				<tr valign="bottom">
					<td class="Taille10">
						<span class="TitreFormulaire">Keywords:</span><br >
						Description:
					</td>
					<td class="Taille10">Immediate Action:</td>
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
						<asp:button text="Search" width="150px" onclick="searchButton" id="SearchCRF" runat="server" />
					</td>
				</tr>
			</table>
			<hr >
			
			<div style="OVERFLOW: auto; WIDTH: 95%; HEIGHT: 55%">
				<!-- TABLE!!!!! -->
				<asp:table id="SearchResult" class="searchMarge" runat="server" />
			</div>
			<br >&nbsp;&nbsp;&nbsp;
			<asp:button text="Delete" id="Delete" onclick="DeleteNCF" runat="server" enabled="False" />
			<asp:button text="Edit" id="Edit" onclick="RedirectEdit" enabled="False" runat="server" />
			<asp:button text="View" id="View" onclick="RedirectView" runat="server" enabled="False" />	
			<asp:button text="View Close-Out Message" onclick="OpenCloseOutMsg" id="ViewCloseOut" runat="server" enabled="False" />	
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<input type="button" value="Back to Main Menu" onclick="javascript:location.replace('Default_EN.aspx');" >
			
		</form>
		<!-- Division for calendar -->
		<div id="Calendrier" style="VISIBILITY:hidden;POSITION:absolute"></div>
		<!-- #include file ="COMMUN/CanevasBas.htm" -->
	</body>
</html>
