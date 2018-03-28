using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MissionSupport.View
{
    public partial class Sites : ContentPage
    {
        public Sites()
        {
            InitializeComponent();

            // TODO: show user location if permission granted
        }

        protected override async void OnAppearing()
        {
            string address = "Tech Tower, Atlanta, GA 30313";
            Geocoder geocoder = new Geocoder();
            var addressMatches = await geocoder.GetPositionsForAddressAsync(address);
            Position pos = new Position();
            try {
                pos = addressMatches.First();
            } catch (InvalidOperationException) {
                await DisplayAlert("Error", "That address could not be found", "OK");
                return;
            }

            Pin testPin = new Pin() {
                Position = pos,
                Label = "TEST"
            };
            
            SitesMap.Pins.Add(testPin);
            SitesMap.MoveToRegion(MapSpan.FromCenterAndRadius(pos, new Distance(1000)));
        }
    }
}
