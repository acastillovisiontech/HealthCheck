using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid CategoryId { get; private set; }

        public Product(string name, Guid categoryId)
        {
            Id = Guid.NewGuid();
            Name = name;
            CategoryId = categoryId;
        }
    }
}
