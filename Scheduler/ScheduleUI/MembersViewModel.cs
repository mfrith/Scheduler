using SchedulerUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Windows.Input;

using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Converters;
using System.Runtime.InteropServices;

namespace ScheduleUI
{
  class MembersViewModel : PropertyChangedBase
  {

    private string _home = string.Empty;
    public MembersViewModel(string location)
    {
      _home = location;
    }

    // the only members list
    private List<MemberModel> _members = new List<MemberModel>();
    
    public List<MemberModel> Members
    {
      get
      { return _members; }

      set {; }
    }

    private ICommand _saveMembersCommand;
    public ICommand SaveMembersCmd
    {
      get { return _saveMembersCommand ?? (_saveMembersCommand = new RelayCommand(() => SaveMembers(), () => true)); }
    }

    public void SaveMembers()
    {
      Save();
    }
    public bool CanEditMember
    {
      get { return string.IsNullOrEmpty(MemberToEdit) ? false : true; }
    }

   
    public string MemberToEdit
    {
      get; set;
    }
    private ICommand _editMemberCommand;
    public ICommand EditMemberCmd
    {
      get
      {
        return _editMemberCommand ?? (_editMemberCommand = new RelayCommand(() => EditMember(), () => CanEditMember));
      }
    }

    private MemberInfoViewModel _currentMemberVM = null;
    public void EditMember()
    {
      // show properties we want to show
      string t = MemberToEdit;
      _currentMemberEdit = _members.Where(it => it.Name == t).First();
      NotifyPropertyChanged(() => MemberSet);

      _currentMemberVM = new MemberInfoViewModel(_currentMemberEdit);

      NotifyPropertyChanged(() => CurrentMemberEdit);
    }

    private readonly List<string> _meetingRoles = new List<string>(new string[] {"Toastmaster","Speaker","General Evaluator",
                                                                  "Evaluator", "Table Topics", "Ah Counter",
                                                                  "Timer", "Grammarian", "Quiz Master", "Video", "Hot Seat" });
    public List<string> Roles
    {
      get { return _meetingRoles; }
    }
    private MemberModel _mme;
    public MemberModel SetMemberRoleStatus
    {
      get { return _mme; }
      set { SetProperty(ref _mme, value, () => SetMemberRoleStatus);

        //NotifyPropertyChanged(() => ShowMemberRoles);
      }
    }
   // public 
    public MemberInfoViewModel CurrentMemberEdit
    {
      get { return _currentMemberVM; }
      //set { }
    }
    private MemberModel _currentMemberEdit;

    public bool MemberSet
    {
      get { return _currentMemberEdit == null ? false : true; }
    }
    //public List<MemberModel> MemberList
    //{
    //  get { return _memberList; }
    //}
    // move these to class view model loading?
    public void Load()
    {
      List<MemberModel> t = new List<MemberModel>();
      //var reader = new JsonTextReader(new StreamReader("c:\\users\\mike\\documents\\ti\\membersstatus.json", Encoding.GetEncoding(1251)));
      //reader.SupportMultipleContent = true;
      //var serializer = new JsonSerializer();

      //while (reader.Read())
      //{
      //  MemberModel themember = new MemberModel();

      //  var b = serializer.Deserialize<string>(reader);
      //}

      if (!Directory.Exists(_home + "\\Data"))
        Directory.CreateDirectory(_home + "\\Data");

      FileStream fs = null;
      if (!File.Exists(_home + "\\Data\\MembersStatus0 - Copy.json"))
      {
        fs = File.Create(_home + "\\Data\\MembersStatus0 - Copy.json");
        fs.Close();
      }

      string json = File.ReadAllText(_home + "\\Data\\MembersStatus0 - Copy.json");
      var memberlist = JsonConvert.DeserializeObject<List<MemberModel>>(json);
      if (memberlist == null)
        _members = new List<MemberModel>();
      else
        _members = new List<MemberModel>(memberlist);

      //using (StreamReader strmreader = new StreamReader("c:\\users\\mike\\documents\\ti\\data\\MembersStatus.json"))
      //{
      //  string member = string.Empty;

      //  while ((member = strmreader.ReadLine()) != null)
      //  {
      //    MemberModel themember = new MemberModel();
      //    var a = themember.Deserialize(member);
      //    t.Add(a);

      //  }
      //  strmreader.Close();
      //  //var e = t.Where(it => it.Name != "Lisa Winn" && it.Name != "Mike Frith");
      //  //List<MemberModel> c = t.Where(it => it.Name != "Mike Frith" && it.Name != "Lisa Winn").ToList();
      //  //List<MemberModel> b = c.Where(it => it.Name != "Lisa Winn").ToList();//.Where(it => it.Name != "Mike Frith");
      //  //_members.Select(it =>)
      //  //_members.Where(it => it.Name != "Lisa Winn");
      //  _members = new List<MemberModel>(t);
      //}

      //File.WriteAllText("C:\\Users\\mike\\Documents\\TI\\Data\\MembersStatus0.json", JsonConvert.SerializeObject(_members));

      //t = JsonConvert.DeserializeObject<List<MemberModel>>(File.ReadAllText("C:\\Users\\mike\\Documents\\TI\\MembersStatus.json"));
      //List<MeetingModelBase> theList = new List<MeetingModelBase>();
      //using (StreamReader strmReader = new StreamReader("C:\\Users\\mike\\Documents\\TI\\Meetings4.json"))//, FileMode.Open, FileAccess.Read))
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

      //  _meetings = new ObservableCollection<MeetingModelBase>(t);

      //}
    }

    private void Save()
    {
      if (File.Exists("C:\\Users\\mike\\Documents\\TI\\MembersStatus.json"))
      {
        //File.Delete("C:\\Users\\mike\\Documents\\TI\\MembersStatus.json");
        
      }
      using (StreamWriter strmWriter = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\MembersStatus.json"))
      {
        // write out all objects(members)
        string member = string.Empty;
        List<MemberModel> SortedList = _members.OrderBy(o => o.Name).ToList();

        foreach (var m in SortedList)
        {
          member = m.Serialize(m);
          strmWriter.WriteLine(member);
        }
      }

    }
  }
}
