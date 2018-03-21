using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MissionSupport.View
{
    public partial class Sites : ContentPage
    {
        private Map map;
        private Pin pin1;

        public Sites()
        {
            InitializeComponent();

            map = new Map(MapSpan.FromCenterAndRadius(
                new Position(36, 36),
                Distance.FromMiles(0.5)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand

            };

            pin1 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(40, 32),
                Label = "Test Pin",
                Address = "www.google.com"
            };
        }
    }
}
