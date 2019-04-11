using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace  WiseThink
{
    public class MultipleSelectCheckBox : CheckBoxList
    {
        public String SelectedValueList
        {
            get
            {
                var values = new StringBuilder();
                foreach (var item in this.Items.Cast<ListItem>().Where(item => item.Selected))
                {
                    if (values.ToString() == "")
                        values.Append(item.Value);
                    else
                        values.Append("," + item.Value);
                }
                return values.ToString();
            }



        }

        public List<int> SelectedValueListAsList
        {
            get
            {
                var values = new List<int>();
                foreach (var item in this.Items.Cast<ListItem>().Where(item => item.Selected))
                {

                    values.Add(Convert.ToInt32(item.Value));


                }
                return values;
            }
            set
            {

                foreach (var item in this.Items.Cast<ListItem>().Where(it => value.Exists(x => x.ToString() == it.Value.ToString())))
                {
                    item.Selected = true;
                }

            }


        }

        public String SelectedTextListForDB
        {
            get
            {
                var values = new StringBuilder();
                foreach (var item in this.Items.Cast<ListItem>().Where(item => item.Selected))
                {
                    if (values.ToString() == "")
                        values.Append("'" + item.Text + "'");
                    else
                        values.Append("," + "'" + item.Text + "'");
                }
                return values.ToString();
            }

        }


        public int Value { get; set; }

    }
}
