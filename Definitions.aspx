<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<%@ Page Language="vb" Codebehind="Definitions.aspx.vb" Inherits="FRANCS.Def" %>

<!doctype html private "-//w3c//dtd html 4.0 transitional//en">
<html>
  <head>
    <title>Definitions</title>
    
    <script runat="server">
		sub Page_Load
			LoadPage()
		end sub
    </script>
    
    <link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
	<script>
		//places current window in the center
		var xpos = (screen.width - 600) / 2
		var ypos = (screen.height - 305) / 2
		moveTo(xpos, ypos);
	</script>
</head>
  <body width="100px" height="100px">
	<!-- #include file ="COMMUN/CanevasHaut.htm" -->
    <form method="post" runat="server" id="Form1">
		<asp:label id="Title" runat="server" />
    <!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
		<br ><br >
		
		<center>
		<table height="80%" width="90%" >
			<tr>
				<td>
					<asp:label class="TitreFormulaire taille16" id="NomDef" runat="server" /> 
				</td>
			</tr>
			<tr valign="Top">
				<td>
					<asp:label id="Def" class="taille12" runat="server" />
				</td>
			</tr>
			<tr valign="Bottom">
				<td colspan="2" align="center">
					<% If Session("LG") = "FR" %>
						<input type="button" value="Fermer"  onclick="javascript:window.close();">
					<% Else %>
						<input type="button" value="Close"  onclick="javascript:window.close();">
					<% End If %>
				</td>
			</tr>
		</table>
		</center>
		
    </form>
	<!-- #include file ="COMMUN/CanevasBas.htm" -->
  </body>
</html>
