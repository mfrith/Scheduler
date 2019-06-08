using SchedulerUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleUI
{
  public class MeetingsViewModel : PropertyChangedBase
  {
    private ObservableCollection<MeetingModel> _meetings;// = new ObservableCollection<MeetingModel>();
    private ObservableCollection<MemberModel> _members = new ObservableCollection<MemberModel>();

    List<string> regularTemplate = new List<string>(new string[] {"Toastmaster","Speaker 1","Speaker 2","General Evaluator",
                                                                  "Evaluator 1", "Evaluator 2", "Table Topics", "Ah Counter",
                                                                  "Timer", "Grammarian", "Quiz Master", "Video", "Hot Seat" });

    //List<string> threeSpeakerTemplate = new List<string>(new string[] {"Toastmaster","Speaker 1","Speaker 2", "Speaker 3", "General Evaluator",
    //                                                              "Evaluator 1", "Evaluator 2", "Evaluator 3", "Ah Counter",
    //                                                              "Timer", "Grammarian", "Quiz Master", "Video", "Hot Seat" });

    //List<string> speakathonTemplate = new List<string>(new string[] {"Toastmaster","Speaker 1","Speaker 2", "Speaker 3", "Speaker 4",
    //                                                              "Speaker 5", "General Evaluator", "Evaluator 1", "Evaluator 2", "Evaluator 3",
    //                                                              "Evaluator 4", "Evaluator 5", "Ah Counter",
    //                                                              "Timer", "Grammarian", "Quiz Master", "Video", "Hot Seat" });

    //private static readonly KeyValuePair<string, string>[] meetingTemplates =
    //{
    //    new KeyValuePair<string, string>("regularTemplate","Regular Meeting"),
    //    new KeyValuePair<string, string>("threeSpeakerTemplate", "Three Speaker Meeting"),
    //    new KeyValuePair<string, string>("speakathonTemplate", "Speakathon")
    //};
    //public KeyValuePair<string, string>[] MeetingTemplates
    //{
    //  get
    //  {
    //    return meetingTemplates;
    //  }
    //}
    private DateTime _meetingDate = DateTime.Now;
    public DateTime MeetingDate
    {
      get { return _meetingDate; }
      set { _meetingDate = value; }
    }
    private List<string> _meetingTemplates = new List<string>(new string[] { "Regular Meeting", "Three Speaker Meeting", "Speakathon" });
    public List<string> MeetingTemplates
    {
      get
      {
        return _meetingTemplates;
      }
    }

    public List<string> CurrentMeetingTemplate
    {
      get { return regularTemplate; }
    }
    private string _meetingTemplate;
    public string MeetingTemplate
    {
      get
      {
        return "regular meeting";
      }

      set
      {
        _meetingTemplate = value;
      }
    }
    public MeetingsViewModel()
    {

    }

    //public bool CanExecute
    //{
    //  get { return this.canExecute; }
    //  set
    //  {
    //    if (this.canExecute == value)
    //    { return; }
    //    this.canExecute = value;
    //  }
    //}

    //private ICommand _generateMeeting;
    //public ICommand GenerateMeetingCmd
    //{
    //  get
    //  {
    //    if (_generateMeeting == null)
    //      _generateMeeting = new RelayCommand(GenerateMeeting);
    //    //_generateMeeting = new RelayCommand(GenerateMeeting, param => this.canExecute);
    //    return _generateMeeting;
    //  }
    //}

    //private bool canExecute = true;

    private ICommand _clickCommand;
    public ICommand GenerateMeetingCmd
    {
      get
      {
        return _clickCommand ?? (_clickCommand = new RelayCommand(() => GenerateMeeting(), () => CanExecute));
      }
    }
    public bool CanExecute
    {
      get
      {
        // check if executing is allowed, i.e., validate, check if a process is running, etc. 
        return true; ;
      }
    }

    public void MyAction()
    {

    }
    private List<string> _currentMeeting;
    public List<string> CurrentMeeting
    {
      get { return _currentMeeting; }
      set { SetProperty(ref _currentMeeting, value, () => CurrentMeeting); }
    }
    public void GenerateMeeting()
    {
      var newMeeting = new MeetingViewModel(MeetingDate, MeetingTemplate, _members);
      newMeeting.Generate();
      CurrentMeeting = newMeeting.ToList();
      

      return;
    }
    public MeetingsViewModel(ObservableCollection<MemberModel> members)
    {
      _members = members;
    }

    private bool _showMeeting;
    public bool ShowMeeting
    {
      get { return _showMeeting; }
      set { _showMeeting = value; }
    }
    public ObservableCollection<MeetingModel> Meetings
    {
      get { return _meetings; }
    }
    public void Load()
    {
      System.IO.FileStream fileStream = new FileStream("C:\\Users\\mikef\\Documents\\Meetings.txt", FileMode.Open, FileAccess.Read);
      StreamReader strmReader = new StreamReader(fileStream);
      string firstLine = strmReader.ReadLine();
      string line;
      char[] delims = new char[] { ',' };
      List<MeetingModel> theList = new List<MeetingModel>();
      while ((line = strmReader.ReadLine()) != null)
      {
        string[] pole = line.Split(delims, StringSplitOptions.None);
        MeetingModel rcd = new MeetingModel(pole, ref _members);

        theList.Add(rcd);
      }

      theList.Sort((x, y) => DateTime.Compare(x.DayOfMeeting, y.DayOfMeeting));
      theList.Reverse();
      _meetings = new ObservableCollection<MeetingModel>(theList);
    }

    public void Save()
    {

    }
  }
}
