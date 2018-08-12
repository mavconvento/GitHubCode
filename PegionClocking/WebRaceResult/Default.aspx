<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebRaceResult.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"
        Modal="false" Style="z-index: 99000" />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblClubName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <%-- <td>
                Choose Club :
            </td>
            <td>
                <telerik:RadComboBox ID="rcbClubName" runat="server" Width="200px" AutoPostBack="true"
                    OnSelectedIndexChanged="rcbClubName_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>--%>
            <td>
                Bird Category :
            </td>
            <td>
                <telerik:RadComboBox ID="rcbBirdCategory" runat="server" Width="200px" AutoPostBack="false"
                    OnSelectedIndexChanged="rcbBirdCategory_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
            <td>
                Race Category :
            </td>
            <td>
                <telerik:RadComboBox ID="rcbGroupCategory" runat="server" Width="200px" AutoPostBack="false"
                    OnSelectedIndexChanged="rcbGroupCategory_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Release Date :
            </td>
            <td>
                <telerik:RadDatePicker ID="rdpReleaseDate" runat="server" Width="100px" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                Search Name:
            </td>
            <td>
                <telerik:RadTextBox ID="rtbSearhName" runat="server" Width="200" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr width="100%">
            <td width="100%">
                <telerik:RadButton ID="radBtnViewResult" runat="server" OnClick="radBtnViewResult_Click"
                    Text="View Result" ValidationGroup="required" Skin="Simple"  Width="100%"/>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:RadGrid ID="rgRaceResult" runat="server" AllowPaging="True" AllowSorting="True"
                    HeaderStyle-VerticalAlign="Top" AutoGenerateColumns="False" CellSpacing="0" GridLines="None"
                    OnPageIndexChanged="rgRaceResult_PageIndexChanged" PageSize="10" OnItemCommand="rgRaceResult_ItemCommand"
                    OnNeedDataSource="rgRaceResult_NeedDataSource" OnItemDataBound="rgRaceResult_ItemDataBound">
                    <MasterTableView DataKeyNames="" AllowMultiColumnSorting="True" Width="100%">
                        <NoRecordsTemplate>
                            <div>
                                No bird is arrive.</div>
                        </NoRecordsTemplate>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="100px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="100px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="Rank" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Rank" DataField="rank" HeaderButtonType="TextButton" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="memberName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" HeaderText="Member/Loft Name" DataField="memberName"
                                HeaderButtonType="TextButton" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="distance" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" HeaderText="Distance (M)" DataField="distance"
                                HeaderButtonType="TextButton" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ringNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" HeaderText="Band/Ring Number" DataField="ringNumber"
                                HeaderButtonType="TextButton" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Sticker Code" DataField="code" HeaderButtonType="TextButton" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="arrival" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" HeaderText="Arrival Time" DataField="arrival"
                                HeaderButtonType="TextButton" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Flight" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Flight" DataField="Flight" HeaderButtonType="TextButton" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Speed" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Speed (MPM)" DataField="speed" HeaderButtonType="TextButton" Visible="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="remarks" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" HeaderText="Source" DataField="remarks" HeaderButtonType="TextButton"
                                Visible="true">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    </HeaderContextMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
