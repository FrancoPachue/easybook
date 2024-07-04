using EasyBook.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EasyBook.Domain.Entities
{
    public class Subscription:Entity
    {
        public Subscription(int id):base(id)
        {
            Parameters = new List<Parameter>();
        }
        public string Endpoint { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
