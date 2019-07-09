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

    public partial class Form1 : Form
    {
        public static readonly Guid IdError = new Guid("5C60F693-BEF5-E011-A485-80EE7300C695");
        WorkerContext db;
        EFGenericRepository<Person> workers = new EFGenericRepository<Person>(new WorkerContext());
        EFGenericRepository<Vacation> vacations = new EFGenericRepository<Vacation>(new WorkerContext());
        EFGenericRepository<Weekend> holydays = new EFGenericRepository<Weekend>(new WorkerContext());


        public Form1()
        {
            db = new WorkerContext();

            //_db = new WorkerContext();
            InitializeComponent();
            //  db.Workers.Load();
            PersonGridView.DataSource = workers.GetSort (u => u.TeamName);
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
            people.TeamName = addForm.textBox3.Text;
            workers.Create(people);
            // db.SaveChanges();
            evenstb();

        }

        public void btnAddVacation(object sender, EventArgs e)
        {
            workers = new EFGenericRepository<Person>(new WorkerContext());
            vacations = new EFGenericRepository<Vacation>(new WorkerContext());
            Vacation holydayn = new Vacation();
            Guid id = SearcId();
            if (id == IdError)
                return;
            //db.Workers.AsNoTracking();
            // db.Workers.Load();

            Person person = workers.FindById(i => i.Id == id);
            if (person == null)
            {
                return;
            }
            // db.Entry(person).Reload();
            // db.Entry(person).State = EntityState.Modified;
            // holydayn.People = db.Peoples.AsNoTracking().ToList().FirstOrDefault(i=>i.Id==id);
            holydayn.Peopleid = person.Id;
            AddHol addHolForm = new AddHol(person.Day - 1);
            DateTime date = new DateTime(person.Year, 1, 1);
            if (person.Day < 1)
            {
                MessageBox.Show("don`t have weekend!!!");
                return;
            }
            addHolForm.dateTimePicker1.Value = date;
            int Daysu = person.Day;

            DialogResult result = addHolForm.ShowDialog(this);
            holydayn.Id = Guid.NewGuid();
            holydayn.FirstDate = addHolForm.dateTimePicker1.Value;


            DateTime CountDate = addHolForm.dateTimePicker1.Value;


            holydayn.IndexDate = false;

            holydayn.SecontDate = addHolForm.dateTimePicker2.Value;
            if (result == DialogResult.Cancel)
                return;
            int IndexDay = Daysu;
            for (DateTime i = addHolForm.dateTimePicker1.Value; i <= addHolForm.dateTimePicker2.Value;)
            {
                if (i.DayOfWeek != DayOfWeek.Sunday && i.DayOfWeek != DayOfWeek.Saturday && !AuditDate(i))
                {
                    IndexDay--;

                }
                i = i.AddDays(1);
            }

            Daysu = IndexDay;
            holydayn.TeamName = person.TeamName;
            holydayn.Days = person.Day - IndexDay;
            person.Day = Daysu;
            if (CountTeam(person.TeamName) - CountWeekend(holydayn.FirstDate,holydayn.SecontDate,person.TeamName) <=1) {
                MessageBox.Show("Date busy");
                return;
            }
            
            vacations.Create(holydayn);
            workers.Update(person);
            // db.Vacations.Add(holydayn);
            addHolForm.Close();
            //db.SaveChanges();
            evenstb();

        }

        private void btnDeletePerson(object sender, EventArgs e)
        {
            Guid id = SearcId();
            if (id == IdError)
                return;
            Person peopl = workers.FindById(id);
            if (peopl == null)
            {
                return;
            }
            workers.Remove(peopl);
            // db.Workers.Remove(peopl);
            // db.SaveChanges();
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
            // PersonGridView.Update();
            // PersonGridView.Refresh();

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
            if (PersonGridView.RowCount > workers.Count())
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
                peopleOne = workers.FindById(id);
                peopleOne.Day = (int)peopleOne.Day + 18;
                peopleOne.Year = (int)peopleOne.Year + 1;
                workers.Update(peopleOne);
            }

            evenstb();
        }

        private void btnSortDate(object sender, EventArgs e)
        {
            Guid id = SearcId();
            if (id == IdError)
                return;

            Person person = workers.FindById(i => i.Id == id);
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
            workers = new EFGenericRepository<Person>(new WorkerContext());
            vacations = new EFGenericRepository<Vacation>(new WorkerContext());
            PersonGridView.DataSource = null;
            // PersonGridView.Update();

            PersonGridView.DataSource = workers.GetSort(u => u.TeamName);
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

        private void AddnewWeekend(object sender, EventArgs e)
        {
            Weekend newWeekend = new Weekend();
            AddnewWeekend newDateWeekend = new AddnewWeekend();
            newDateWeekend.dateTimePicker1.Value = DateTime.Now;
            newDateWeekend.dateTimePicker2.MaxDate = newDateWeekend.dateTimePicker1.Value.AddDays(5);
            newDateWeekend.dateTimePicker2.MinDate = newDateWeekend.dateTimePicker1.Value;
            DialogResult result = newDateWeekend.ShowDialog(this);
            newWeekend.Id = Guid.NewGuid();
            newWeekend.startDate = newDateWeekend.dateTimePicker1.Value;
            newWeekend.EndDate = newDateWeekend.dateTimePicker2.Value;
            holydays.Create(newWeekend);
        }

        private void ShowDateWeekend(object sender, EventArgs e)
        {


            newWeekendDatagridview newWeekend = new newWeekendDatagridview();


            DialogResult result = newWeekend.ShowDialog(this);
            // PersonGridView.Update();
            // PersonGridView.Refresh();

            if (result == DialogResult.OK)
            {

                newWeekend.Close();
                return;
            }
        }
        private bool AuditDate(DateTime date)
        {
            bool TrueorFalse = false;
            List<Weekend> list = holydays.Get().ToList();
            foreach (var i in list)
            {
                if (((date.Date >= i.startDate.Date) && (date.Date <= i.EndDate.Date)) || (date.Date == i.startDate.Date))
                {
                    TrueorFalse = true;
                }
            }
            return TrueorFalse;
        }

        private void Addday(object sender, EventArgs e)
        {
            Guid id = SearcId();
            Person person = workers.FindById(id);
            if (person == null)
                return;
            person.Day++;
            workers.Update(person);
            evenstb();
        }
        private int CountWeekend(DateTime StartDay, DateTime EndDay,string TeamName) {
            int Count = 0;
            List<Vacation> list = vacations.Get(i=>((DateTime.Compare(StartDay, i.FirstDate)<=0) || DateTime.Compare(i.SecontDate,EndDay)>=0) &&(i.TeamName==TeamName)).ToList();
            if (list == null)
                return 0;
            Count = list.Count();
            return Count;
        }
        private int CountTeam(string TeamName) {
            List<Person> list = workers.Get(i => i.TeamName == TeamName).ToList();
            return list.Count;
        }

        private void PersonGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SubtractDay(object sender, EventArgs e)
        {
            Guid id = SearcId();
            Person person = workers.FindById(id);
            if (person == null)
                return;
            person.Day--;
            workers.Update(person);
            evenstb();

        }
    }

}
