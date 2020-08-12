using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{
    public partial class MainForm : Form
    {
        public void Evaluate(string expression)
        {
            if(expression != null && expression.Length >= 0)
            {
                NCalc.Expression exp = new NCalc.Expression(expression);
                object eval = null;
                try
                {
                    eval = exp.Evaluate();
                }
                catch(ArgumentException)
                {
                    this.AddList("Error in the expression.");
                    return;
                }
                catch(EvaluateException e)
                {
                    this.AddList("Error while evaluating expression, Error Message" + e.Message);
                    return;
                }
                
                if(exp.HasErrors())
                {
                    this.AddList("Error while evaluating expression, Error Message" + exp.Error);
                }
                else
                {
                    this.AddList(expression, eval);
                }
            }
        }
        public void AddList(string expression, object result)
        {
            this.lstLog.Items.Add(this.GetLogListItem(expression, result.ToString(), null, Color.Green));
        }
        public void AddList(string error)
        {
            this.lstLog.Items.Add(this.GetLogListItem(error, "<ERROR>", null, Color.Red));
        }
        public ListViewItem GetLogListItem(string text1, string text2, Color? b_color = null, Color? f_color = null)
        {
            ListViewItem item = new ListViewItem();
            item.Text = text1;
            item.SubItems.Add(text2);

            if (b_color != null && b_color.HasValue)
                item.BackColor = b_color.Value;

            if (f_color != null && f_color.HasValue)
                item.ForeColor = f_color.Value;

            return item;
        }
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void tbExpression_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string expression = this.tbExpression.Text;
                this.tbExpression.Text = "";
                this.Evaluate(expression);
            }
        }

        private void contextMenuLog_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.contextMenuLog.Close();
            if(e.ClickedItem == this.removeToolStripMenuItem)
            {
                var selected_items = this.lstLog.SelectedItems;
                if(selected_items != null && selected_items.Count > 0)
                {
                    var selected_item = selected_items[0];
                    this.lstLog.Items.Remove(selected_item);
                }
            }
            else if(e.ClickedItem == this.clearToolStripMenuItem)
            {
                this.lstLog.Items.Clear();
            }
        }
    }
}
