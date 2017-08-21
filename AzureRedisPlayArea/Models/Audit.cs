using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureRedisPlayArea.Models
{
    public class Audit
    {
        public string Name { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
