<%@ Page Language="C#" MasterPageFile="steam.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SteamStore.index" %>
<%@ Register Src="~/GamesMenu.ascx" TagPrefix="rwc" TagName="GamesMenu" %>
<%@ Register Src="~/smallGameView.ascx" TagPrefix="rwc" TagName="smallGameView" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Games | Steampowered</title>
    <link href="styles/gamesmenu.css" rel="stylesheet" />
    <link href="styles/games.css" rel="stylesheet" />
    <link href="styles/smallGame.css" rel="stylesheet" />
</asp:Content>

<asp:Content ContentPlaceHolderID="contentPlaceHolder" runat="server">
   <rwc:GamesMenu runat="server" id="GamesMenu" />
    <div id="innerContent" clientidmode="Static" runat="server">
        
    </div>
</asp:Content>