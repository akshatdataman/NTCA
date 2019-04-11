<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="ViewCWWObligations.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.ViewCWWObligations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= cgvObligationCWW.ClientID %>');
            var windowUrl = 'about:blank';


            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
        'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head><b><title>CWW Obligations</title></b></head>');
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
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <div class="row">
                <h4>
                    <b>VIEW CWW OBLIGATIONS</b>
                    <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/samplePDFIcon.png"
                        OnClick="imgbtnPdf_click" CssClass="col-sm-offset-4" />
                    <cc:CustomImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/Images/btn-print.png"
                        OnClientClick="return printGrid();" /></h4>
            </div>
            <div class="col-sm-12 table-responsive TopMargin ">
                <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cc:CustomGridView ID="cgvObligationCWW" runat="server" CssClass="table  col-sm-12"
                            AutoGenerateColumns="False" DataKeyNames="ObligationId" AllowPaging="False" OnPageIndexChanging="cgvObligationCWW_PageIndexChanging">
                            <EmptyDataTemplate>
                                <div class="EmpltyGridView">
                                    &quot; Sorry! There is no data &quot;</div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Obligations of the Field Director">
                                    <ItemTemplate>
                                        <asp:Label ID="lblObligationId" runat="server" Visible="false" Text='<%#Eval("ObligationId") %>'></asp:Label>
                                        <asp:Label ID="lblObligation" runat="server" Text='<%#Eval("Descriptions") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Complied /</br>Not Complied /</br>Not Applicable">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompledOrNotOrNotApplicable" runat="server" Text='<%#Eval("CompledOrNotOrNotApplicable") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Level of Compliance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLevelOfCompliance" runat="server" Text='<%#Eval("LevelOfCompliance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="If not, reason there for">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReasonIfNotCompiled" runat="server" Text='<%#Eval("ReasonIfNotCompiled") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </cc:CustomGridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-sm-10 text-center TopMargin">
                    <cc:CustomButton ID="btnBack" CssClass="btn-primary btn " runat="server" PostBackUrl="Home.aspx"
                        Text="Back" />
                </div>
                <div class="TopMargin">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
