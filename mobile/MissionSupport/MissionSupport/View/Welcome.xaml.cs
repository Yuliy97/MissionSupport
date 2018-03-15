using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MissionSupport.View;
using MissionSupport.Model;


namespace MissionSupport.View
{
    public partial class Welcome : ContentPage
    {
        private IDatabase database;
        public Welcome()
        {
            InitializeComponent();
            database = new FakeDatabase();
        }
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login(database));
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register(database));
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
