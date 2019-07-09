using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFAplicationVacation
{
    public class Person
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string TeamName { get; set; }
        public int Day { get; set; }
        public ICollection<Vacation> HolyDays { get; set; } = new List<Vacation>();

    }
}
