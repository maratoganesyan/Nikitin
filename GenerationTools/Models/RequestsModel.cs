using Nikitin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nikitin.GenerationTools.Models
{
    internal class RequestsModel
    {
        public DateTime CurrentDateTime { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public List<Request> requests { get; set; }

        public int AllRequestCount { get; set; }

        public RequestsModel(List<Request> requests, DateTime start, DateTime end)
        { 
            CurrentDateTime = DateTime.Now;
            StartDateTime = start;
            EndDateTime = end;
            this.requests = requests;
            AllRequestCount = requests.Count;
        }
    }
}
