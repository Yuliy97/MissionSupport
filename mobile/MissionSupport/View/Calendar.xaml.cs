using System;
using System.Collections.Generic;
using System.Globalization;

using Xamarin.Forms;

namespace MissionSupport.View
{
    public partial class Calendar : ContentPage
    {
        public Calendar()
        {
            InitializeComponent();
        }

        //private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var calendar = sender as Calendar;

        //    // ... See if a date is selected. 

        //    if (calendar.SelectedDate.HasValue)
        //    {
        //        // ... Display SelectedDate in Title. 
        //        DateTime date = calendar.SelectedDate.Value;
        //        this.Title = date.ToShortDateString();
        //    }
        //}
        //calendarView = new Calendar();

        private void DatePicker_OnSelected(object sender, DateChangedEventArgs e) {
            MainLabel.Text = e.NewDate.ToString();
        }
    }
}
