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

    public partial class Form1 : Form
    {
        public static readonly Guid IdError = new Guid("5C60F693-BEF5-E011-A485-80EE7300C695");
        WorkerContext db;
        public Form1()
        {
            db = new WorkerContext();
            //_db = new WorkerContext();
            InitializeComponent();
            db.Workers.Load();
            PersonGridView.DataSource = db.Workers.Local.ToBindingList();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAddnewPerson(object sender, EventArgs e)
        {
            Add addForm = new Add();
            DialogResult result = addForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            Person people = new Person();
            people.Id = Guid.NewGuid();
            people.Name = addForm.textBox1.Text;
            people.LastName = addForm.textBox2.Text;
            people.Day = (int)addForm.numericUpDown1.Value;
            people.Year = (int)addForm.numericUpDown2.Value;
            db.Workers.Add(people);
            db.SaveChanges();
            evenstb();

        }

        public void btnAddVacation(object sender, EventArgs e)
        {

            Vacation holydayn = new Vacation();

            int Daysu;


            Guid id = SearcId();
            if (id == IdError)
                return;
            db.Workers.AsNoTracking();
            // db.Workers.Load();

            Person person = db.Workers.FirstOrDefault(i => i.Id == id);
            if (person == null)
            {
                return;
            }
            db.Entry(person).Reload();
            db.Entry(person).State = EntityState.Modified;
            // holydayn.People = db.Peoples.AsNoTracking().ToList().FirstOrDefault(i=>i.Id==id);
            holydayn.Peopleid = person.Id;
            AddHol addHolForm = new AddHol(person.Day);
            DateTime date = new DateTime(person.Year, 1, 1);
            if (person.Day < 1)
            {
                MessageBox.Show("don`t have weekend!!!");
                return;
            }
            addHolForm.dateTimePicker1.Value = date;
            addHolForm.dateTimePicker2.MaxDate = addHolForm.dateTimePicker1.Value.AddDays(person.Day);
            addHolForm.dateTimePicker2.MinDate = addHolForm.dateTimePicker1.Value;
            DialogResult result = addHolForm.ShowDialog(this);
            holydayn.Id = Guid.NewGuid();
            holydayn.FirstDate = addHolForm.dateTimePicker1.Value;

            holydayn.IndexDate = false;
            addHolForm.dateTimePicker2.MaxDate = addHolForm.dateTimePicker1.Value.AddDays(person.Day);
            holydayn.SecontDate = addHolForm.dateTimePicker2.Value;
            if (result == DialogResult.Cancel)
                return;
            Daysu = person.Day - holydayn.SecontDate.Subtract(holydayn.FirstDate).Days;
            person.Day = Daysu;
            holydayn.Days = holydayn.SecontDate.Subtract(holydayn.FirstDate).Days;
            db.Vacations.Add(holydayn);
            addHolForm.Close();
            db.SaveChanges();
            evenstb();

        }

        private void btnDeletePerson(object sender, EventArgs e)
        {
            Guid id = SearcId();
            if (id == IdError)
                return;
            Person peopl = db.Workers.FirstOrDefault(c => c.Id == id);
            if (peopl == null)
            {
                return;
            }
            db.Workers.Remove(peopl);
            db.SaveChanges();
            evenstb();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnShowDate(object sender, EventArgs e)
        {

            Guid id = SearcId();
            if (id == IdError)
                return;
            date addHolForm = new date(id);

            addHolForm.onupdate += new ONupdate(evenstb);
            DialogResult result = addHolForm.ShowDialog(this);
            PersonGridView.Update();
            PersonGridView.Refresh();

            if (result == DialogResult.OK)
            {
                addHolForm.onupdate -= evenstb;
                addHolForm.Close();
                return;
            }
        }

        private void btnNextYear(object sender, EventArgs e)
        {

            Guid id = new Guid();
            Add addForm = new Add();
            int index = 0;
            if (PersonGridView.RowCount > db.Workers.Count())
            {
                index = PersonGridView.RowCount - 1;
            }
            else
            {
                index = PersonGridView.RowCount;
            }
            for (int i = 0; i < index; i++)
            {
                id = (Guid)PersonGridView[0, i].Value;
                Person people = new Person();
                Person peopleOne = new Person();
                peopleOne = db.Workers.Find(id);
                peopleOne.Day = (int)peopleOne.Day + 18;
                peopleOne.Year = (int)peopleOne.Year + 1;
                db.SaveChanges();
            }

            evenstb();
        }

        private void btnSortDate(object sender, EventArgs e)
        {
            Guid id = SearcId();
            if (id == IdError)
                return;

            Person person = db.Workers.FirstOrDefault(i => i.Id == id);
            if (person == null)
                return;
            Sort sort = new Sort(id, person.Name, person.LastName);
            DialogResult result = sort.ShowDialog(this);
            sort.Close();

        }
        private void update(object sender, EventArgs e)
        {

            this.Refresh();
        }
        void evenstb()
        {
            PersonGridView.DataSource = null;
            // PersonGridView.Update();
            db.Workers.Load();
            PersonGridView.DataSource = db.Workers.AsNoTracking().ToList();
            //  PersonGridView.Refresh();
        }
        public Guid SearcId()
        {

            //Guid id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E");
            if (PersonGridView.SelectedRows.Count > 0)
            {
                //int index = PersonGridView.SelectedRows[0].Index;


                //bool converted = Int32.TryParse(PersonGridView[0, index].Value.ToString(), out id);
                //if (!converted)
                //    return -1;   
                Guid id = (Guid)PersonGridView.CurrentRow.Cells[0].Value;
                return id;
            }
            else
            {
                return IdError;
            }

        }
    }



}
