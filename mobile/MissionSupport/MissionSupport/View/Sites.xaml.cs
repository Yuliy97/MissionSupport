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

            // TODO: show user location if permission granted
        }
    }
}
