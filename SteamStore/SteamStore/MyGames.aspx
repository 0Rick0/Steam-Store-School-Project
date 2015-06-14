<%@ Page Title="" Language="C#" MasterPageFile="~/steam.Master" AutoEventWireup="true" CodeBehind="MyGames.aspx.cs" Inherits="SteamStore.MyGames" %>
<%@ Register Src="~/smallGameView.ascx" TagPrefix="rwc" TagName="smallGameView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>My Games | Steampowered</title>
    <link href="styles/MyGames.css" rel="stylesheet" />
    <link href="styles/smallGame.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <div id="innerContent" clientidmode="Static" runat="server">
        <h2>My Games</h2>
    </div>
</asp:Content>
