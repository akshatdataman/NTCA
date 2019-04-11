<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master" AutoEventWireup="true" CodeBehind="ViewQuarterlyReport.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.ViewQuarterlyReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     function printGrid() {
         var gridData = document.getElementById('<%= gvNonRecurring.ClientID %>');
         var windowUrl = 'about:blank';
         //set print document name for gridview
         var uniqueName = new Date();
         var windowName = 'Print_' + uniqueName.getTime();

         var prtWindow = window.open(windowUrl, windowName,
        'left=100,top=100,right=100,bottom=100,width=700,height=500');
         prtWindow.document.write('<html><head><b><title>APO</title></b></head>');
         prtWindow.document.write('<body style="background:none !important">');
         prtWindow.document.write(gridData.outerHTML);
         prtWindow.document.write('</body></html>');
         prtWindow.document.close();
         prtWindow.focus();
         prtWindow.print();
         prtWindow.close();
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
<div class="row minheight">
        <div class="row">
            <h4 id="SubmitApoHeader" runat="server">
                <b>View Current Financial Year Apo</b>
                <cc:CustomImageButton ID="imgbtnWord" runat="server" ImageUrl="~/Images/Word-icon.png"
                     CssClass="col-sm-offset-8" />
                <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/samplePDFIcon.png"
                     CssClass="" />
                <cc:CustomImageButton ID="imgbtnExcel" runat="server" ImageUrl="~/Images/sampleExcelIcon.jpg"
                    CssClass="" />
                    <cc:CustomImageButton ID="imgbtnPrint" Visible="false" runat="server" ImageUrl="~/Images/btn-print.png" 
                    OnClientClick = "return printGrid();"/>
                
                    
            </h4>
        </div>
        <div class="col-sm-12  leftBorder minHeight" id="MainContent" runat="server">
            <!-- Non-Recurring-->
            <div id="Non-RecurringDiv">
                <div class="headingCss">
                    <h3>
                        Non-Recurring</h3>
                </div>
                <div>
                    <cc:CustomGridView ID="gvNonRecurring" runat="server" AutoGenerateColumns="false" 
                                         CssClass="table table-bordered table-responsive tablett ">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                    <asp:Label ID="lblActivityTypeId" runat="server" Visible="false" Text='<%#Eval("ActivityTypeId") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Activity">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="500px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Released Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReleasedAmount" runat="server" Text='<%#Eval("ReleasedAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Quantity Assessment">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="less_size" Text=""></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Location">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLocation" runat="server" CssClass="less_size" Text=""></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Assessment Physical Target">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPhysicalTarget" runat="server" req="1" CssClass="less_size" Text=""
                                                        TextMode="MultiLine" Rows="4" Columns="3"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Assessment Financial Target">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFinancial" runat="server" CssClass="less_size" Text=""></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                </div>
                <div class="table-responsive tableWidthAPO">
                </div>
            </div>
            <div id="RecuringDiv" runat="server">
                <div>
                    <div class="headingCss">
                        <h3>
                            Recurring</h3>
                    </div>
                    <!--Here is Recuring-->
                    <div>
                        <cc:CustomGridView ID="gvRecurring" runat="server" AutoGenerateColumns="false" 
                                        CssClass="table table-bordered table-responsive tablett ">
                                        <Columns>
                                           <%-- <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                    <asp:Label ID="lblActivityTypeId" runat="server" Visible="false" Text='<%#Eval("ActivityTypeId") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Activity">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Released Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReleasedAmount" runat="server" Text='<%#Eval("ReleasedAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Quantity Assessment">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="less_size" Text=""></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Location">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLocation" runat="server" CssClass="less_size" Text=""></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Assessment Physical Target">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPhysicalTarget" runat="server" req="1" CssClass="less_size" Text=""></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Assessment Financial Target">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFinancial" runat="server" CssClass="less_size" Text=""></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                    </div>
                </div>
            </div>
            
        </div>
        <div class="text-center TopMargin">
           <%-- <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                OnClick="btnBack_Click" />--%>
        </div>
    </div>

</asp:Content>
