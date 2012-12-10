<%@ Page Language="vb" Codebehind="Report.aspx.vb" Inherits="FRANCS.Report" %>
<HTML>
	<HEAD>
		<title>Reports</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
			<script language="Javascript" src="COMMUN/NCR.js"></script>
			<script runat="server">
			sub Page_Load
				Session("LG") = "EN"
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
	<body onBeforeUnload="backToMainMenu('EN');" class="noir">
		<form runat="server" class="centre">
			<br>
			<table class="largeur100 contourRapport">
				<tr>
					<td width="200">&nbsp;&nbsp;&nbsp;<img src="Images/LAURENTIDE_logo.gif" width="195" height="50"></td>
					<td class="centre gras taille16" width="*">
						FeedBack Report
					</td>
					<td align="right" width="200"><img src="Images/CT2006_BLK_EN.jpg" width="80" height="74">&nbsp;&nbsp;&nbsp;</td>
				</tr>
				<tr>
				</tr>
			</table>
			<table class="largeur100 bordureRapport" padding="0" spacing="0">
				<tr>
					<td class="gras taille10 celluleRapport" width="25%">
						<asp:Checkbox id="Internal" runat="server" enabled="False" />
						&nbsp; Internal
					</td>
					<td class="gras taille10 celluleRapport" width="25%">
						<asp:Checkbox id="External" runat="server" enabled="False" />
						&nbsp; External
					</td>
					<td class="gras taille10 celluleRapport" width="25%">
						ID#: &nbsp;&nbsp;
						<asp:label id="NCFNo" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport" width="25%">
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport">
						<asp:Checkbox id="NonConformance" runat="server" enabled="False" />
						&nbsp; Non Conformance
					</td>
					<td class="gras taille10 celluleRapport" colspan="2">
						<asp:Checkbox id="Opportunity" runat="server" enabled="False" />
						&nbsp; Opportunity for Improvement
					</td>
					<td class="gras taille10 celluleRapport"></td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" celluleRapport colspan="4">
						Customer Name:&nbsp;
						<asp:Label id="Customer" runat="server" />
					</td>
				</tr>
				<tr width="30px">
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="centre italic gras taille12 background-gris" colspan="4">
						Administration Details
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport background-gris">
						Issued By:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="IssuedBy" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport background-gris">
						Date of Issue:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="IssueDate" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport background-gris">
						Telephone:
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
						Contact/Telephone:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="ContactTel" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport background-gris">
						Order Number:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="OrderNo" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport" colspan="2"></td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport background-gris">
						Category Type:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="CategoryType" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport background-gris">
						Category:
					</td>
					<td class="taille10 celluleRapport">
						<asp:Label id="Category" runat="server" />
					</td>
				</tr>
				<TR>
					<TD class="gras taille10 celluleRapport background-gris" height="18">Vendor:</TD>
					<TD class="taille10 celluleRapport" height="18">
						<asp:Label id="Vendor_Name" runat="server"></asp:Label></TD>
					<TD class="gras taille10 celluleRapport background-gris" height="18">PO Number</TD>
					<TD class="taille10 celluleRapport" height="18">
						<asp:Label id="PONo" runat="server"></asp:Label></TD>
				</TR>
				<tr width="30px">
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="centre italic gras taille12 background-gris" colspan="4">
						Feedback Information
					</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="2">
						Will this non conformance affect quality?
						<asp:Label id="Production" runat="server" />
					</td>
					<td class="gras taille10 celluleRapport" colspan="2">
						Can this feedback be shared?
						<asp:Label id="Shared" runat="server" />
					</td>
				</tr>
				<tr width="30px">
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="gras taille10 celluleRapport" colspan="4">
						Description of Deficiency / Deviation
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
						Disposition / Immediate Action
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
						Suspected Cause of Deficiency / Deviation
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
