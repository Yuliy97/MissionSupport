using System;
using System.Collections.Generic;
using Xamarin.Forms;

using MissionSupport.Model;

namespace MissionSupport.View
{
    public partial class Main : MasterDetailPage
    {
        private IDatabase database;

        public Main(IDatabase database)
        {
            InitializeComponent();
            this.database = database;
            Detail = new NavigationPage(new Home());
            IsPresented = false;
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void Home_Clicked(object sender, EventArgs e)  
        {  
            Detail = new NavigationPage(new Home());  
            IsPresented = false;  
        } 
        private void Profile_Clicked(object sender, EventArgs e)  
        {  
            Detail = new NavigationPage(new Profile());  
            IsPresented = false;  
        }  
  
        private void Sites_Clicked(object sender, EventArgs e)  
        {  
            Detail = new NavigationPage(new Sites(database));  
            IsPresented = false;  
        }  
        private void Calendar_Clickded(object sender, EventArgs e)  
        {  
            Detail = new NavigationPage(new Calendar());  
            IsPresented = false;  
        }  
        private void About_Clickded(object sender, EventArgs e)  
        {  
            Detail = new NavigationPage(new About());  
            IsPresented = false;  
        }  
    }
}
