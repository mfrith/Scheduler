using SchedulerUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Windows.Input;

//using Newtonsoft.Json;
using System.Text;

namespace ScheduleUI
{
  class MembersViewModel : PropertyChangedBase
  {
    public MembersViewModel()
    {

    }

    private List<MemberModel> _members = new List<MemberModel>();
    //private List<MemberModel> _memberList = null;
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


    public void EditMember()
    {
      // show properties we want to show
      string t = MemberToEdit;
      _currentMemberEdit = _members.Where(it => it.Name == t).First();
      NotifyPropertyChanged(() => MemberSet);

      MemberInfoViewModel currentMemberVM = new MemberInfoViewModel(_currentMemberEdit);

      //_currentMemberEdit = a.First();
    }

    public MemberModel CurrentMemberEdit
    {
      get { return _currentMemberEdit; }
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
      //var reader = new JsonTextReader(new StreamReader("c:\\users\\mike\\documents\\ti\\membersstatus1.json", Encoding.GetEncoding(1251)));
      //reader.SupportMultipleContent = true;
      //var serializer = new JsonSerializer();

      //while (reader.Read())
      //{
      //  MemberModel themember = new MemberModel();

      //  var b = serializer.Deserialize<string>(reader);
      //}

      using (StreamReader strmreader = new StreamReader("c:\\users\\mike\\documents\\ti\\MembersStatus.json"))
      {
        string member = string.Empty;

        while ((member = strmreader.ReadLine()) != null)
        {
          MemberModel themember = new MemberModel();
          var a = themember.Deserialize(member);
          t.Add(a);

        }
        strmreader.Close();
        //  //var e = t.Where(it => it.Name != "Lisa Winn" && it.Name != "Mike Frith");
        //  //List<MemberModel> c = t.Where(it => it.Name != "Mike Frith" && it.Name != "Lisa Winn").ToList();
        //  //List<MemberModel> b = c.Where(it => it.Name != "Lisa Winn").ToList();//.Where(it => it.Name != "Mike Frith");
        //  //_members.Select(it =>)
        //  //_members.Where(it => it.Name != "Lisa Winn");
        _members = new List<MemberModel>(t);
        //}

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

      }
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
