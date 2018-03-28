using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

using MissionSupport.Model;

namespace MissionSupport.View
{
    public partial class Sites : ContentPage
    {
        private IDatabase database;

        public Sites(IDatabase database)
        {
            InitializeComponent();
            this.database = database;
            // TODO: show user location if permission granted
        }

        protected override async void OnAppearing()
        {
            List<Site> sitesList = new List<Site>();
            sitesList.Add(new Site("Tech Tower", "Tech Tower, Atlanta, GA 30313", DateTime.Now));
            sitesList.Add(new Site("CDC", "1600 Clifton Rd, Atlanta, GA 30333", DateTime.Now));
            sitesList.Add(new Site("Emory", "1648 Pierce Dr NE, Atlanta, GA 30307", DateTime.Now));

            Geocoder geocoder = new Geocoder();

            foreach (Site localSite in sitesList) {
                database.addSite(localSite);
                Site site = database.getSiteByName(localSite.Name);

                var positions = await geocoder.GetPositionsForAddressAsync(site.Address);
                Position position = new Position();
                try {
                    position = positions.First();
                } catch (InvalidOperationException) {
                    await DisplayAlert("Error", "That address could not be found", "OK");
                    return;
                }

                Pin pin = new Pin() {
                    Position = position,
                    Label = site.Name
                };

                SitesMap.Pins.Add(pin);
                SitesMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, new Distance(10000)));
            }
        }
    }
}
