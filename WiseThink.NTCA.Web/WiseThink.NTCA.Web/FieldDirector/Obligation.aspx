<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master" AutoEventWireup="true" CodeBehind="Obligation.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.Obligation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
 <div class="row">
                <div class="col-md-3">
                    <div id="leftPartMy1" class="">
                        <div id="leftNav">
                            <div class="panel-group" id="accordion">
                                <div class="panel panel-default ">
                                    <div id="panel1" class="panel-collapse collapse in menuleft">
                                        <div class="table-responsive">
                                            <ul>
                                                <li class="FirstSubMenu1  BackgroundColor "><a class="submenu" href="ViewAPO.htm">Anti-Poaching</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Strengthening</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Addressing man animal</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Tiger Safari</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Research and field equipment</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Mainstreaming wildlife concerns</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Staff Development</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Project Allowance</a> </li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Staff welfare</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Habitat Improvement</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Co-occurence agenda in buffer</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Rehabilitation package</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Fostering Tourism</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Acquisition of private land</a></li>
                                                <li class="BackgroundColor"><a href="ViewAPO.htm">Deciding inviolate spaces</a></li>
                                                <li class="BackgroundColor "><a href="UtilizationCertificate.htm">Utilization Certificate</a></li>
                                                <li class="BackgroundColor "><a href="#">Spill Over</a></li>
                                                <li class="BackgroundColor "><a href="#">Revalidation</a></li>
                                                <li class="BackgroundColor "><a href="#">Adjustment</a></li>
                                                <li class="BackgroundColor Current"><a href="obligations.htm">Obligations</a></li>
                                                <li class="BackgroundColor "><a href="ViewFeedback.htm">Feedback of Compliance MOU</a></li>
                                                <li class="BackgroundColor"><a href="Checklist.htm">Checklist</a></li>
                                                <li class="BackgroundColor"><a href="FlexiAmount.htm">Flexi Amount</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-9 leftBorder">
                    <div class="table-responsive ">
                    <cc:CustomGridView ID="gvobligation" runat="server" CssClass="table table-bordered" 
                            AutoGenerateColumns="false" onrowcreated="gvobligation_RowCreated" ShowHeader="false">
                    <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                    <asp:Label ID="lblSrNO" runat="server" Text="1"></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField>
                    <ItemTemplate>
                    <asp:Label ID="lblDescripition" runat="server" Text="Description"></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                    <ItemTemplate>
                    <cc:CustomRadioButton ID="rboCompile" runat="server" Text="Compile" /><br />
                    <cc:CustomRadioButton ID="rboNotCompile" runat="server" Text="Not Compile" /><br />
                    <cc:CustomRadioButton ID="rboNotApplicable" runat="server" Text="Not Applicable" />
                    </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField>
                    <ItemTemplate>
                    <cc:CustomTextBox ID="txtCompile" runat="server"></cc:CustomTextBox>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                    <ItemTemplate>
                    <cc:CustomTextBox ID="txtNotCompile" runat="server"></cc:CustomTextBox>
                    </ItemTemplate>
                    </asp:TemplateField>

                    </Columns>
                    </cc:CustomGridView>
                        
                    </div>
                    <span><b>NOTE: On Submit, APO will get submitted to Chief Wildlife Warden.</b></span>
                    <div class="row text-center marginButtonBottom">
                        <div class="col-sm-offset-4 col-sm-2 marginButton">
                            <button type="button" class="btn btn-primary">
                                Save</button>
                        </div>
                        <div class=" col-sm-2 marginButton">
                            <a type="button" href="FinalAPO.htm" class="btn btn-primary">Submit</a>
                        </div>
                        <div class="col-sm-2 marginButton">
                            <button type="button" class="btn btn-primary ">
                                Back</button>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
