<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master" AutoEventWireup="true" CodeBehind="EditFDFeedBackMOU.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.EditFDFeedBackMOU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
<div>
        <div class="row minheight">
            <h4>
                <b>Feedback of Compliance MOU</b></h4>
            <div class="col-sm-3">
                <div id="leftPartMy1" class="">
                    <div id="leftNav">
                        <div class="panel-group" id="accordion">
                            <div class=" panel-default ">
                                <div id="panel1" class="panel-collapse collapse in menuleft">
                                    <div class="table-responsive">
                                        <ul>
                                            <li class="FirstSubMenu1  BackgroundColor"><a class="submenu" href="SubmitAPO.aspx">
                                                Non-Recurring</a></li>
                                            <li class="BackgroundColor"><a href="SubmitAPO.aspx">Recurring</a></li>
                                            <li class="BackgroundColor "><a href="FDUtilizationCertificate.aspx">Utilization Certificate</a></li>
                                            <li class="BackgroundColor "><a href="#">Spill Over</a></li>
                                            <li class="BackgroundColor "><a href="#">Revalidation</a></li>
                                            <li class="BackgroundColor "><a href="#">Adjustment</a></li>
                                            <li class="BackgroundColor "><a href="ObligationFD.aspx">Obligations</a></li>
                                            <li class="BackgroundColor Current"><a href="FDFeedbackMOU.aspx">Feedback of Compliance MOU</a></li>
                                            <li class="BackgroundColor "><a href="CheckList.aspx">Checklist</a></li>
                                            <li class="BackgroundColor"><a href="FDFlexiAmont.aspx">Flexi Amount</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-9  leftBorder minHeight Home_margin" id="MainContent">
            
                <b>Score Categories:</b>
                <span class="text-danger">POOR: 0-20</span>
                <span class="text-success col-sm-offset-1">FAIR: 20-40</span>
                <span class="col-sm-offset-1" style="color:Teal">GOOD: 40-60</span>
                <span class="text-warning col-sm-offset-1">VERY GOOD: >60-100</span>
                <br />
                
                 
                <div class=" table-responsive TopMargin ">
                    <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                             <div class="row">
                

                </div>
                <cc:CustomGridView ID="gvFeebbackMOU" runat="server" CssClass="table  col-sm-12"
                                AutoGenerateColumns="False" DataKeyNames="SrNo" AllowPaging="True" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo" runat="server" Text='<%#Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Obligations of the Field Director">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSanctionAmount" runat="server"  Text='<%#Eval("Description") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Compiled / Not Compiled">
                                        <ItemTemplate>
                                            <cc:CustomRadioButtonList ID="rbolst" runat="server">
                                            <asp:ListItem>Compiled</asp:ListItem>
                                            <asp:ListItem>Not Compiled</asp:ListItem>
                                            </cc:CustomRadioButtonList>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="If not, reason there for">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtReason" runat="server" CssClass="less_size"  ></cc:CustomTextBox>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Score">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtScore" runat="server"  CssClass="less_size"></cc:CustomTextBox>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Compliance process Underway">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtCompliance" runat="server" CssClass="less_size"></cc:CustomTextBox>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtRemarks" TextMode="MultiLine" Rows="4" runat="server" CssClass="less_size"></cc:CustomTextBox>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    
                                </Columns>
                            </cc:CustomGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   
                   
                </div>
            </div>
        </div>
    </div>
</asp:Content>
