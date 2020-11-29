﻿using SchedulerUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace ScheduleUI
{
  public class InverseBoolConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return !(bool)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotSupportedException();
    }
  }
  public class MainViewModel
  {
    ObservableCollection<object> _tabs;

    public MainViewModel()
    {
      _tabs = new ObservableCollection<object>();
      string home = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

      GeneralViewModel generalVM = new GeneralViewModel(home);
      MembersViewModel membersVM = new MembersViewModel(home);
      membersVM.Load();
      MeetingsViewModel meetingsVM = new MeetingsViewModel(membersVM.Members, home);
      meetingsVM.Load();
      
      ReportsViewModel reportsVM = new ReportsViewModel(meetingsVM.Meetings.ToList(), membersVM.Members, home);
      
      _tabs.Add(generalVM);
      _tabs.Add(membersVM);
      _tabs.Add(meetingsVM);
      _tabs.Add(reportsVM);

      DateTime today = DateTime.Today.Date;
      ObservableCollection<MeetingModelBase> meetings = meetingsVM.Meetings;
      List<MeetingModelBase> meetingsToResolve = new List<MeetingModelBase>();
      foreach (MeetingModelBase mtg in meetings)
      {
        if (mtg.Resolved == "0" || mtg.Resolved == null)
          meetingsToResolve.Add(mtg);
      }
      //var meetingsToResolve = meetings.Where(it => it.Resolved == "0").Select(it => it).ToList();
      if (meetingsToResolve.Count() > 0)
      {
        //var meetingsR = meetingsToResolve.OrderBy

        //foreach(var meeting in meetingsR)
        //{
        //  DateTime dom = DateTime.ParseExact(meeting.DayOfMeeting, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //  if (dom < today )
        //  {

        //  }
        //}

        DateTime lastMeeting = DateTime.ParseExact(meetings[0].DayOfMeeting, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);
        // need to find the meeting(s) to resolve
        // DateTime lastMeetingResolved = meetings.w
        bool resolved = "1" == meetings[0].Resolved;
        if (!resolved)
        {
          // pop up view
          //MeetingViewModel currentMeeting = new MeetingViewModel(meetings[0]);
          meetings[0].Resolved = "1";
        }
      }
      //load data files?
      // does it exist? if not create it. if it does, load it.
      //var now = DateTime.Today.ToShortDateString();
      DateTime now2 = DateTime.Today.Date;
      // any meetings missed

    }
    public void CheckDataFiles()
    {

    }
    public ObservableCollection<object> Tabs { get { return _tabs; } }
  }
}
