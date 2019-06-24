using SchedulerUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace ScheduleUI
{
  public class MeetingsViewModel : PropertyChangedBase
  {
    private ObservableCollection<MeetingModel> _meetings;// = new ObservableCollection<MeetingModel>();
    private ObservableCollection<MemberModel> _members = new ObservableCollection<MemberModel>();
    private ObservableCollection<MemberModel> _temporarymemberList;

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

    private ICommand _generateCommand;
    public ICommand GenerateMeetingCmd
    {
      get
      {
        return _generateCommand ?? (_generateCommand = new RelayCommand(() => GenerateMeeting(), () => CanExecuteGenerate));
      }
    }
    public bool CanExecuteGenerate
    {
      get
      {
        // check if executing is allowed, i.e., validate, check if a process is running, etc. 
        return true; ;
      }
    }

    private ICommand _resetCommand;
    public ICommand ResetCmd
    {
      get
      {
        return _resetCommand ?? (_resetCommand = new RelayCommand(() => ResetMeeting(), () => CanExecuteReset));
      }
    }
    public bool CanExecuteReset
    {
      get
      {
        // check if executing is allowed, i.e., validate, check if a process is running, etc. 
        return true; ;
      }
    }

    private ICommand _okCmd;
    public ICommand OKCmd
    {
      get
      {
        return _okCmd ?? (_okCmd = new RelayCommand(() => OK(), () => CanExecuteOK));
      }
    }

    public bool CanExecuteOK
    {
      get
      {
        // check if executing is allowed, i.e., validate, check if a process is running, etc. 
        return true; ;
      }
    }

    public void OK()
    {
      //push values into meeting model and save

      _newMeeting.Save(_listofmeetingids.Max() + 1);
      //var json =  JsonSerializer.ToString<MeetingModel>(_newMeeting);
      // update member info for last time being role
    }
    public void ResetMeeting()
    {
      CurrentMeeting = null;
      _generateButtonEnabled = true;
      _roleListVisible = false;
      List<MemberModel> temp = _members.ToList();
      _temporarymemberList = new ObservableCollection<MemberModel>(temp);
      NotifyPropertyChanged(() => RoleListVisible);
      NotifyPropertyChanged(() => GenerateButtonEnabled);
      NotifyPropertyChanged(() => ResetButtonEnabled);

    }
    private List<string> _currentMeeting;
    public List<string> CurrentMeeting
    {
      get { return _currentMeeting; }
      set { SetProperty(ref _currentMeeting, value, () => CurrentMeeting); }
    }

    private MeetingViewModel _newMeeting;
    public void GenerateMeeting()
    {
      var t = _members.Where(it => it.MemberID > 0 && it.Name != "Mike Frith");
      List<MemberModel> temp = t.ToList();
      _temporarymemberList = new ObservableCollection<MemberModel>(temp);
      _newMeeting = new MeetingViewModel(MeetingDate, MeetingTemplate, _temporarymemberList);
      _newMeeting.Generate();
      CurrentMeeting = _newMeeting.ToList();
      _generateButtonEnabled = false;
      _roleListVisible = true;
      NotifyPropertyChanged(() => RoleListVisible);
      NotifyPropertyChanged(() => GenerateButtonEnabled);
      NotifyPropertyChanged(() => ResetButtonEnabled);
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

    private List<int> _listofmeetingids = new List<int>();
    private ObservableCollection<MeetingModelRegular> _meetingsRegular = null;
    public void Load()
    {
      List<MeetingModel> theList = new List<MeetingModel>();
      using (StreamReader strmReader = new StreamReader("C:\\Users\\mike\\Documents\\TI\\Meetings.dat"))//, FileMode.Open, FileAccess.Read))
      {
        //StreamReader strmReader = new StreamReader(fileStream);
        string firstLine = strmReader.ReadLine();
        string line;
        char[] delims = new char[] { ',' };
        while ((line = strmReader.ReadLine()) != null)
        {
          string[] pole = line.Split(delims, StringSplitOptions.None);
          MeetingModel rcd = new MeetingModel(pole, ref _members);

          theList.Add(rcd);
          _listofmeetingids.Add(Int32.Parse(pole[0]));
        }
      }

      using (StreamWriter strmWriter = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\Meetings2.json", true))
      {
        theList.Sort((x, y) => DateTime.Compare(x.DayOfMeeting, y.DayOfMeeting));
        theList.Reverse();
        _meetings = new ObservableCollection<MeetingModel>(theList);
        _meetingsRegular = new ObservableCollection<MeetingModelRegular>();
        foreach (var meeting in _meetings)
        {

          MeetingModelRegular mreg = new MeetingModelRegular();
          mreg.AhCounter = meeting.Ah?.Name;
          //mreg.Attendees = meeting.Attendees.Select(it => it.ToString();
          mreg.DayOfMeeting = meeting.DayOfMeeting.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
          mreg.Evaluator1 = meeting.Evaluator1?.Name;
          mreg.Evaluator2 = meeting.Evaluator2?.Name;
          mreg.GeneralEvaluator = meeting.GeneralEvaluator?.Name;
          mreg.Grammarian = meeting.Gram?.Name;
          mreg.HotSeat = meeting.HotSeat?.Name;
          mreg.MeetingType = meeting.MeetingType.ToString();
          mreg.QuizMaster = meeting.Quiz?.Name;
          mreg.Resolved = meeting.Resolved == true ? "1" : "0";
          mreg.Speaker1 = meeting.Speaker1?.Name;
          mreg.Speaker2 = meeting.Speaker2?.Name;
          mreg.TableTopics = meeting.TT?.Name;
          mreg.Timer = meeting.Timer?.Name;
          mreg.Toastmaster = meeting.Toastmaster?.Name;
          //mreg.TTContestants = meeting.TTContestants?.ToString();
          mreg.TTWinner = meeting.TTWinner?.Name;
          mreg.Video = meeting.Video?.Name;
          _meetingsRegular.Add(mreg);
          //var json = JsonSerializer.ToString<MeetingModelRegular>(mreg);
          var t = mreg.Serialize(mreg);
          strmWriter.WriteLine(t);
        }

        //strmWriter.Close();
      }
    }


    public void Save()
    {

    }

    private bool _generateButtonVisibility = false;
    public bool GenerateButtonVisibility
    {
      get { return _generateButtonVisibility; }
      set { SetProperty(ref _generateButtonVisibility, value, () => GenerateButtonVisibility); }
    }

    private bool _generateButtonEnabled = true;
    public bool GenerateButtonEnabled
    {
      get { return _generateButtonEnabled; }
      set { SetProperty(ref _generateButtonEnabled, value, () => GenerateButtonEnabled); }
    }

    public bool ResetButtonEnabled
    {
      get
      {
        bool enabled = !_generateButtonEnabled;
        return enabled;
      }
    }

    private bool _roleListVisible = false;
    public bool RoleListVisible
    {
      get { return _roleListVisible; }
      set { SetProperty(ref _roleListVisible, value, () => RoleListVisible); }
    }

  }
}
