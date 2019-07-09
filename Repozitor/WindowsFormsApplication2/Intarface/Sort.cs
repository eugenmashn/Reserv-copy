using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
namespace WFAplicationVacation
{
    public partial class Sort : Form
    {
       
        WorkerContext _db;
        Guid _Id;
        int YearIndex;
        public Sort(Guid Id, string name,string lastName)
        {
            _Id = Id;
            InitializeComponent();
            _db = new WorkerContext();
            //_db.Workers.Load();
            //_db.Vacations.Load();
            linkLabel2.Text = name;
            linkLabel3.Text = lastName;
            Person peopl = _db.Workers.Find(_Id);
            YearIndex =peopl.Year;
            numericUpDown1.Value = YearIndex;
            if (_db.Vacations.Count(i => i.FirstDate.Year == YearIndex)<1) {
                    MessageBox.Show("not booked");
               
            }
            else {

                BindingSource DatedbOne = new BindingSource();
                var DatedbOnek = from w in _db.Vacations
                                where w.FirstDate.Year == YearIndex
                                where w.People.Id==_Id
                                select w;
                var qieryAsList = new BindingList<Vacation>(DatedbOnek.ToList());
                DatedbOne.DataSource = qieryAsList;
                dataGridView1.DataSource = DatedbOne;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            YearIndex = (int)numericUpDown1.Value;
            if (_db.Vacations.Count(i => i.FirstDate.Year == YearIndex) < 1)
            {
                MessageBox.Show("not booked");

            }
            else
            {

                BindingSource DatedbOne = new BindingSource();
                var DatedbOnek = from w in _db.Vacations
                                 where w.FirstDate.Year == YearIndex
                                 where w.People.Id==_Id
                                 select w;
                var qieryAsList = new BindingList<Vacation>(DatedbOnek.ToList());
                DatedbOne.DataSource = qieryAsList;
                dataGridView1.DataSource = DatedbOne;

            }
        }


        //private void button4_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView1.SelectedRows.Count > 0)
        //    {


        //        int index = dataGridView1.SelectedRows[0].Index;
        //        int id = 0;

        //        bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
        //        date addHolForm = new date(id);
        //        DialogResult result = addHolForm.ShowDialog(this);

              

        //    }
        //}

        private void Sort_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
