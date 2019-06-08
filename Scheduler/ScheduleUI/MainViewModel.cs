using System.Collections.ObjectModel;

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

    }

    public ObservableCollection<object> Tabs { get { return _tabs; } }
  }
}
