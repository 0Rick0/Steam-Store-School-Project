<%@ Page Title="" Language="C#" MasterPageFile="~/steam.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="SteamStore.styles.error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>What did you do!</title>
    <style>
        .error {
            width: 100%;
            height: 200px;
            line-height: 100px;
            font-size: 100px;
            color: #fff;
            background-color: #000000;
            background-color: rgba(0, 0, 0, 0.2);
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <div class="error">
        What dit you do!<br/>
        <%=Server.UrlDecode(Request.QueryString["errorMessage"]) %>
    </div>
</asp:Content>
