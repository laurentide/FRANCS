<%@ Page Language="vb" Codebehind="FormSaved.aspx.vb" Inherits="FRANCS.FormSaved" %>
<!doctype html Private "-//w3c//dtd html 4.0 transitional//en">
<html>
	<head>
		<title>Non Confirmit� des Produits & Services / Opportunit� de Perfectionnement</title>
		
		<script runat="server">
			sub Page_Load
				Session("LG") = "FR"
				LoadPage()
			end sub
		</script>
		
		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css" />
	</head>
	<body>
		<!-- #include file ="COMMUN/CanevasHaut.htm" -->	
			Non Conformit� / Action Pr�ventive
		<!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
			<br /><br /><br /><br />			
			<table align="center">
				<tr>
					<td class="centre">
						<br /><br /><br /><br />&nbsp;&nbsp;
						<span class=" taille13 centre gras">
							Le formulaire a �t� sauvegard� avec succ�s.<br /><br />
							Vous serez redirig� au <a href="default_FR.aspx">Menu Principal</a> une fois le rapport ferm�.
						</span>
					</td>
				</tr>			
			</table>
		<!-- #include file ="COMMUN/CanevasBas.htm" -->
	</body>
</html>

