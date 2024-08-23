<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreDetails.aspx.cs" Inherits="Hangout.StoreDetails" %>

<!DOCTYPE html>
<head runat="server">
    <title></title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f5f5f5;
            margin: 20px;
        }

        .container {
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
            margin: 0 auto;
            position: relative; /* Position for absolute elements */
            overflow: hidden; /* Hide overflow */
        }

        .details {
            margin-bottom: 10px;
        }

        .customButton {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #428bca;
            color: #ffffff;
            border: none;
            cursor: pointer;
            margin: 10px;
            border-radius: 5px;
        }

        /* Circular button with image */
        .circleButton {
            width: 50px;
            height: 50px;
            background-image: url('/Images/weather.png');
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center;
            border: none;
            border-radius: 50%;
            display: flex;
            justify-content: center;
            align-items: center;
            margin-left: 10px; /* Add space between buttons */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <a> http://webstrar24.fulton.asu.edu/page5/Service1.svc </a>
            <div class="details">
                <b><asp:Label ID="Label6" runat="server" Text="Store Name: "></asp:Label></b>
                <asp:Label ID="Label1" runat="server" Text="Store Name"></asp:Label>                
            </div>
            <div class="details">
                <b><asp:Label ID="Label7" runat="server" Text="Address: "></asp:Label></b>
                <asp:Label ID="Label2" runat="server" Text="Store Address"></asp:Label>
            </div>
            <div>
            <asp:Button ID="Button1" runat="server" Text="Get Reviews" CssClass="customButton" OnClick="Button1_Click" />
            <asp:Button ID="Button3" runat="server" CssClass="customButton" Text="Like" OnClick="Button3_Click" />
            <asp:Label ID="Label5" runat="server" Text=" "></asp:Label>
            <asp:Button ID="Button5" runat="server" CssClass="customButton" Text="Other's who Liked!!" OnClick="Button5_Click" />
            <div>
                <asp:Label ID="Label3" runat="server" Text=" "></asp:Label>
                <asp:Label ID="Label8" runat="server" Text=" "></asp:Label>
            </div>
            

            </div>
            <div>
                <!-- Circular button with weather icon and helper text -->
            <b>Click on the icon to get weather details</b><br/>
            <asp:Button ID="Button2" runat="server" Text="" CssClass="circleButton" ToolTip="Check Today's Weather" OnClick="Button2_Click" />
            <asp:Label ID="Label4" runat="server" Text=" "></asp:Label>
            </div>
            <div>
                 <asp:Button ID="Button4" runat="server" CssClass="customButton" Text="Home" OnClick="Button4_Click" />
            </div>
        </div>
    </form>
</body>
</html>