<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MAVCPigeonClockingWebsite.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        html, body
        {
            height: 100%;
        }
        #main
        {
            border-style: solid;
            margin: 1% 20% 1% 20%;
            height: 450px;
        }
        .clubLogo
        {
            width: 80px;
            height: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div align="center" id="main">
        <div id="content" style="height: 100%;">
            <table>
                <tr>
                    <td colspan="2" align="center">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label1" runat="server" Text="Label" Font-Size="12">Choose Club Name</asp:Label>
                    </td>
                    <td width="73%" align="left">
                        <asp:DropDownList ID="cmbClubName" runat="server" Font-Size="10" Width="100%" OnSelectedIndexChanged="cmbClubName_OnSelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnGo" runat="server" Text="GO" Width="50px" 
                            onclick="btnGo_Click" Enabled="False" />
                    </td>
                </tr>
                <%--<tr>
                    <td align="left">
                        <asp:Label ID="Label2" runat="server" Text="Label" Font-Size="12">Select Race Date</asp:Label>
                    </td>
                    <td align="left">
                        <input type="text" id="simple-date" />
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
<%--                <tr>
                    <td align="center">
                        <asp:Button ID="btnViewPrevious" runat="server" Text="Previous Race Results" Font-Bold="true"
                            OnClick="btnViewPrevious_OnClick" Enabled="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnViewRaceResult" runat="server" Text="View Race Result" Font-Bold="true"
                            OnClick="btnViewResult_OnClick" Enabled="false" Width="100%" />
                    </td>
                </tr>--%>
            </table>
        </div>
    </div>
    <iframe width="625" height="650" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.com.ph/maps/myplaces?hl=en&amp;ctz=-480&amp;ie=UTF8&amp;ll=14.588567,121.131134&amp;spn=0.188388,0.338173&amp;t=m&amp;z=12"></iframe><br /><small>View <a href="https://maps.google.com.ph/maps/myplaces?hl=en&amp;ctz=-480&amp;ie=UTF8&amp;ll=14.588567,121.131134&amp;spn=0.188388,0.338173&amp;t=m&amp;z=12&amp;source=embed" style="color:#0000FF;text-align:left">Untitled</a> in a larger map</small>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphButtonBar" runat="server">
</asp:Content>
