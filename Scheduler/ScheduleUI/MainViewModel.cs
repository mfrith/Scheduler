using SchedulerUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScheduleUI
{
  public class MainViewModel
  {
    ObservableCollection<object> _tabs;

    public MainViewModel()
    {
      _tabs = new ObservableCollection<object>();
      GeneralViewModel generalVM = new GeneralViewModel();
      MembersViewModel membersVM = new MembersViewModel();
      membersVM.Load();
      MeetingsViewModel meetingsVM = new MeetingsViewModel(membersVM.Members);
      meetingsVM.Load();
      
      ReportsViewModel reportsVM = new ReportsViewModel(meetingsVM.Meetings);
      
      _tabs.Add(generalVM);
      _tabs.Add(membersVM);
      _tabs.Add(meetingsVM);
      _tabs.Add(reportsVM);

      DateTime today = DateTime.Today.Date;
      ObservableCollection<MeetingModel> meetings = meetingsVM.Meetings;
      var meetingsToResolve = meetings.Where(it => it.Resolved == false);
      if (meetingsToResolve.Count() > 0)
      {
        var meetingsR = meetingsToResolve.OrderBy(it => it.DayOfMeeting);

        foreach(var meeting in meetingsR)
        {
          if (meeting.DayOfMeeting < today )
          {

          }
        }
      }

      DateTime lastMeeting = meetings[0].DayOfMeeting;
      // need to find the meeting(s) to resolve
     // DateTime lastMeetingResolved = meetings.w
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

    public ObservableCollection<object> Tabs { get { return _tabs; } }
  }
}
