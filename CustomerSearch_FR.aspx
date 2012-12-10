<%@ Register TagPrefix="CC" TagName="Combobox" Src="/FRANCS/COMBOBOX/Combobox.ascx" %>
<%@ Page Language="vb" CodeBehind="CustomerSearch.aspx.vb" Inherits="FRANCS.CustomerSearch" %>

<!DOCTYPE HTML Private "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>Customer Search</title>
    
    <script runat="server">
		sub Page_Load
			Session("LG") = "FR"
			LoadPage()
		end sub
    </script>
    
    <link rel="stylesheet" type="text/css" href="COMMUN/Styles.css">
	<script>
		//places current window in the center
		var xpos = (screen.width - 700) / 2
		var ypos = (screen.height - 500) / 2
		moveTo(xpos, ypos);
	</script>
</head>
  <body width="100px" height="100px">
	<!-- #include file ="COMMUN/CanevasHaut.htm" -->
    <form method="post" runat="server">
		Recherche d'un Client
    <!-- #include file ="COMMUN/CanevasApresTitre.htm" -->
		<br >
		<table class="TitreFormulaire">
			<tr>
				<td># Client:</td>
				<td>Nom du Client :</td>
				<td>Ville:</td>
			</tr>
			<tr>
				<td><asp:TextBox id="CustomerNo" runat="server" size="10" cssclass="taille10" /></td>
				<td><asp:TextBox id="Customer" runat="server" size="30" cssclass="taille10" /></td>
				<td><asp:textbox id="City" size="15" runat="server" cssclass="taille10" /></td>
				<td><asp:button text="Rechercher" onclick="showCustomers" runat="server" /></td>
			</tr>
		</table>
		
		<hr />
		<div style="OVERFLOW: auto; WIDTH: 95%; HEIGHT: 70%">
			<!-- TABLE!!!!! -->
			<asp:table id="SearchResult" class="searchMarge" width="95%" runat="server" />
		</div>
		<hr />
		<center>
			<asp:button text="Sélectionner le Client" onclick="SelectCustomer"  runat="server" />
		</center>
    </form>
	<!-- #include file ="COMMUN/CanevasBas.htm" -->
  </body>
</html>
