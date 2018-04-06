using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionSupport.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MissionSupport.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private IDatabase database;

        public Register(IDatabase database)
        {
            InitializeComponent();

            this.database = database;
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            User newUser = new User(EmailEntry.Text, UsernameEntry.Text, FirstNameEntry.Text, LastNameEntry.Text);
            if (await database.addUser(newUser, PasswordEntry.Text))
            {
                await DisplayAlert("Register", "Register Success", "OK");
            }
            else
            {
                await DisplayAlert("Register", "Register Fail", "OK");
            }
        }
    }
}