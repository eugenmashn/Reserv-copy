using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAplicationVacation
{
    public partial class AddnewWeekend : Form
    {
        public AddnewWeekend()
        {
            InitializeComponent();
            dateTimePicker1.ValueChanged += Limited;
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddDays(15);
            dateTimePicker2.MinDate = dateTimePicker1.Value;
           // dateTimePicker1.ValueChanged -= Limited;
        }
        private void Limited(object sender, EventArgs e)
        {
            DateTime CountDate = dateTimePicker1.Value;
            if (dateTimePicker1.Value > dateTimePicker2.MaxDate)
            {
                dateTimePicker2.MaxDate = dateTimePicker1.Value.AddDays(15);
            }
            else
            {
                dateTimePicker2.MinDate = dateTimePicker1.Value;
            }
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddDays(15);
          
        }
        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void AddnewWeekend_Load(object sender, EventArgs e)
        {

        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
