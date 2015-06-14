<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GamesMenu.ascx.cs" Inherits="SteamStore.GamesMenu" %>
<div id="categories">
    <ul id="topUl" runat="server">
        
    </ul>
    <div id="sbc">
            <form action="search.aspx" method="get" id="sb"><input type="text" name="q" placeholder="Search games or users"><input type="submit" value="Search"></form>

    </div>
</div>
