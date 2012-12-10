<%@ Page Language="vb" Codebehind="Default.aspx.vb" Inherits="FRANCS._Default" %>
<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Non Confirming Products & Services / Opportunity for Improvement</title>
		<script runat="server">
			sub Page_Load
				Session("NCF") = Nothing
				Session("LG") = "EN"
				Session("Assignments") = Nothing
			end sub
		</script>
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
	</HEAD>
	<body>
		<form runat="server">
			<!-- #include file ="COMMUN/CanevasHaut.htm" --> Non Conformance / Preventive 
			Action 
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
								Documenting Non Conformances
								<ul>
									<li>
										<a href="GeneralInfo_EN.aspx" class="texteBleu taille10">New</a>
									<li>
										<a href="Search_EN.aspx?T=EDIT" class="texteBleu taille10">Edit / Delete / View</a></li>
								</ul>
							</li>
						</td>
					</tr>
					<% If User.IsInRole("LCLMTL\LCL_Feedback_Committee") = true Or _
						 User.IsInRole("LCLMTL\LCL_IT_Projects") = true or 1=1 then %>
					<tr>
						<td>&nbsp;&nbsp;
							<LI class="TitreFormulaire taille13">
								Administrative Tools
								<UL>
									<li>
										<asp:linkButton Text="Assignments" class="texteBleu taille10" onClick="Assignments" runat="server"
											id="LinkButton1" />
									<li>
										<a href="ReviewSearch_EN.aspx" class="texteBleu taille10">Review FeedBack</a>
									<LI>
										<A class="texteBleu taille10" href="http://lcl-faxs/Reports1/Pages/Folder.aspx?ItemPath=%2fFRANCS&amp;IsDetailsView=False">
											Reports</A></LI></UL>
							</LI>
							<P class="texteBleu taille9"><STRONG></STRONG>&nbsp;</P>
						</td>
					</tr>
					<% End if %>
				</table>
			</ul>
			<!-- #include file ="COMMUN/CanevasBas.htm" -->
		</form>
	</body>
</HTML>
