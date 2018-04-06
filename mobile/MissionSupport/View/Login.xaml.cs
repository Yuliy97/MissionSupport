using System;

using Xamarin.Forms;

using MissionSupport.Model;

namespace MissionSupport.View
{
    public partial class Login : ContentPage
    {
        private IDatabase database;

        public Login(IDatabase database)
        {
            InitializeComponent();

            this.database = database;
        }

        private async void SignInCheck(object sender, EventArgs e) 
        {
            if (await database.login(UsernameEntry.Text, PasswordEntry.Text)) {
                await DisplayAlert("Login", "Login Success", "OK");
            } else {
                await DisplayAlert("Login", "Login Fail", "OK");
            }
        }
    }
}
