<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<%@ Page Language="vb" Codebehind="Contact.aspx.vb" Inherits="FRANCS.newContact" %>
<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>Ajouter / Modifier un Contact</title>
    
    <script runat="server">
		sub Page_Load
			Session("LG") = "FR"
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
    <form method="post" runat="server">
		Ajouter / Modifier un Contact
    <!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
		<br ><br >
		<table class="TitreFormulaire">
			<tr>
				<td>Client:</td>
				<td><cc:combobox id="Customer" runat="server" width="350" cssclass="taille10"></cc:combobox></td>
			</tr>
		</table>
		
		<table class="TitreFormulaire">
			<tr>
				<td>Nom du Contact:</td>
				<td><asp:textbox id="Contact" maxlength="30" runat="server" cssclass="taille10" /></td>
			</tr>
		</table>
		
		<table class="TitreFormulaire">
			<tr>
				<td>#tel du Contact:</td>
				<td><asp:textbox id="ContactTel" maxlength="20"  runat="server" cssclass="taille10" /></td>
			</tr>
		</table>
			
		<br ><br >	
		<center>
			<asp:button text="Ok" onclick="saveContact" runat="server" width="50px" />
			<input type="button" value="Annuler" onclick="javascript:window.close();">
		</center>
    </form>
	<!-- #include file ="COMMUN/CanevasBas.htm" -->
  </body>
</html>
