using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace WFAplicationVacation
{
    public class WorkerContext : DbContext
    {
        public WorkerContext() : base("Workers")
        {
        }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Person> Workers { get; set; }
        public DbSet<Weekend> HolyDays { get; set; } 

    }
}
