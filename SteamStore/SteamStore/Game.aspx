<%@ Page Title="" Language="C#" MasterPageFile="~/steam.Master" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="SteamStore.Game" %>

<%@ Register Src="~/GamesMenu.ascx" TagPrefix="rwc" TagName="GamesMenu" %>
<%@ Register Src="~/gameViewPack.ascx" TagPrefix="rwc" TagName="gameViewPack" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="head" runat="server">
    <title><%= GameName %> | Steampowered</title>
    <link href="styles/gamesmenu.css" rel="stylesheet" />
    <link href="styles/gameview.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.11.3.min.js"></script>
    <script>
        $(document).ready(function () {
            var imgs = $("#imagesContainer");
            function setCss(value) {
                imgs.animate({
                    "margin-left": value
                }, 500);
            }
            $("#next").click(function () {
                imgs.finish();
                if (imgs.css("margin-left") == "-<%=(Imagescnt-1)*504%>px") {
                    setCss("0px");
                } else {
                    setCss("-=504px");
                }
            });
            $("#prev").click(function () {
                imgs.finish();
                if (imgs.css("margin-left") == "0px") {
                    setCss("-<%=(Imagescnt-1)*504%>px");
                } else {
                    setCss("+=504px");
                }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="ctContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <rwc:GamesMenu runat="server" ID="GamesMenu" />
    <div id="innerContent">
        <div id="images">
            <div id="imagesContainer" clientidmode="Static" runat="server">
            </div>
            <div id="next">Next</div>
            <div id="prev">Prev</div>
        </div>
        <div class="titleDiv"><%= GameCategorie + ": " + GameName %></div>
        <div id="leftContent" class="leftContent" runat="server">
        </div>
        <div class="rightContent"></div>
    </div>
</asp:Content>
