<%@ Page Language="vb" Codebehind="GeneralInfo.aspx.vb" Inherits="FRANCS.GeneralInfo" %>
<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Informations générales </title>
		<script runat="server">
		sub Page_Load
			Session("LG") = "FR"
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
					<!-- #include file ="Menu_FR.htm" -->
					<td width="*">
						<table border="0" class="pasBordure" width="100%" cellspacing="0">
							<tr>
								<td align="center" colspan="8" class="formulaireTitre taille14 gras souligne">
									Informations Générales
								</td>
							</tr>
							<tr>
								<td class="formulaireLigne1" colspan="8"></td>
							</tr>
							<!-- Form Type -->
							<tr>
								<td class="TitreFormulaire" colspan="2"><br>
									&nbsp;&nbsp; 1 - * Quel type de formulaire désirez-vous remplir?
								</td>
							</tr>
							<tr>
								<td class="indentation">
									<asp:radiobutton class="taille10" text="Non Conformité" id="FormType1" groupname="FormType" runat="server" />
									<a href="#" onclick="javascript:Definitions(1);">(?)</a>
								</td>
								<td>
									<asp:radiobutton class="taille10" text="Opportunité d'amélioration" id="FormType2" groupname="FormType"
										runat="server" />
									<a href="#" onclick="javascript:Definitions(2);">(?)</a>
								</td>
								<td width="400"></td>
							</tr>
							<!-- Feedback Type -->
							<tr>
								<td class="TitreFormulaire" colspan="2"><br>
									&nbsp;&nbsp; 2 - * D'où provient cette information?
								</td>
							</tr>
							<tr>
								<td class="indentation">
									<asp:radiobutton class="taille10" text="Interne" id="FeedbackType1" groupname="FeedbackType" runat="server" />
								</td>
								<td>
									<asp:radiobutton class="taille10" text="Externe" id="FeedbackType2" groupname="FeedbackType" runat="server" />
								</td>
							</tr>
							<!-- Customer Information -->
							<tr>
								<td class="TitreFormulaire" colspan="2"><br>
									&nbsp;&nbsp; 3 - Informations sur le Client
								</td>
							</tr>
							<tr>
								<td class="indentation taille10" colspan="3">
									* Client
									<%If Session("Edit") = true Then %>
									<a href="#" onclick="javascript:CustomerSearch('FR');">(Recherche Avancée)</a>
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
									<a href="#" onclick="javascript:Contact('new', 'FR');">(nouveau)</a> <a href="#" onclick="javascript:Contact('edit', 'FR');">
										(modifier)</a>
									<% End If %>
								</td>
								<td class="taille10"># tel du contact</td>
							</tr>
							<tr>
								<td>
									<cc:combobox id="ContactName" runat="server" width="150" cssclass="marge taille10" autopostback="true"></cc:combobox>
								</td>
								<td><asp:textbox id="ContactTel" enabled="False" runat="server" class="taille10" /></td>
							</tr>
							<tr>
								<td class="indentation taille10">* Constaté par</td>
								<td class="taille10">* Date du constat</td>
								<td class="taille10">Téléphone</td>
							</tr>
							<tr>
								<td>
									<asp:textbox class="marge taille10" enabled="False" id="IssuedBy" runat="server" />
								</td>
								<td>
									<asp:textbox id="IssueDate" runat="server" width="120px" class="taille10" />
									<a href="#" id="calIssue" onclick="cal1xx.select(document.forms[0].IssueDate,'calIssue','MM/dd/yyyy'); return false;">
										<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21"> </a>
								</td>
								<td><asp:textbox id="Tel" enabled="False" runat="server" class="taille10" /></td>
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
									&nbsp;&nbsp; 4 - * Quel est le type de plainte?
								</td>
								<td class="TitreFormulaire" colspan="2"><br>
									La catégorie?
									<span class="taille9">(Sélectionnez un type d'abord)</span>
								</td>
							</tr>
							<tr>
								<td>
									<cc:combobox id="CategoryTypes" runat="server" width="150" cssclass="marge taille10" autopostback="true"></cc:combobox>
								</td>
								<td colspan="2">
									<cc:combobox id="Categories" runat="server" width="400" cssclass="taille10"></cc:combobox>
								</td>
							</tr>
							<!-- Affect Product -->
							<tr valign="bottom">
								<td colspan="3"><table>
										<tr valign="bottom">
											<td class="TitreFormulaire" colspan="2"><br>
												&nbsp;&nbsp; 5 - * Ce constat affectera-t-il la production?
											</td>
											<td>
												<asp:radiobutton class="taille10" text="Oui" id="AffectProd1" groupname="AffectProd" runat="server" />
												<asp:radiobutton class="taille10" text="Non" id="AffectProd2" groupname="AffectProd" runat="server" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<!-- Shared -->
							<tr valign="bottom">
								<td colspan="3"><table>
										<tr valign="bottom">
											<td class="TitreFormulaire" colspan="2"><br>
												&nbsp;&nbsp; 6 - * Cette rétroaction peut-elle être partagée avec les autres 
												membres afin de résoudre le problème?
											</td>
											<td>
												<asp:radiobutton class="taille10" text="Oui" id="Shared1" groupname="Shared" runat="server" />
												<asp:radiobutton class="taille10" text="Non" id="Shared2" groupname="Shared" runat="server" />
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
						<input type="button" value="Retour au Menu Principal" onclick="javascript:mainPage('FR');">
						<% Else %>
						<input type="button" value="Retour au Menu Principal" onclick="javascript:location.replace('Default_FR.aspx');">
						<% End If %>
					</td>
					<td align="right">
						<asp:button text="Suivant >" onclick="NextPage" runat="server" id="Button1" />&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
