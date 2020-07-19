using System;
using System.Collections.Generic;

namespace Ghoul.Application.Model.Build
{
    public class RunApplicationModel
    {
        public string ID { get; set; }
        public IEnumerable<RunLogApplicationModel> Logs { get; set; }
        public string BuildID { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }

    public class RunLogApplicationModel
    {
        public string LogType { get; set; }
        public string Content { get; set; }
    }
}