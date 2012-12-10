<%@ Page Language="vb" Codebehind="SuspectedCause.aspx.vb" Inherits="FRANCS.SectionC" %>

<!doctype html Private "-//w3c//dtd html 4.0 transitional//en">
<html>
  <head>
    <title>Suspected Cause of Deficiency</title>
    
    <script runat="server">
		sub Page_Load
			Session("LG") = "EN"
			LoadPage()
		end sub
    </script>
    
    <link rel="stylesheet" type="text/css" href="COMMUN/Styles.css" >
    <script language="javascript" src="COMMUN/NCR.js"></script>
  </head>
  <body id="SC">

    <form runat="server" id="Form1">
		<table border="1" height="88%" width="100%" class="fondBleuPale">
			<tr valign="top">
				<!-- #include file ="Menu_EN.htm" -->
				
				<td width="*" > 
					
					<table border="0" class="pasBordure" width="100%" cellspacing="0">
						<tr>
							<td align="center" colspan="8" class="formulaireTitre taille14 gras souligne">
								Suspected Cause of Deficiency 
							</td>
						</tr>
						<tr><td class="formulaireLigne1" colspan="8"></td></tr>
						
						<!-- Description of Deficiency -->
						<tr>
							<td class="TitreFormulaire" colspan="2"><br >&nbsp;&nbsp;
								* Suspected Cause of Deficiency / Deviation:
							</td>
						</tr>
						<tr>
							<td class="indentation"><br >&nbsp;
								<asp:textbox rows="20" id="cause" columns="100" textmode="MultiLine" class="taille10" runat="server" />
							</td>
							<td width="400"></td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		
		<br />
		<table width="100%">
			<tr>
				<td>&nbsp;&nbsp;&nbsp;
					<%if Session("Edit") = true Then %>
						<input type="button" value="Back to Main Menu" onclick="javascript:mainPage('EN');" >
					<% Else %>
						<input type="button" value="Back to Main Menu" onclick="javascript:location.replace('Default_EN.aspx');" >
					<% End If %>
				</td>
				<td align="right">
					<asp:button text="< Previous" onclick="PreviousPage" runat="server" />
					<asp:button text="Next >" onclick="NextPage" runat="server" />&nbsp;&nbsp;&nbsp;
				</td>
			</tr>
		</table>

    </form>
  </body>
</html>
