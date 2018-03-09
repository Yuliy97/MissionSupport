using System;
using System.Collections.Generic;
using MissionSupport.Model;

using Xamarin.Forms;
//using MissionSupport.Model;


namespace MissionSupport.View
{
    public partial class Login : ContentPage
    {
        private IDatabase database;

        public Login(IDatabase database)
        {
            InitializeComponent();

            this.database = database;
            //SignInCheck();
        }

        void SignInCheck(object sender, EventArgs e) {
            
            if (database.login(UsernameEntry.Text, PasswordEntry.Text)) {
                DisplayAlert("Login", "Login Success", "OK");
            } else {
                DisplayAlert("Login", "Login Fail", "OK");
            }

        }
    }
}
