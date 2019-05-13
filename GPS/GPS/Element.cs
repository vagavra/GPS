using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GPS
{
    [Table(name: "Element")]
    public partial class Element
    {
        public int ElementId { get; set; }
        public string Name { get; set; }
        public bool IsNode { get; set; } // true -> node, false -> edge


        private ICollection<Characteristic> Characteristics
        {
            get;
            set;
        }

        // konektiraj na bazu
        public static Element Get(DataContext db, int elementID)
        {
            IQueryable<Element> query = from element in db.GetTable<Element>() where element.ElementId == elementID select element;

            return query.Single();
        }
        /*
        public override string ToString()
        {
            return;
        }*/

    }
}
