<div id="CO">
	<br>
	<center><span class="taille14 gras souligne">Close-Out</span></center>
	<br>
	<table cellpadding="3">
		<tr>
			<td>
				<span class="TitreFormulaire">Closed-Out date:</span>&nbsp;&nbsp;&nbsp;
				<asp:textbox maxlength="10" size="15" runat="server" id="CloseOutDate" />
				<a href="#" id="anchor3" onclick="cal1xx.select(document.forms[0].CloseOutDate,'anchor3','MM/dd/yyyy'); return false;">
					<img src="CALENDAR/images/calendar.jpg" border="0" width="23" height="21"> </a>
			</td>
		</tr>
		<TR>
			<TD class="TitreFormulaire" colSpan="2">
				<asp:CheckBox id="HasProducedChange" runat="server" Text="This feedback has produced a change"></asp:CheckBox></TD>
		</TR>
		<TR>
			<TD class="TitreFormulaire" colSpan="2">
				<asp:CheckBox id="FollowUp" runat="server" Text="Follow Up"></asp:CheckBox></TD>
		</TR>
		<tr>
			<td class="TitreFormulaire" colspan="2">Closed-Out Communications:</td>
		</tr>
		<tr>
			<td class="taille10" colspan="2">
				<asp:textbox rows="15" id="CloseOutMsg" columns="100" textmode="MultiLine" class="taille10" runat="server" />
			</td>
		</tr>
	</table>
</div>
