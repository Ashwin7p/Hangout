<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="Hangout.Staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Page</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }

        .form-container {
            width: 100%;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        label {
            display: block;
            margin-bottom: 8px;
            color: #333;
        }

        .input-box {
            padding: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
            width: calc(100% - 32px);
        }

        .action-button {
            border: none;
            padding: 10px;
            border-radius: 5px;
            background-color: #3498db;
            color: #fff;
            cursor: pointer;
            font-size: 16px;
            transition: background-color 0.3s ease;
        }

        .action-button:hover {
            background-color: #267bb8;
        }

        .result-label {
            font-size: 20px;
            color: #333;
        }
      body {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background-color: #f5f5f5;
        margin: 0;
        padding: 0;
    }

    .form-container {
        width: 50%;
        margin: 50px auto;
        padding: 20px;
        background-color: #fff;
        border: 1px solid #ddd;
        border-radius: 8px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

    label {
        display: block;
        margin-bottom: 8px;
        color: #333;
    }

    .input-box,
    #username_input,
    #pwd_input {
        padding: 10px;
        border: 1px solid #ccc;
        border-radius: 5px;
        font-size: 16px;
        width: calc(100% - 22px);
        margin-bottom: 16px;
    }

    /* Improved alignment for labels and input boxes */
    .form-container div {
        margin-bottom: 16px;
    }

    .form-container div label,
    .form-container div input {
        display: inline-block;
        /* Adjusted width for better alignment */
        vertical-align: top;
    }

    #make_admin {
        border: none;
        padding: 10px;
        border-radius: 5px;
        background-color: #3498db;
        color: #fff;
        cursor: pointer;
        font-size: 16px;
        transition: background-color 0.3s ease;
        margin-bottom: 16px;
    }

    .result-label {
        font-size: 20px;
        color: #333;
        margin-bottom: 16px;
    }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <div class="form-container">
            <h4>Welcome Staff/Admin!!</h4>
            <div>
                <asp:Button ID="Button2" runat="server" Text="Get Users History" CssClass="action-button" OnClick="Button2_Click" />
            </div>
            <div>
                 <asp:TextBox ID="TextBox1" runat="server" Height="257px" TextMode="MultiLine" Width="464px"></asp:TextBox>
            </div>
            <div>
            </div>
            <div>
            </div>
            <h5>Admin Tool: Grant Admin Access to a Member</h5>
            <div>
                <label for="AdminUsername">Username</label>
                <asp:TextBox ID="AdminUsername" CssClass="input-box" runat="server" Width="349px" />
            </div>
            <asp:Button ID="GrantAdminBtn" OnClick="GrantAdminBtn_Click" runat="server" Text="Grant Admin" CssClass="action-button" />
            <h2>OR</h2>
            <h4>
            <asp:Label ID="Label1" runat="server" Text="Make new Admin!!"></asp:Label>
            </h4>
            <div>
                <asp:Label ID="user_label" runat="server" Text="Username"></asp:Label>&nbsp;
                <asp:TextBox ID="username_input" runat="server" OnTextChanged="username_input_TextChanged"></asp:TextBox>
           </div>
            <div>
                <asp:Label ID="pwd_label" runat="server" Text="Password"></asp:Label>&nbsp;
                <asp:TextBox ID="pwd_input" runat="server"></asp:TextBox>
            </div>
                <asp:Button ID="make_admin" runat="server" Text="Make Admin" OnClick="make_admin_Click" />
            <div>
            <asp:Label ID="AdminStatus" Text="Status" runat="server" CssClass="result-label" />
            </div>
            <h2>
                <asp:Button ID="HomeBtn" Text="Home" runat="server" OnClick="HomeBtn_Click" CssClass="action-button" style="margin-right: 10px;" />
            <asp:Button ID="Button1" runat="server" OnClick="LogoutBtn_Click" Text="Logout" CssClass="action-button" />
            </h2>
        </div>
    </form>
</body>
</html>