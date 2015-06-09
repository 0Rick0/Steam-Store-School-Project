<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SteamStore.Login" MasterPageFile="steam.Master" %>

<%@ Register Src="~/GamesMenu.ascx" TagPrefix="uc1" TagName="GamesMenu" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Login | Steampowered</title>
    <link href="styles/gamesmenu.css" rel="stylesheet" />
    <link href="styles/login.css" rel="stylesheet" />
</asp:Content>

<asp:Content ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <uc1:GamesMenu runat="server" ID="GamesMenu" />
    <div id="frmLogin">
        <form id="form1" runat="server" class="loginItem">
            <asp:Label runat="server" Text="Gebruikersnaam" CssClass="loginLabel"></asp:Label>
            <asp:TextBox runat="server" ID="username" CssClass="loginTb"></asp:TextBox>
            <asp:Label runat="server" ID="lblPassword" Text="Wachtwoord" CssClass="loginLabel"></asp:Label>
            <asp:TextBox runat="server" ID="password" TextMode="Password" CssClass="loginTb"></asp:TextBox>
            <asp:Button runat="server" ID="submit" CssClass="subButton" Text="Sign In" OnClick="submit_Click" />
        </form>
        <form action="#signup.aspx" class="loginItem">
            <input type="submit" class="subButton" value="Sign Up">
        </form>
        <div id="info" class="loginItem">
            Log in of maak een account om spellen te kunnen kopen, spelen en om te chatten met vrienen.
        </div>
        <%-- Chrome rendering bug, float right push down login and sb. fix js reaplly?--%>
    </div>

</asp:Content>
