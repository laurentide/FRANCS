<%@ Page Language="vb" Codebehind="Suggestions.aspx.vb" Inherits="FRANCS.SectionD" %>
<!doctype html Private "-//w3c//dtd html 4.0 transitional//en">
<HTML>
	<HEAD>
		<title>Suggestions</title>
		<script runat="server">
		sub Page_Load
			Session("LG") = "FR"
			LoadPage()
		end sub
		</script>
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
			<script language="javascript" src="COMMUN/NCR.js"></script>
	</HEAD>
	<body id="SD">
		<form runat="server" id="Form1">
			<table border="1" height="88%" width="100%" class="fondBleuPale">
				<tr valign="top">
					<!-- #include file ="Menu_FR.htm" -->
					<td width="*">
						<table border="0" class="pasBordure" width="100%" cellspacing="0">
							<tr>
								<td align="center" colspan="8" class="formulaireTitre taille14 gras souligne">
									Suggestions
								</td>
							</tr>
							<tr>
								<td class="formulaireLigne1" colspan="8"></td>
							</tr>
							<!-- Description of Deficiency -->
							<tr>
								<td class="TitreFormulaire" colspan="2"><br>
									&nbsp;&nbsp; * Action Suggérée par l'employé :
								</td>
							</tr>
							<tr>
								<td class="indentation"><br>
									&nbsp;
									<asp:textbox rows="20" id="Suggestions" columns="100" textmode="MultiLine" class="taille10" runat="server" />
								</td>
								<td width="400"></td>
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
						<asp:button text="< Précédent" onclick="PreviousPage" runat="server" id="Previous" />
						<%if Session("Edit") = true Then %>
						<asp:button text="Terminer" onclick="Finish" runat="server" id="Button2" />&nbsp;&nbsp;&nbsp;
						<% Else %>
						<asp:button text="Imprimer le Rapport" onclick="PrintReport" runat="server" id="Print" />&nbsp;&nbsp;&nbsp;
						<% End If %>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
