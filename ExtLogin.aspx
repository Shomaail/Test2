<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExtLogin.aspx.cs" Inherits="_ExtLogin" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Faculty Promotion System</title>

    <link rel="icon" href="images/favicon.png">
    <link rel="SHORTCUT ICON" href="images/favicon.png" />
    <!-- Bootstrap Core CSS -->
    <link href="App_Themes/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="App_Themes/3-col-portfolio.css" rel="stylesheet">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>

<body>
    <form id="form2" runat="server">

        <div class="black-stripe">
            <div class="container">
                <a href="http://www.kfupm.edu.sa">kfupm.edu.sa</a>
            </div>
        </div>


        <!-- Page Content -->
        <div class="container">


            <!-- Logo -->
            <div class="page-header">
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <img src="images/slide.png" alt="FPS" class="img-responsive">
                </div>
            </div>

            <hr>
            <div class="row">
                <div class="col-md-4 portfolio-item" align="center"></div>
                <div class="col-md-4 portfolio-item" align="center">
                    <h3>Login Page for External Reviewers</h3>
                    <p class="text-justify">
                        Please login using the username and password provided to you through email
                    </p>
                    <br>
                    <p class="text-justify">
                        <asp:Label ID="lblUserName" runat="server" Text="Username:"></asp:Label>
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="* Username cannot be empty"
                            Font-Size="X-Small" ControlToValidate="txtUserName"></asp:RequiredFieldValidator><br />
                        <asp:Label ID="lblPassword" runat="server" Text="Password:" Height="18px" Width="78px"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Height="22px" Width="155px"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="* Password cannot be empty"
                            Font-Size="X-Small" ControlToValidate="txtPassword"></asp:RequiredFieldValidator><br />
                        <asp:Button ID="Button1" class="btn btn-primary btn-md" runat="server" Text="Login" OnClick="Button1_Click" /><br />
                        <br />
                        <asp:Label ID="lblEx" runat="server" Visible="False" Font-Size="X-Small" ForeColor="Red"></asp:Label>
                    </p>
                </div>
                <div class="col-md-4 portfolio-item" align="center"></div>
            </div>

            <!-- Projects Row -->
            <!-- /.row -->


            <hr>

            <!-- Footer -->
            <footer>
                <div id="divFooter" runat="server" class="row">
                    <div class="col-lg-12" align="center">
                        <p>All Rights Reserved Copyright &copy; 2009 </p>
                        <p>
                            Faculty Promotion System, Office of the Vice President of Research and Innovation
                        <br>
                            King Fahd University of Petroleum &amp; Minerals| Dhahran, 31261 | Saudi Arabia | +966 (3) 860 0000.
                        </p>
                    </div>
                </div>
                <!-- /.row -->
            </footer>
            <!-- /.container -->
        </div>
        <!-- jQuery -->
        <script src="Scripts/jquery.js"></script>

        <!-- Bootstrap Core JavaScript -->
        <script src="Scripts/bootstrap.min.js"></script>
    </form>
</body>

</html>

