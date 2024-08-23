<%@ Page Title="LikePage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Likes.aspx.cs" Inherits="Hangout.Likes" ValidateRequest="false"%>
<%-- change the inherits attribute to your application name --%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <h3>This page displays the information of all the liked stores</h3>
        <p>Note: the user id in Likes.xml is hard-coded to be 123456, which needs to be changed to the current user's id</p>
        <asp:TextBox ID="txt" runat="server" TextMode="MultiLine" Font-Size="Small" Width="5000px" MaxLength="5000" Height="300px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Show Liked Stores" OnClick="Button1_Click" />

        <h3>Like new stores:</h3>
        <p>Note: This is just a showcase of the Like Button, the textboxes below can be changed into the labels from the store page (if the user clicks on the like button next to a specific store it then extracts that store's info and store it in the xml file</p>
        <p>store type:
        <asp:TextBox ID="storeType" runat="server" Width="200px">enter store type</asp:TextBox></p>
        <p>store name:
        <asp:TextBox ID="storeName" runat="server" Width="200px">enter store name</asp:TextBox></p>
        <p>Address:
        <asp:TextBox ID="address" runat="server" Width="200px">enter address</asp:TextBox></p>
        <p>Zipcode:
        <asp:TextBox ID="zipcode" runat="server" Width="200px">enter zipcode</asp:TextBox></p>
        <p>Details:
        <asp:TextBox ID="details" runat="server" Width="200px">enter store details</asp:TextBox></p>
        <p>Note: I only added two reviews for simplicity reasons, but you can modify Button2_Click for it to add a variable number of reviews</p>
        <p>Review1:</p>
        <p>Star:
        <asp:TextBox ID="star1" runat="server" Width="200px">enter star level</asp:TextBox></p>
        <p>Comment:
        <asp:TextBox ID="comment1" runat="server" Width="200px">enter comment</asp:TextBox></p>
        <p>Review2:</p>
        <p>Star:
        <asp:TextBox ID="star2" runat="server" Width="200px">enter star level</asp:TextBox></p>
        <p>Comment:
        <asp:TextBox ID="comment2" runat="server" Width="200px">enter comment</asp:TextBox></p>
        <p><asp:Button ID="Button2" runat="server" Text="Like" OnClick="Button2_Click" /></p>
        <p><asp:Label ID="Liked" runat="server"></asp:Label></p>
    </main>
</asp:Content>
