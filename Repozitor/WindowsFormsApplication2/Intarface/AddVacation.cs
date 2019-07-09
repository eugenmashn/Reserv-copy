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
    public partial class AddHol : Form
    {
        EFGenericRepository<Weekend> holydays = new EFGenericRepository<Weekend>(new WorkerContext());
        int Day;
        int _Day;
        public AddHol(int _day)
        {
             Day = _day;
            _Day = _day;
   
            InitializeComponent();
            DateTime CountDate = dateTimePicker1.Value;
            dateTimePicker1.ValueChanged += Limited;
            int Indexday=Day;
            
    
           // dateTimePicker1.ValueChanged -= Limited;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void Limited(object sender,EventArgs e) {
            int IndexDay = Day;
            DateTime CountDate = dateTimePicker1.Value;
           
            if (dateTimePicker1.Value > dateTimePicker2.MaxDate)
            {
                for (int i = 0; i <= IndexDay; i++)
                {
                    if (CountDate.DayOfWeek == DayOfWeek.Sunday || CountDate.DayOfWeek == DayOfWeek.Saturday||AuditDate(CountDate))
                    {
                        IndexDay++;

                    }
                    CountDate = CountDate.AddDays(1);
                }
          
                dateTimePicker2.MaxDate = dateTimePicker1.Value.AddDays(IndexDay);
                dateTimePicker2.MinDate = dateTimePicker1.Value;
            }
            else {
                IndexDay = Day;
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            for (int i = 0; i <= IndexDay; i++) {
                if (CountDate.DayOfWeek == DayOfWeek.Sunday || CountDate.DayOfWeek == DayOfWeek.Saturday || AuditDate(CountDate)) {
                    IndexDay++;
                
                }
                    CountDate = CountDate.AddDays(1);
                }
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddDays(IndexDay);
                Day = _Day;
            }
        }

        private void AddHol_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private bool AuditDate(DateTime date) {
            bool TrueorFalse=false;
            List<Weekend> list= holydays.Get().ToList();
            foreach (var i in list) {
                if (((DateTime.Compare(date.Date , i.startDate.Date)>=0) && DateTime.Compare(date.Date ,i.EndDate.Date)<=0) || (date == i.startDate)) {
                    TrueorFalse = true;
                }
            }
            return TrueorFalse;
        }
        
    }
}
