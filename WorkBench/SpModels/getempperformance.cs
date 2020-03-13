using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkBench.SpModels
{
    public class spgetempperformance
    {
        public string EmpName { get; set; }
        public int? totaltask { get; set; }
        public int? pendingtask { get; set; }
        public int? completedtask { get; set; }
        //public List<PerformanceDetails> PerformanceDetails { get; set; }

    }
    //public class PerformanceDetails
    //{
    //    public int? totaltask { get; set; }
    //    public int? pendingtask { get; set; }
    //    public int? completedtask { get; set; }

    //}
}
