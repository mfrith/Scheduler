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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ScheduleUI
{
  public class MeetingsViewModel : PropertyChangedBase
  {
    private ObservableCollection<MeetingModelBase> _meetings;// = new ObservableCollection<MeetingModel>();
    private List<MemberModel> _members = new List<MemberModel>();
    private List<MemberModel> _temporarymemberList;

    List<string> regularTemplate = new List<string>(new string[] {"Toastmaster","Speaker 1","Speaker 2","General Evaluator",
                                                                  "Evaluator 1", "Evaluator 2", "Table Topics", "Ah Counter",
                                                                  "Timer", "Grammarian", "Quiz Master", "Video", "Hot Seat" });

    List<string> threeSpeakerTemplate = new List<string>(new string[] {"Toastmaster","Speaker 1","Speaker 2", "Speaker 3", "General Evaluator",
                                                                  "Evaluator 1", "Evaluator 2", "Evaluator 3", "Ah Counter",
                                                                  "Timer", "Grammarian", "Quiz Master", "Video", "Hot Seat" });

    List<string> speakathonTemplate = new List<string>(new string[] {"Toastmaster","Speaker 1","Speaker 2", "Speaker 3", "Speaker 4",
                                                                  "Speaker 5", "General Evaluator", "Evaluator 1", "Evaluator 2", "Evaluator 3",
                                                                  "Evaluator 4", "Evaluator 5", "Ah Counter",
                                                                  "Timer", "Grammarian", "Quiz Master", "Video", "Hot Seat" });

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

    private bool _generateForMonth = true;
    public bool GenerateForMonth
    {
      get { return _generateForMonth; }
      set { _generateForMonth = value;
        NotifyPropertyChanged(() => GenerateForMonth);
        NotifyPropertyChanged(() => IsGenerateForMonth);
      }
    }

    private bool _generateForFriday = true;
    public bool GenerateForFriday
    {
      get { return _generateForFriday; }
      set
      {
        _generateForFriday = value;
        NotifyPropertyChanged(() => GenerateForFriday);
        //NotifyPropertyChanged(() => IsGenerateForMonth);
      }
    }
    //ItemsSource="{Binding Months}" SelectedIndex="0" SelectedItem="{Binding MonthToGenerate}"/>
    private string _monthToGenerateFor = "January";

    private List<string> _months = new List<string>(new string[] { "January", "February", "March","April","May","June","July","August","September","October","November","December"});

    public string MonthToGenerateFor
    {
      get { return _monthToGenerateFor; }
      set { _monthToGenerateFor = value; }
    }
    public List<string> Months
    {
      get { return _months; }
      set {; }
    }
    public bool IsGenerateForMonth
    {
      get { return !_generateForMonth; }
      set {; }
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
    private string _meetingTemplate = "Regular Meeting";
    public string MeetingTemplate
    {
      get
      {
        return _meetingTemplate;
      }

      set
      {
        _meetingTemplate = value;
      }
    }

    //          <ComboBox DockPanel.Dock="Left" ItemsSource="{Binding MeetingTemplateEnumValues}" SelectedIndex="0" SelectedItem="{Binding MeetingTemplateEnum}"/>

    //public MeetingTypeEnum
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
      _temporarymemberList = new List<MemberModel>(temp);
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

    private MeetingModelRegularVM _newMeeting;
    public void GenerateMeeting()
    {
      //_meetingDate
      var a = _meetingTemplate;
      var b = _meetingDate;

      // this won't work the way I want it to.
      //MeetingModelBase c = new MeetingModelBase(MeetingTemplate, MeetingDate);
      // need to create class depending on template specified

      //if (MeetingTemplate == )
      var t = _members.Where(it => it.IsCurrentMember == true);
      //t = _members.Where(it => it.Name != "Lisa Winn");
      List<MemberModel> temp = t.ToList();
      _temporarymemberList = new List<MemberModel>(temp);
      List<MeetingModelRegular> list = null;
      if (GenerateForMonth)
      {
        // generate one meeting at a time and show on pane
        // write them out when okayed, then show the next one
        _newMeeting = new MeetingModelRegularVM(_temporarymemberList);
        _newMeeting.Month = MonthToGenerateFor;
        list = _newMeeting.GenerateForMonth(GenerateForFriday);
        

      }
      else
      {
        _newMeeting = new MeetingModelRegularVM(MeetingDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture), MeetingTemplate, _temporarymemberList);
        _newMeeting.Generate();
        CurrentMeeting = _newMeeting.ToList();
        _generateButtonEnabled = false;
        _roleListVisible = true;
        NotifyPropertyChanged(() => RoleListVisible);
        NotifyPropertyChanged(() => GenerateButtonEnabled);
        NotifyPropertyChanged(() => ResetButtonEnabled);
      }
      // _members.Sort();
      IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
      timeFormat.DateTimeFormat = "yyyy-MM-dd";
      File.WriteAllText("C:\\Users\\mike\\Documents\\TI\\Data\\MembersStatus0.json", JsonConvert.SerializeObject(_members, timeFormat));
      
      return;
    }
    public MeetingsViewModel(List<MemberModel> members)
    {
      _members = members;
    }

    private bool _showMeeting;
    public bool ShowMeeting
    {
      get { return _showMeeting; }
      set { _showMeeting = value; }
    }
    public ObservableCollection<MeetingModelBase> Meetings
    {
      get { return _meetings; }
    }

    private List<int> _listofmeetingids = new List<int>();
    private ObservableCollection<MeetingModelRegular> _meetingsRegular = null;
    public void Load()
    {
      //List<MeetingModel> theList = new List<MeetingModel>();
      //using (StreamReader strmReader = new StreamReader("C:\\Users\\mike\\Documents\\TI\\Meetings.dat"))//, FileMode.Open, FileAccess.Read))
      //{
      //  //StreamReader strmReader = new StreamReader(fileStream);
      //  string firstLine = strmReader.ReadLine();
      //  string line;
      //  char[] delims = new char[] { ',' };
      //  while ((line = strmReader.ReadLine()) != null)
      //  {
      //    string[] pole = line.Split(delims, StringSplitOptions.None);
      //    MeetingModel rcd = new MeetingModel(pole, ref _members);

      //    theList.Add(rcd);
      //    _listofmeetingids.Add(Int32.Parse(pole[0]));
      //  }
      //}

      List<MeetingModelBase> theList = new List<MeetingModelBase>();

      // next 3 lines for reading and writing
      string json = File.ReadAllText("C:\\Users\\mike\\Documents\\TI\\Data\\Meetings5.json");
      var meetingList = JsonConvert.DeserializeObject<List<MeetingModelBase>>(json);
      _meetings = new ObservableCollection<MeetingModelBase>(meetingList);

      //File.WriteAllText("myobjects.json", JsonConvert.SerializeObject(playerList));


      //using (StreamReader strmReader = new StreamReader("C:\\Users\\mike\\Documents\\TI\\Data\\Meetings4.json"))//, FileMode.Open, FileAccess.Read))
      //{
      //  //var thefile = strmReader.ReadToEnd();
      //  var t = new List<MeetingModelBase>();
      //  string meeting;
      //  while ((meeting = strmReader.ReadLine()) != null)
      //  {
      //    MeetingModelBase bah = new MeetingModelBase();
      //    var a = bah.Deserialize(meeting);
      //    t.Add(a);
      //  }

      //  //t.Reverse();
      //  _meetings = new ObservableCollection<MeetingModelBase>(t);

      //}

      //File.WriteAllText("C:\\Users\\mike\\Documents\\TI\\Data\\Meetings5.json", JsonConvert.SerializeObject(_meetings));
      //using (StreamWriter strmWriter = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\Meetings5.json", true))
      //{
      //  //theList.Sort((x, y) => DateTime.Compare(x.DayOfMeeting, y.DayOfMeeting));
      //  //theList.Reverse();
      //  _meetings = new ObservableCollection<MeetingModel>(theList);
      //  _meetingsRegular = new ObservableCollection<MeetingModelRegular>();
      //  foreach (var meeting in _meetings)
      //  {

      //    MeetingModelRegular mreg = new MeetingModelRegular();
      //    mreg.AhCounter = meeting.Ah?.Name;
      //    //mreg.Attendees = meeting.Attendees?.Select(it => it == ).ToList();
      //    mreg.DayOfMeeting = meeting.DayOfMeeting.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
      //    mreg.Evaluator1 = meeting.Evaluator1?.Name;
      //    mreg.Evaluator2 = meeting.Evaluator2?.Name;
      //    mreg.GeneralEvaluator = meeting.GeneralEvaluator?.Name;
      //    mreg.Grammarian = meeting.Gram?.Name;
      //    mreg.HotSeat = meeting.HotSeat?.Name;
      //    mreg.MeetingType = meeting.MeetingType.ToString();
      //    mreg.QuizMaster = meeting.Quiz?.Name;
      //    mreg.Resolved = meeting.Resolved == true ? "1" : "0";
      //    mreg.Speaker1 = meeting.Speaker1?.Name;
      //    mreg.Speaker2 = meeting.Speaker2?.Name;
      //    mreg.TableTopics = meeting.TT?.Name;
      //    mreg.Timer = meeting.Timer?.Name;
      //    mreg.Toastmaster = meeting.Toastmaster?.Name;
      //    //mreg.TTContestants = meeting.TTContestants?.ToString();
      //    mreg.TTWinner = meeting.TTWinner?.Name;
      //    mreg.Video = meeting.Video?.Name;
      //    mreg.ID = meeting.ID.ToString();
      //    _meetingsRegular.Add(mreg);
      //    //var json = JsonSerializer.ToString<MeetingModelRegular>(mreg);
      //    var t = mreg.Serialize(mreg);
      //    strmWriter.WriteLine(t);
      //  }
      //  //WriteMeetingToFile("C:\\Users\\mike\\Documents\\TI\\Meetings4.txt", _meetingsRegular[50]);


      //  strmWriter.Close();

      //}

    }

    private void WriteMeetingToFile(string path, MeetingModelBase meeting)
    {
      System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
      using (FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write))
      {
        formatter.Serialize(stream, meeting);
      }
    }
    public void Save()
    {
      if (File.Exists("C:\\Users\\mike\\Documents\\TI\\Meetings.json"))
      {
        //File.Delete("C:\\Users\\mike\\Documents\\TI\\MembersStatus.json");

      }
      using (StreamWriter strmWriter = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\Meetings2.json"))
      {
        // write out all objects(members)
        string meeting = string.Empty;
        foreach (var m in _meetings)
        {
          meeting = m.Serialize(m);
          strmWriter.WriteLine(meeting);
        }

        //var thefile = strmReader.ReadToEnd();
        var t = new List<MeetingModelBase>();
        
          MeetingModelBase bah = new MeetingModelBase();
          var a = bah.Deserialize(meeting);
          t.Add(a);
       

        t.Reverse();
        _meetings = new ObservableCollection<MeetingModelBase>(t);

      }
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
