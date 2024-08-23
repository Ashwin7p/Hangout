<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Hangout.SignUp"  EnableViewState="true" EnableViewStateMac="false"%>

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
                 Common Signup Page  for Staff(Admin) or Member!!<br/>
                 </li>
                 <li>
                 <b>Please check isStaff to create a Staff(Admin) user.(even though not necessary)</b><br/> 
                 </li>
                 <li>Even though signed up for accessing pages again log in!!</li>
             </ul>
            </div>
            <div>
            <div>
                <label for="TextBox1">Username</label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="TextBox2">Password</label>
                <asp:TextBox ID="TextBox2" TextMode="Password" runat="server" EnableViewState="true"></asp:TextBox>
            </div>
            <div>
                &nbsp;<asp:CheckBox ID="CheckBoxIsStaff" runat="server" Text="Check if user is a staff member" />
            </div>
            </div>
            <div>
                <asp:Image ID="captchaImageBox" runat="server"  Width="100px" Height="30px" />
                <asp:Button ID="RegenerateCaptcha" text="Regenerate" runat="server" Height="29px" Width="133px" OnClick="RegenerateCaptcha_Click"  /><br /><br />
                <asp:TextBox  ID ="captchaUserInput" runat="server" Width="300px"></asp:TextBox><br /><br />
                <asp:Button ID="SignUpButton" runat="server" Text="Sign Up" OnClick="SignUpButton_Click" />
                <asp:Label ID="Label1" runat="server" Text="[]"></asp:Label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Home" />
            </div>
        </div>
    </form>
</body>
</html>