using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Domain.Entities
{
    public class Component
    {

        public int Id { get; set; }
       
        public string Name { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<Subcomponent> Subcomponents { get; set; }

    }
}
