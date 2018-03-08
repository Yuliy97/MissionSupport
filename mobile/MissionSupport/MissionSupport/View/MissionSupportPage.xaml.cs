using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using MissionSupport.View;
using MissionSupport.Model;

namespace MissionSupport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MissionSupportPage : ContentPage
    {
        private IDatabase database;

        public MissionSupportPage()
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
