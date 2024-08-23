<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="Hangout.Member" %>

<!DOCTYPE html>

<head runat="server">
    <title></title>
    <style>
       body {
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            color: #333;
            margin: 0;
            padding: 0;
            overflow: hidden; /* Ensure no scrollbars */
            position: relative;
        }
        
        /* Full-size background image */
        .background-image {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: -1;
            background-image: url('https://api.mapbox.com/styles/v1/foursquare/ckb8bcjeo06mx1imop002fnln/static/-112.07404,33.44838,12.8/1280x1280@2x?access_token=pk.eyJ1IjoiZm91cnNxdWFyZSIsImEiOiJjRGRqOVZZIn0.rMLhJeqI_4VnU2YdIJvD3Q');
            background-size: cover;
            background-position: center;
        }
        
        /* Overlay on top of the image */
        .overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(179, 212, 252, 0.7); /* Adjust the color and opacity as needed */
            z-index: -1; /* Behind other elements */
        }
        .largeLogo {
            font-size: 48px;
            color: #cc1543; /* Foursquare red color */
            margin-bottom: 10px;
        }
        
        .tagline {
            font-size: 18px;
            color: #666;
            margin-bottom: 20px;
        }
        
        .input-holder {
            position: relative;
            display: inline-block;
            width: 100%;
        }
        
        .input-holder input[type="text"] {
            width: calc(100% - 28px);
            padding: 10px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        
        .submitButton {
            background-color: #cc1543; /* Foursquare red color */
            color: #fff;
            border: none;
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            border-radius: 4px;
        }
        
        .chiclets {
            list-style: none;
            padding: 0;
            margin: 20px 0;
        }
        
        .simpleChiclet {
            display: inline-block;
            margin-right: 10px;
        }
        
        .chicletLink {
            text-decoration: none;
            color: #cc1543; /* Foursquare red color */
            border: 1px solid #cc1543; /* Foursquare red color */
            padding: 8px 12px;
            border-radius: 4px;
            transition: all 0.3s ease;
        }
        
        .chicletLink:hover {
            background-color: #cc1543; /* Foursquare red color */
            color: #fff;
        }
        ul {
        list-style: decimal;
        padding: 0;
        margin: 0;
    }

    /* Style for list items */
    ul#dynamicList1 li {
        list-style-type: decimal;
        margin-bottom: 5px; /* Adjust spacing between list items */
    }

    ul#dynamicList1 li a {
        list-style-type: decimal;
        color: #1a0dab; /* Google's link color */
        text-decoration: none;
        display: block;
        padding: 5px 0; /* Adjust vertical padding */
    }

    ul#dynamicList1 li a:hover {
        text-decoration: underline; /* Underline on hover */
    }
        /* Add your existing styles here */
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div class="background-image"></div>
        <div class="searchContent">
        <h1 class="largeLogo">Foursquare</h1>
        <h2 class="tagline">Find the best places to eat, drink, shop, or visit in any city in the world. Access over 75 million short tips from local experts.</h2>
     <div class="row">
    <section class="col-md-6" aria-labelledby="FindNearByStoresService">
                <h3>Active User</h3><asp:Label ID="Label13" runat="server" Text="User"></asp:Label>
        <br/>
        <br/>
        <hr/>
        <a> http://webstrar24.fulton.asu.edu/page5/Service1.svc </a>
        <br/>
        <h2 id="FindNearByStoresService">Find NearBy Stores</h2>
        <h4>Help you find the nearest stores as per your choice and zipcode !!</h4>
        <br/>
        <div class="container">
            <div class="row">
            <div class="col-md-6"><asp:Label ID="Label1" runat="server" Text="Store Name"></asp:Label></div>
                <div class="col-md-6">
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </div>
               <div class="col-md-6"><sub>** Please cross check StoreName since its an expression match !!</sub></div>
            </div>
            <%--<div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-3"><asp:Button ID="Button2" runat="server" Text="Convert" OnClick="Button2_Click"/></div>
            </div>--%>
            
            <br/>
            <div class="row">
            <div class="col-md-6"> <asp:Label ID="Label3" runat="server" Text="SortBy"></asp:Label></div>
                <div class="col-md-6">
                    <asp:DropDownList ID="DropDownList3" runat="server">
                     <asp:ListItem>rating</asp:ListItem>
                     <asp:ListItem>relevance </asp:ListItem>
                     <asp:ListItem>distance</asp:ListItem>
                     <asp:ListItem>popularity</asp:ListItem>
                 </asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="row">
            <div class="col-md-6"> 
            <asp:Label ID="Label2" runat="server" Text="ZipCode"></asp:Label>
            </div>
            <div class="col-md-6"> <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></div>
            </div>
            <br/>
            <div class="row">
            <div class="col-md-6"><sub>** Please select a Category and enter a US ZipCode!!</sub></div>
            <div class="col-md-6"><asp:Button ID="Button2" runat="server" Text="Search" OnClick="Button2_Click" />
                </div>
            </div>
        </div> 
            <div class="row">
            <div class="col-md-6"> <b><asp:Label ID="Label4" runat="server" Text="Places Around !!"></asp:Label></b></div>
            <div class="col-md-6"><asp:Label ID="Label5" runat="server" Text="[]"></asp:Label></div>
        </div>
   </section>
    <hr />
   
   </div>
         <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Logout" CssClass="logout-btn" />
            <asp:Button ID="Button4" runat="server" Text="Home" CssClass="home-btn" OnClick="Button4_Click" />
        </div>
                        <div class="overlay"></div>

    </form>
</body>
</html>