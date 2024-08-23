<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx" Inherits="Hangout.ErrorPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Page</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            color: #333;
            text-align: center;
            padding: 20px;
        }

        #errorContainer {
            max-width: 600px;
            margin: 0 auto;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h2 {
            color: #e44d26; /* A shade of red (you can choose a color of your preference) */
        }

        p {
            font-size: 16px;
        }

        /* Add custom styles for the buttons */
        .Button1 {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #428bca;
            color: #ffffff;
            border: none;
            cursor: pointer;
            margin-right: 10px;
        }

        /* Add more styles as needed */
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="errorContainer">
            <h2>Error Page!!</h2>
            <p><%: Errormsg %>
                <asp:Button ID="Button1" runat="server" Text="Home" CssClass="Button1" OnClick="home_button_clicked" />
            </p>
            <!-- You can add more elements or information here if needed -->
        </div>
    </form>
</body>
</html>