﻿using SchedulerUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace ScheduleUI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      this.DataContext = new MainViewModel();
      MainViewModel a = (MainViewModel)this.DataContext;
      MeetingsViewModel b = (MeetingsViewModel)a.Tabs[2];
      ObservableCollection<MeetingModel> meetings = b.Meetings;
      DateTime lastMeeting = meetings[0].DayOfMeeting;
      bool resolved = meetings[0].Resolved;
      if (!resolved)
      {
        // pop up view
        //MeetingViewModel currentMeeting = new MeetingViewModel(meetings[0]);
        meetings[0].Resolved = true;
      }
      //load data files?
      // does it exist? if not create it. if it does, load it.
      //var now = DateTime.Today.ToShortDateString();
      DateTime now2 = DateTime.Today.Date;
      // any meetings missed
    }

    private void GenerateMeeting_Click(object sender, RoutedEventArgs e)
    {
      MeetingView mv = new MeetingView();

      //mv.ShowDialog();
      // when is the next meeting?
      DateTime now = DateTime.Today.Date;
      MainViewModel a = (MainViewModel)this.DataContext;
      MeetingsViewModel b = (MeetingsViewModel)a.Tabs[2];
      ObservableCollection<MeetingModel> meetings = b.Meetings;
      DateTime lastMeeting = meetings[0].DayOfMeeting;

      DayOfWeek dow = lastMeeting.DayOfWeek;
      if (dow == DayOfWeek.Friday) // last friday of the month?
      {
        // next meeting is Wednesday, mostly
        int month = lastMeeting.Month;
        int year = lastMeeting.Year;

        DateTime nextMonth = new DateTime(year, ++month, 1);
      }
      var t = this.Members;
      var Members = (MembersViewModel)a.Tabs[1];
      //List<MemberModel> members = Members.MemberList;
      ObservableCollection<MemberModel> members = Members.Members;
      DateTime dayofmeeting = now;
      var s1 = members.OrderBy(m => m.Speaker).First();
      members.Remove(s1);
      s1.Speaker = dayofmeeting;
      var s2 = members.OrderBy(m => m.Speaker).First();
      members.Remove(s2);
      s2.Speaker = dayofmeeting;

    }
      List<DateTime> GetMonthlyMeetings(DateTime startDate)
    {
      // assume startDate is a wednesday
      DateTime firstWednesday = startDate;
      DateTime secondWednesday = startDate.AddDays(7);
      DateTime thirdWednesday = startDate.AddDays(14);
      DateTime fourthWednesday = startDate.AddDays(21);
      DateTime fifthWednesday = startDate.AddDays(28);
      var daysinmonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
      DateTime lastDayOfMonth = new DateTime(2019, startDate.Month, daysinmonth);

      DateTime g = lastDayOfMonth;
      while (g.DayOfWeek != DayOfWeek.Friday)
      {
        g = g.AddDays(-1);
      }
      DateTime fridayMeeting = g;
      List<DateTime> meetings = new List<DateTime>();

      var month = startDate.Month;
      // handle months with holidays differently - November, December, January, July, etc
      if (month == 11) //november
      {
        // account for Thanksgiving
      }

      if (month == 12) //december
      {
        // account for Christmas
        meetings.Add(firstWednesday);
        meetings.Add(secondWednesday);
        meetings.Add(thirdWednesday);
        return meetings;
      }

      meetings.Add(firstWednesday);
      meetings.Add(secondWednesday);
      meetings.Add(thirdWednesday);
      if (fridayMeeting > thirdWednesday && fridayMeeting < fourthWednesday)
      {
        meetings.Add(fridayMeeting);
        meetings.Add(fourthWednesday);
      }
      else if (fridayMeeting > fourthWednesday && fridayMeeting < fifthWednesday)
      {
        meetings.Add(fourthWednesday);
        meetings.Add(fridayMeeting);
        if (fifthWednesday <= lastDayOfMonth)
          meetings.Add(fifthWednesday);
      }
      else if (fridayMeeting > fourthWednesday && fridayMeeting > fifthWednesday)
      {
        meetings.Add(fourthWednesday);
        meetings.Add(fifthWednesday);
        meetings.Add(fridayMeeting);
      }
      return meetings;
    }

  }
}
