<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="OverAllRaceResult.aspx.cs" Inherits="MAVCPigeonClockingWebsite.OverAllRaceResult" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link rel="stylesheet" href="~/Styles/PigeonClocking_Screen.css" type="text/css"
        media="screen" />
    <style type="text/css">
        div.centered
        {
            text-align: center;
            margin-left: 10px;
            width: 100%;
        }
        .centered table, .centered tbody, .centered tbody tr
        {
            margin: 0;
            text-align: left;
            width: 100%;
        }
        
        div .main
        {
            margin-left: 30px;
            margin-right: 30px;
        }
        
        .leftside
        {
            width: 100px;
            text-align: right;
            font-weight: bold;
        }
        .rightside
        {
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="centered">
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
        <hr size="1" style="color: Black; width: 100%; color: Black; margin: 0px !important" />
        <table style="margin: 0px; padding: 0px;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblDate" runat="server" Text="Select Race :"></asp:Label>
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
            </tr>
        </table>
        <hr size="1" style="color: Black; width: 100%; color: Black; margin: 0px !important" />
        <table class="HTMLTable">
            <tr>
                <td style="width: 30%">
                    <table class="HTMLTable">
                        <tr>
                            <td class="leftside">
                                <asp:Label ID="Label3" runat="server" Text="Lap 1 :"></asp:Label>
                            </td>
                            <td class="rightside">
                                <asp:Label ID="lblLap1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftside">
                                <asp:Label ID="Label4" runat="server" Text="Lap 2 :"></asp:Label>
                            </td>
                            <td class="rightside">
                                <asp:Label ID="lblLap2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftside">
                                <asp:Label ID="Label5" runat="server" Text="Lap 3 :"></asp:Label>
                            </td>
                            <td class="rightside">
                                <asp:Label ID="lblLap3" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftside">
                                <asp:Label ID="Label7" runat="server" Text="Lap 4 :"></asp:Label>
                            </td>
                            <td class="rightside">
                                <asp:Label ID="lblLap4" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="HTMLTable">
                        <tr>
                            <td class="leftside">
                                <asp:Label ID="Label6" runat="server" Text="Lap 5 :"></asp:Label>
                            </td>
                            <td class="rightside">
                                <asp:Label ID="lblLap5" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftside">
                                <asp:Label ID="Label9" runat="server" Text="Lap 6 :"></asp:Label>
                            </td>
                            <td class="rightside">
                                <asp:Label ID="lblLap6" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftside">
                                <asp:Label ID="Label11" runat="server" Text="Lap 7 :"></asp:Label>
                            </td>
                            <td class="rightside">
                                <asp:Label ID="lblLap7" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftside">
                                <asp:Label ID="Label13" runat="server" Text="Lap 8 :"></asp:Label>
                            </td>
                            <td class="rightside">
                                <asp:Label ID="lblLap8" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr size="1" style="color: Black; width: 100%; color: Black; margin: 0px !important" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="HTMLTable">
                        <tr>
                            <td colspan="4" class="TD_Heading">
                                <asp:Label ID="Label2" runat="server"><b>Over All Race Result:</b></asp:Label>
                                <telerik:RadGrid ID="rgResults" runat="server" AllowPaging="True" AllowSorting="True"
                                    ItemStyle-VerticalAlign="Top" AlternatingItemStyle-VerticalAlign="Top" AutoGenerateColumns="False"
                                    CellSpacing="0" GridLines="None" OnNeedDataSource="rgResults_NeedDataSource"
                                    OnItemCommand="rgResults_ItemCommand" OnItemDataBound="rgResults_ItemDataBound">
                                    <MasterTableView DataKeyNames="" AllowSorting="True" AllowPaging="True" PageSize="10">
                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Rank"
                                                UniqueName="Rank" DataField="Rank" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="MemberID"
                                                UniqueName="MemberID" DataField="MemberID" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Member Name"
                                                UniqueName="MemberName" DataField="MemberName" HeaderStyle-VerticalAlign="Top"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Wrap="false" HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Band No."
                                                UniqueName="BandNo" DataField="BandNo" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Points"
                                                UniqueName="Points" DataField="Points" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Lap 1"
                                                UniqueName="Lap1" DataField="Lap1" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Lap 2"
                                                UniqueName="Lap2" DataField="Lap2" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Lap 3"
                                                UniqueName="Lap3" DataField="Lap3" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Lap 4"
                                                UniqueName="Lap4" DataField="Lap4" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Lap 5"
                                                UniqueName="Lap5" DataField="Lap5" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Lap 6"
                                                UniqueName="Lap6" DataField="Lap6" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Lap 7"
                                                UniqueName="Lap7" DataField="Lap7" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Lap 8"
                                                UniqueName="Lap8" DataField="Lap8" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Total Speed"
                                                UniqueName="Total" DataField="Total" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                                HeaderStyle-Width="35px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Group/Category "
                                                UniqueName="GroupCategory" DataField="GroupCategory" HeaderStyle-VerticalAlign="Top"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Wrap="false" HeaderStyle-Width="35px" Visible="false">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                    </ClientSettings>
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
                                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                                    </HeaderContextMenu>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphButtonBar" runat="server">
</asp:Content>
