

<div id=GI><br>
<center><span class="taille14 gras souligne">General Information</SPAN></CENTER><br>
<table cellPadding=2>
  <TR>
    <TD class=TitreFormulaire>ARC #</TD>
    <TD class=taille10>
<asp:label id=NCFNo runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire>Date of issue:</TD>
    <td class=taille10><asp:label id=IssueDate runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire>Form type:</TD>
    <td class=taille10><asp:label id=FormType runat="server"></asp:Label></TD>
    <td class=TitreFormulaire>Feedback type:</TD>
    <td class=taille10><asp:dropdownlist id=FeedbackType runat="server"></asp:DropDownList></TD></TR>
  <tr>
    <td class=TitreFormulaire>Customer:</TD>
    <td class=taille10><asp:label id=Customer runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire>Contact:</TD>
    <td class=taille10><asp:label id=Contact runat="server"></asp:label></TD>
    <td class=TitreFormulaire>Contact Telephone #:</TD>
    <td class=taille10><asp:label id=contactTel runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire>Issued by:</TD>
    <td class=taille10><asp:label id=IssuedBy runat="server"></asp:label></TD>
    <td class=TitreFormulaire>Telephone #:</TD>
    <td class=taille10><asp:label id=EmployeeTel runat="server"></asp:label></TD></TR>
  <tr>
    <td class=TitreFormulaire>Order #:</TD>
    <td class=taille10><asp:label id=OrderNo runat="server"></asp:label></TD>
    <td class=TitreFormulaire>Category Type:</TD>
    <td class=taille10><cc:combobox id=CategoryTypes 
       runat="server" autopostback="true" 
      width="200"></cc:combobox></TD></TR>
  <tr>
    <td class=TitreFormulaire>Affecting Quality:</TD>
    <td class=taille10><asp:dropdownlist id=Production runat="server"></asp:DropDownList></TD>
    <td class=TitreFormulaire>Category:</TD>
    <td class=taille10><cc:combobox id=Categories 
       runat="server" width="300"></cc:combobox></TD></TR>
  <tr>
    <td colSpan=4><span class=TitreFormulaire 
      >Can this information be 
      shared?:</SPAN>&nbsp;&nbsp;&nbsp; <asp:label class=taille10 id=shared runat="server"></asp:label></TD></TR></TABLE>
<hr>
<br>
<table>
  <tr>
    <td class=TitreFormulaire>Assigned To:</TD>
    <td class=taille10><asp:dropdownlist id=AssignedTo runat="server" width="150px"></asp:DropDownList></TD></TR>
  <TR>
    <TD class=TitreFormulaire>Delegated To:</TD>
    <TD class=taille10><asp:textbox id=DelegatedTo runat="server" Width="440px"></asp:TextBox></TD></TR>
  <tr>
    <td class=TitreFormulaire>Follow-up Date:</TD>
    <td class=taille10><asp:textbox id=FollowUpDate runat="server" maxlength="10" size="15"></asp:TextBox><A id=anchor1 onclick="cal1xx.select(document.forms[0].FollowUpDate,'anchor1','MM/dd/yyyy'); return false;" href="#" ><IMG height=21 src="CALENDAR/images/calendar.jpg" width=23 border=0 > </A></TD></TR>
  <tr>
    <td class=TitreFormulaire>Target Close Out Date:</TD>
    <td class=taille10><asp:textbox id=TargetCloseOutDate runat="server" maxlength="10" size="15"></asp:textbox>
				<a href="#" id="anchor2" onclick="cal1xx.select(document.forms[0].TargetCloseOutDate,'anchor2','MM/dd/yyyy'); return false;">
					<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21"> </a></TD></TR></TABLE>
	<hr>
</DIV>
