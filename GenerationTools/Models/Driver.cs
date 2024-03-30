using Nikitin.Models;
using Nikitin.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nikitin.GenerationTools.Models
{
    internal class Driver
    {
        public DateTime CurrentDateTime { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public List<Request> requests { get; set; }

        public int AllRequestCount { get; set; }

        public int BlackIceCount { get; set; }

        public int SnowFallCount { get; set; }

        public string DriverFIO {  get; set; }

        public Driver(Employee employee, List<Request> requests, DateTime start, DateTime end) 
        { 
            CurrentDateTime = DateTime.Now;
            StartDateTime = start;
            EndDateTime = end;
            this.requests = requests;
            AllRequestCount = requests.Count();
            BlackIceCount = requests.Count(p => p.IdPurityStatus == 1);
            SnowFallCount = requests.Count(p => p.IdPurityStatus == 2);
            DriverFIO = $"{employee.Surname} {employee.Name} {employee.Patronymic}";
        }
    }
}
