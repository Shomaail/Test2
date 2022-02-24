<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
    <style>
        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            background-color: #fefefe;
            margin: auto;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
        }

        /* The Close Button */
        /*.close {
            color: #aaaaaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }*/

        /*.close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }*/

        .auto-style1 {
            color: #FF6600;
        }
    </style>
</head>
<body>

    <%--<h2>Modal Example</h2>--%>

    <!-- Trigger/Open The Modal -->
    <%--<button id="myBtn">Open Modal</button>--%>

    <!-- The Modal -->

    <%--To make the unavailability Message uncomment the following div only --%>
    <%--<div id="myModal" class="modal">
        <div class="modal-content">
            <span>
                <img src="images/WebMaintenance.jpg" /></span>
            <span class="close">×</span>
            <p>
                Due to some maintenance issues the website is temporarily down 
                 
            </p>
            <p>
                Sorry for the inconvenience.
            </p>
            <p>
                Faculty Promotion System Administration.
            </p>
        </div>
        <!-- Modal content -->

    </div>--%>
<%--   <div class="modal-content">

            <span>
                <img src="images/giphy.gif" style="align-self: center; height: 118px; width: 178px;" />
                <img src="images/servermigration.jpg" style="align-self: center; height: 118px; width: 178px;" /></span>
            <span class="close">×</span>
            <p>
                Faculty Promotion website is down since 4:15 pm 19th March 2017 for some Maitenance issue. 
                
            </p>
            <p>
                

            </p>

            We are sorry for the inconvenience caused due to delay. 
            
            <p>
                ---------------------------------------
            </p>
            <p>
                Faculty Promotion System Administration.
            </p>

        </div>--%>
    <script>
        // Get the modal
        var modal = document.getElementById('myModal');

        // Get the button that opens the modal
        var btn = document.getElementById("myBtn");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks the button, open the modal
        window.onload = function () {
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        //span.onclick = function () {
        //    modal.style.display = "none";
        //}

        // When the user clicks anywhere outside of the modal, close it
        //window.onclick = function (event) {
        //    if (event.target == modal) {
        //        modal.style.display = "none";
        //    }
        //}
    </script>
    <form id="form1" runat="server">

        <div class="black-stripe">
            <div class="container">
                <a href="http://www.kfupm.edu.sa">kfupm.edu.sa</a>
            </div>
        </div>
        <!-- Projects Row -->
        <!-- Page Content -->
        <div class="container">


            <!-- Logo -->
            <div class="page-header">
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <img src="images/slide.png" alt="KFUPM Faculty Promotion System " class="responsive">
                     <map name="Map">
                    <area shape="circle" coords="798,157,46" href="#">
                </map>
    </div>
    

            <hr>
    <div class="row">
        <div class="col-md-4 portfolio-item">
            <h3>Promotion Applications</h3>
            <p class="text-left">
                Open the concerned promotion application 
					or start your own promotion application and complete the task assigned to you
            </p>
            <br>

            <div runat="server" id="divLogin" visible="true">
                <asp:Button ID="btnLogin" class="btn btn-primary btn-md" runat="server" Text="KFUPM Employees login" OnClick="btnLogin_Click" />
            </div>
            <div runat="server" id="divAfterLogin" visible="false">
                <asp:Label runat="server" ID="lblLoginMessage" Style="color: #FF0035"></asp:Label>
                <br />
                <asp:Button ID="btnLogout" class="btn btn-primary btn-md" runat="server" Text="Logout" OnClick="btnLogout_Click" />
            </div>



                      <%--  <a class="btn btn-primary btn-md" role="button" href="services.html">KFUPM Employees login</a>--%>
        </div>
        <div class="col-md-4 portfolio-item">
            <h3>External Evaluation</h3>
            <p>Download and view the research Publications of the candidate and fill out the External Evaluation form.</p>
            <br>
            <asp:Button class="btn btn-primary btn-md" ID="btnExtLogin" runat="server" Text="External Reviewer Login" OnClick="btnExtLogin_Click" />
        </div>
        <div class="col-md-4 portfolio-item">
            <h4>Useful Links and Downloads:</h4>
            <ul class="text-left">
                <!--	<li><a href="#">Faculty Promotion System on ADSL network</a></li> -->

                <li style="font-size:larger"><a href="Documents/Guidelines.pdf" target="_blank">Faculty Promotion Guidelines</a></li>
                <li style="font-size:larger"><a href="FlowChartKFUPMFacultyPromotionSystem_Non-Technical.pdf" target="_blank">Faculty Promotion Process</a></li>
                <li style="font-size:larger"><a href="Documents/Applicant%20CV%20Research%20Activities%20Section-Format-R.pdf" target="_blank">Sample C.V.</a></li>
                <li style="font-size:larger"><a href="Help.aspx" target="_blank">Help</a></li>

            </ul>
        </div>
    </div>

    <!-- Projects Row -->
    <!-- /.row -->


    <br />
    <br />
    <br />
    <hr>
    </div>
        <!-- /.container -->

    <!-- /.row -->


    <br />
    <br />
    <br />
    <hr>
    <p></p>
    <!-- Footer -->
    <footer>
        <div class="row">
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
    <!-- jQuery -->
    <script src="Scripts/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="Scripts/bootstrap.min.js"></script>
    </form>
</body>
</html>



