using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WiseThink.NTCA.BAL
{
    public class CustomOperation
    {

    }
    public class EditTemplateClass : ITemplate
    {
        string _id = string.Empty;
        public EditTemplateClass(string val)
        {
            _id = val;
        }
        public void InstantiateIn(Control container)
        {
            LinkButton lb = new LinkButton();
            lb.ID = _id;
            lb.Text = _id;
            lb.CommandName = _id;
            lb.CausesValidation = false;
            lb.ViewStateMode = ViewStateMode.Enabled;
            if (_id == "Cancel")
                lb.CausesValidation = false;

            container.Controls.Add(lb);
        }

        private void Lb_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gridViewRow = (GridViewRow)lb.NamingContainer;
            GridView gv = (GridView)gridViewRow.NamingContainer;
            int index = gridViewRow.RowIndex;
            string ParaNoTCP = ((TextBox)gv.Rows[index].FindControl("ParaNoTCP")).Text;
            string SubItem = ((TextBox)gv.Rows[index].FindControl("SubItem")).Text;
            string NumberOfItem = ((TextBox)gv.Rows[index].FindControl("NumberOfItems")).Text;
            string UnitPrice = ((TextBox)gv.Rows[index].FindControl("UnitPrice")).Text;
            string Justification = ((TextBox)gv.Rows[index].FindControl("Justification")).Text;
        }

        private void Lb_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                LinkButton lb = (LinkButton)sender;
                GridViewRow gridViewRow = (GridViewRow)lb.NamingContainer;
                GridView gv = (GridView)gridViewRow.NamingContainer;
                int index = gridViewRow.RowIndex;
                string ParaNoTCP = ((TextBox)gv.Rows[index].FindControl("ParaNoTCP")).Text;
                string SubItem = ((TextBox)gv.Rows[index].FindControl("SubItem")).Text;
                string NumberOfItem = ((TextBox)gv.Rows[index].FindControl("NumberOfItems")).Text;
                string UnitPrice = ((TextBox)gv.Rows[index].FindControl("UnitPrice")).Text;
                string Justification = ((TextBox)gv.Rows[index].FindControl("Justification")).Text;
            }

        }
    }
    public class EditTemplateClassForTextBox : Page, ITemplate
    {
        string _id = string.Empty;
        public EditTemplateClassForTextBox(string val)
        {
            _id = val;
        }

        public void InstantiateIn(Control container)
        {
            TextBox tb = new TextBox();
            tb.ID = _id;
            tb.DataBinding += Tb_DataBinding;
            
            //tb.TextChanged += Tb_TextChanged;
            container.Controls.Add(tb);
        }

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            HiddenField hf = new HiddenField();
            hf.ID = _id;
            hf.Value = tb.Text;
        }

        private void Tb_DataBinding(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            GridViewRow row = (GridViewRow)tb.NamingContainer;
            tb.Text = DataBinder.Eval(row.DataItem, _id).ToString();
           
        }
    }
    public class ItemTemplateClassForControls : ITemplate
    {
        string _id = string.Empty;
        public ItemTemplateClassForControls(string val)
        {
            _id = val;
        }
        public void InstantiateIn(Control container)
        {
            LinkButton lb = new LinkButton();
            lb.ID = _id;
            lb.Text = _id;
            lb.CommandName = _id;
            lb.CausesValidation = false;
            container.Controls.Add(lb);
        }
    }
    public class ItemTemplateClass : ITemplate
    {
        string _id = string.Empty;
        public ItemTemplateClass(string value)
        {
            _id = value;
        }
        public void InstantiateIn(Control container)
        {
            Label lc = new Label();
            lc.ID = _id;
            lc.DataBinding += Lc_DataBinding;
            container.Controls.Add(lc);
        }

        private void Lc_DataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            GridViewRow row = (GridViewRow)lbl.NamingContainer;
            lbl.Text = DataBinder.Eval(row.DataItem, _id).ToString();
        }
    }
}
