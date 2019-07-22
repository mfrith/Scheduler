using SchedulerUI;
using System.Collections.ObjectModel;

namespace ScheduleUI
{
  public class ReportsViewModel
  {
    ObservableCollection<MeetingModelBase> _meetings;
    public ReportsViewModel(System.Collections.ObjectModel.ObservableCollection<SchedulerUI.MeetingModelBase> meetings)
    {
      _meetings = meetings;
    }

    public ObservableCollection<MeetingModelBase> Meetings
    {
      get { return _meetings; }
    }
  }
}
