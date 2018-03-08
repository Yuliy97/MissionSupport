using System;
using System.Collections.Generic;
using MissionSupport.Model;

using Xamarin.Forms;

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
    }
}
