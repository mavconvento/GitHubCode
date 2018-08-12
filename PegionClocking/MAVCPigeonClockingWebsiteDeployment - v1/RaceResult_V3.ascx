<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RaceResult_V3.ascx.cs"
    Inherits="MAVCPigeonClockingWebsite.RaceResult_V3" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <table class="HTMLTable">
        <tr>
            <td colspan="4" class="TD_Heading">
                <asp:Label ID="Label1" runat="server"><b>Race Results (V3):</b></asp:Label>
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
                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Member Name"
                                UniqueName="MemberName" DataField="MemberName" HeaderStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="false" HeaderStyle-Width="35px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Distance (M)"
                                UniqueName="Distance" DataField="Distance" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                HeaderStyle-Width="35px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Band No."
                                UniqueName="BandNo" DataField="BandNo" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                HeaderStyle-Width="35px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Sticker Code"
                                UniqueName="StickerCode" DataField="StickerCode" HeaderStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="false" HeaderStyle-Width="35px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Arrival Time"
                                UniqueName="ArrivalTime" DataField="ArrivalTime" HeaderStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="false" HeaderStyle-Width="35px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Flight"
                                UniqueName="Flight" DataField="Flight" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                HeaderStyle-Width="35px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" HeaderText="Speed (MPM)"
                                UniqueName="Speed" DataField="Speed" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                HeaderStyle-Width="35px">
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
</div>
