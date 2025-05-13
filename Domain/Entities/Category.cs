using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; private set; }

        public Category(string name)
        {
            Name = name;
        }

        public Category()
        {
            Name = string.Empty;
        }
    }
}
