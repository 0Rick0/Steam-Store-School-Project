<%@ Page Title="" Language="C#" MasterPageFile="~/steam.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="SteamStore.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Profile | SteamStore</title>
    <link href="styles/Profile.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <div id="innerContent">
        <% if (IsSelf) %>
        <%
           { %>
        <form action="/addBalance.aspx" method="post" id="addBalanceForm">
            <label for="addbalance">Add balance to account</label>
            <input type="number" id="addbalance" name="addbalance"/>
            <input type="submit"/>
        </form>
        <% } %>
        <div id="top">
            <h1><%=Username %> <%= IsSelf?" - "+Balance.ToString("f2")+"€":"" %></h1>
        </div>
        
        <h2>Friends</h2>
        <div id="friends" runat="server" ClientIDMode="Static">
            
        </div>
        <h2>Achievements</h2>
        <div id="achievements" runat="server" ClientIDMode="Static">
            
        </div>
    </div>
</asp:Content>
