<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Hangout._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <head>
        <link href="styles.css" rel="stylesheet" type="text/css" />
    </head>
    
    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Functionality Testing in Application:</h1>
            <ul>
                <li><b>Staff Page:</b> To Test the Make Admin Functionality</li>
                <li><b>Member Page:</b> To Test the Services and access</li>
                <li><b>Trigger Error:</b> To Simulate an Error, to test the Global.asax error Handling</li>
                <li><b>Likes Page:</b> To Get all the Stores Liked by the user</li>
                <li>Test Hashing Services following the SHA256 Algorithm</li>
                <li>Test Stemming Services</li>
            </ul>
            <div class="button-container">
                <asp:Button ID="Button1" runat="server" Text="Member Page" CssClass="custom-btn" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Staff Page" CssClass="custom-btn" OnClick="Button2_Click" />
            </div>
        </section>
        <div>
            <asp:Button ID="TriggerErrorButton" runat="server" Text="Trigger Error" OnClick="TriggerErrorButton_Click" CssClass="error-btn" />
            <asp:Button ID="Button5" runat="server" Text="Likes Page" CssClass="custom-btn" OnClick="Button5_Click" />
        </div>
        </br>
        <h4><b style="color: blue;">Hashing Service</b></h4>
        <div>
            <b><asp:Label ID="Label1" runat="server" Text="HashInput"></asp:Label></b>&nbsp;&nbsp;
            <asp:TextBox ID="hash_input" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="Button3" runat="server" Text="Hash" OnClick="Button3_Click1" />&nbsp;&nbsp;
            <b><asp:Label ID="Label2" runat="server" Text="Output"></asp:Label></b>&nbsp;
            &nbsp;<asp:TextBox ID="hash_output" runat="server"></asp:TextBox>
        </div>
        <br/>
        <h4><b style="color: blue;">Stemming Service</b></h4>
    <div>
    <b><asp:Label ID="Label3" runat="server" Text="Stemming Input"></asp:Label></b>&nbsp;&nbsp;
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="Button4" runat="server" Text="Stem" OnClick="Button4_Click" />&nbsp;&nbsp;
    <b><asp:Label ID="Label4" runat="server" Text="Stemming Output"></asp:Label></b>&nbsp;
    &nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </div>
        <hr/>
        <h4>Flow Diagram</h4>
       <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/FlowChart.jpeg" style="width: 800px; height: 800px;"/>
    </main>
</asp:Content>
