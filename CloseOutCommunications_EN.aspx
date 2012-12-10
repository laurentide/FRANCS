<%@ Page Language="vb" CodeBehind="CloseOutCommunications.aspx.vb" Inherits="FRANCS.CloseOutCommunications" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>Close-Out Communications</title>
    
    <script runat="server">
		sub Page_Load
			Session("LG") = "EN"
			LoadPage()
		end sub
    </script>
    
    <link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
	<script>
		//places current window in the center
		//resize window
		resizeTo(700, 475);
		
		var xpos = (screen.width - 700) / 2
		var ypos = (screen.height - 475) / 2
		moveTo(xpos, ypos);
		
		
	</script>
</head>
  <body>
	<!-- #include file ="COMMUN/CanevasHaut.htm" -->
    <form method="post" runat="server" id="Form1">
		Close-Out Communications
    <!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
		<br ><br >
		<center>
			<asp:textbox rows="9" id="CloseOutMsg" readonly="true"
			columns="65" textmode="MultiLine" class="fondBleuPale taille10" runat="server" />
		
		<br /><br />
			<input type="button" value="Close Window" onclick="javascript:window.close();">
		</center>
    </form>
	<!-- #include file ="COMMUN/CanevasBas.htm" -->
  </body>
</html>
