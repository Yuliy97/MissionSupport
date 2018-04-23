import { Component, OnInit, ViewChild } from '@angular/core';
import { CalendarComponent } from 'ng-fullcalendar';
import { Options } from 'fullcalendar';

var _events = [{title: 'Mission Support Capstone', start: '2018-04-23', end: '2018-04-24'}];

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  event_name: string;
  attendees: string;
  event_description: string;
  start_date: string;
  end_date: string;

  calendarOptions: Options;
  @ViewChild(CalendarComponent) ucCalendar: CalendarComponent;
  constructor() {}
  ngOnInit() {
     this.calendarOptions = {
        editable: true,
        eventLimit: false,
        header: {
          left: 'prev,next today',
          center: 'title',
          right: 'month,agendaWeek,agendaDay,listMonth'
        },
        events: _events,
        eventColor: '#378006',
        eventTextColor: 'white',
        eventClick: function(event) {
          console.log("HELLO");
          alert('Event ' + event.title);
        }
      };
  }

  // format(str) {
  //   if (str) {
  //     var result = str.substring(6, 10) + "-" + str.substring(0, 2) + "-" + str.substring(3, 5)
  //     return result;
  //   } else {
  //     return "2018-04-01";
  //   }
  // }

  on_add() {
    const event = {
      title: this.event_name,
      start: "2018-04-25",
      end: "2018-04-26"
    }
    this.ucCalendar.fullCalendar('renderEvent', event);
  }

}
