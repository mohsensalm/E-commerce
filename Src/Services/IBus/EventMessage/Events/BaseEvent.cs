using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMessage.Events
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            ID = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
        }
        public Guid ID { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
