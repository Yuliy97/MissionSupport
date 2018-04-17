using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms.Maps;

namespace MissionSupport.Model
{
    public class Site
    {
        private static int currentID = 0;

        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Description { get; private set; }
        public Position Location { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public Site(string name, string address, string description, Position location, DateTime createdOn)
        {
            initialize(name, address, description, location, createdOn);
        }

        public Site(string name, string address, string description)
        {
            initialize(name, address, description, null, null);
        }

        private void initialize(string name, string address, string description, Position? location, DateTime? createdOn)
        {
            ID = currentID;
            currentID++;

            Name = name;
            Address = address;
            Description = description;
            if (location == null) {
                Task.Run(async () => await generateLocation());
            } else {
                Location = location.Value;
            }
            CreatedOn = (createdOn == null) ? DateTime.Now : createdOn.Value;
        }

        private async Task generateLocation()
        {
            Geocoder geocoder = new Geocoder();
            Location = (await geocoder.GetPositionsForAddressAsync(Address)).First();
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public static async Task<bool> validAddress(string address)
        {
            Geocoder geocoder = new Geocoder();
            var positions = await geocoder.GetPositionsForAddressAsync(address);

            try {
                Position position = positions.First();
            } catch (InvalidOperationException) {
                return false;
            }

            return true;
        }
    }
}
