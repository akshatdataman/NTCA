using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace WiseThink.NTCA.BAL
{
    public class APOBAL : Page
    {
        private int tigerReserveIdG = 0;
        private int activityTypeIdG = 0;
        private int areaIdG = 0;
        private int activityItemIdG = 0;
        private int countG = 0;
        #region Design Pattern
        private static APOBAL instance;
        private static object syncRoot = new Object();
        private APOBAL() { }

        public static APOBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new APOBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
        #region Methods

        public void UpdateDiaryNumber(int apoFileID)
        {
            DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateDiaryNumber, CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter {Name="@APOFileID",Type=DbType.Int32,value=apoFileID }
            });
        }

        public DataSet GetDashboardForFD(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDashboardForFD, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }

        public DataSet GetRatio(int tigerreserveId)
        {
            try
            {
                return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spSelectRatio, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerreserveId},
             });
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetAPOAmount(int tigerreserveId)
        {
            //DataTable dt = new DataTable();
            DataSet ds = DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPOAmount, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
               {
               
                   new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerreserveId}
               
               });
           /* if (ds.Tables[0].Rows.Count > 0)
            {

                //ds = ds.Tables[0].Rows[0];
                DataRow row = ds.Tables[0].NewRow();
                row[0] = "<b>Total</b>";
                for (int i = 1; i < ds.Tables[0].Columns.Count; i++)
                {
                    decimal Total = 0;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        Total += Convert.ToDecimal(ds.Tables[0].Rows[j][i]);
                    }
                    row[i] = "<b>" + Total.ToString("#.000") + "</b>";

                }
                ds.Tables[0].Rows.Add(row);
                ds.AcceptChanges();
            }*/


            return ds;
        }



        public void GenerateGridFooter(object sender, GridView Grid)
        {
            var rowcount = Grid.Rows.Count;
            GridViewRow footerRow = new GridViewRow(rowcount + 1, 0, DataControlRowType.Footer, DataControlRowState.Insert);
            //Adding Year Column
            TableCell FooterCell = new TableCell();
            FooterCell.Text = "Total Recuring Core";
            FooterCell.HorizontalAlign = HorizontalAlign.Center;
            FooterCell.ColumnSpan = 7; // For merging first, second row cells to one
            FooterCell.CssClass = "HeaderStyle";
            footerRow.Cells.Add(FooterCell);

            //Adding Period Column
            FooterCell = new TableCell();
            FooterCell.Text = "Total";
            FooterCell.HorizontalAlign = HorizontalAlign.Center;
            FooterCell.CssClass = "HeaderStyle";
            footerRow.Cells.Add(FooterCell);

            //Adding Audited By Column
            FooterCell = new TableCell();
            FooterCell.Text = "Total of end";
            FooterCell.HorizontalAlign = HorizontalAlign.Center;
            FooterCell.ColumnSpan = 3;
            FooterCell.CssClass = "HeaderStyle";
            footerRow.Cells.Add(FooterCell);



            //Adding the Row at the 0th position (first row) in the Grid
            Grid.Controls[0].Controls.AddAt(rowcount + 1, footerRow);
        }

        public void GenerateGridHeader(object sender, GridView Grid)
        {

            GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Adding Year Column
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Activity";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2; // For merging first, second row cells to one
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Period Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Item";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Para No. CSS PT Guidelines";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Item Type";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Para No. TCP";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Current Financial Year";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 4; // For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "GPS";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Justification";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Upload Document";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Action";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding the Row at the 0th position (first row) in the Grid
            Grid.Controls[0].Controls.AddAt(0, HeaderRow);

            GridViewRow Row = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell newCell = new TableCell();

            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Name of Sub Item";
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);

            newCell = new TableCell();
            //newCell.ColumnSpan = 1;
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "No. of Items (Physical Target)";
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);

            //newCell = new TableCell();
            //newCell.HorizontalAlign = HorizontalAlign.Center;
            //newCell.Text = "Specification / Description";
            //newCell.CssClass = "HeaderStyle";
            //Row.Cells.Add(newCell);


            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Unit Price (Rs.)";
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);

            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Total (Financial Target)";
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);
            Grid.Controls[0].Controls.AddAt(1, Row);




            ///*footer */
            //int rowcount = Grid.Rows.Count;
            //GridViewRow footerRow = new GridViewRow(rowcount + 1, 0, DataControlRowType.Footer, DataControlRowState.Insert);
            ////Adding Year Column
            //TableCell FooterCell = new TableCell();
            //FooterCell.Text = "Total Recuring Core";
            //FooterCell.HorizontalAlign = HorizontalAlign.Center;
            //FooterCell.ColumnSpan = 7; // For merging first, second row cells to one
            //FooterCell.CssClass = "HeaderStyle";
            //footerRow.Cells.Add(FooterCell);

            ////Adding Period Column
            //FooterCell = new TableCell();
            //FooterCell.Text = "Total";
            //FooterCell.HorizontalAlign = HorizontalAlign.Center;
            //FooterCell.CssClass = "HeaderStyle";
            //footerRow.Cells.Add(FooterCell);

            ////Adding Audited By Column
            //FooterCell = new TableCell();
            //FooterCell.Text = "Total of end";
            //FooterCell.HorizontalAlign = HorizontalAlign.Center;
            //FooterCell.ColumnSpan = 3;
            //FooterCell.CssClass = "HeaderStyle";
            //footerRow.Cells.Add(FooterCell);



            ////Adding the Row at the 0th position (first row) in the Grid
            //Grid.Controls[0].Controls.AddAt(rowcount + 1, footerRow);

        }
        public void ViewGenerateGridHeader(object sender, GridView Grid)
        {
            GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Adding Year Column
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Activity";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2; // For merging first, second row cells to one
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Period Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Item";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Para No. CSS PT Guidelines";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.Width = 60;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Sub Item Type";
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderCell.RowSpan = 2;
            //HeaderCell.CssClass = "HeaderStyle";
            //HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Para No. TCP";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.Width = 40;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Amount released PFY";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);


            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Current Financial Year";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 4; // For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderCell.Width = 370;
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "GPS";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderCell.Width = 130;
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Justification";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.Width = 150;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);



            //Adding the Row at the 0th position (first row) in the Grid
            Grid.Controls[0].Controls.AddAt(0, HeaderRow);

            GridViewRow Row = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell newCell = new TableCell();
            newCell.ColumnSpan = 1;
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "No. of Items (Physical Target)";
            newCell.CssClass = "HeaderStyle";
            newCell.Width = 50;
            Row.Cells.Add(newCell);

            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Sub-item Details";
            newCell.CssClass = "HeaderStyle";
            newCell.Width = 200;
            Row.Cells.Add(newCell);


            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Unit Price (Rs.)";
            newCell.CssClass = "HeaderStyle";
            newCell.Width = 60;
            Row.Cells.Add(newCell);

            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Total (Financial Target)";
            newCell.CssClass = "HeaderStyle";
            newCell.Width = 60;
            Row.Cells.Add(newCell);
            Grid.Controls[0].Controls.AddAt(1, Row);
        }

        public void GenerateQuarterlyReportHeader(object sender, GridView Grid)
        {
            GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Adding Year Column
            TableCell HeaderCell = new TableCell();


            HeaderCell = new TableCell();
            //HeaderCell.Text = "Sr.No";
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderCell.RowSpan = 2;
            //HeaderCell.CssClass = "HeaderStyle";
            //HeaderRow.Cells.Add(HeaderCell);



            HeaderCell.Text = "Activity";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 3; // For merging first, second row cells to one
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Period Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Item";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 3;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column


            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Sanctioned By Govt. Target";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 5; // For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Progress Report of Assessment";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 6; // For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);


            //Adding the Row at the 0th position (first row) in the Grid
            Grid.Controls[0].Controls.AddAt(0, HeaderRow);

            GridViewRow Row = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell newCell = new TableCell();
            newCell.ColumnSpan = 1;
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Quantity";
            newCell.RowSpan = 2;
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);

            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Location";
            newCell.RowSpan = 2;
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);


            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Amount Released From Govt.";
            newCell.ColumnSpan = 3;
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);

            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Quantity";
            newCell.RowSpan = 2;
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);

            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Location";
            newCell.RowSpan = 2;
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);

            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Physical Progress";
            newCell.RowSpan = 2;
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);

            newCell = new TableCell();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            newCell.Text = "Financial Progress";
            newCell.ColumnSpan = 3;
            newCell.CssClass = "HeaderStyle";
            Row.Cells.Add(newCell);


            Grid.Controls[0].Controls.AddAt(1, Row);



            GridViewRow Row1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell newCel2 = new TableCell();
            newCel2.ColumnSpan = 1;
            newCel2.HorizontalAlign = HorizontalAlign.Center;
            newCel2.Text = "Central Govt.";
            newCel2.CssClass = "HeaderStyle";
            Row1.Cells.Add(newCel2);

            newCel2 = new TableCell();
            newCel2.HorizontalAlign = HorizontalAlign.Center;
            newCel2.Text = "State Govt.";
            newCel2.CssClass = "HeaderStyle";
            Row1.Cells.Add(newCel2);


            newCel2 = new TableCell();
            newCel2.HorizontalAlign = HorizontalAlign.Center;
            newCel2.Text = "Total";
            newCel2.CssClass = "HeaderStyle";
            Row1.Cells.Add(newCel2);

            newCel2 = new TableCell();
            newCel2.HorizontalAlign = HorizontalAlign.Center;
            newCel2.Text = "Central Govt.";
            newCel2.CssClass = "HeaderStyle";
            Row1.Cells.Add(newCel2);

            newCel2 = new TableCell();
            newCel2.HorizontalAlign = HorizontalAlign.Center;
            newCel2.Text = "State Govt.";
            newCel2.CssClass = "HeaderStyle";
            Row1.Cells.Add(newCel2);


            newCel2 = new TableCell();
            newCel2.HorizontalAlign = HorizontalAlign.Center;
            newCel2.Text = "Total";
            newCel2.CssClass = "HeaderStyle";
            Row1.Cells.Add(newCel2);
            Grid.Controls[0].Controls.AddAt(2, Row1);

            //GridViewRow Row2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell newCel3 = new TableCell();


            //Grid.Controls[0].Controls.AddAt(2, Row2);
        }

        public void GenerateAPOAmountBreakUpGridHeader(object sender, GridView Grid, string TigerReserveID)
        {
            try
            {
                if (Convert.ToInt64(TigerReserveID) > 0)
                {
                    GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                    // Fetching sharing ratio from the tiger reserve master table to display amount accordingly.
                    DataSet dt = new DataSet();
                    dt = APOBAL.Instance.GetRatio(Convert.ToInt16(TigerReserveID));

                    TableCell HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderCell.RowSpan = 2; // For merging first, second row cells to one
                    HeaderCell.CssClass = "HeaderStyle";
                    HeaderRow.Cells.Add(HeaderCell);

                    //Adding Period Column
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Recurring";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.CssClass = "HeaderStyle";
                    HeaderRow.Cells.Add(HeaderCell);

                    //Adding Audited By Column
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Non Recurring";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderCell.ColumnSpan = 2;
                    // HeaderCell.Width = 60;
                    HeaderCell.CssClass = "HeaderStyle";
                    HeaderRow.Cells.Add(HeaderCell);

                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Total <br/> (Rs in Lakhs)";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderCell.RowSpan = 2;
                    HeaderCell.CssClass = "HeaderStyle";
                    HeaderRow.Cells.Add(HeaderCell);

                    //Adding the Row at the 0th position (first row) in the Grid
                    Grid.Controls[0].Controls.AddAt(0, HeaderRow);

                    GridViewRow Row = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell newCell = new TableCell();
                    newCell = new TableCell();
                    newCell.Text = "Central Share ( " + (dt.Tables[0].Rows[0][0]) + "% )<br/> (Rs in Lakhs)";
                    newCell.HorizontalAlign = HorizontalAlign.Center;
                    newCell.CssClass = "HeaderStyle";
                    Row.Cells.Add(newCell);


                    //Adding Revenue Column
                    newCell = new TableCell();
                    newCell.Text = "State Share ( " + (100 - Convert.ToInt16(dt.Tables[0].Rows[0][0])) + "% )<br/> (Rs in Lakhs)";
                    newCell.HorizontalAlign = HorizontalAlign.Center;
                    newCell.CssClass = "HeaderStyle";
                    Row.Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = "Central Share ( " + dt.Tables[0].Rows[0][1] + "% )<br/> (Rs in Lakhs)";
                    newCell.HorizontalAlign = HorizontalAlign.Center;
                    newCell.CssClass = "HeaderStyle";
                    Row.Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = "State Share ( " + (100 - Convert.ToInt16(dt.Tables[0].Rows[0][1])) + "% )<br/> (Rs in Lakhs)";
                    newCell.HorizontalAlign = HorizontalAlign.Center;
                    newCell.CssClass = "HeaderStyle";
                    Row.Cells.Add(newCell);
                    Grid.Controls[0].Controls.AddAt(1, Row);

                }
            }
            catch (Exception ex) { }

        }

        private DataTable dtSubItem()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn(DataBaseFields.ParaNoTCP, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.SubItem, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.NumberOfItems, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.UnitPrice, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.Total, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.GPS, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.Justification, typeof(string)));
            dt.Columns.Add(new DataColumn(DataBaseFields.Document, typeof(string)));
            dt.Columns.Add(new DataColumn("Action", typeof(string)));
            dr = dt.NewRow();
            dr[DataBaseFields.ParaNoTCP] = "13.1";
            dr[DataBaseFields.SubItem] = "Road No-1";
            dr[DataBaseFields.NumberOfItems] = "2";
            dr[DataBaseFields.UnitPrice] = "100000";
            dr[DataBaseFields.Total] = "200000";
            dr[DataBaseFields.GPS] = "90.00, 120.12";
            dr[DataBaseFields.Justification] = "Justification for Road No. 1";
            dr[DataBaseFields.Document] = "Road Map1.png";
            dr["Action"] = string.Empty;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[DataBaseFields.ParaNoTCP] = "13.2";
            dr[DataBaseFields.SubItem] = "Road No-2";
            dr[DataBaseFields.NumberOfItems] = "5";
            dr[DataBaseFields.UnitPrice] = "100000";
            dr[DataBaseFields.Total] = "500000";
            dr[DataBaseFields.GPS] = "80.00, 124.13";
            dr[DataBaseFields.Justification] = "Justification for Road No. 2";
            dr[DataBaseFields.Document] = "Road Map2.png";
            dr["Action"] = string.Empty;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[DataBaseFields.ParaNoTCP] = "7.1";
            dr[DataBaseFields.SubItem] = "A.k - 47";
            dr[DataBaseFields.NumberOfItems] = "2";
            dr[DataBaseFields.UnitPrice] = "1500000";
            dr[DataBaseFields.Total] = "3000000";
            dr[DataBaseFields.GPS] = string.Empty;
            dr[DataBaseFields.Justification] = "Justification for A.k - 47";
            dr[DataBaseFields.Document] = "AK-47.png";
            dr["Action"] = string.Empty;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[DataBaseFields.ParaNoTCP] = "7.2";
            dr[DataBaseFields.SubItem] = "Rifle";
            dr[DataBaseFields.NumberOfItems] = "3";
            dr[DataBaseFields.UnitPrice] = "100000";
            dr[DataBaseFields.Total] = "300000";
            dr[DataBaseFields.GPS] = string.Empty;
            dr[DataBaseFields.Justification] = "Justification for Rifle";
            dr[DataBaseFields.Document] = "Rifle.png";
            dr["Action"] = string.Empty;
            dt.Rows.Add(dr);

            //dr = dt.NewRow();

            //Store the DataTable in ViewState
            //ViewState["CurrentTable"] = dt;

            //gv.DataSource = dt;
            //gv.DataBind();
            return dt;
        }
        public void GenerateBlankDataRow(object sender, GridView Grid, int count)
        {
            GridViewRow row = new GridViewRow(count, 1, DataControlRowType.DataRow, DataControlRowState.Insert);
            TableCell tableCell;
            tableCell = new TableCell();
            tableCell.Text = string.Empty;
            tableCell.HorizontalAlign = HorizontalAlign.Left;
            tableCell.ColumnSpan = 4;
            //tableCell.CssClass = "HeaderStyle";
            HtmlGenericControl span1 = new HtmlGenericControl("span1");
            span1.InnerHtml = tableCell.Text;

            tableCell.Controls.Add(span1);
            row.Cells.AddAt(0, tableCell);

            tableCell = new TableCell();
            tableCell.Text = string.Empty;
            tableCell.HorizontalAlign = HorizontalAlign.Left;
            tableCell.ColumnSpan = 10;
            //tableCell.CssClass = "HeaderStyle";
            HtmlGenericControl span2 = new HtmlGenericControl("span2");
            span2.InnerHtml = tableCell.Text;
            GridView gv = new GridView();
            gv.ID = "gvSubItem";
            gv.Width = tableCell.BorderWidth;
            gv.CssClass = "col-sm-12 table table-bordered table-responsive tablett";
            gv.DataSource = dtSubItem();
            gv.DataBind();
            tableCell.Controls.Add(gv);
            tableCell.Controls.Add(span2);
            row.Cells.AddAt(1, tableCell);

            Grid.Controls[0].Controls.AddAt(count, row);


        }
        //GridViewRow rowG = null;
        GridView gridG = null;
        public void GenerateBlankDataRow(object sender, GridView Grid, int count, int tigerReserveId, int activityTypeId, int areaId, int activityItemId, string from)
        {
            tigerReserveIdG = tigerReserveId;
            areaIdG = areaId;
            activityItemIdG = activityItemId;
            activityTypeIdG = activityTypeId;
            countG = count;
            gridG = Grid;

            GridView gv = new GridView();
            gv.EnableViewState = false;
            Grid.EnableViewState = false;
            gv.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
            gv.RowUpdating += new GridViewUpdateEventHandler(Gv_RowUpdating);
            gv.RowCommand += new GridViewCommandEventHandler(Gv_RowCommand);
            gv.RowCancelingEdit += new GridViewCancelEditEventHandler(Gv_RowCancelingEdit);
            gv.RowDeleting += new GridViewDeleteEventHandler(Gv_RowDeleting);
            gv.RowEditing += new GridViewEditEventHandler(Gv_RowEditing);
            gv.ID = "gvSubItem";
            gv.DataKeyNames = new string[] { "SubItemId" };
            //  gv.Width = tableCell.BorderWidth;
            gv.CssClass = "col-sm-13 table table-bordered table-responsive tablett";
            gv.AutoGenerateColumns = false;

            #region Adding Column in Grid View
            TemplateField tfield = new TemplateField();
            tfield.HeaderText = "Sr. No.";
            tfield.ItemTemplate = new ItemTemplateClass("RowNumber");

            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "Sub-Item Type Id";
            tfield.ItemTemplate = new ItemTemplateClass("SubItemTypeId");
            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "Sub Item Type";
            tfield.ItemTemplate = new ItemTemplateClass("SubItemType");
            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "ParaNoTCP";
            tfield.EditItemTemplate = new EditTemplateClassForTextBox("ParaNoTCP");
            tfield.ItemTemplate = new ItemTemplateClass("ParaNoTCP");
            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "Name Of Sub Item";
            tfield.EditItemTemplate = new EditTemplateClassForTextBox("SubItem");
            tfield.ItemTemplate = new ItemTemplateClass("SubItem");
            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "Number Of Items";
            tfield.EditItemTemplate = new EditTemplateClassForTextBox("NumberOfItems");
            tfield.ItemTemplate = new ItemTemplateClass("NumberOfItems");
            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "Unit Price";
            tfield.EditItemTemplate = new EditTemplateClassForTextBox("UnitPrice");
            tfield.ItemTemplate = new ItemTemplateClass("UnitPrice");
            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "Total";
            tfield.ItemTemplate = new ItemTemplateClass("Total");
            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "GPS";
            tfield.EditItemTemplate = new EditTemplateClassForTextBox("GPS");
            tfield.ItemTemplate = new ItemTemplateClass("GPS");
            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "Justification";
            tfield.EditItemTemplate = new EditTemplateClassForTextBox("Justification");
            tfield.ItemTemplate = new ItemTemplateClass("Justification");
            gv.Columns.Add(tfield);

            tfield = new TemplateField();
            tfield.HeaderText = "Document";
            tfield.ItemTemplate = new ItemTemplateClass("Document");
            gv.Columns.Add(tfield);

            //CommandField cf = new CommandField();
            //cf.ButtonType = ButtonType.Link;
            //cf.HeaderText = "Action";
            //cf.CausesValidation = false;
            //cf.ShowEditButton = true;
            //cf.UpdateText = "Update";
            //gv.Columns.Add(cf);
            //tfield = new TemplateField();
            //tfield.ShowHeader = false;
            //tfield.ItemTemplate = new ItemTemplateClassForControls("Edit");
            ////gv.Columns.Add(tfield);
            //tfield.EditItemTemplate = new EditTemplateClass("Update");
            //gv.Columns.Add(tfield);
            //tfield = new TemplateField();
            //tfield.ItemTemplate = new ItemTemplateClassForControls("Delete");
            //gv.Columns.Add(tfield);
            #endregion


            gv.Columns[4].ItemStyle.Width = 140;
            gv.Columns[8].ItemStyle.Width = 100;
            gv.Columns[9].ItemStyle.Width = 200;

            gv.RowCommand += Gv_RowCommand;
            gv.RowEditing += Gv_RowEditing;
            gv.RowDeleting += Gv_RowDeleting;
            gv.RowUpdating += Gv_RowUpdating;
            gv.RowCancelingEdit += Gv_RowCancelingEdit;
            GridBind(gv, from);
        }



        private void GridBind(GridView gv, string from)
        {
            GridViewRow row = new GridViewRow(countG, 1, DataControlRowType.DataRow, DataControlRowState.Insert);
            TableCell tableCell;
            tableCell = new TableCell();
            tableCell.Text = string.Empty;
            tableCell.HorizontalAlign = HorizontalAlign.Left;
            tableCell.ColumnSpan = 1;

            HtmlGenericControl span1 = new HtmlGenericControl("span1");
            span1.InnerHtml = tableCell.Text;

            tableCell.Controls.Add(span1);
            row.Cells.AddAt(0, tableCell);
            tableCell = new TableCell();
            tableCell.Text = string.Empty;
            tableCell.HorizontalAlign = HorizontalAlign.Left;
            tableCell.ColumnSpan = 13;
            if (from == "AdditionalAPO")
            {
                gv.DataSource = GetSubItemDetailActivityItemWiseFromAdditionalAPO(tigerReserveIdG, activityTypeIdG, areaIdG, activityItemIdG);
            }
            else
            {
                gv.DataSource = GetSubItemsDetailActivityItemWise(tigerReserveIdG, activityTypeIdG, areaIdG, activityItemIdG);
            }
            gv.DataBind();
            gv.Columns[1].Visible = false;
            tableCell.Controls.Add(gv);
            row.Cells.AddAt(1, tableCell);
            gridG.Controls[0].Controls.AddAt(countG, row);
        }
        private void Gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region edit
            if (e.CommandName == "Edit")
            {
                LinkButton lb = e.CommandSource as LinkButton;
                //    GridViewRow gridViewRow = (GridViewRow)lb.NamingContainer;
                //    GridView gv = (GridView)gridViewRow.NamingContainer;
                //    gv.EditIndex = gridViewRow.RowIndex;
                //    gv.DataSource = GetSubItemsDetailActivityItemWise(tigerReserveIdG, activityTypeIdG, areaIdG, activityItemIdG);
                //    gv.DataBind();
                //    gv.Parent.NamingContainer.DataBind();
            }
            #endregion
            #region update
            if (e.CommandName == "Update")
            {
                LinkButton lb = e.CommandSource as LinkButton;
                //GridViewRow gridViewRow = (GridViewRow)lb.NamingContainer;
                //GridView gv = (GridView)gridViewRow.NamingContainer;

                //int index = gridViewRow.RowIndex;
                //string ParaNoTCP = ((TextBox)gv.Rows[index].FindControl("ParaNoTCP")).Text;
                //string SubItem = ((TextBox)gv.Rows[index].FindControl("SubItem")).Text;
                //string NumberOfItem = ((TextBox)gv.Rows[index].FindControl("NumberOfItems")).Text;
                //string UnitPrice = ((TextBox)gv.Rows[index].FindControl("UnitPrice")).Text;
                //string Justification = ((TextBox)gv.Rows[index].FindControl("Justification")).Text;
            }
            #endregion
        }
        private void Gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gv = sender as GridView;
            int index = e.RowIndex;
            string ParaNoTCP = ((TextBox)gv.Rows[index].FindControl("ParaNoTCP")).Text;
            string SubItem = ((TextBox)gv.Rows[index].FindControl("SubItem")).Text;
            string NumberOfItem = ((TextBox)gv.Rows[index].FindControl("NumberOfItems")).Text;
            string UnitPrice = ((TextBox)gv.Rows[index].FindControl("UnitPrice")).Text;
            string Justification = ((TextBox)gv.Rows[index].FindControl("Justification")).Text;

            //string ParaNoTCP = ViewState["ParaNoTCP"].ToString();
            //string SubItem = ViewState["SubItem"].ToString();
            //string NumberOfItem = ViewState["NumberOfItems"].ToString();
            //string UnitPrice = ViewState["UnitPrice"].ToString();
            //string Justification = ViewState["Justification"].ToString();
        }

        private void Gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView gv = sender as GridView;
            gv.EditIndex = -1;
            gv.Parent.NamingContainer.DataBind();
        }

        private void Gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void Gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gv = sender as GridView;
            gv.EditIndex = e.NewEditIndex;
            gv.Parent.NamingContainer.DataBind();

        }


        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lb = e.Row.FindControl("MarkAsCompleteButton") as LinkButton;

                //                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);

            }
        }
        public DataSet GetAPOStateAndTigerReserveId(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStateAndTigerReserveId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet GetLoggedInUserTigerReserveId(string loggedInUser)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetLoggedInUserTigerReserveId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.LoggedInUser,Type=System.Data.DbType.String,value=loggedInUser},
             });
        }
        public DataSet GetStateIdTigerReserveIdAndFinancialYear(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStateIdTigerReserveIdAndFinancialYear, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet GetLoggedInUserStateId(string loggedInUser)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetLoggedInUserStateId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.LoggedInUser,Type=System.Data.DbType.String,value=loggedInUser},
             });
        }
        public DataSet GetDashboardForCWW(int stateId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDashboardForCWW, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.Int32,value=stateId},
             });
        }
        public DataSet GetDashboardForNTCA()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDashboardForNTCA, System.Data.CommandType.StoredProcedure);
        }

        public DataSet GetDashboardForAdmin()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDashboardForAdmin, System.Data.CommandType.StoredProcedure);
        }

        public DataSet GetApoFormat()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPOFormatData, System.Data.CommandType.StoredProcedure);
        }
        public DataSet GetApoDraftData(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPODraftData, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public DataSet GetApoDraftData(int tigerReserveId,int activityTypeId,int areaId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPODraftData_New, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name="@ActivityTypeID",Type=System.Data.DbType.Int32,value=activityTypeId},
                new CommanParameter{Name="@AreaID",Type=System.Data.DbType.Int32,value=areaId},
             });
        }
        public DataSet GetAdditionApoDraftData(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAdditionalAPODraftData, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }

        public DataSet GetDeleteEntryStatus(int tigerReserveId, int ActivityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDeleteEntryStatus, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=ActivityItemId}
             });
        }

        public int SubmitAPO(APO apo)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSubmitAPO, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=apo.TigerReserveId},
             new CommanParameter{Name=DataBaseFields.IsFilled,Type=System.Data.DbType.Boolean,value=apo.IsFilled},
             new CommanParameter{Name=DataBaseFields.AreaId,Type=System.Data.DbType.Int32,value=apo.AreaId},
             new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=apo.ActivityTypeId},
             new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=apo.ActivityId},
             new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=apo.ActivityItemId},
             new CommanParameter{Name=DataBaseFields.ActivitySubItemId,Type=System.Data.DbType.Int32,value=apo.SubItemId},
             new CommanParameter{Name=DataBaseFields.SubItem,Type=System.Data.DbType.String,value=apo.SubItem},
             new CommanParameter{Name=DataBaseFields.ParaNoCSSPTGuidelines,Type=System.Data.DbType.String,value=apo.ParaNoCSSPTGuidelines},
             new CommanParameter{Name=DataBaseFields.ParaNoTCP,Type=System.Data.DbType.String,value=apo.ParaNoTCP},
             new CommanParameter{Name=DataBaseFields.NumberOfItems,Type=System.Data.DbType.Decimal,value=apo.NumberOfItems},
             new CommanParameter{Name=DataBaseFields.Specification,Type=System.Data.DbType.String,value=apo.Specification},
             new CommanParameter{Name=DataBaseFields.UnitPrice,Type=System.Data.DbType.Decimal,value=apo.UnitPrice},
             new CommanParameter{Name=DataBaseFields.Total,Type=System.Data.DbType.Decimal,value=apo.Total},
             new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=apo.GPS},
             new CommanParameter{Name=DataBaseFields.Justification,Type=System.Data.DbType.String,value=apo.Justification},
             new CommanParameter{Name=DataBaseFields.Document,Type=System.Data.DbType.String,value=apo.Document},
            });

        }
        public int AddSubItemInAdditionalAPO(APO apo)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddSubItemInAdditionalAPO, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=apo.TigerReserveId},
             new CommanParameter{Name=DataBaseFields.IsFilled,Type=System.Data.DbType.Boolean,value=apo.IsFilled},
             new CommanParameter{Name=DataBaseFields.AreaId,Type=System.Data.DbType.Int32,value=apo.AreaId},
             new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=apo.ActivityTypeId},
             new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=apo.ActivityId},
             new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=apo.ActivityItemId},
             new CommanParameter{Name=DataBaseFields.ActivitySubItemId,Type=System.Data.DbType.Int32,value=apo.SubItemId},
             new CommanParameter{Name=DataBaseFields.SubItem,Type=System.Data.DbType.String,value=apo.SubItem},
             new CommanParameter{Name=DataBaseFields.ParaNoCSSPTGuidelines,Type=System.Data.DbType.String,value=apo.ParaNoCSSPTGuidelines},
             new CommanParameter{Name=DataBaseFields.ParaNoTCP,Type=System.Data.DbType.String,value=apo.ParaNoTCP},
             new CommanParameter{Name=DataBaseFields.NumberOfItems,Type=System.Data.DbType.Decimal,value=apo.NumberOfItems},
             new CommanParameter{Name=DataBaseFields.Specification,Type=System.Data.DbType.String,value=apo.Specification},
             new CommanParameter{Name=DataBaseFields.UnitPrice,Type=System.Data.DbType.Decimal,value=apo.UnitPrice},
             new CommanParameter{Name=DataBaseFields.Total,Type=System.Data.DbType.Decimal,value=apo.Total},
             new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=apo.GPS},
             new CommanParameter{Name=DataBaseFields.Justification,Type=System.Data.DbType.String,value=apo.Justification},
             new CommanParameter{Name=DataBaseFields.Document,Type=System.Data.DbType.String,value=apo.Document},
            });
        }
        public int AddSubItemInAPO(APO apo)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddSubItemInAPO, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=apo.TigerReserveId},
             new CommanParameter{Name=DataBaseFields.IsFilled,Type=System.Data.DbType.Boolean,value=apo.IsFilled},
             new CommanParameter{Name=DataBaseFields.AreaId,Type=System.Data.DbType.Int32,value=apo.AreaId},
             new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=apo.ActivityTypeId},
             new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=apo.ActivityId},
             new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=apo.ActivityItemId},
             new CommanParameter{Name=DataBaseFields.ActivitySubItemId,Type=System.Data.DbType.Int32,value=apo.SubItemId},
             new CommanParameter{Name=DataBaseFields.SubItem,Type=System.Data.DbType.String,value=apo.SubItem},
             new CommanParameter{Name=DataBaseFields.ParaNoCSSPTGuidelines,Type=System.Data.DbType.String,value=apo.ParaNoCSSPTGuidelines},
             new CommanParameter{Name=DataBaseFields.ParaNoTCP,Type=System.Data.DbType.String,value=apo.ParaNoTCP},
             new CommanParameter{Name=DataBaseFields.NumberOfItems,Type=System.Data.DbType.Decimal,value=apo.NumberOfItems},
             new CommanParameter{Name=DataBaseFields.Specification,Type=System.Data.DbType.String,value=apo.Specification},
             new CommanParameter{Name=DataBaseFields.UnitPrice,Type=System.Data.DbType.Decimal,value=apo.UnitPrice},
             new CommanParameter{Name=DataBaseFields.Total,Type=System.Data.DbType.Decimal,value=apo.Total},
             new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=apo.GPS},
             new CommanParameter{Name=DataBaseFields.Justification,Type=System.Data.DbType.String,value=apo.Justification},
             new CommanParameter{Name=DataBaseFields.Document,Type=System.Data.DbType.String,value=apo.Document},
            });

        }
        public int SubmitAdditionalAPO(APO apo)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSubmitAdditionalAPO, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=apo.TigerReserveId},
             new CommanParameter{Name=DataBaseFields.IsFilled,Type=System.Data.DbType.Boolean,value=apo.IsFilled},
             new CommanParameter{Name=DataBaseFields.AreaId,Type=System.Data.DbType.Int32,value=apo.AreaId},
             new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=apo.ActivityTypeId},
             new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=apo.ActivityId},
             new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=apo.ActivityItemId},
             new CommanParameter{Name=DataBaseFields.ParaNoCSSPTGuidelines,Type=System.Data.DbType.String,value=apo.ParaNoCSSPTGuidelines},
             new CommanParameter{Name=DataBaseFields.ParaNoTCP,Type=System.Data.DbType.String,value=apo.ParaNoTCP},
             new CommanParameter{Name=DataBaseFields.NumberOfItems,Type=System.Data.DbType.Decimal,value=apo.NumberOfItems},
             new CommanParameter{Name=DataBaseFields.Specification,Type=System.Data.DbType.String,value=apo.Specification},
             new CommanParameter{Name=DataBaseFields.UnitPrice,Type=System.Data.DbType.Decimal,value=apo.UnitPrice},
             new CommanParameter{Name=DataBaseFields.Total,Type=System.Data.DbType.Decimal,value=apo.Total},
             new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=apo.GPS},
             new CommanParameter{Name=DataBaseFields.Justification,Type=System.Data.DbType.String,value=apo.Justification},
             new CommanParameter{Name=DataBaseFields.Document,Type=System.Data.DbType.String,value=apo.Document},
            });

        }
        public DataSet GetApoForModification(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPOForEdit, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }

        public DataSet GetApoTotal(int tigerreserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPOTotal, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
              new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerreserveId}
            });
        
        }

        public DataSet GetAdditionalApoTotal(int tigerreserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAdditionalAPOTotal, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
              new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerreserveId}
            });

        }
        public DataSet GetChildRecordDetails(int ActivityItemID, int TigerReserveID)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetChildRecordDetails, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                 new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=ActivityItemID},
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=TigerReserveID},
             });
        }
        public void DeleteChildRecord(string id)
        {
            DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.DeleteChildRecords, CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter {Name=DataBaseFields.Id,Type=DbType.Int32,value=id }
            });
        }
        public void UpdateChildRecordInDb(string id, string paraNo, string subItem, string noOfItems,
            string unitPrice, string justification,string Total)
        {
            DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.UpdateChildRecords, CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.Id,Type=DbType.Int32,value=id},
                new CommanParameter{Name=DataBaseFields.ParaNo,Type=DbType.String,value=paraNo},
                new CommanParameter{Name=DataBaseFields.SubItem,Type=DbType.String,value=subItem},
                new CommanParameter{Name=DataBaseFields.NoOfItem,Type=DbType.String,value=noOfItems},
                new CommanParameter{Name=DataBaseFields.UnitPrice,Type=DbType.String,value=unitPrice},
                new CommanParameter{Name=DataBaseFields.Justification,Type=DbType.String,value=justification},
                new CommanParameter{Name=DataBaseFields.Total,Type=DbType.String,value=Total}
            });
        }
        public DataSet GetAdditionalApoForModification(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAdditionalAPOForEdit, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public DataSet UpdateAPO(string inputJSON)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spUpdateAPO, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.inputJSON,Type=System.Data.DbType.String,value=inputJSON},
             });
        }
        public int ModifyAPO(APO apo)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spModifyAPO, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@ID",Type=System.Data.DbType.Int32,value=apo.APOId},
             new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=apo.TigerReserveId},
             new CommanParameter{Name=DataBaseFields.IsFilled,Type=System.Data.DbType.Boolean,value=apo.IsFilled},
             new CommanParameter{Name=DataBaseFields.AreaId,Type=System.Data.DbType.Int32,value=apo.AreaId},
             new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=apo.ActivityTypeId},
             new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=apo.ActivityId},
             new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=apo.ActivityItemId},
             new CommanParameter{Name=DataBaseFields.ParaNoCSSPTGuidelines,Type=System.Data.DbType.String,value=apo.ParaNoCSSPTGuidelines},
             new CommanParameter{Name=DataBaseFields.ParaNoTCP,Type=System.Data.DbType.String,value=apo.ParaNoTCP},
             new CommanParameter{Name=DataBaseFields.NumberOfItems,Type=System.Data.DbType.Decimal,value=apo.NumberOfItems},
             new CommanParameter{Name=DataBaseFields.Specification,Type=System.Data.DbType.String,value=apo.Specification},
             new CommanParameter{Name=DataBaseFields.UnitPrice,Type=System.Data.DbType.Decimal,value=apo.UnitPrice},
             new CommanParameter{Name=DataBaseFields.Total,Type=System.Data.DbType.Decimal,value=apo.Total},
             new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=apo.GPS},
             new CommanParameter{Name=DataBaseFields.Justification,Type=System.Data.DbType.String,value=apo.Justification},
             new CommanParameter{Name=DataBaseFields.Document,Type=System.Data.DbType.String,value=apo.Document},
             new CommanParameter{Name=DataBaseFields.ModifiedBy,Type=System.Data.DbType.String,value=apo.LoggedInUser},
            });
        }
        public int ModifyAdditionalAPO(APO apo)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spModifyAdditionalAPO, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@AdditionalApoId",Type=System.Data.DbType.Int32,value=apo.APOId},
             new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=apo.TigerReserveId},
             new CommanParameter{Name=DataBaseFields.IsFilled,Type=System.Data.DbType.Boolean,value=apo.IsFilled},
             new CommanParameter{Name=DataBaseFields.AreaId,Type=System.Data.DbType.Int32,value=apo.AreaId},
             new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=apo.ActivityTypeId},
             new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=apo.ActivityId},
             new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=apo.ActivityItemId},
             new CommanParameter{Name=DataBaseFields.ParaNoCSSPTGuidelines,Type=System.Data.DbType.String,value=apo.ParaNoCSSPTGuidelines},
             new CommanParameter{Name=DataBaseFields.ParaNoTCP,Type=System.Data.DbType.String,value=apo.ParaNoTCP},
             new CommanParameter{Name=DataBaseFields.NumberOfItems,Type=System.Data.DbType.Decimal,value=apo.NumberOfItems},
             new CommanParameter{Name=DataBaseFields.Specification,Type=System.Data.DbType.String,value=apo.Specification},
             new CommanParameter{Name=DataBaseFields.UnitPrice,Type=System.Data.DbType.Decimal,value=apo.UnitPrice},
             new CommanParameter{Name=DataBaseFields.Total,Type=System.Data.DbType.Decimal,value=apo.Total},
             new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=apo.GPS},
             new CommanParameter{Name=DataBaseFields.Justification,Type=System.Data.DbType.String,value=apo.Justification},
             new CommanParameter{Name=DataBaseFields.Document,Type=System.Data.DbType.String,value=apo.Document},
             new CommanParameter{Name=DataBaseFields.ModifiedBy,Type=System.Data.DbType.String,value=apo.LoggedInUser},
            });
        }

        public int DeleteApoEntry(int tigerReserveId, int activityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spDeleteApoEntry, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
            });
        }
        public int AddGPS(int tigerReserveId, int activityItemId, int subItemId, string gps, string subItemName)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddGPS, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                new CommanParameter{Name=DataBaseFields.SubItemId,Type=System.Data.DbType.String,value=subItemId},
                new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=gps},
                new CommanParameter{Name="@SubitemName",Type=System.Data.DbType.String,value=subItemName}
            });
        }
        public int AddGPSFD(int tigerReserveId, int activityItemId, int subItemId, string gps, string subitem)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddGPS_FD, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                new CommanParameter{Name=DataBaseFields.SubItemId,Type=System.Data.DbType.String,value=subItemId},
                new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=gps},
                new CommanParameter{Name=DataBaseFields.SubItem,Type=System.Data.DbType.String,value=subitem}
            });
        }
        public int UpdateGPS(int gpsId, int tigerReserveId, int activityItemId, string gps)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateGPS, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.GPSId,Type=System.Data.DbType.String,value=gpsId},
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=gps},
            });
        }
        public int DeleteGPS(int gpsId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spDeleteGPS, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.GPSId,Type=System.Data.DbType.String,value=gpsId},
              //    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
              //  new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
              //new CommanParameter{Name=DataBaseFields.SubItem,Type=System.Data.DbType.String,value=SubitemName}
            });
        }
        public DataSet GetGPS(int tigerReserveId, int activityItemId, string SubitemName, string callFrom)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetGPS, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                    new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                    new CommanParameter{Name="@SubitemName",Type=System.Data.DbType.String,value=SubitemName},
                    new CommanParameter{Name="@CallFrom",Type=System.Data.DbType.String,value=callFrom},
                 });
        }
        public DataSet GetSubItemsDetailsForView(int tigerReserveId, int activityTypeId, int activityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetSubItemsForView, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                    new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.String,value=activityTypeId},
                    new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                 });
        }

        public int DeleteAdditionalApoApoEntry(int tigerReserveId, int activityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spDeleteAdditionalApoEntry, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
            });
        }
        public int AddAdditionalApoGPS(int tigerReserveId, int activityItemId, string gps, string subItemId, string subItemName)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddGPSAdditionalApo, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=gps},
                new CommanParameter{Name="@SubitemId",Type=System.Data.DbType.String,value=subItemId},
                new CommanParameter{Name="@SubitemName",Type=System.Data.DbType.String,value=subItemName}
            });
        }
        public int UpdateAdditionalApoGPS(int gpsId, int tigerReserveId, int activityItemId, string gps)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateAdditionalApoGPS, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.GPSId,Type=System.Data.DbType.String,value=gpsId},
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.String,value=gps},
            });
        }
        public int DeleteAdditionalApoGPS(int gpsId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spDeleteAdditionalApoGPS, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.GPSId,Type=System.Data.DbType.String,value=gpsId},
            });
        }
        public DataSet GetAdditionalApoGPS(int tigerReserveId, int activityItemId, string SubitemName)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAdditionalApoGPS, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                    new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                    new CommanParameter{Name="@SubitemName",Type=System.Data.DbType.String,value=SubitemName}
                 });
        }
        public DataSet GetDurationToFillApo()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetNumberDaysToFillApo, System.Data.CommandType.StoredProcedure);
        }




        #region Apo View
        public DataSet GetLoggedUserRole(string userName)
        {

            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.CheckUserRoles, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name="@UserName",Type=System.Data.DbType.String,value=userName},
             });
        }
        public DataSet GetCurrentYearAPOFileId(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCuurntFinancialYearApoId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                 });
        }
        public DataSet GetPreviousYearAPOFileId(int tigerReserveId, string previousFinancialYear)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetPreviousFinancialYearApoId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                    new CommanParameter{Name="@PreviousFinancialYear",Type=System.Data.DbType.String,value=previousFinancialYear},
                 });
        }
        public DataSet GetApoForView(int tigerReserveId, string previousFinancialYear)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewAPOForFD, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name="@PFYYear",Type=System.Data.DbType.String,value=previousFinancialYear},
             });
        }
        public DataSet GetApoForViewFromHome(int tigerReserveId, string previousFinancialYear)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewAPOForFD, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name="@PFYYear",Type=System.Data.DbType.String,value=previousFinancialYear},
             });
        }
        public DataSet GetPreviousYearApoForView(int tigerReserveId, string previousFinancialYear)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewAPOForPFY, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name="@PFYYear",Type=System.Data.DbType.String,value=previousFinancialYear},
             });
        }
        #endregion
        #region APO Status
        public DataSet GetStatusList()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStatusList, System.Data.CommandType.StoredProcedure);
        }
        public DataSet GetCurrentAPOStatus(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCurrentAPOStatus, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet GetRegionalOfficerDiaryNumber(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spRegionalOfficerDiaryNumber, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        //public int UpdateStatus(int apoFileId,int statusId,bool isApproved, string comments, string roDiaryNumber)
        //{
        //    return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateAPOStatus, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
        //    {
        //        new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.Int32,value=apoFileId},
        //        new CommanParameter{Name="@StatusId",Type=System.Data.DbType.Int32,value=statusId},
        //        new CommanParameter{Name="@IsApproved",Type=System.Data.DbType.Boolean,value=isApproved},
        //        new CommanParameter{Name="@Comments",Type=System.Data.DbType.String,value=comments},
        //        new CommanParameter{Name="@RegionalOfficerDiaryNumner",Type=System.Data.DbType.String,value=roDiaryNumber},
        //    });
        //}
        public int UpdateStatus(int apoFileId, int statusId, bool isApproved, string comments, string roDiaryNumber, string reasonForByPass)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateAPOStatus, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.Int32,value=apoFileId},
                new CommanParameter{Name="@StatusId",Type=System.Data.DbType.Int32,value=statusId},
                new CommanParameter{Name="@IsApproved",Type=System.Data.DbType.Boolean,value=isApproved},
                new CommanParameter{Name="@Comments",Type=System.Data.DbType.String,value=comments},
                new CommanParameter{Name="@RegionalOfficerDiaryNumner",Type=System.Data.DbType.String,value=roDiaryNumber},
                new CommanParameter{Name="@ReasonForByPass",Type=System.Data.DbType.String,value=reasonForByPass},
            });
        }
        #endregion
        #region Apo Approval
        public DataSet GetAPOForApproval(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPOForApproval, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.String,value=apoFileId},
                 });
        }
        public DataSet GetAPOFileId(int tigerReserveId, string CallFrom)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPOFileId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                    new CommanParameter{Name="@CallFrom",Type=System.Data.DbType.String,value=CallFrom}
                 });
        }
        #endregion
        #region Current financial year approved Apo
        public DataSet GetCurrentFinancialYearApprovedApo()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetApprovedAPOTitle, System.Data.CommandType.StoredProcedure);
        }
        #endregion

        #region
        public DataSet GetApprovedAPOTitleForCWW(string UserName)
        {
       
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetApprovedAPOTitleforCWW, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name="@UserName",Type=System.Data.DbType.String,value=UserName},
             });

        }

        #endregion
        
        #region Apo due for submission
        public DataSet GetApoDueForSubmission()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPODueForSubmission, System.Data.CommandType.StoredProcedure);
        }

        public int APOMoveToCWLW(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAllowAPODueForSubmission, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                {
                  new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.Int32,value=apoFileId},
                });
        }
        public int AllowAfterDueDate(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAllowSubmissionAfterDueDate, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                {
                  new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.Int32,value=apoFileId},
                });
        }
        public int AdditionalAPOMoveToCWLW(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAllowAdditionalAPODueForSubmission, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                {
                  new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.Int32,value=apoFileId},
                });
        }
        #endregion

        #region Sub Items
        public DataSet GetSubItems(int tigerReserveId, int activityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetSubItems, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                    new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                 });
        }
        public DataSet GetSubItemsActivityItemWise(int activityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetSubItemListItemWise, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                 });
        }
        public DataSet GetSubItemDetailActivityItemWiseFromAdditionalAPO(int tigerReserveId, int activityTypeId, int areaId, int activityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetSubItemDetailsFromAdditionalAPO, CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter {Name=DataBaseFields.TigerReserveId,Type=DbType.String,value=tigerReserveId },
                new CommanParameter {Name=DataBaseFields.ActivityTypeId,Type=DbType.String,value=activityTypeId },
                new CommanParameter {Name=DataBaseFields.AreaId,Type=DbType.String,value=areaId },
                new CommanParameter {Name=DataBaseFields.ActivityItemId,Type=DbType.String,value=activityItemId }
            });
        }
        public DataSet GetSubItemsDetailActivityItemWise(int tigerReserveId, int activityTypeId, int areaId, int activityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetSubItemDetail, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                    new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.String,value=activityTypeId},
                    new CommanParameter{Name=DataBaseFields.AreaId,Type=System.Data.DbType.String,value=areaId},
                    new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                 });
        }
        public DataSet GetSubItemsDetailActivityItemWise(int tigerReserveId, int activityTypeId, int activityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewSubItemDetail, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.String,value=tigerReserveId},
                    new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.String,value=activityTypeId},
                    new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.String,value=activityItemId},
                 });
        }
        #endregion
        #endregion
    }
}
