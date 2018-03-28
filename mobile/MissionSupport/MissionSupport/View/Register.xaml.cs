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

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            User newUser = new User(EmailEntry.Text, UsernameEntry.Text, FirstNameEntry.Text, LastNameEntry.Text);
            if (database.addUser(newUser, PasswordEntry.Text))
            {
                DisplayAlert("Register", "Register Success", "OK");
            }
            else
            {
                DisplayAlert("Register", "Register Fail", "OK");
            }
        }
    }
}