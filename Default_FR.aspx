<%@ Page Language="vb" Codebehind="Default.aspx.vb" Inherits="FRANCS._Default" %>
<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Non Confirmité des Produits & Services / Opportunité de Perfectionnement</title>
		<script runat="server">
			sub Page_Load
				Session("NCF") = Nothing
				Session("LG") = "FR"
			end sub
		</script>
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
	</HEAD>
	<body>
		<form runat="server">
			<!-- #include file ="COMMUN/CanevasHaut.htm" --> Non Conformité / Action 
			Préventive 
			<!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
			<br>
			<br>
			<ul type="square">
				<table align="center">
					<tr>
						<td>
							<br>
							<br>
							&nbsp;&nbsp;
							<li class="TitreFormulaire taille13">
								Documentation de Non Conformité
								<ul>
									<li>
										<a href="GeneralInfo_FR.aspx" class="texteBleu taille10"><FONT color="#010066">Nouvelle</FONT></a><FONT color="#010066">
										</FONT>
									<li>
										<a href="Search_FR.aspx?T=EDIT" class="texteBleu taille10"><FONT color="#010066">Modifier 
												/ Supprimer / Consulter</FONT></a></li>
								</ul>
							</li>
						</td>
					</tr>
					<% If User.IsInRole("LCLMTL\LCL_Feedback_Committee") = true Or _
						 User.IsInRole("LCLMTL\LCL_IT_Projects") = true or 1=1 Then %>
					<tr>
						<td>&nbsp;&nbsp;
							<LI class="TitreFormulaire taille13">
								Outils Administratifs
								<UL>
									<li>
										<asp:linkButton Text="Assignations" class="texteBleu taille10" onClick="Assignments" runat="server"
											id="LinkButton1" />
									<li>
										<a href="ReviewSearch_FR.aspx" class="texteBleu taille10">Suivi des Rétroactions</a>
									<LI>
										<FONT class="texteBleu" color="#000166" size="2"><U><A class="texteBleu taille10" href="http://lcl-faxs/Reports1/Pages/Folder.aspx?ItemPath=%2fFRANCS&amp;IsDetailsView=False">
													Rapports</A></U></FONT></LI></UL>
							</LI>
							<P class="texteBleu taille9">&nbsp;</P>
						</td>
					</tr>
					<% End if %>
				</table>
			</ul>
			<!-- #include file ="COMMUN/CanevasBas.htm" -->
		</form>
	</body>
</HTML>
