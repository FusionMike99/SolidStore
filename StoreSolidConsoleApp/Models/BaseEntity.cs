using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Models
{
    public class BaseEntity
    {
        public Guid ID { get; protected set; }

        public BaseEntity()
        {
            ID = Guid.NewGuid();
        }
    }
}
