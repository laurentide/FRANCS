<%@ Page Language="vb" Codebehind="FormSaved.aspx.vb" Inherits="FRANCS.FormSaved" %>
<!doctype html Private "-//w3c//dtd html 4.0 transitional//en">
<html>
	<head>
		
		<title>Non Confirming Products & Services / Opportunity for Improvement</title>
		
		<script runat="server">
			sub Page_Load
				Session("LG") = "EN"
				LoadPage()
			end sub
		</script>

		<link rel="stylesheet" type="text/css" href="COMMUN/Styles.css" />
	</head>
	<body>
		<!-- #include file ="COMMUN/CanevasHaut.htm" -->	
		Non Conformance / Preventive Action
		<!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
			<br /><br /><br /><br />			
			<table align="center">
				<tr>
					<td class="centre">
						<br /><br /><br /><br />&nbsp;&nbsp;
						<span class=" taille13 centre gras">
							Your form has been successfuly saved.<br /><br />
							You will be redirected to the <a href="default_EN.aspx">Main Menu</a> when the report will be closed.
						</span>
					</td>
				</tr>			
			</table>
		<!-- #include file ="COMMUN/CanevasBas.htm" -->
	</body>
</html>

