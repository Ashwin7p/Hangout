<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Hangout.Login" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link href="stylesLogin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="form-container">
            <div>
            <ul>
                <li>
                Common Login page for Staff(Admin) or Member!!<br/>
                </li>
                <li>
                <b>Please check isStaff to logging in as a Staff(Admin) user.</b><br/> 
                </li>
                <li>
                Even Though Admin(Staff) has access to member page he can only login as staff and access any pages<br/>
                </li>
            </ul>
           </div>
                            <asp:Label ID="Label1" runat="server" Text="[]"></asp:Label>
            <div>
                <label for="TextBox1">Username</label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="TextBox2">Password</label>
                <asp:TextBox ID="TextBox2" TextMode="Password" runat="server"></asp:TextBox>
            </div>
            <div>
            &nbsp;<asp:CheckBox ID="CheckBoxIsStaff" runat="server" Text="Check if user is a staff member" />
            </div>
            <div>
                <asp:Button ID="Button1" runat="server" Text="Login" CssClass="login-btn" OnClick="Button1_Click" />

            </div>
            <div>
                <asp:Button ID="SignUpButton" runat="server" Text="Sign Up" OnClick="SignUpButton_Click" />
            </div>
            <div>
<asp:Button ID="Button2" runat="server" Text="Home" OnClick="Button2_Click" />
</div>
        </div>
    </form>
</body>
</html>