﻿using System;

using Xamarin.Forms;

using MissionSupport.Model;

namespace MissionSupport.View
{
    public partial class ViewSite : ContentPage // TODO: make this a tabbed page with info, calendar, etc tabs
    {
        public Site Site { get; private set; }

        private IDatabase database;

        public ViewSite(IDatabase database, Site site)
        {
            InitializeComponent();

            Site = site;
            this.database = database;
        }

        protected override void OnAppearing()
        {
            Title = Site.Name;
            NameLabel.Text = "Name: " + Site.Name;
            AddressLabel.Text = "Address: " + Site.Address;
        }
    }
}
