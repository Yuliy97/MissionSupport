using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

using MissionSupport.Model;

namespace MissionSupport.View
{
    public partial class Sites : ContentPage
    {
        private Distance DEFAULT_ZOOM = new Distance(10000);

        private IDatabase database;

        public Sites(IDatabase database)
        {
            InitializeComponent();
            this.database = database;
            // TODO: show user location if permission granted

            Geocoder geocoder = new Geocoder();

            Site displaySite = null;
            foreach (Site site in database.getSites()) {
                addPin(site);
                displaySite = site;
            }

            if (displaySite != null) {
                SitesMap.MoveToRegion(MapSpan.FromCenterAndRadius(displaySite.Location, DEFAULT_ZOOM));
            }
        }

        private async void AddSite_Clicked(object sender, EventArgs e)
        {
            AddSite addSite = new AddSite(database);
            await Navigation.PushAsync(addSite);
            addSite.Disappearing += (x, y) => {
                if (addSite.NewSite != null) {
                    addPin(addSite.NewSite);
                    SitesMap.MoveToRegion(MapSpan.FromCenterAndRadius(addSite.NewSite.Location, DEFAULT_ZOOM));
                }
            };
        }

        private void addPin(Site site)
        {
            Pin pin = new Pin() {
                Position = site.Location,
                Label = site.Name
            };

            SitesMap.Pins.Add(pin);
        }
    }
}
