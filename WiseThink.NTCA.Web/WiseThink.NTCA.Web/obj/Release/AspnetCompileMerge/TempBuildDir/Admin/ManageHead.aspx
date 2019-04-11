<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeBehind="ManageHead.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.WebForm8" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <div class="row minheight">
        <div class="input-lg Border">
            Manage Heads</div>
        <div>
            <div class="container TopMargin col-sm-9 col-sm-offset-3">
                <div class=" form-group">
                    <label id="lblSelectedHead" class="col-sm-2">
                        Select Head</label>
                    <div class="col-sm-4 ">
                        <cc:CustomDropDownList ID="ddlSelectHead" runat="server" CssClass=" form-control">
                            <asp:ListItem>--Select Head--</asp:ListItem>
                            <asp:ListItem>Major Head</asp:ListItem>
                            <asp:ListItem>Sub-Head</asp:ListItem>
                            <asp:ListItem>Unit</asp:ListItem>
                        </cc:CustomDropDownList>
                    </div>
                </div>
                <div class="  form-group">
                    <div class="col-sm-2 ">
                        <label id="Label1">
                            Head Name</label>
                    </div>
                    <div class="col-sm-4 ">
                        <cc:CustomTextBox ID="describition" runat="server" PlaceHolder="Head Name" CssClass="form-control autoDisable">
                        </cc:CustomTextBox>
                    </div>
                </div>
                <div class=" form-group">
                    <div class="col-sm-2 ">
                        <label id="Label2">
                            Description</label>
                    </div>
                    <div class="col-sm-4 ">
                        <cc:CustomTextBox ID="CustomTextBox1" runat="server" PlaceHolder="Description" TextMode="MultiLine"
                            CssClass="form-control autoDisable">
                        </cc:CustomTextBox>
                    </div>
                
                </div>

                <div class=" form-group col-sm-7 text-center">
                    <cc:CustomButton ID="btnBack" CssClass="btn-primary btn " runat="server" Text="Add New" />  
                </div>
            </div>
            <div class="table-responsive col-sm-offset-2 col-sm-9">
                <cc:CustomGridView ID="gv" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered col-sm-10"
                    AlternatingRowStyle-BackColor="#f3f8fc">
                    <Columns>
                        <asp:BoundField DataField="HeadName" HeaderText="Head Name">
                        <ItemStyle Width="90px" VerticalAlign="Middle" HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description">                        
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <cc:CustomImageButton ID="imgbtnAction" runat="server" ImageUrl="../Images/edit.png"/>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                    Data is not found
                    </EmptyDataTemplate>
                </cc:CustomGridView>
            </div>
        </div>
    </div>
</asp:Content>
