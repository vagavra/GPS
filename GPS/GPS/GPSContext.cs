using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GPS
{
    class GPSContext: DbContext
    {
        public DbSet<Element> Elements { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
    }
}
