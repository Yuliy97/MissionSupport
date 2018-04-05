using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionSupport.Model
{
    public class Site
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public Site(string name, string address, DateTime createdOn)
        {
            Name = name;
            Address = address;
            CreatedOn = createdOn;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + 19 * Address.GetHashCode() + 37 * CreatedOn.GetHashCode();
        }
    }
}
