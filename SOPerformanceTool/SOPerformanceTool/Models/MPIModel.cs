using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPerformanceTool.Models
{
    public class MPIModel
    {
        public string Labor { get; set; }
        public string RepliedVol { get; set; }
        public double ReplyMPI { get; set; }
    }

    public class AgentMPIModel
    {
        public string Alias { get; set; }
        public string Labor { get; set; }
        public string SDReplied { get; set; }
        public double ReplyMPI { get; set; }
    }
}
