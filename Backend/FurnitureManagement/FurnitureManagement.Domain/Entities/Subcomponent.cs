using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Domain.Entities
{
    public class Subcomponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string CustomNotes { get; set; }
        public int Count { get; set; }


        [NotMapped]
        public int TotalQuantity => Count * (Component?.Quantity ?? 1);

        public float DetailLength { get; set; }
        public float DetailWidth { get; set; }
        public float DetailThickness { get; set; }

      
        public float CuttingLength { get; set; }
        public float CuttingWidth { get; set; }
        public float CuttingThickness { get; set; }

       
        public float FinalLength { get; set; }
        public float FinalWidth { get; set; }
        public float FinalThickness { get; set; }

       
        public string VeneerInner { get; set; }
        public string VeneerOuter { get; set; }

        public int ComponentId { get; set; }
        public Component Component { get; set; }
    }

}
