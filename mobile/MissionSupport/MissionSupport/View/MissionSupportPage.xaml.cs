using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using MissionSupport.View;

namespace MissionSupport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MissionSupportPage : ContentPage
    {
        public MissionSupportPage()
        {
            InitializeComponent();


        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}
