<%@ Control Language="vb" inherits="UserControls.CC_Controls.ComboBox" debug="true" %>

<script language="javascript" src="COMBOBOX/ComboBox.js"></script>
<table cellpadding="0" cellspacing="0">
	<tr valign="top">
		<td>
			<asp:textbox id="Texte" Runat="server" onKeyUp="javascript:TextChange();" />
			<input type="hidden" id="TextisChanged" runat="server" name="TextisChanged">
		</td>
		<td>
			<img src="images\arrow.jpg" align="top" height="22" id="Arrow" onclick="ShowList();"
				runat="server">
		</td>
	</tr>
</table>
<DIV ID="DivList" runat="server" STYLE="VISIBILITY:hidden;POSITION:absolute;layer-background-color:white">
	<asp:ListBox Runat="server" id="listBox" onClick="javascript:FillText();" />
</DIV>
<script language="javascript">
	document.body.attachEvent("onclick", HideList);
</script>

