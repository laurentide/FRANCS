<%@ Page Language="vb" Codebehind="GeneralInfo.aspx.vb" Inherits="FRANCS.GeneralInfo" %>
<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>General information</title>
		<script runat="server">
		sub Page_Load
			Session("LG") = "EN"
			LoadPage()
		end sub
		</script>
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
			<link rel="stylesheet" type="text/css" href="CALENDAR/Calendar.css">
				<script language="Javascript" src="COMMUN/NCR.js"></script>
				<script language="JavaScript" src="CALENDAR/CalendarPopup.js"></script>
	</HEAD>
	<body id="GI">
		<!-- Division for calendar -->
		<div id="Calendrier" style="VISIBILITY:hidden;POSITION:absolute;BACKGROUND-COLOR:white;layer-background-color:white"></div>
		<form runat="server">
			<table border="1" height="88%" width="100%" class="fondBleuPale">
				<tr valign="top">
					<!-- #include file ="Menu_EN.htm" -->
					<td width="*">
						<table border="0" class="pasBordure" width="100%" cellspacing="0">
							<tr>
								<td align="center" colspan="8" class="formulaireTitre taille14 gras souligne">
									General Information
								</td>
							</tr>
							<tr>
								<td class="formulaireLigne1" colspan="8"></td>
							</tr>
							<!-- Form Type -->
							<tr>
								<td class="TitreFormulaire" colspan="2"><br>
									&nbsp;&nbsp; 1 - * What kind of form do you wish to fill?
								</td>
							</tr>
							<tr>
								<td class="indentation">
									<asp:radiobutton class="taille10" text="Non Conformance" id="FormType1" groupname="FormType" runat="server" />
									<a href="#" onclick="javascript:Definitions(1);">(?)</a>
								</td>
								<td>
									<asp:radiobutton class="taille10" text="Opportunity for Improvement" id="FormType2" groupname="FormType"
										runat="server" />
									<a href="#" onclick="javascript:Definitions(2);">(?)</a>
								</td>
								<td width="400"></td>
							</tr>
							<!-- Feedback Type -->
							<tr>
								<td class="TitreFormulaire" colspan="2"><br>
									&nbsp;&nbsp; 2 - * Where is the feedback from?
								</td>
							</tr>
							<tr>
								<td class="indentation">
									<asp:radiobutton class="taille10" text="Internal" id="FeedbackType1" groupname="FeedbackType" runat="server" />
								</td>
								<td>
									<asp:radiobutton class="taille10" text="External" id="FeedbackType2" groupname="FeedbackType" runat="server" />
								</td>
							</tr>
							<!-- Customer Information -->
							<tr>
								<td class="TitreFormulaire" colspan="2"><br>
									&nbsp;&nbsp; 3 - Customer Information
								</td>
							</tr>
							<tr>
								<td class="indentation taille10" colspan="3">
									* Customer
									<%If Session("Edit") = true Then %>
									<a href="#" onclick="javascript:CustomerSearch('EN');">(Advanced Search)</a>
									<% End If %>
								</td>
							</tr>
							<tr>
								<td colspan="3">
									<cc:combobox id="Customer" runat="server" width="500" cssclass="marge taille10" autopostback="true"></cc:combobox>
								</td>
							</tr>
							<tr>
								<td class="taille10 indentation">
									* Contact &nbsp;
									<%If Session("Edit") = true Then %>
									<a href="#" onclick="javascript:Contact('new', 'EN');">(new)</a> <a href="#" onclick="javascript:Contact('edit', 'EN');">
										(edit)</a>
									<% End If %>
								</td>
								<td class="taille10">Contact Telephone #</td>
							</tr>
							<tr>
								<td>
									<cc:combobox id="ContactName" runat="server" width="150" cssclass="marge taille10" autopostback="true"></cc:combobox>
								</td>
								<td><asp:textbox id="ContactTel" enabled="False" runat="server" class="taille10" /></td>
							</tr>
							<tr>
								<td class="indentation taille10">* Issued by</td>
								<td class="taille10">* Date of issue</td>
								<td class="taille10">Telephone</td>
							</tr>
							<tr>
								<td height="28">
									<asp:textbox class="marge taille10" enabled="False" id="IssuedBy" runat="server" />
								</td>
								<td height="28">
									<asp:textbox id="IssueDate" runat="server" width="120px" class="taille10" />
									<a href="#" id="calIssue" onclick="cal1xx.select(document.forms[0].IssueDate,'calIssue','MM/dd/yyyy'); return false;">
										<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21"> </a>
								</td>
								<td height="28"><asp:textbox id="Tel" enabled="False" runat="server" class="taille10" /></td>
							</tr>
							<!-- Vendor -->
							<!-- Ajout DDN : 2007/01/28 -->
							<tr>
								<td class="indentation taille10">Order #</td>
								<td class="taille10">Vendor</td>
								<td class="taille10">PO #</td>
							</tr>
							<tr>
								<td height="25">
									<asp:textbox class="marge taille10" maxlength="10" id="OrderNo" runat="server" />
								</td>
								<td height="25">
									<cc:combobox id="Vendor" runat="server" class="taille10" autopostback="false"></cc:combobox>
								</td>
								<td height="25">
									<asp:textbox class="taille10" id="PONo" runat="server" MaxLength="10" />
								</td>
							</tr>
							<!-- Category -->
							<tr>
								<td class="TitreFormulaire"><br>
									&nbsp;&nbsp; 4 - * &nbsp;&nbsp;Type?
								</td>
								<td class="TitreFormulaire" colspan="2"><br>
									Category?
									<span class="taille9">(Choose a type first)</span>
								</td>
							</tr>
							<tr>
								<td>
									<cc:combobox id="CategoryTypes" runat="server" width="150" cssclass="marge taille10" autopostback="true"></cc:combobox>
								</td>
								<td colspan="2">
									<cc:combobox id="Categories" runat="server" width="230" cssclass="taille10"></cc:combobox>
								</td>
							</tr>
							<!-- Affect Product -->
							<tr valign="bottom">
								<td class="TitreFormulaire" colspan="3">
									<table>
										<tr valign="bottom">
											<td class="TitreFormulaire" valign="buttom"><br>
												&nbsp;&nbsp; 5 - * Will this non comformance affect Quality / Customer Service?
											</td>
											<td>
												<asp:radiobutton class="taille10" text="Yes" id="AffectProd1" groupname="AffectProd" runat="server" />
												<asp:radiobutton class="taille10" text="No" id="AffectProd2" groupname="AffectProd" runat="server" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<!-- Shared -->
							<tr valign="bottom">
								<td class="TitreFormulaire" colspan="3">
									<table>
										<tr valign="bottom">
											<td class="TitreFormulaire" valign="buttom"><br>
												&nbsp;&nbsp; 6 - * Can this feedback be shared with other members of the 
												company in order to obtain resolution?
											</td>
											<td>
												<asp:radiobutton class="taille10" text="Yes" id="Shared1" groupname="Shared" runat="server" />
												<asp:radiobutton class="taille10" text="No" id="Shared2" groupname="Shared" runat="server" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<table width="100%">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;
						<%if Session("Edit") = true Then %>
						<input type="button" value="Back to Main Menu" onclick="javascript:mainPage('EN');">
						<% Else %>
						<input type="button" value="Back to Main Menu" onclick="javascript:location.replace('Default_EN.aspx');">
						<% End If %>
					</td>
					<td align="right">
						<asp:button text="Next >" onclick="NextPage" runat="server" id="Button1" />&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
