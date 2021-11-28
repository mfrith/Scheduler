using SchedulerUI;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScheduleUI
{
  public class MeetingTemplateSelector : DataTemplateSelector
  {
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      FrameworkElement element = container as FrameworkElement;
      
      if (item != null)
      {

        var mtg = item as MeetingResolutionViewModel;
        if (mtg.MeetingType == "1")
          return element.FindResource("RegularMeetingTemplate") as DataTemplate;
        else if (mtg.MeetingType == "2")
          return element.FindResource("RegularMeetingTemplate2") as DataTemplate;

      }
      return base.SelectTemplate(item, container);
    }
  }
  public class MeetingsResolutionViewModel : ViewModelBase
  {
    private MeetingResolutionViewModel _currentMeetingToResolveVM;
    private List<MeetingModelBase> _meetingsToResolve;
    private List<MemberModel> _members;
    private int _meetingCount = 0;
    private int _currentMeetingIndex = -1;

    public MeetingsResolutionViewModel()
    {

    }
    public MeetingsResolutionViewModel(List<MeetingModelBase> meetingsToResolve, List<MemberModel> members)
    {
      _meetingsToResolve = meetingsToResolve;
      _meetingCount = _meetingsToResolve.Count;

      _members = members;
    }

    public void Resolve()
    {
      MeetingsResolutionView view = new MeetingsResolutionView();
      //MeetingResolutionView view = new MeetingResolutionView();
      CurrentMeetingToResolve = new MeetingResolutionViewModel(_meetingsToResolve[0], _members);
      _currentMeetingIndex++;
      view.DataContext = this;
      view.ShowDialog();
    }

    public MeetingResolutionViewModel CurrentMeetingToResolve
    {
      get { return _currentMeetingToResolveVM; }
      set { _currentMeetingToResolveVM = value; }
    }

    private ICommand _nextMeetingToResolveCommand;
    public ICommand NextMeetingToResolveCmd
    {
      get
      {
        return _nextMeetingToResolveCommand ?? (_nextMeetingToResolveCommand = new RelayCommand(() => NextMeetingToResolve(), () => CanExecuteNextMeetingToResolve));
      }
    }

    public void NextMeetingToResolve()
    {
      _currentMeetingIndex++;
      _currentMeetingToResolveVM = new MeetingResolutionViewModel(_meetingsToResolve[_currentMeetingIndex], _members);
      NotifyPropertyChanged(() => CurrentMeetingToResolve);
    }

    public bool CanExecuteNextMeetingToResolve
    {
      get
      {
        // check if executing is allowed, i.e., validate, check if a process is running, etc. 
        // has the meeting happened yet?
        int nextMeeting = _currentMeetingIndex + 1;
        if (nextMeeting + 1 > _meetingCount)
          return false;
        else
          return true; ;
      }
    }

    private ICommand _prevMeetingToResolveCommand;
    public ICommand PreviousMeetingToResolveCmd
    {
      get
      {
        return _prevMeetingToResolveCommand ?? (_prevMeetingToResolveCommand = new RelayCommand(() => PreviousMeetingToResolve(), () => CanExecutePreviousMeetingToResolve));
      }
    }

    public void PreviousMeetingToResolve()
    {
      _currentMeetingIndex--;
      _currentMeetingToResolveVM = new MeetingResolutionViewModel(_meetingsToResolve[_currentMeetingIndex], _members);
      NotifyPropertyChanged(() => CurrentMeetingToResolve);
    }

    public bool CanExecutePreviousMeetingToResolve
    {
      get
      {
        // check if executing is allowed, i.e., validate, check if a process is running, etc. 
        int previousMeeting = _currentMeetingIndex - 1;
        if (previousMeeting < 0)
          return false;
        else
          return true; ;
      }
    }

    private ICommand _saveMeetingCommand;
    public ICommand SaveMeetingCmd
    {
      get
      {
        return _saveMeetingCommand ?? (_saveMeetingCommand = new RelayCommand(() => SaveMeeting(), () => CanExecuteSaveMeeting));
      }
    }

    public void SaveMeeting()
    {
      // save all? or just mark as resolved and save when we close?
      //_currentMeetingToResolveVM
      // 
    }

    public bool CanExecuteSaveMeeting
    {
      get
      {
          return true; ;
      }
    }
  }
}
