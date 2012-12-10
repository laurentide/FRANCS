

<div id=GI><br>
<center><span class="taille14 gras souligne">Informations Générales</SPAN></CENTER><br>
<table cellPadding=2>
  <TR>
    <TD class=TitreFormulaire>ARC #</TD>
    <TD class=taille10><asp:label id=NCFNo runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire>Date du constat:</TD>
    <td class=taille10><asp:label id=IssueDate runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire>Type du formulaire:</TD>
    <td class=taille10><asp:label id=FormType runat="server"></asp:Label></TD>
    <td class=TitreFormulaire>Type de rétroaction:</TD>
    <td class=taille10><asp:dropdownlist id=FeedbackType runat="server"></asp:DropDownList></TD></TR>
  <tr>
    <td class=TitreFormulaire>Client:</TD>
    <td class=taille10><asp:label id=Customer runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire>Contact:</TD>
    <td class=taille10><asp:label id=Contact runat="server"></asp:label></TD>
    <td class=TitreFormulaire>#tel du Contact:</TD>
    <td class=taille10><asp:label id=contactTel runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire>Constaté par:</TD>
    <td class=taille10><asp:label id=IssuedBy runat="server"></asp:label></TD>
    <td class=TitreFormulaire>Téléphone:</TD>
    <td class=taille10><asp:label id=EmployeeTel runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire># de commande:</TD>
    <td class=taille10><asp:label id=OrderNo runat="server"></asp:label></TD>
    <td class=TitreFormulaire>Type de Categorie:</TD>
    <td class=taille10><cc:combobox id=CategoryTypes 
       runat="server" autopostback="true" 
      width="200"></cc:combobox></TD></TR>
  <tr>
    <td class=TitreFormulaire>Affecte la production:</TD>
    <td class=taille10><asp:dropdownlist id=Production runat="server"></asp:DropDownList></TD>
    <td class=TitreFormulaire>Catégorie:</TD>
    <td class=taille10><cc:combobox id=Categories 
       runat="server" width="370"></cc:combobox></TD></TR>
  <tr>
    <td colSpan=4><span class=TitreFormulaire 
      >Cette information peut être 
      partagée?:</SPAN>&nbsp;&nbsp;&nbsp; <asp:label class=taille10 id=shared runat="server"></asp:label></TD></TR></TABLE>
<hr>
<br>
<table>
  <tr>
    <td class=TitreFormulaire>Assigné à:</TD>
    <td class=taille10><asp:dropdownlist id=AssignedTo runat="server" width="150px"></asp:DropDownList></TD></TR>
  <TR>
    <TD class=TitreFormulaire>Délégué à:</TD>
    <TD class=taille10><asp:textbox id=DelegatedTo runat="server" Width="440px"></asp:TextBox></TD></TR>
  <tr>
    <td class=TitreFormulaire>Date de Suivi:</TD>
    <td class=taille10><asp:textbox id=FollowUpDate runat="server" maxlength="10" size="15"></asp:TextBox><A id=anchor1 onclick="cal1xx.select(document.forms[0].FollowUpDate,'anchor1','MM/dd/yyyy'); return false;" href="#" ><IMG height=21 src="CALENDAR/images/calendar.jpg" width=23 border=0 > </A></TD></TR>
  <tr>
    <td class=TitreFormulaire>Date de Fermeture 
Prévue:</TD>
    <td class=taille10>
				<asp:textbox size="15" maxlength="10" id="TargetCloseOutDate" runat="server" />
				<a href="#" id="anchor2" onclick="cal1xx.select(document.forms[0].TargetCloseOutDate,'anchor2','MM/dd/yyyy'); return false;">
					<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21"> </a></TD></TR></TABLE>
	<hr>
</DIV>
