using System;

using Xamarin.Forms;

using MissionSupport.Model;

namespace MissionSupport.View
{
    public partial class AddSite : ContentPage // TODO: cancel button
    {
        public Site NewSite { get; private set; }

        private IDatabase database;
        private bool showDescriptionPlaceholder;

        public AddSite(IDatabase database)
        {
            InitializeComponent();

            NewSite = null;
            this.database = database;
            showDescriptionPlaceholder = true;
        }

        private async void CreateButton_Clicked(object sender, EventArgs e)
        {
            tryRemovePlaceholder();

            string name = NameEntry.Text;
            string address = AddressEntry.Text;
            string description = DescriptionEditor.Text;

            if (!await Site.validAddress(address)) {
                await DisplayAlert("Add Site", "Invalid address", "OK");
                return;
            }

            Site site = new Site(name, address, description);
            if (await database.addSite(site)) {
                NewSite = site;
                await Navigation.PopAsync();
            } else {
                await DisplayAlert("Add Site", "Failed to add site", "OK");
            }
        }

        private void DescriptionEditor_Focused(object sender, FocusEventArgs e)
        {
            tryRemovePlaceholder();
        }

        private void tryRemovePlaceholder()
        {
            if (showDescriptionPlaceholder) {
                DescriptionEditor.Text = "";
                DescriptionEditor.TextColor = Color.Black;
                showDescriptionPlaceholder = false;
            }
        }
    }
}
