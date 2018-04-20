using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MissionSupport.View
{
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();
            Fayaz.Source = ImageSource.FromResource("MissionSupport.Images.fayaz.jpg");
            Taylor.Source = ImageSource.FromResource("MissionSupport.Images.taylor.jpg");
            Yuli.Source = ImageSource.FromResource("MissionSupport.Images.yuli.jpg");
            Abdullah.Source = ImageSource.FromResource("MissionSupport.Images.abdullah.jpg");
            Maddie.Source = ImageSource.FromResource("MissionSupport.Images.maddie.jpeg");
            An.Source = ImageSource.FromResource("MissionSupport.Images.an.jpg");
        }
    }
}
