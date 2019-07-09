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
    public partial class newWeekendDatagridview : Form
    {
        Guid _Id;
    
        EFGenericRepository<Weekend> holydays = new EFGenericRepository<Weekend>(new WorkerContext());

        public newWeekendDatagridview()
        {
            InitializeComponent();
       
            if (holydays.Count() < 1)
            {
                MessageBox.Show("Don`t have weekend");

            }
            else
            {
                BindingSource DatedbOne = new BindingSource();
                var DatedbOneK = holydays.Get();

                var qieryAsList = new BindingList<Weekend>(DatedbOneK.ToList());
                DatedbOne.DataSource = qieryAsList;
                dataGridView1.DataSource = DatedbOne;
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void NewWeekendDatagridview_Load(object sender, EventArgs e)
        {

        }

        private void delete(object sender, EventArgs e)
        {
            Guid id = (Guid)dataGridView1.CurrentRow.Cells[0].Value;
            holydays.Remove(holydays.FindById(id));
            var DatedbOneK = holydays.Get();
            BindingSource DatedbOne = new BindingSource();
            var qieryAsList = new BindingList<Weekend>(DatedbOneK.ToList());
            DatedbOne.DataSource = qieryAsList;
            dataGridView1.DataSource = DatedbOne;

        }
    }
}
