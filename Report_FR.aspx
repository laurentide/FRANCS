<%@ Page Language="vb" Codebehind="Report.aspx.vb" Inherits="FRANCS.Report" %>
<HTML>
	<HEAD>
		<title>Rapports</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
			<script language="Javascript" src="COMMUN/NCR.js"></script>
			<script runat="server">
			sub Page_Load
				Session("LG") = "FR"
				LoadPage()
			end sub
			</script>
			<script>
		//places current window in the center
		var xpos = (screen.width - 800) / 2
		var ypos = (screen.height - 600) / 2
		moveTo(xpos, ypos);
		
		self.print()
			</script>
	</HEAD>
	<body onBeforeUnload="backToMainMenu('FR');" class="noir">
		<form runat="server">
			<br>
			<table class="largeur100 contourRapport">
				<tr>
					<td width="260">&nbsp;&nbsp;&nbsp;<img src="Images/LAURENTIDE_logo.gif" width="195" height="50"></td>
					<td class="centre gras taille16" width="*">
						Rapport des R�troactions
					</td>
					<td align="right" width="200"><img src="Images/CT2006_BLK_FR.jpg" width="74" height="71">&nbsp;&nbsp;&nbsp;</td>
				</tr>
			</table>
			<table class="largeur100 bordureRapport" padding="0" spacing="0">
				<tr>
					<td class="gras taille10 celluleRapport" width="25%">
						<asp:Checkbox id="Internal" runat="server" enabled="False" />
						&nbsp; Interne
					</td>
					<td class="gras taille10 celluleRapport" width="25%">
						<asp:Checkbox id="External" runat="server" enabled="False" />
						&nbsp; Externe
					</td>
					<td class="gras taille10 celluleRapport" width="25%">
						#ID: &nbsp;&nbsp;
						<asp:label id="NCFNo" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport" width="25%"></td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport">
						<asp:Checkbox id="NonConformance" runat="server" enabled="False" />
						&nbsp; Non Conformit�
					</td>
					<td class="gras taille10 celluleRapport" colspan="2">
						<asp:Checkbox id="Opportunity" runat="server" enabled="False" />
						&nbsp; Opportunit� d'am�lioration
					</td>
					<td class="gras taille10 celluleRapport"></td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" celluleRapport colspan="4">
						Nom du Client:&nbsp;
						<asp:Label id="Customer" runat="server" />
					</td>
				</tr>
				<tr width="30px">
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="centre italic gras taille12 background-gris" colspan="4">
						D�tails Administratifs
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport background-gris">
						Constat� par:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="IssuedBy" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport background-gris">
						Date du constat:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="IssueDate" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport background-gris">
						T�l�phone:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="EMPTel" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport" colspan="2"></td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport background-gris">
						Contact:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="Contact" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport background-gris">
						Contact/T�l�phone:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="ContactTel" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport background-gris" height="17">
						Num�ro de la commande:
					</td>
					<td class="taille10 celluleRapport" height="17">
						<asp:Label id="OrderNo" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport" colspan="2" height="17"></td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport background-gris" height="11">
						Type de Categorie :
					</td>
					<td class="taille10 celluleRapport" height="11">
						<asp:Label id="CategoryType" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport background-gris" height="11">
						Cat�gorie:
					</td>
					<td class="taille10 celluleRapport" height="11">
						<asp:Label id="Category" runat="server" />
					</td>
				</tr>
				<TR>
					<TD class="gras taille10 celluleRapport background-gris" height="18">Fournisseur:</TD>
					<TD class="taille10 celluleRapport" height="18">
						<asp:Label id="Vendor_Name" runat="server"></asp:Label></TD>
					<TD class="gras taille10 celluleRapport background-gris" height="18">Num�ro de PO:</TD>
					<TD class="taille10 celluleRapport" height="18">
						<asp:Label id="PONo" runat="server"></asp:Label></TD>
				</TR>
				<tr width="30px">
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="centre italic gras taille12 background-gris" colspan="4">
						Informations sur la r�troaction
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="2">
						Ce constat affectera-t-il la production?
						<asp:Label id="Production" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport" colspan="2">
						Cette r�troaction peut-elle �tre partag�e?
						<asp:Label id="Shared" runat="server" />
					</td>
				</tr>
				<tr width="30px">
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="4">
						Description du probl�me
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="4">
						<asp:textbox rows="8" id="Deficiency" width="100%" textmode="MultiLine" class="taille10" runat="server"
							readonly="true" Height="100%" />
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="4">
						Action Imm�diate
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="4">
						<asp:textbox rows="8" id="ActionT" width="100%" textmode="MultiLine" class="taille10" runat="server"
							readonly="true" Height="100%" />
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="4">
						Cause soup�onn�e du probl�me
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="4">
						<asp:textbox rows="8" id="Cause" width="100%" textmode="MultiLine" class="taille10" runat="server"
							readonly="true" Height="100%" />
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="4">
						Suggestions
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="4">
						<asp:textbox rows="8" id="Suggestions" width="100%" textmode="MultiLine" class="taille10" runat="server"
							readonly="true" Height="100%" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
