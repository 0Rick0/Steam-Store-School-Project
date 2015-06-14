<%@ Page Title="" Language="C#" MasterPageFile="~/steam.Master" AutoEventWireup="true" CodeBehind="BuyGame.aspx.cs" Inherits="SteamStore.BuyGame" %>

<%@ Register Src="~/GamesMenu.ascx" TagPrefix="uc1" TagName="GamesMenu" %>


<asp:Content ID="ctHead" ContentPlaceHolderID="head" runat="server">
    <title>Buy | SteamStore</title>
    <link href="styles/buyGame.css" rel="stylesheet" />
    <link href="styles/gamesmenu.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ctContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <uc1:GamesMenu runat="server" ID="GamesMenu" />
    <div class="innerContent">
        <div id="left">
            <h1><%=GameTile %> For <%= GamePrice.ToString("f2") %>€</h1>
            <%=Games %>
        </div>
        <form id="frmRight" runat="server">
            <asp:Label runat="server" >Balance before: <%=Balance.ToString("f2") %>€</asp:Label><br />
            <asp:Label runat="server" ID="balAfter">Balance After: <%=(Balance-GamePrice).ToString("f2") %>€</asp:Label>
            <asp:Button Text="Buy" runat="server" CssClass="BuyButton" OnClick="Unnamed3_Click"/>
        </form>
    </div>
</asp:Content>
