<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" Inherits="Forms_Correspondence" CodeFile="Correspondence.aspx.cs" %>

<%@ MasterType VirtualPath="Forms.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
    $(document).ready(function () {
    $('#tableCorrespondence').DataTable({
                               "pageLength": 25,
                                    });
    });

    </script>
    <%--<style>
        table tr td{
            border:1px solid black;
        }
    </style>--%>
    <h3>All Relevant Email Correspondences in the Application</h3>
    <br />
    <div runat="server" id="divMsg" class="alert alert-warning" role="alert" visible="false">
        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
        <b>NOTE: </b>
        <asp:Label runat="server" ID="lblMessage"></asp:Label>
    </div>
    <br />
    <table id="tableCorrespondence">
        <thead>
            <tr>
                <th style="background-color:#007d40; color:white" scope="col">#</th>
                <th style="background-color:#007d40; color:white" scope="col">From</th>
                <th style="background-color:#007d40; color:white" scope="col">To</th>
                <th style="background-color:#007d40; color:white" scope="col">Email</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptCorrespondence" runat="server" ItemType="BL.Data.SentEmail">
                <ItemTemplate>
                    <tr >
                        <td ><%# Container.ItemIndex +1 %>   </td>

                        <td><strong><%# GetNameThruEmail(Item.FromEmail) %></strong>
                            <br />
                            <%# Item.FromEmail.Contains("@") ? Item.FromEmail : Item.FromEmail%></td>
                        <td><strong><%# GetNameThruEmail(Item.ToEmail) %></strong>
                            <br />
                            <%# Item.ToEmail.Contains("@") ? Item.ToEmail : Item.ToEmail%></td>
                        <td>
                            <strong>Sent Date: </strong><%# Item.SentDate %>
                            <br />
                            <strong>Subject: </strong><%# Item.Subject%>
                            <br />
                            <asp:TextBox ID="tbBody" Width="100%" Height="100px" runat="server" TextMode="MultiLine" Text='<%#Cryptography.Decrypt(Item.Body)%>'></asp:TextBox>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <br />
</asp:Content>

