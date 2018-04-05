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
            Geocoder geocoder = new Geocoder();

            foreach (Site site in database.getSites()) {
                var positions = await geocoder.GetPositionsForAddressAsync(site.Address);
                Position position = new Position();
                try {
                    position = positions.First();
                } catch (InvalidOperationException) {
                    await DisplayAlert("Error", "A site address could not be found", "OK");
                    continue;
                }

                Pin pin = new Pin() {
                    Position = position,
                    Label = site.Name
                };

                SitesMap.Pins.Add(pin);
                SitesMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, new Distance(10000)));
            }
        }

        private async void AddSite_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddSite());
            //await ((NavigationPage) Main.Instance.Detail).PushAsync(new AddSite());
        }
    }
}
