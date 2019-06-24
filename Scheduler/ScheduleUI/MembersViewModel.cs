using SchedulerUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ScheduleUI
{
  class MembersViewModel
  {
    public MembersViewModel()
    {

    }

    private ObservableCollection<MemberModel> _members = new ObservableCollection<MemberModel>();
    private List<MemberModel> _memberList = null;
    public ObservableCollection<MemberModel> Members
    {
      get
      { return _members; }

      set {; }
    }

    public List<MemberModel> MemberList
    {
      get { return _memberList; }
    }
    // move these to class view model loading?
    public void Load()
    {
      // File.ReadAllText 

      //System.IO.FileStream fileStream = new FileStream("C:\\Users\\mike\\Documents\\TI\\Members.txt", FileMode.Open, FileAccess.Read);
      StreamReader strmReader = new StreamReader("C:\\Users\\mike\\Documents\\TI\\Members.csv");
      string firstLine = strmReader.ReadLine();
      string line;
      char[] delims = new char[] { ',' };
      while ((line = strmReader.ReadLine()) != null)
      {
        string[] pole = line.Split(delims, StringSplitOptions.None);
        MemberModel rcd = new MemberModel(pole);
        _members.Add(rcd);

      }
      strmReader.Close();
      _memberList = _members.ToList();
      //fileStream.Close();
    }

    private void Save()
    {
      System.IO.StreamWriter file = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\Members.txt");


    }
  }
}
