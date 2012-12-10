<%@ Page Language="vb" CodeBehind="Default.aspx.vb" AutoEventWireup="false" Inherits="FRANCS._Default" %>
<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML xmlns:v xmlns:o>
	<HEAD>
		<title>Système de Rétroaction et de Non Conformité</title>
		<script runat="server">
			sub Page_Load
				Session("NCF") = Nothing
			end sub
		</script>
		<link href="COMMUN/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<div align="center">
			<form id="Link" method="post" runat="server">
				<!-- #include file ="COMMUN/CanevasHaut.htm" -->
				<!-- #include file ="COMMUN/CanevasApresTitre.htm" --><br>
				<SPAN class="TitreFormulaire taille16"></SPAN><SPAN class="TitreFormulaire taille16"><SPAN lang="EN" style="FONT-SIZE: 86pt; COLOR: #3366ff; LINE-HEIGHT: 115%; FONT-FAMILY: 'Times New Roman'; mso-ansi-language: EN; mso-fareast-font-family: 'Times New Roman'; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">Feedback</SPAN>&nbsp;
				</SPAN>
				<SPAN class="TitreFormulaire taille16">
					<I>
						<SPAN lang="EN" style="FONT-SIZE: 72pt; COLOR: #7598d9; FONT-FAMILY: 'Times New Roman'; mso-ansi-language: EN">&nbsp; focus <SPAN class="TitreFormulaire taille16">
								<br>
								<I>
									<SPAN lang="EN" style="FONT-SIZE: 72pt; COLOR: #7598d9; FONT-FAMILY: 'Times New Roman'; mso-ansi-language: EN">
										<asp:image id="Image3" runat="server" ImageAlign="Bottom" Width="315px" Height="61px" ImageUrl="http://lcl-faxs/FRANCS/images/wave.jpg"></asp:image>
									</SPAN></I></SPAN> </SPAN></I>
				</SPAN>
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="1">
					<TR>
						<TD><asp:button id="cmdIT" style="WHITE-SPACE: normal" runat="server" Width="300px" Height="100px"
								Text="Requête IT/&#13;IT Request"></asp:button></TD>
						<TD>
							<asp:button id="cmdOMCI" style="WHITE-SPACE: normal" text="Amélioration en Entrée de commande&#13;Order Entry Improvement"
								runat="server" Width="300px" Height="100px"></asp:button>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:button id="cmdConcierge" style="WHITE-SPACE: normal" runat="server" Width="300px" Height="100px"
								Text="Comm. sur la batîsse&#13;Building Concern"></asp:button>
						</TD>
						<TD><asp:button id="cmdSugg" style="WHITE-SPACE: normal" text="Suggestions pour le comité Feedback&#13;Suggestions for feedback comm."
								runat="server" Width="300px" Height="100px"></asp:button></TD>
					</TR>
					<TR>
						<TD><asp:button id="cmdFrench" style="WHITE-SPACE: normal" runat="server" Width="300px" Height="100px"
								Text="Entrer un ARC Francais"></asp:button></FONT></TD>
						<TD>
							<asp:button id="cmdEnglish" style="WHITE-SPACE: normal" runat="server" Width="300px" Height="100px"
								Text="Enter a Arc in English"></asp:button></TD>
					</TR>
				</TABLE>
				&nbsp;
				<asp:image id="Image2" runat="server" Width="177px" Height="127px" ImageUrl="http://lcl-faxs/FRANCS/images/tcc2.jpg"></asp:image><asp:image id="Image1" runat="server" Width="156px" Height="127px" ImageUrl="http://lcl-faxs/FRANCS/images/tcc.jpg"></asp:image>
				<!-- #include file ="COMMUN/CanevasBas.htm" --> </B></SPAN>
			</form>
		</div>
	</body>
</HTML>
