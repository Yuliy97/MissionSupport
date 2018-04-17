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

            refreshMap();
        }

        private async void AddSite_Clicked(object sender, EventArgs e) // TODO: only allow for logged in users
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

        private async void Pin_Clicked(object sender, EventArgs e)
        {
            Pin clickedPin = (Pin) sender;
            Site site = await database.getSiteByName(clickedPin.Label);
            ViewSite viewSite = new ViewSite(database, site);
            await Navigation.PushAsync(viewSite);
        }

        private void refreshMap()
        {
            SitesMap.Pins.Clear();

            Site displaySite = null;
            foreach (Site site in database.getSites()) {
                addPin(site);
                displaySite = site;
            }

            if (displaySite != null) {
                SitesMap.MoveToRegion(MapSpan.FromCenterAndRadius(displaySite.Location, DEFAULT_ZOOM));
            }
        }

        private void addPin(Site site)
        {
            Pin pin = new Pin() {
                Position = site.Location,
                Label = site.Name
            };
            pin.Clicked += Pin_Clicked;

            SitesMap.Pins.Add(pin);
        }
    }
}
