using System;

using Xamarin.Forms;

using MissionSupport.Model;

namespace MissionSupport.View
{
    public partial class AddSite : ContentPage // TODO: cancel button
    {
        public Site NewSite { get; private set; }

        private IDatabase database;

        public AddSite(IDatabase database)
        {
            InitializeComponent();

            NewSite = null;
            this.database = database;
        }

        private async void CreateButton_Clicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string address = AddressEntry.Text;

            if (!await Site.validAddress(address)) {
                await DisplayAlert("Add Site", "Invalid address", "OK");
                return;
            }

            Site site = new Site(name, address);
            if (await database.addSite(site)) {
                NewSite = site;
                await Navigation.PopAsync();
            } else {
                await DisplayAlert("Add Site", "Failed to add site", "OK");
            }
        }
    }
}
