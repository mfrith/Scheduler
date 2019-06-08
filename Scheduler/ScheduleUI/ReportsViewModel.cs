using SchedulerUI;
using System.Collections.ObjectModel;

namespace ScheduleUI
{
  public class ReportsViewModel
  {
    ObservableCollection<MeetingModel> _meetings;
    public ReportsViewModel(System.Collections.ObjectModel.ObservableCollection<SchedulerUI.MeetingModel> meetings)
    {
      _meetings = meetings;
    }

    public ObservableCollection<MeetingModel> Meetings
    {
      get { return _meetings; }
    }
  }
}
