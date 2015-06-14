<%@ Page Title="" Language="C#" MasterPageFile="~/steam.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="SteamStore.search" %>

<%@ Register Src="~/GamesMenu.ascx" TagPrefix="uc1" TagName="GamesMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Search | SteamStore</title>
    <link href="styles/gamesmenu.css" rel="stylesheet" />
    <link href="styles/search.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <uc1:GamesMenu runat="server" ID="GamesMenu" />
    <div id="innerContent" runat="server" ClientIDMode="Static">
        
    </div>
</asp:Content>
