<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="RaceResult.aspx.cs" Inherits="MAVCPigeonClockingWebsite.RaceResult" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/RaceDetails.ascx" TagName="RaceDetails" TagPrefix="uc1" %>
<%@ Register Src="~/RaceResult_V1.ascx" TagName="RaceResultV1" TagPrefix="uc2" %>
<%@ Register Src="~/RaceResult_V3.ascx" TagName="RaceResultV3" TagPrefix="uc3" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="centered">
        <table>
            <tr>
                <td>
                    <uc1:RaceDetails ID="raceDetails" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc2:RaceResultV1 ID="raceResultV1" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc3:RaceResultV3 ID="raceResultV3" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphButtonBar" runat="server">
</asp:Content>
