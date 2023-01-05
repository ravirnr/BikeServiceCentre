using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeServiceCentre.Data
{
    internal class InventryModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public string Quantity { get; set; }
        public Guid ApproveBy { get; set; }
        public string TakenBy { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
