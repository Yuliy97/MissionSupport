using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MissionSupport.View
{
    public partial class Main : MasterDetailPage
    {
        public Main()
        {
            InitializeComponent();
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
            Detail = new NavigationPage(new Sites());  
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
