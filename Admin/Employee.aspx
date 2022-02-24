<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_Employee" MaintainScrollPositionOnPostback="true" CodeFile="Employee.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function() {
    bindDataTable(); // bind data table on first page load
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable); // bind data table on every UpdatePanel refresh
});

        function bindDataTable() {
    var topProductsTable = $('#tableEmployee').dataTable(
        {"pageLength": 25,
            buttons: {
                buttons: [
                    { extend: 'excel', text: 'Export to Excel', exportOption: { page: 'current' }, footer: true }
                ]
            }

        });
}
        
           
    </script>
    <script lang="javascript">
        function Changed(textControl) {
            //alert(textControl.value);

            if (textControl.value.includes("@")) {
                textControl.value = textControl.value.padStart(8, "0");
                // document.getElementById('ContentPlaceHolder1_lblEmployeeIDAdd').innerHTML = textControl.value.padStart(8, "0");
            } else {
                textControl.value = '@' + textControl.value.padStart(8, "0");
                //document.getElementById('ContentPlaceHolder1_tbEmployeeIDAdd').innerHTML = '@' + textControl.value.padStart(8, "0");
            }

        }
        //function zeroFill(num, numLength) {
        //    var numberStr = num.toString();

        //    return numberStr.padStart(numLength, "0");
        //}
    </script>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
           
            <div>
                <h1>Employees</h1>
                <hr>
                <button type="button" class="btn btn-outline-primary btn-sm" data-toggle="modal" data-target="#myModal"><i class="fa fa-plus" aria-hidden="true"></i>Add  employee</button>
                <br />
                <div class="col-sm-1">
                    Department:
                </div>
                <div class="col-sm-5">

                    <asp:DropDownList ID="ddlCollege" runat="server" ItemType="BL.Data.Department"
                        DataTextField="Text" class="form-control"
                        DataValueField="Value" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <br />

                <br />

                <%-- <div class="form-group">
            <asp:TextBox runat="server" class="form-control" AutoPostBack="true" ID="tbSearch" OnTextChanged="tbSearch_TextChanged" 
            placeholder="Enter Search Text here"
            ToolTip="Search"></asp:TextBox>
        </div>--%>
                <%--<asp:LinkButton runat="server" ID="lbtnSearch" ToolTip="Search" CssClass="btn btn-primary" OnClick="lbtnSearch_Click" ><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>--%>

                <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <h4 class="modal-title" id="myModalLabel">Add  employee</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <h5>Personal details</h5>
                                    <hr>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">EmployeeID</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="text" required class="form-control" id="tbEmployeeIDAdd" placeholder="EmployeeID"
                                                onchange="javascript: Changed( this );">
                                            <asp:Label runat="server" ID="lblEmployeeIDAdd"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Title</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlTitleNameAdd">
                                                <asp:ListItem Text="Prof. " Value="Prof. "></asp:ListItem>
                                                <asp:ListItem Selected="True" Text="Dr. " Value="Dr. "></asp:ListItem>
                                                <asp:ListItem Text="Mr. " Value="Mr. "></asp:ListItem>
                                                <asp:ListItem Text="Mrs. " Value="Mrs. "></asp:ListItem>
                                                <asp:ListItem Text="Miss. " Value="Miss. "></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<input runat="server" type="text" class="form-control" id="tbTitleAdd" placeholder="Title">--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Name</label>
                                        <div class="col-sm-9">
                                            <input type="text" runat="server" class="form-control" id="tbName_1Add" placeholder="Input First Name" />
                                            <input type="text" runat="server" class="form-control" id="tbName_2Add" placeholder="Input Second Name" />
                                            <input type="text" runat="server" class="form-control" id="tbName_3Add" placeholder="Input Third Name" />
                                            <input type="text" runat="server" class="form-control" id="tbName_4Add" placeholder="Input Last Name" />


                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Academic Rank</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlRankAdd">
                                                <asp:ListItem Text="Assistant Professor" Value="Assistant Professor"></asp:ListItem>
                                                <asp:ListItem Text="Associate Professor" Value="Associate Professor"></asp:ListItem>
                                                <asp:ListItem Text="Professor" Value="Professor"></asp:ListItem>
                                            </asp:DropDownList><br>
                                            <%--<input runat="server" type="text" class="form-control" id="tbRankAdd" placeholder="Rank">--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Specialty</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="text" class="form-control" id="tbSpecialtyAdd" placeholder="Specialty">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Sex</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSexAdd">
                                                <asp:ListItem Text="Male" Value="true" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="false"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Nationality</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="ddlNationalityAdd" runat="server" class="form-control">
                                                <asp:ListItem Selected="true">Select Country</asp:ListItem>
                                                <asp:ListItem>United States</asp:ListItem>
                                                <asp:ListItem>Afghanistan</asp:ListItem>
                                                <asp:ListItem>Albania</asp:ListItem>
                                                <asp:ListItem>Algeria</asp:ListItem>
                                                <asp:ListItem>American Samoa</asp:ListItem>
                                                <asp:ListItem>Andorra</asp:ListItem>
                                                <asp:ListItem>Angola</asp:ListItem>
                                                <asp:ListItem>Anguilla</asp:ListItem>
                                                <asp:ListItem>Antarctica</asp:ListItem>
                                                <asp:ListItem>Antigua And Barbuda</asp:ListItem>
                                                <asp:ListItem>Argentina</asp:ListItem>
                                                <asp:ListItem>Armenia</asp:ListItem>
                                                <asp:ListItem>Aruba</asp:ListItem>
                                                <asp:ListItem>Australia</asp:ListItem>
                                                <asp:ListItem>Austria</asp:ListItem>
                                                <asp:ListItem>Azerbaijan</asp:ListItem>
                                                <asp:ListItem>Bahamas</asp:ListItem>
                                                <asp:ListItem>Bahrain</asp:ListItem>
                                                <asp:ListItem>Bangladesh</asp:ListItem>
                                                <asp:ListItem>Barbados</asp:ListItem>
                                                <asp:ListItem>Belarus</asp:ListItem>
                                                <asp:ListItem>Belgium</asp:ListItem>
                                                <asp:ListItem>Belize</asp:ListItem>
                                                <asp:ListItem>Benin</asp:ListItem>
                                                <asp:ListItem>Bermuda</asp:ListItem>
                                                <asp:ListItem>Bhutan</asp:ListItem>
                                                <asp:ListItem>Bolivia</asp:ListItem>
                                                <asp:ListItem>Bosnia And Herzegowina</asp:ListItem>
                                                <asp:ListItem>Botswana</asp:ListItem>
                                                <asp:ListItem>Bouvet Island</asp:ListItem>
                                                <asp:ListItem>Brazil</asp:ListItem>
                                                <asp:ListItem>British Indian Ocean Territory</asp:ListItem>
                                                <asp:ListItem>Brunei Darussalam</asp:ListItem>
                                                <asp:ListItem>Bulgaria</asp:ListItem>
                                                <asp:ListItem>Burkina Faso</asp:ListItem>
                                                <asp:ListItem>Burundi</asp:ListItem>
                                                <asp:ListItem>Cambodia</asp:ListItem>
                                                <asp:ListItem>Cameroon</asp:ListItem>
                                                <asp:ListItem>Canada</asp:ListItem>
                                                <asp:ListItem>Cape Verde</asp:ListItem>
                                                <asp:ListItem>Cayman Islands</asp:ListItem>
                                                <asp:ListItem>Central African Republic</asp:ListItem>
                                                <asp:ListItem>Chad</asp:ListItem>
                                                <asp:ListItem>Chile</asp:ListItem>
                                                <asp:ListItem>China</asp:ListItem>
                                                <asp:ListItem>Christmas Island</asp:ListItem>
                                                <asp:ListItem>Cocos (Keeling) Islands</asp:ListItem>
                                                <asp:ListItem>Colombia</asp:ListItem>
                                                <asp:ListItem>Comoros</asp:ListItem>
                                                <asp:ListItem>Congo</asp:ListItem>
                                                <asp:ListItem>Cook Islands</asp:ListItem>
                                                <asp:ListItem>Costa Rica</asp:ListItem>
                                                <asp:ListItem>Cote D'Ivoire</asp:ListItem>
                                                <asp:ListItem>Croatia (Local Name: Hrvatska)</asp:ListItem>
                                                <asp:ListItem>Cuba</asp:ListItem>
                                                <asp:ListItem>Cyprus</asp:ListItem>
                                                <asp:ListItem>Czech Republic</asp:ListItem>
                                                <asp:ListItem>Denmark</asp:ListItem>
                                                <asp:ListItem>Djibouti</asp:ListItem>
                                                <asp:ListItem>Dominica</asp:ListItem>
                                                <asp:ListItem>Dominican Republic</asp:ListItem>
                                                <asp:ListItem>East Timor</asp:ListItem>
                                                <asp:ListItem>Ecuador</asp:ListItem>
                                                <asp:ListItem>Egypt</asp:ListItem>
                                                <asp:ListItem>El Salvador</asp:ListItem>
                                                <asp:ListItem>Equatorial Guinea</asp:ListItem>
                                                <asp:ListItem>Eritrea</asp:ListItem>
                                                <asp:ListItem>Estonia</asp:ListItem>
                                                <asp:ListItem>Ethiopia</asp:ListItem>
                                                <asp:ListItem>Falkland Islands (Malvinas)</asp:ListItem>
                                                <asp:ListItem>Faroe Islands</asp:ListItem>
                                                <asp:ListItem>Fiji</asp:ListItem>
                                                <asp:ListItem>Finland</asp:ListItem>
                                                <asp:ListItem>France</asp:ListItem>
                                                <asp:ListItem>French Guiana</asp:ListItem>
                                                <asp:ListItem>French Polynesia</asp:ListItem>
                                                <asp:ListItem>French Southern Territories</asp:ListItem>
                                                <asp:ListItem>Gabon</asp:ListItem>
                                                <asp:ListItem>Gambia</asp:ListItem>
                                                <asp:ListItem>Georgia</asp:ListItem>
                                                <asp:ListItem>Germany</asp:ListItem>
                                                <asp:ListItem>Ghana</asp:ListItem>
                                                <asp:ListItem>Gibraltar</asp:ListItem>
                                                <asp:ListItem>Greece</asp:ListItem>
                                                <asp:ListItem>Greenland</asp:ListItem>
                                                <asp:ListItem>Grenada</asp:ListItem>
                                                <asp:ListItem>Guadeloupe</asp:ListItem>
                                                <asp:ListItem>Guam</asp:ListItem>
                                                <asp:ListItem>Guatemala</asp:ListItem>
                                                <asp:ListItem>Guinea</asp:ListItem>
                                                <asp:ListItem>Guinea-Bissau</asp:ListItem>
                                                <asp:ListItem>Guyana</asp:ListItem>
                                                <asp:ListItem>Haiti</asp:ListItem>
                                                <asp:ListItem>Honduras</asp:ListItem>
                                                <asp:ListItem>Hong Kong</asp:ListItem>
                                                <asp:ListItem>Hungary</asp:ListItem>
                                                <asp:ListItem>Icel And</asp:ListItem>
                                                <asp:ListItem>India</asp:ListItem>
                                                <asp:ListItem>Indonesia</asp:ListItem>
                                                <asp:ListItem>Iran (Islamic Republic Of)</asp:ListItem>
                                                <asp:ListItem>Iraq</asp:ListItem>
                                                <asp:ListItem>Ireland</asp:ListItem>
                                                <asp:ListItem>Israel</asp:ListItem>
                                                <asp:ListItem>Italy</asp:ListItem>
                                                <asp:ListItem>Jamaica</asp:ListItem>
                                                <asp:ListItem>Japan</asp:ListItem>
                                                <asp:ListItem>Jordan</asp:ListItem>
                                                <asp:ListItem>Kazakhstan</asp:ListItem>
                                                <asp:ListItem>Kenya</asp:ListItem>
                                                <asp:ListItem>Kiribati</asp:ListItem>
                                                <asp:ListItem>Korea</asp:ListItem>
                                                <asp:ListItem>Kuwait</asp:ListItem>
                                                <asp:ListItem>Kyrgyzstan</asp:ListItem>
                                                <asp:ListItem>Lao People'S Dem Republic</asp:ListItem>
                                                <asp:ListItem>Latvia</asp:ListItem>
                                                <asp:ListItem>Lebanon</asp:ListItem>
                                                <asp:ListItem>Lesotho</asp:ListItem>
                                                <asp:ListItem>Liberia</asp:ListItem>
                                                <asp:ListItem>Libyan Arab Jamahiriya</asp:ListItem>
                                                <asp:ListItem>Liechtenstein</asp:ListItem>
                                                <asp:ListItem>Lithuania</asp:ListItem>
                                                <asp:ListItem>Luxembourg</asp:ListItem>
                                                <asp:ListItem>Macau</asp:ListItem>
                                                <asp:ListItem>Macedonia</asp:ListItem>
                                                <asp:ListItem>Madagascar</asp:ListItem>
                                                <asp:ListItem>Malawi</asp:ListItem>
                                                <asp:ListItem>Malaysia</asp:ListItem>
                                                <asp:ListItem>Maldives</asp:ListItem>
                                                <asp:ListItem>Mali</asp:ListItem>
                                                <asp:ListItem>Malta</asp:ListItem>
                                                <asp:ListItem>Marshall Islands</asp:ListItem>
                                                <asp:ListItem>Martinique</asp:ListItem>
                                                <asp:ListItem>Mauritania</asp:ListItem>
                                                <asp:ListItem>Mauritius</asp:ListItem>
                                                <asp:ListItem>Mayotte</asp:ListItem>
                                                <asp:ListItem>Mexico</asp:ListItem>
                                                <asp:ListItem>Micronesia, Federated States</asp:ListItem>
                                                <asp:ListItem>Moldova, Republic Of</asp:ListItem>
                                                <asp:ListItem>Monaco</asp:ListItem>
                                                <asp:ListItem>Mongolia</asp:ListItem>
                                                <asp:ListItem>Montserrat</asp:ListItem>
                                                <asp:ListItem>Morocco</asp:ListItem>
                                                <asp:ListItem>Mozambique</asp:ListItem>
                                                <asp:ListItem>Myanmar</asp:ListItem>
                                                <asp:ListItem>Namibia</asp:ListItem>
                                                <asp:ListItem>Nauru</asp:ListItem>
                                                <asp:ListItem>Nepal</asp:ListItem>
                                                <asp:ListItem>Netherlands</asp:ListItem>
                                                <asp:ListItem>Netherlands Ant Illes</asp:ListItem>
                                                <asp:ListItem>New Caledonia</asp:ListItem>
                                                <asp:ListItem>New Zealand</asp:ListItem>
                                                <asp:ListItem>Nicaragua</asp:ListItem>
                                                <asp:ListItem>Niger</asp:ListItem>
                                                <asp:ListItem>Nigeria</asp:ListItem>
                                                <asp:ListItem>Niue</asp:ListItem>
                                                <asp:ListItem>Norfolk Island</asp:ListItem>
                                                <asp:ListItem>Northern Mariana Islands</asp:ListItem>
                                                <asp:ListItem>Norway</asp:ListItem>
                                                <asp:ListItem>Oman</asp:ListItem>
                                                <asp:ListItem>Pakistan</asp:ListItem>
                                                <asp:ListItem>Palau</asp:ListItem>
                                                <asp:ListItem>Panama</asp:ListItem>
                                                <asp:ListItem>Papua New Guinea</asp:ListItem>
                                                <asp:ListItem>Paraguay</asp:ListItem>
                                                <asp:ListItem>Peru</asp:ListItem>
                                                <asp:ListItem>Philippines</asp:ListItem>
                                                <asp:ListItem>Pitcairn</asp:ListItem>
                                                <asp:ListItem>Poland</asp:ListItem>
                                                <asp:ListItem>Portugal</asp:ListItem>
                                                <asp:ListItem>Puerto Rico</asp:ListItem>
                                                <asp:ListItem>Qatar</asp:ListItem>
                                                <asp:ListItem>Reunion</asp:ListItem>
                                                <asp:ListItem>Romania</asp:ListItem>
                                                <asp:ListItem>Russian Federation</asp:ListItem>
                                                <asp:ListItem>Rwanda</asp:ListItem>
                                                <asp:ListItem>Saint K Itts And Nevis</asp:ListItem>
                                                <asp:ListItem>Saint Lucia</asp:ListItem>
                                                <asp:ListItem>Saint Vincent, The Grenadines</asp:ListItem>
                                                <asp:ListItem>Samoa</asp:ListItem>
                                                <asp:ListItem>San Marino</asp:ListItem>
                                                <asp:ListItem>Sao Tome And Principe</asp:ListItem>
                                                <asp:ListItem>Saudi Arabia</asp:ListItem>
                                                <asp:ListItem>Senegal</asp:ListItem>
                                                <asp:ListItem>Seychelles</asp:ListItem>
                                                <asp:ListItem>Sierra Leone</asp:ListItem>
                                                <asp:ListItem>Singapore</asp:ListItem>
                                                <asp:ListItem>Slovakia (Slovak Republic)</asp:ListItem>
                                                <asp:ListItem>Slovenia</asp:ListItem>
                                                <asp:ListItem>Solomon Islands</asp:ListItem>
                                                <asp:ListItem>Somalia</asp:ListItem>
                                                <asp:ListItem>South Africa</asp:ListItem>
                                                <asp:ListItem>South Georgia , S Sandwich Is.</asp:ListItem>
                                                <asp:ListItem>Spain</asp:ListItem>
                                                <asp:ListItem>Sri Lanka</asp:ListItem>
                                                <asp:ListItem>St. Helena</asp:ListItem>
                                                <asp:ListItem>St. Pierre And Miquelon</asp:ListItem>
                                                <asp:ListItem>Sudan</asp:ListItem>
                                                <asp:ListItem>Suriname</asp:ListItem>
                                                <asp:ListItem>Svalbard, Jan Mayen Islands</asp:ListItem>
                                                <asp:ListItem>Sw Aziland</asp:ListItem>
                                                <asp:ListItem>Sweden</asp:ListItem>
                                                <asp:ListItem>Switzerland</asp:ListItem>
                                                <asp:ListItem>Syrian Arab Republic</asp:ListItem>
                                                <asp:ListItem>Taiwan</asp:ListItem>
                                                <asp:ListItem>Tajikistan</asp:ListItem>
                                                <asp:ListItem>Tanzania, United Republic Of</asp:ListItem>
                                                <asp:ListItem>Thailand</asp:ListItem>
                                                <asp:ListItem>Togo</asp:ListItem>
                                                <asp:ListItem>Tokelau</asp:ListItem>
                                                <asp:ListItem>Tonga</asp:ListItem>
                                                <asp:ListItem>Trinidad And Tobago</asp:ListItem>
                                                <asp:ListItem>Tunisia</asp:ListItem>
                                                <asp:ListItem>Turkey</asp:ListItem>
                                                <asp:ListItem>Turkmenistan</asp:ListItem>
                                                <asp:ListItem>Turks And Caicos Islands</asp:ListItem>
                                                <asp:ListItem>Tuvalu</asp:ListItem>
                                                <asp:ListItem>Uganda</asp:ListItem>
                                                <asp:ListItem>Ukraine</asp:ListItem>
                                                <asp:ListItem>United Arab Emirates</asp:ListItem>
                                                <asp:ListItem>United Kingdom</asp:ListItem>
                                                <asp:ListItem>United States</asp:ListItem>
                                                <asp:ListItem>United States Minor Is.</asp:ListItem>
                                                <asp:ListItem>Uruguay</asp:ListItem>
                                                <asp:ListItem>Uzbekistan</asp:ListItem>
                                                <asp:ListItem>Vanuatu</asp:ListItem>
                                                <asp:ListItem>Venezuela</asp:ListItem>
                                                <asp:ListItem>Viet Nam</asp:ListItem>
                                                <asp:ListItem>Virgin Islands (British)</asp:ListItem>
                                                <asp:ListItem>Virgin Islands (U.S.)</asp:ListItem>
                                                <asp:ListItem>Wallis And Futuna Islands</asp:ListItem>
                                                <asp:ListItem>Western Sahara</asp:ListItem>
                                                <asp:ListItem>Yemen</asp:ListItem>
                                                <asp:ListItem>Yugoslavia</asp:ListItem>
                                                <asp:ListItem>Zaire</asp:ListItem>
                                                <asp:ListItem>Zambia</asp:ListItem>
                                                <asp:ListItem>Zimbabwe</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Type of Appointment</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlContractTypeAdd">
                                                <asp:ListItem Text="Full Time" Value="Full Time" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Joint" Value="Joint"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Join Date</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="date" class="form-control" id="tbJoinDateAdd" placeholder="Join Date">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Initial Contract Date with Principal Employer</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="date" class="form-control" id="tbContractDateAdd" placeholder="Initial Contract Date with Principal Employer">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Department</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="text" class="form-control" id="tbMajorAdd" placeholder="Department">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">College</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlDepartmentAdd" DataTextField="Text" DataValueField="Value">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Date of Birth</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="date" class="form-control" id="tbDateOfBirthAdd" placeholder="Date of Birth">
                                        </div>
                                    </div>


                                    <h5>Contact details</h5>
                                    <hr>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Email</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="email" class="form-control" id="tbEmailAdd" placeholder="Email">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Second email</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="text" class="form-control" id="tbSecondEmailAdd" placeholder="Second email">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Mail Code </label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="text" class="form-control" id="tbPOBoxAdd" placeholder="Mail Code">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Pager</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="text" class="form-control" id="tbPagerAdd" placeholder="Pager">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Office Phone</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="text" class="form-control" id="tbPhoneAdd" placeholder="Office  Phone">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-3 control-label">Mobile Phone</label>
                                        <div class="col-sm-9">
                                            <input runat="server" type="text" class="form-control" id="tbMobileAdd" placeholder="Mobile Phone ">
                                        </div>
                                    </div>

                                    <%--  
                    <h5>User details</h5>
                    <hr>
                 <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Username</label>
                      <div class="col-sm-9"><input runat="server" type="text" class="form-control" id="tbUsernameAdd" placeholder="Username"></div>
                    </div>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Password</label>
                      <div class="col-sm-9"><input runat="server" type="text" class="form-control" id="tbPasswordAdd" placeholder="Password"></div>
                    </div>                    
                                    --%>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <asp:LinkButton runat="server" ID="lbtnAddEmployee" CssClass="btn btn-primary" OnClick="lbtnAddEmployee_Click"> Add to List</asp:LinkButton>

                            </div>

                        </div>
                    </div>
                </div>

                <asp:LinkButton ID="lbtnSynch" runat="server" OnClick="lbtnSynch_Click">Synch From ERP</asp:LinkButton>

                <hr>


                <table id="tableEmployee" class="table">
                    <thead>
                        <tr>
                            <th scope="col">Serail #</th>
                            <th scope="col">Personal details</th>
                            <th scope="col">Contact details</th>
                            <th scope="col">User details</th>
                            <th scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody>

                        <asp:Repeater ID="rptEmployee" runat="server" ItemType="BL.Data.Employee" OnItemCommand="rptEmployee_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <th>
                                        <div style="overflow: auto; width: 20px;"><%# Container.ItemIndex + 1 %></div>
                                    </th>
                                    <td>
                                        <div style="overflow: auto; width: 300px;">
                                            <strong>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Item.NameString +" ("+ Item.Status + ")"%>'></asp:Label>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlTitleNameEdit" Visible="false" SelectedValue='<%# string.IsNullOrEmpty(Item.Title)? "Dr. " : Item.Title %>'>
                                                    <asp:ListItem Text="Prof. " Value="Prof. "></asp:ListItem>
                                                    <asp:ListItem Text="Dr. " Value="Dr. "></asp:ListItem>
                                                    <asp:ListItem Text="Mr. " Value="Mr. "></asp:ListItem>
                                                    <asp:ListItem Text="Mrs. " Value="Mrs. "></asp:ListItem>
                                                    <asp:ListItem Text="Miss. " Value="Miss. "></asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<input type="text" runat="server" class="form-control" id="tbTitleEdit" visible="false" placeholder="Input Title" value='<%# Item.Title%>' />--%>
                                                <input type="text" runat="server" class="form-control" id="tbName_1Edit" visible="false" placeholder="Input First Name" value='<%# Item.Name_1%>' />
                                                <input type="text" runat="server" class="form-control" id="tbName_2Edit" visible="false" placeholder="Input Second Name" value='<%# Item.Name_2%>' />
                                                <input type="text" runat="server" class="form-control" id="tbName_3Edit" visible="false" placeholder="Input Third Name" value='<%# Item.Name_3%>' />
                                                <input type="text" runat="server" class="form-control" id="tbName_4Edit" visible="false" placeholder="Input Last Name" value='<%# Item.Name_4%>' />

                                            </strong>
                                            <br />
                                            <strong>Sex:</strong>
                                            <asp:Label ID="lblSex" runat="server" Text='<%# Item.Sex.HasValue? (Item.Sex.Value ? "Male" :"Female"): "" %>'></asp:Label>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSexEdit" Visible="false">
                                                <asp:ListItem Text="Male" Value="true" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="false"></asp:ListItem>
                                            </asp:DropDownList><br />
                                            <strong>Nationality</strong>
                                            <asp:Label ID="lblNationality" runat="server" Text='<%# Item.Nationality%>'></asp:Label>
                                            <asp:DropDownList ID="ddlNationalityEdit" runat="server" class="form-control" Visible="false">
                                                <asp:ListItem Selected="true">Select Country</asp:ListItem>
                                                <asp:ListItem>United States</asp:ListItem>
                                                <asp:ListItem>Afghanistan</asp:ListItem>
                                                <asp:ListItem>Albania</asp:ListItem>
                                                <asp:ListItem>Algeria</asp:ListItem>
                                                <asp:ListItem>American Samoa</asp:ListItem>
                                                <asp:ListItem>Andorra</asp:ListItem>
                                                <asp:ListItem>Angola</asp:ListItem>
                                                <asp:ListItem>Anguilla</asp:ListItem>
                                                <asp:ListItem>Antarctica</asp:ListItem>
                                                <asp:ListItem>Antigua And Barbuda</asp:ListItem>
                                                <asp:ListItem>Argentina</asp:ListItem>
                                                <asp:ListItem>Armenia</asp:ListItem>
                                                <asp:ListItem>Aruba</asp:ListItem>
                                                <asp:ListItem>Australia</asp:ListItem>
                                                <asp:ListItem>Austria</asp:ListItem>
                                                <asp:ListItem>Azerbaijan</asp:ListItem>
                                                <asp:ListItem>Bahamas</asp:ListItem>
                                                <asp:ListItem>Bahrain</asp:ListItem>
                                                <asp:ListItem>Bangladesh</asp:ListItem>
                                                <asp:ListItem>Barbados</asp:ListItem>
                                                <asp:ListItem>Belarus</asp:ListItem>
                                                <asp:ListItem>Belgium</asp:ListItem>
                                                <asp:ListItem>Belize</asp:ListItem>
                                                <asp:ListItem>Benin</asp:ListItem>
                                                <asp:ListItem>Bermuda</asp:ListItem>
                                                <asp:ListItem>Bhutan</asp:ListItem>
                                                <asp:ListItem>Bolivia</asp:ListItem>
                                                <asp:ListItem>Bosnia And Herzegowina</asp:ListItem>
                                                <asp:ListItem>Botswana</asp:ListItem>
                                                <asp:ListItem>Bouvet Island</asp:ListItem>
                                                <asp:ListItem>Brazil</asp:ListItem>
                                                <asp:ListItem>British Indian Ocean Territory</asp:ListItem>
                                                <asp:ListItem>Brunei Darussalam</asp:ListItem>
                                                <asp:ListItem>Bulgaria</asp:ListItem>
                                                <asp:ListItem>Burkina Faso</asp:ListItem>
                                                <asp:ListItem>Burundi</asp:ListItem>
                                                <asp:ListItem>Cambodia</asp:ListItem>
                                                <asp:ListItem>Cameroon</asp:ListItem>
                                                <asp:ListItem>Canada</asp:ListItem>
                                                <asp:ListItem>Cape Verde</asp:ListItem>
                                                <asp:ListItem>Cayman Islands</asp:ListItem>
                                                <asp:ListItem>Central African Republic</asp:ListItem>
                                                <asp:ListItem>Chad</asp:ListItem>
                                                <asp:ListItem>Chile</asp:ListItem>
                                                <asp:ListItem>China</asp:ListItem>
                                                <asp:ListItem>Christmas Island</asp:ListItem>
                                                <asp:ListItem>Cocos (Keeling) Islands</asp:ListItem>
                                                <asp:ListItem>Colombia</asp:ListItem>
                                                <asp:ListItem>Comoros</asp:ListItem>
                                                <asp:ListItem>Congo</asp:ListItem>
                                                <asp:ListItem>Cook Islands</asp:ListItem>
                                                <asp:ListItem>Costa Rica</asp:ListItem>
                                                <asp:ListItem>Cote D'Ivoire</asp:ListItem>
                                                <asp:ListItem>Croatia (Local Name: Hrvatska)</asp:ListItem>
                                                <asp:ListItem>Cuba</asp:ListItem>
                                                <asp:ListItem>Cyprus</asp:ListItem>
                                                <asp:ListItem>Czech Republic</asp:ListItem>
                                                <asp:ListItem>Denmark</asp:ListItem>
                                                <asp:ListItem>Djibouti</asp:ListItem>
                                                <asp:ListItem>Dominica</asp:ListItem>
                                                <asp:ListItem>Dominican Republic</asp:ListItem>
                                                <asp:ListItem>East Timor</asp:ListItem>
                                                <asp:ListItem>Ecuador</asp:ListItem>
                                                <asp:ListItem>Egypt</asp:ListItem>
                                                <asp:ListItem>El Salvador</asp:ListItem>
                                                <asp:ListItem>Equatorial Guinea</asp:ListItem>
                                                <asp:ListItem>Eritrea</asp:ListItem>
                                                <asp:ListItem>Estonia</asp:ListItem>
                                                <asp:ListItem>Ethiopia</asp:ListItem>
                                                <asp:ListItem>Falkland Islands (Malvinas)</asp:ListItem>
                                                <asp:ListItem>Faroe Islands</asp:ListItem>
                                                <asp:ListItem>Fiji</asp:ListItem>
                                                <asp:ListItem>Finland</asp:ListItem>
                                                <asp:ListItem>France</asp:ListItem>
                                                <asp:ListItem>French Guiana</asp:ListItem>
                                                <asp:ListItem>French Polynesia</asp:ListItem>
                                                <asp:ListItem>French Southern Territories</asp:ListItem>
                                                <asp:ListItem>Gabon</asp:ListItem>
                                                <asp:ListItem>Gambia</asp:ListItem>
                                                <asp:ListItem>Georgia</asp:ListItem>
                                                <asp:ListItem>Germany</asp:ListItem>
                                                <asp:ListItem>Ghana</asp:ListItem>
                                                <asp:ListItem>Gibraltar</asp:ListItem>
                                                <asp:ListItem>Greece</asp:ListItem>
                                                <asp:ListItem>Greenland</asp:ListItem>
                                                <asp:ListItem>Grenada</asp:ListItem>
                                                <asp:ListItem>Guadeloupe</asp:ListItem>
                                                <asp:ListItem>Guam</asp:ListItem>
                                                <asp:ListItem>Guatemala</asp:ListItem>
                                                <asp:ListItem>Guinea</asp:ListItem>
                                                <asp:ListItem>Guinea-Bissau</asp:ListItem>
                                                <asp:ListItem>Guyana</asp:ListItem>
                                                <asp:ListItem>Haiti</asp:ListItem>
                                                <asp:ListItem>Honduras</asp:ListItem>
                                                <asp:ListItem>Hong Kong</asp:ListItem>
                                                <asp:ListItem>Hungary</asp:ListItem>
                                                <asp:ListItem>Icel And</asp:ListItem>
                                                <asp:ListItem>India</asp:ListItem>
                                                <asp:ListItem>Indonesia</asp:ListItem>
                                                <asp:ListItem>Iran (Islamic Republic Of)</asp:ListItem>
                                                <asp:ListItem>Iraq</asp:ListItem>
                                                <asp:ListItem>Ireland</asp:ListItem>
                                                <asp:ListItem>Israel</asp:ListItem>
                                                <asp:ListItem>Italy</asp:ListItem>
                                                <asp:ListItem>Jamaica</asp:ListItem>
                                                <asp:ListItem>Japan</asp:ListItem>
                                                <asp:ListItem>Jordan</asp:ListItem>
                                                <asp:ListItem>Kazakhstan</asp:ListItem>
                                                <asp:ListItem>Kenya</asp:ListItem>
                                                <asp:ListItem>Kiribati</asp:ListItem>
                                                <asp:ListItem>Korea</asp:ListItem>
                                                <asp:ListItem>Kuwait</asp:ListItem>
                                                <asp:ListItem>Kyrgyzstan</asp:ListItem>
                                                <asp:ListItem>Lao People'S Dem Republic</asp:ListItem>
                                                <asp:ListItem>Latvia</asp:ListItem>
                                                <asp:ListItem>Lebanon</asp:ListItem>
                                                <asp:ListItem>Lesotho</asp:ListItem>
                                                <asp:ListItem>Liberia</asp:ListItem>
                                                <asp:ListItem>Libyan Arab Jamahiriya</asp:ListItem>
                                                <asp:ListItem>Liechtenstein</asp:ListItem>
                                                <asp:ListItem>Lithuania</asp:ListItem>
                                                <asp:ListItem>Luxembourg</asp:ListItem>
                                                <asp:ListItem>Macau</asp:ListItem>
                                                <asp:ListItem>Macedonia</asp:ListItem>
                                                <asp:ListItem>Madagascar</asp:ListItem>
                                                <asp:ListItem>Malawi</asp:ListItem>
                                                <asp:ListItem>Malaysia</asp:ListItem>
                                                <asp:ListItem>Maldives</asp:ListItem>
                                                <asp:ListItem>Mali</asp:ListItem>
                                                <asp:ListItem>Malta</asp:ListItem>
                                                <asp:ListItem>Marshall Islands</asp:ListItem>
                                                <asp:ListItem>Martinique</asp:ListItem>
                                                <asp:ListItem>Mauritania</asp:ListItem>
                                                <asp:ListItem>Mauritius</asp:ListItem>
                                                <asp:ListItem>Mayotte</asp:ListItem>
                                                <asp:ListItem>Mexico</asp:ListItem>
                                                <asp:ListItem>Micronesia, Federated States</asp:ListItem>
                                                <asp:ListItem>Moldova, Republic Of</asp:ListItem>
                                                <asp:ListItem>Monaco</asp:ListItem>
                                                <asp:ListItem>Mongolia</asp:ListItem>
                                                <asp:ListItem>Montserrat</asp:ListItem>
                                                <asp:ListItem>Morocco</asp:ListItem>
                                                <asp:ListItem>Mozambique</asp:ListItem>
                                                <asp:ListItem>Myanmar</asp:ListItem>
                                                <asp:ListItem>Namibia</asp:ListItem>
                                                <asp:ListItem>Nauru</asp:ListItem>
                                                <asp:ListItem>Nepal</asp:ListItem>
                                                <asp:ListItem>Netherlands</asp:ListItem>
                                                <asp:ListItem>Netherlands Ant Illes</asp:ListItem>
                                                <asp:ListItem>New Caledonia</asp:ListItem>
                                                <asp:ListItem>New Zealand</asp:ListItem>
                                                <asp:ListItem>Nicaragua</asp:ListItem>
                                                <asp:ListItem>Niger</asp:ListItem>
                                                <asp:ListItem>Nigeria</asp:ListItem>
                                                <asp:ListItem>Niue</asp:ListItem>
                                                <asp:ListItem>Norfolk Island</asp:ListItem>
                                                <asp:ListItem>Northern Mariana Islands</asp:ListItem>
                                                <asp:ListItem>Norway</asp:ListItem>
                                                <asp:ListItem>Oman</asp:ListItem>
                                                <asp:ListItem>Pakistan</asp:ListItem>
                                                <asp:ListItem>Palau</asp:ListItem>
                                                <asp:ListItem>Panama</asp:ListItem>
                                                <asp:ListItem>Papua New Guinea</asp:ListItem>
                                                <asp:ListItem>Paraguay</asp:ListItem>
                                                <asp:ListItem>Peru</asp:ListItem>
                                                <asp:ListItem>Philippines</asp:ListItem>
                                                <asp:ListItem>Pitcairn</asp:ListItem>
                                                <asp:ListItem>Poland</asp:ListItem>
                                                <asp:ListItem>Portugal</asp:ListItem>
                                                <asp:ListItem>Puerto Rico</asp:ListItem>
                                                <asp:ListItem>Qatar</asp:ListItem>
                                                <asp:ListItem>Reunion</asp:ListItem>
                                                <asp:ListItem>Romania</asp:ListItem>
                                                <asp:ListItem>Russian Federation</asp:ListItem>
                                                <asp:ListItem>Rwanda</asp:ListItem>
                                                <asp:ListItem>Saint K Itts And Nevis</asp:ListItem>
                                                <asp:ListItem>Saint Lucia</asp:ListItem>
                                                <asp:ListItem>Saint Vincent, The Grenadines</asp:ListItem>
                                                <asp:ListItem>Samoa</asp:ListItem>
                                                <asp:ListItem>San Marino</asp:ListItem>
                                                <asp:ListItem>Sao Tome And Principe</asp:ListItem>
                                                <asp:ListItem>Saudi Arabia</asp:ListItem>
                                                <asp:ListItem>Senegal</asp:ListItem>
                                                <asp:ListItem>Seychelles</asp:ListItem>
                                                <asp:ListItem>Sierra Leone</asp:ListItem>
                                                <asp:ListItem>Singapore</asp:ListItem>
                                                <asp:ListItem>Slovakia (Slovak Republic)</asp:ListItem>
                                                <asp:ListItem>Slovenia</asp:ListItem>
                                                <asp:ListItem>Solomon Islands</asp:ListItem>
                                                <asp:ListItem>Somalia</asp:ListItem>
                                                <asp:ListItem>South Africa</asp:ListItem>
                                                <asp:ListItem>South Georgia , S Sandwich Is.</asp:ListItem>
                                                <asp:ListItem>Spain</asp:ListItem>
                                                <asp:ListItem>Sri Lanka</asp:ListItem>
                                                <asp:ListItem>St. Helena</asp:ListItem>
                                                <asp:ListItem>St. Pierre And Miquelon</asp:ListItem>
                                                <asp:ListItem>Sudan</asp:ListItem>
                                                <asp:ListItem>Suriname</asp:ListItem>
                                                <asp:ListItem>Svalbard, Jan Mayen Islands</asp:ListItem>
                                                <asp:ListItem>Sw Aziland</asp:ListItem>
                                                <asp:ListItem>Sweden</asp:ListItem>
                                                <asp:ListItem>Switzerland</asp:ListItem>
                                                <asp:ListItem>Syrian Arab Republic</asp:ListItem>
                                                <asp:ListItem>Taiwan</asp:ListItem>
                                                <asp:ListItem>Tajikistan</asp:ListItem>
                                                <asp:ListItem>Tanzania, United Republic Of</asp:ListItem>
                                                <asp:ListItem>Thailand</asp:ListItem>
                                                <asp:ListItem>Togo</asp:ListItem>
                                                <asp:ListItem>Tokelau</asp:ListItem>
                                                <asp:ListItem>Tonga</asp:ListItem>
                                                <asp:ListItem>Trinidad And Tobago</asp:ListItem>
                                                <asp:ListItem>Tunisia</asp:ListItem>
                                                <asp:ListItem>Turkey</asp:ListItem>
                                                <asp:ListItem>Turkmenistan</asp:ListItem>
                                                <asp:ListItem>Turks And Caicos Islands</asp:ListItem>
                                                <asp:ListItem>Tuvalu</asp:ListItem>
                                                <asp:ListItem>Uganda</asp:ListItem>
                                                <asp:ListItem>Ukraine</asp:ListItem>
                                                <asp:ListItem>United Arab Emirates</asp:ListItem>
                                                <asp:ListItem>United Kingdom</asp:ListItem>
                                                <asp:ListItem>United States</asp:ListItem>
                                                <asp:ListItem>United States Minor Is.</asp:ListItem>
                                                <asp:ListItem>Uruguay</asp:ListItem>
                                                <asp:ListItem>Uzbekistan</asp:ListItem>
                                                <asp:ListItem>Vanuatu</asp:ListItem>
                                                <asp:ListItem>Venezuela</asp:ListItem>
                                                <asp:ListItem>Viet Nam</asp:ListItem>
                                                <asp:ListItem>Virgin Islands (British)</asp:ListItem>
                                                <asp:ListItem>Virgin Islands (U.S.)</asp:ListItem>
                                                <asp:ListItem>Wallis And Futuna Islands</asp:ListItem>
                                                <asp:ListItem>Western Sahara</asp:ListItem>
                                                <asp:ListItem>Yemen</asp:ListItem>
                                                <asp:ListItem>Yugoslavia</asp:ListItem>
                                                <asp:ListItem>Zaire</asp:ListItem>
                                                <asp:ListItem>Zambia</asp:ListItem>
                                                <asp:ListItem>Zimbabwe</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <strong>Academic Rank:</strong>
                                            <asp:Label ID="lblRank" runat="server" Text='<%# Item.Rank %>'></asp:Label>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlRankEdit"
                                                Visible="false">
                                                <asp:ListItem Text="Assistant Professor" Value="Assistant Professor"></asp:ListItem>
                                                <asp:ListItem Text="Associate Professor" Value="Associate Professor"></asp:ListItem>
                                                <asp:ListItem Text="Visiting Associate Professor" Value="Visiting Associate Professor"></asp:ListItem>
                                                <asp:ListItem Text="Professor" Value="Professor"></asp:ListItem>
                                                <asp:ListItem Text="Visiting Professor" Value="Visiting Professor"></asp:ListItem>
                                                <asp:ListItem Text="Adjunct Professor" Value="Adjunct Professor"></asp:ListItem>
                                                <asp:ListItem Text="Joint Professor" Value="Joint Professor"></asp:ListItem>
                                                <asp:ListItem Text="Chair Professor" Value="Chair Professor"></asp:ListItem>
                                                <asp:ListItem Text="Lecturer" Value="Lecturer"></asp:ListItem>
                                                <asp:ListItem Text="Researcher Administrative Development" Value="Researcher Administrative Development"></asp:ListItem>
                                                <asp:ListItem Text="Researcher" Value="Researcher"></asp:ListItem>
                                                <asp:ListItem Text="Research Scientist III" Value="Research Scientist III"></asp:ListItem>
                                                <asp:ListItem Text="Associate Director, Professional and Continuing Education in CPG" Value="Associate Director, Professional and Continuing Education in CPG"></asp:ListItem>
                                                <asp:ListItem Text="Instructor" Value="Instructor"></asp:ListItem>
                                                <asp:ListItem Text="Scientific Council Coordinator" Value="Scientific Council Coordinator"></asp:ListItem>
                                                <asp:ListItem Text="Graduate Assistant" Value="Graduate Assistant"></asp:ListItem>
                                                <asp:ListItem Text="Instructor" Value="Instructor"></asp:ListItem>
                                                <asp:ListItem Text="Select Rank" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <strong>Specialty:</strong>
                                            <asp:Label ID="lblSpecialty" runat="server" Text='<%# Item.Specialty%>'></asp:Label>
                                            <input type="text" runat="server" class="form-control" id="tbSpecialtyEdit" visible="false" placeholder="Input Specialty" value='<%# Item.Specialty%>' /><br>
                                            <strong>Major:</strong>
                                            <asp:Label ID="lblMajor" runat="server" Text='<%# Item.Major%>'></asp:Label>
                                            <input type="text" runat="server" class="form-control" id="tbMajorEdit" visible="false" placeholder="Input Department" value='<%# Item.Major%>' /><br>
                                            <strong>Department:</strong>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%# Item.Department%>'></asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlDepartmentEdit" type="text" Visible="false"
                                                SelectedValue='<%# Item.DepartmentID == null ? "0" : Item.DepartmentID.ToString() %>' AppendDataBoundItems="True"
                                                class="form-control" DataTextField="Text" DataValueField="Value" OnDataBinding="ddlDepartmentEdit_DataBinding">
                                            </asp:DropDownList>
                                            <br>
                                            <strong>Initial Contract Date with Principal Employer:</strong>
                                            <asp:Label ID="lblContractDate" runat="server" Text='<%# Item.ContractDate.HasValue ? Item.ContractDate.Value.ToString("MMMM dd, yyyy"): ""%>'></asp:Label>
                                            <input type="date" runat="server" class="form-control" id="tbContractDateEdit" visible="false" placeholder="Input Initial Contract Date with Principal Employer" value='<%# Item.ContractDate.HasValue ? Item.ContractDate.Value.ToString("yyyy-MM-dd"): "" %>' /><br>
                                            <strong>Date of Promotion or Appointment to Current Academic Rank:</strong>
                                            <asp:Label ID="lblJoinDate" runat="server" Text='<%# Item.JoinDate.HasValue ? Item.JoinDate.Value.ToString("MMMM dd, yyyy"): ""%>'></asp:Label>
                                            <input type="date" runat="server" class="form-control" id="tbJoinDateEdit" visible="false" placeholder="Input Date of Promotion or Appointment to Current Academic Rank" value='<%# Item.JoinDate.HasValue ? Item.JoinDate.Value.ToString("yyyy-MM-dd"): "" %>' /><br>
                                            <strong>Type of Appointment:</strong>
                                            <asp:Label ID="lblContractTtype" runat="server" Text='<%# Item.ContractType %>'></asp:Label>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlContractTypeEdit" Visible="false">
                                                <asp:ListItem Text="Full Time" Value="Full Time" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Joint Appointment" Value="Joint Appointment"></asp:ListItem>
                                            </asp:DropDownList><br>
                                        </div>
                                        <strong>Date of Birth:</strong>
                                        <asp:Label ID="lblDateOfBirth" runat="server" Text='<%# Item.DateOfBirth.HasValue ? Item.DateOfBirth.Value.ToString("MMMM dd, yyyy"): ""%>'></asp:Label>
                                        <input type="date" runat="server" class="form-control" id="tbDateOfBirthEdit" visible="false" placeholder="Input Date of Birth" value='<%# Item.DateOfBirth.HasValue ? Item.DateOfBirth.Value.ToString("yyyy-MM-dd"): "" %>' /><br>
                                    </td>
                                    <td>
                                        <div style="overflow: auto; width: 300px;">
                                            <strong>Email: </strong>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Item.Email%>'></asp:Label>
                                            </a><input type="text" runat="server" class="form-control" id="tbEmailEdit" visible="false" placeholder="Input Email" value='<%# Item.Email%>' /><br>
                                            <strong>Second Email: </strong><a href='<%# "mailto:"+Item.SecondEmail%>'>
                                                <asp:Label ID="lblSecondEmail" runat="server" Text='<%# Item.SecondEmail%>'></asp:Label>
                                            </a>
                                            <input type="text" runat="server" class="form-control" id="tbSecondEmailEdit" visible="false" placeholder="Input Second Email" value='<%# Item.SecondEmail%>' /><br>

                                            <strong>Mail Code: </strong>
                                            <asp:Label ID="lblPOBox" runat="server" Text='<%# Item.POBox%>'></asp:Label>
                                            <input type="text" runat="server" class="form-control" id="tbPOBoxEdit" visible="false" placeholder="Input Mail Code " value='<%# Item.POBox%>' />
                                            <br>
                                            <strong>Pager:</strong>
                                            <asp:Label ID="lblPager" runat="server" Text='<%# Item.Pager%>'></asp:Label>
                                            <input type="text" runat="server" class="form-control" id="tbPagerEdit" visible="false" placeholder="Input Pager" value='<%# Item.Pager%>' /><br>
                                            <strong>Office Phone:</strong>
                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Item.Phone%>'></asp:Label>
                                            <input type="text" runat="server" class="form-control" id="tbPhoneEdit" visible="false" placeholder="Input Office Phone" value='<%# Item.Phone%>' /><br>
                                            <strong>Mobile Phone:</strong>
                                            <asp:Label ID="lblMobile" runat="server" Text='<%# Item.Mobile%>'></asp:Label>
                                            <input type="text" runat="server" class="form-control" id="tbMobileEdit" visible="false" placeholder="Input Mobile Phone" value='<%# Item.Mobile%>' /><br>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="overflow: auto; width: 200px;">
                                            <strong>Username: </strong><%# Item.Email%><br>
                                            <strong>Employee Number: </strong><%# Item.EmployeeID%>
                                            <br>
                                        </div>
                                    </td>
                                    <td>
                                        <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument="<%# Item.EmployeeID%>" CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument="<%# Item.EmployeeID%>" CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                                        </div>
                                        <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                            <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server" CommandArgument="<%# Item.EmployeeID %>" CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" ToolTip="Cancel" runat="server" CommandArgument="<%# Item.EmployeeID %>" CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tbody>
                </table>

            </div>

            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
            <asp:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
                TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
                BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
                RepositionMode="RepositionOnWindowScroll">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="background: #009966; padding: 10px">
                <fieldset style="margin: 5px; border: 2px solid #009966; height: 90%;">
                    <legend style="font-style: italic; font-weight: bold; background-color: #009966; color: white">Message </legend>
                    <br />
                    <div style="justify-content: center">
                        <asp:Label ForeColor="White" ID="labelProgrammaticPopup0" Font-Size="Large" runat="server"></asp:Label>
                    </div>
                    <div align="center">
                        <br />
                        <asp:LinkButton ID="hideModalPopupViaServer0" runat="server" OnClick="hideModalPopupViaServer0_Click" class="btn btn-default"
                            ToolTip="Press this button to send the email">OK <span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
                    </div>
                </fieldset>
            </asp:Panel>
         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
