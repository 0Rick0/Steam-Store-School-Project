<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="gameViewPack.ascx.cs" Inherits="SteamStore.gameViewPack" %>

<div class="packContainer">
    <div class="packDescription">
        <div class="packTitle"><%= PackTitle %></div>
        <div class="packContent"><%=PackGames %></div>
    </div>
    <div class="price">
        <div class="discount">
            <%=Discount %>
        </div>
        <div class="innerPrice">
            <div class="oldPrice"><%=OldPrice %></div>
            <div class="newPrice"><%=NewPrice %></div>
        </div>
        <a href="/buy.aspx?packId=<%=PackId %>" class="buy">Buy!</a>
    </div>
</div>
