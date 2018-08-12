<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RaceDetails.ascx.cs"
    Inherits="MAVCPigeonClockingWebsite.RaceDetails" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link rel="stylesheet" href="~/Styles/PigeonClocking_Screen.css" type="text/css"
    media="screen" />
<style type="text/css">
    body
    {
        background-color: White;
    }
    
    .leftside
    {
        width: 350px;
        text-align: left;
    }
    
    .rightside
    {
        width: 300px;
        text-align: left;
    }
    
    
    .leftside table
    {
        margin: 0px;
        width: 100%;
    }
    
    .innerLeftSide
    {
        width: 120px;
        text-align: right;
    }
    
    .lblBold label
    {
        font-weight: bold;
    }
    
    .middleside
    {
        width: 300px;
        }
    
</style>
<div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <table class="HTMLTable" align="center" width="100%">
        <tr>
            <td align="left" valign="middle">
                <table>
                    <tr>
                        <td width="80px" align="center">
                            <img id="Logo" src="Images/ClubLogo/MMFC.png" alt="Logo" width="50" height="50" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblClubName" runat="server" Text="" Font-Bold="true" Font-Size="20px">Pigeon Sports Pilipinas</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div>
    <hr size="1" style="color: Black; width: 100%; color: Black; margin: 0px !important" />
</div>
<div>
    <table style="margin: 0px; padding: 0px;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblDate" runat="server" Text="Date :"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbDate" runat="server" Width="220px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblCategory" runat="server" Text="Category :"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbCategory" runat="server" Width="220px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblGroup" runat="server" Text="Group :"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbGroup" runat="server">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr size="1" style="color: Black; width: 100%; color: Black; margin: 2px !important" />
                <asp:Button ID="Button1" runat="server" Text="View Race Result" Width="100%" Height="25px"
                    Font-Bold="true" OnClick="Button1_Click" />
                <hr size="1" style="color: Black; width: 100%; color: Black; margin: 2px !important" />
            </td>
            <td>
            <asp:Button ID="Button2" runat="server" Text="View Over All Race Result" Width="100%" Height="25px"
                    Font-Bold="true" OnClick="Button2_Click" />
            </td>
        </tr>
    </table>
</div>
<div class="lblBold">
    <table class="HTMLTable" align="left" width="100%">
        <tr>
            <td class="leftside">
                <table>
                    <tr>
                        <td class="innerLeftSide">
                            <asp:Label ID="Label1" runat="server" Text="Location :" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblLocation" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="middleside">
                <table>
                    <tr>
                        <td class="innerLeftSide">
                            <asp:Label ID="Label3" runat="server" Text="Time Released :" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTimeReleased" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="rightside">
                <table>
                    <tr>
                        <td class="innerLeftSide">
                            <asp:Label ID="Label5" runat="server" Text="Minimum Speed:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMinimumSpeed" runat="server" Text="" Font-Bold="true" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="leftside">
                <table>
                    <tr>
                        <td class="innerLeftSide">
                            <asp:Label ID="Label6" runat="server" Text="Coordinates :" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCoordinates" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="middleside">
                <table>
                    <tr>
                        <td class="innerLeftSide">
                            <asp:Label ID="Label8" runat="server" Text="Total Bird Entry :" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalBirdEntry" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="rightside">
                <table>
                    <tr>
                        <td class="innerLeftSide">
                            <asp:Label ID="Label4" runat="server" Text="Stop Time:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStopTimeDetails" runat="server" Text="" Font-Bold="true" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="leftside">
                <table>
                    <tr>
                        <td class="innerLeftSide">
                            <asp:Label ID="Label10" runat="server" Text="Lap :" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblLap" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="middleside">
                <table>
                    <tr>
                        <td class="innerLeftSide">
                            <asp:Label ID="Label12" runat="server" Text="Total SMS :" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalSMS" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr size="1" style="color: Black; width: 100%; color: Black; margin: 0px !important" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td width="120px">
                                        <asp:Label ID="Label2" runat="server" Text="Label" Font-Bold="true">Enter Name :</asp:Label>
                                    </td>
                                    <td width="200px">
                                        <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="rbtnSearch" runat="server" Text="Search" OnClick="rbtnSearch_OnClick">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
