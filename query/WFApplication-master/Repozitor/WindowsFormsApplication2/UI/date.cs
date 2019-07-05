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
namespace WindowsFormsApplication2
{

    public delegate void ONupdate();
    public partial class date : Form
    {

        public event ONupdate onupdate;
        Guid _Id;
        Vacation personOfweekend;
        WorkerContext _db;
        public class HollydayTwo {
            public Guid Id;
            public DateTime FirstDate;
            public DateTime SecondDate;
        }
        protected virtual void Onstartevent() {
            if (onupdate != null) {
                onupdate();
            }
        }

        public date(Guid Id)
        {
            InitializeComponent();
            _db = new WorkerContext();
            _Id = Id;
            //_db.Workers.Load();
            //_db.Vacations.Load();
            if (_db.Vacations.Count(i=>i.Peopleid==_Id)<1)
            {
               MessageBox.Show("Don`t have weekend");
            
            }
            else {
               
            BindingSource DatedbOne = new BindingSource();
            var DatedbOneK=from w in _db.Vacations
                       where (w.Peopleid== _Id)
                       select w;
            personOfweekend = _db.Vacations.FirstOrDefault(q => q.People.Id == _Id);
            checkBox1.Checked = personOfweekend.IndexDate;
            var qieryAsList = new BindingList<Vacation>(DatedbOneK.ToList());
            DatedbOne.DataSource = qieryAsList;
            dataGridView1.DataSource = DatedbOne;
            }
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void date_Load(object sender, EventArgs e)
        {

        }
   

        private void btnDeleteDate(object sender, EventArgs e)
        {
          
                int DaysRegain;

               Guid id =SearcId();
               if (id == Form1.IdError)
                    return;
            Vacation peoplday = _db.Vacations.FirstOrDefault(c => c.Id == id);
                Person people = _db.Workers.FirstOrDefault(c => c.Id == peoplday.Peopleid);
                BindingSource DatedbOne = new BindingSource();
                var DatedbOneK = from w in _db.Vacations
                                 where (w.Peopleid == _Id)
                                 select w;

                if (peoplday.IndexDate == true)
                {
                    MessageBox.Show("it is used");
                }
                else
                {
                   // holydayn.SecontDate.Subtract(holydayn.FirstDate).Days
                    DaysRegain = peoplday.People.Day + peoplday.SecontDate.Subtract(peoplday.FirstDate).Days;
                    people.Day = DaysRegain;
                   _db.Vacations.Remove(peoplday);

                    _db.SaveChanges();
      
                    DatedbOneK = from w in _db.Vacations
                                 where (w.Peopleid == _Id)
                                 select w;
                   
                    var qieryAsList = new BindingList<Vacation>(DatedbOneK.ToList());
                    DatedbOne.DataSource = qieryAsList;
                    dataGridView1.DataSource = DatedbOne;
                    dataGridView1.Update();
                    dataGridView1.Refresh();
                
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Guid id = SearcId();
            if (id == Form1.IdError)
                return;
            Vacation peoplday = _db.Vacations.FirstOrDefault(c => c.Id == id);
            if (peoplday == null)
                return;
                peoplday.IndexDate = true;
                _db.SaveChanges();        
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Onstartevent();
        }
         public Guid SearcId() {

            Guid id = Form1.IdError;
            if (dataGridView1.SelectedRows.Count > 0)
            {

               

               id = (Guid)dataGridView1.CurrentRow.Cells[0].Value;
              
                   
            }
                return id;
        }
    }
}
