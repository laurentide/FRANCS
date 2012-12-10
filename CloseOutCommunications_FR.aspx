<%@ Page Language="vb" CodeBehind="CloseOutCommunications.aspx.vb" Inherits="FRANCS.CloseOutCommunications" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>Message de Fermeture</title>
    
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
		
		//resize window
		resizeTo(700, 475);
		
	</script>
</head>
  <body>
	<!-- #include file ="COMMUN/CanevasHaut.htm" -->
    <form method="post" runat="server" id="Form1">
		Message de Fermeture
    <!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
		<br ><br >
		<center>
			<asp:textbox rows="9" id="CloseOutMsg" readonly="true"
			columns="65" textmode="MultiLine" class="fondBleuPale taille10" runat="server" />
		
		<br /><br />
			<input type="button" value="Fermer" onclick="javascript:window.close();">
		</center>
    </form>
	<!-- #include file ="COMMUN/CanevasBas.htm" -->
  </body>
</html>
