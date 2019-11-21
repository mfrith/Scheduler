using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;



namespace Scheduler
{
  /// <summary>
  /// Interaction logic for Home.xaml
  /// </summary>
  public partial class Home : Page
  {
    private List<MemberModel> _membersOriginal = new List<MemberModel>();

    public Home()
    {
      InitializeComponent();
    }
    private void GetMembers(ref List<MemberModel> members)
    {
      if (_membersOriginal.Count > 40)
      {
        members = _membersOriginal.ToList();
        return;
      }

      SqlConnection conn = new SqlConnection();
      conn.ConnectionString = @"Server=.\SQLEXPRESS;Database=Scheduler;Integrated Security=true;";
      DataSet dsMembers = new DataSet();
      SqlDataAdapter daMembers = new SqlDataAdapter("select * from Members where memberID > 0", conn);
      daMembers.Fill(dsMembers);
      DataTable dtMembers = dsMembers.Tables["Table"];
      foreach (DataRow rowMember in dtMembers.Rows)
      {
        MemberModel theMember = new MemberModel();
        theMember.MemberID = (int)rowMember.ItemArray[1];
        theMember.Name = rowMember.ItemArray[2].ToString();

        if (theMember.Name == "Mike Frith" || theMember.Name == "Lisa Winn")
            continue;

        if (!System.DBNull.Value.Equals(rowMember.ItemArray[3]))
          theMember.Toastmaster = (DateTime)rowMember.ItemArray[3];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[4]))
          theMember.Speaker = (DateTime)rowMember.ItemArray[4];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[5]))
          theMember.GeneralEvaluator = (DateTime)rowMember.ItemArray[5];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[6]))
          theMember.Evaluator = (DateTime)rowMember.ItemArray[6];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[7]))
          theMember.TT = (DateTime)rowMember.ItemArray[7];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[8]))
          theMember.Ah = (DateTime)rowMember.ItemArray[8];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[9]))
          theMember.Gram = (DateTime)rowMember.ItemArray[9];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[10]))
          theMember.Timer = (DateTime)rowMember.ItemArray[10];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[11]))
          theMember.Quiz = (DateTime)rowMember.ItemArray[11];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[12]))
          theMember.Video = (DateTime)rowMember.ItemArray[12];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[13]))
          theMember.HotSeat = (DateTime)rowMember.ItemArray[13];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[14]))
          theMember.HasBeenOfficer = (bool)rowMember.ItemArray[14];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[15]))
          theMember.CanBeEvaluator = (bool)rowMember.ItemArray[15];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[16]))
          theMember.CanBeToastmaster = (bool)rowMember.ItemArray[16];
        if (!System.DBNull.Value.Equals(rowMember.ItemArray[17]))
        {
          string daysStr = rowMember.ItemArray[17].ToString();
          if (string.IsNullOrWhiteSpace(daysStr))
          {
            theMember.MeetingsOut = new List<int>(0);
            _membersOriginal.Add(theMember);
            continue;
          }
          string[] days = daysStr.Trim().Split(',');
          List<int> daysMissing = days.Select(int.Parse).ToList();
          theMember.MeetingsOut = daysMissing;

        }
        else
          theMember.MeetingsOut = new List<int>(0);
        _membersOriginal.Add(theMember);
      }

      members = _membersOriginal.ToList();

    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {

      List<MemberModel> members= null;// = new List<MemberModel>();

      // this builds an array of the wednesdays in the month.
      // need to work on adding the last friday to the array.
      // then I can just pull the meetings missed in DateTime format

      //DateTime today = DateTime.Today;
      //DateTime a = new DateTime(2018, 11, 1);
      //int daysUntilFirstWednesday = ((int)DayOfWeek.Wednesday - (int)a.DayOfWeek + 7) % 7;
      //DateTime b = a.AddDays(daysUntilFirstWednesday);
      //DateTime d = a.AddDays(daysUntilFirstWednesday + 7);
      //DateTime f = a.AddDays(daysUntilFirstWednesday + 14);
      //DateTime g = a.AddDays(daysUntilFirstWednesday + 21);
      //DateTime h = a.AddDays(daysUntilFirstWednesday + 28);
      //var daysinmonth = DateTime.DaysInMonth(2018, 11);
      //DateTime c = new DateTime(2018, 11, daysinmonth);
      //DayOfWeek r = c.DayOfWeek;
      //if (h >= c)
      //{
      //  string t = "don't add to array";
      //}

      //MeetingModel one = new MeetingModel();
      //DataSet dsMembers = new DataSet();
      //SqlDataAdapter daMembers = new SqlDataAdapter("select * from Members where memberID > 0", conn);
      //daMembers.Fill(dsMembers);
      //DataTable dtMembers = dsMembers.Tables["Table"];
      //foreach (DataRow rowMember in dtMembers.Rows)
      //{
      //  MemberModel theMember = new MemberModel();
      //  theMember.MemberID = (int)rowMember.ItemArray[1];
      //  theMember.Name = rowMember.ItemArray[2].ToString();
      //  if (theMember.Name == "Mike Frith" || theMember.MemberID == 0 || theMember.Name == "Abhijit Roy" || theMember.Name == "Na Zhang" || theMember.Name == "Lisa Winn" 
      //                                                                || theMember.Name == "Wes Jones" || theMember.Name == "Justin Fan" || theMember.Name == "Suzanne Boden" 
      //                                                                || theMember.Name == "Christine Ma" || theMember.Name == "Mohini Todkari" || theMember.Name == "Qing Shi"
      //                                                                || theMember.Name == "Jhonatan" || theMember.Name == "Sarah Powell")
      //    continue;
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[3]))
      //    theMember.Toastmaster = (DateTime)rowMember.ItemArray[3];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[4]))
      //    theMember.Speaker = (DateTime)rowMember.ItemArray[4];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[5]))
      //    theMember.GeneralEvaluator = (DateTime)rowMember.ItemArray[5];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[6]))
      //    theMember.Evaluator = (DateTime)rowMember.ItemArray[6];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[7]))
      //    theMember.TT = (DateTime)rowMember.ItemArray[7];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[8]))
      //    theMember.Ah = (DateTime)rowMember.ItemArray[8];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[9]))
      //    theMember.Gram = (DateTime)rowMember.ItemArray[9];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[10]))
      //    theMember.Timer = (DateTime)rowMember.ItemArray[10];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[11]))
      //    theMember.Quiz = (DateTime)rowMember.ItemArray[11];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[12]))
      //    theMember.Video = (DateTime)rowMember.ItemArray[12];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[13]))
      //    theMember.HotSeat = (DateTime)rowMember.ItemArray[13];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[14]))
      //    theMember.HasBeenOfficer = (bool)rowMember.ItemArray[14];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[15]))
      //    theMember.CanBeEvaluator = (bool)rowMember.ItemArray[15];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[16]))
      //    theMember.CanBeToastmaster = (bool)rowMember.ItemArray[16];
      //  if (!System.DBNull.Value.Equals(rowMember.ItemArray[17]))
      //  {
      //    string daysStr = rowMember.ItemArray[17].ToString();
      //    if (string.IsNullOrWhiteSpace(daysStr))
      //    {
      //      theMember.MeetingsOut = new List<int>(0);
      //      members.Add(theMember);
      //      continue;
      //    }
      //    string[] days = daysStr.Trim().Split(',');
      //    List<int> daysMissing = days.Select(int.Parse).ToList();
      //    theMember.MeetingsOut = daysMissing;

      //  }
      //  else
      //    theMember.MeetingsOut = new List<int>(0);
      //  members.Add(theMember);
      //}
      GetMembers(ref members);
      List<DateTime> meetings = GetMonthlyMeetings(new DateTime(2019, 10, 2));
      int NumberOfMeetings = 6;//  meetings.Count;
      List<MemberModel> speakers = GetRoles(members, NumberOfMeetings, "speaker");
      List<string> snames = new List<string>();
      foreach (var s in speakers)
      {
        snames.Add(s.Name);
      }

      //List<MemberModel> speakers = GetSpeakers(members, new DateTime(2018, 11, 7));
      List<MemberModel> evaluators = GetRoles(members, NumberOfMeetings, "evaluator");
      List<string> enames = new List<string>();
      foreach (var s in evaluators)
      {
        enames.Add(s.Name);
      }

      List<MemberModel> generalEvaluators = GetRoles(members, NumberOfMeetings, "generalevaluator");
      List<string> gnames = new List<string>();
      foreach (var s in generalEvaluators)
      {
        gnames.Add(s.Name);
      }

      List<MemberModel> toastmasters = GetRoles(members, NumberOfMeetings, "toastmaster");
      List<string> tnames = new List<string>();
      foreach (var s in toastmasters)
      {
        tnames.Add(s.Name);
      }
      List<MemberModel> hotseat = GetRoles(members, NumberOfMeetings, "hotseat");
      List<string> hnames = new List<string>();
      foreach (var s in hotseat)
      {
        hnames.Add(s.Name);
      }
      if (members.Count < NumberOfMeetings)
      {
        members.Clear();
        GetMembers(ref members);

      }
      List<MemberModel> tableTopics = GetRoles(members, NumberOfMeetings, "tabletopics");
      List<string> ttnames = new List<string>();
      foreach (var s in tableTopics)
      {
        ttnames.Add(s.Name);
      }

      if (members.Count < NumberOfMeetings)
      {
        members.Clear();
        GetMembers(ref members);
      }
     // }
      List<MemberModel> grammarians = GetRoles(members, NumberOfMeetings, "grammarian");
      List<string> grnames = new List<string>();
      foreach (var s in grammarians)
      {
        grnames.Add(s.Name);
      }
      if (members.Count < NumberOfMeetings)
      {
        members.Clear();
        GetMembers(ref members);
      }
      List<string> timernames = new List<string>();
      //if (members.Count >= 5)
      //{
        List<MemberModel> timers = GetRoles(members, NumberOfMeetings, "timer");
        foreach (var s in timers)
        {
          timernames.Add(s.Name);
        }
      // }
      if (members.Count < NumberOfMeetings)
      {
        members.Clear();
        GetMembers(ref members);
      }
      List<string> ahnames = new List<string>();

      //if (members.Count >= 5)
      //{
        List<MemberModel> ahcounters = GetRoles(members, NumberOfMeetings, "ah");
        foreach (var s in ahcounters)
        {
          ahnames.Add(s.Name);
        }
   //   }
      List<string> quiznames = new List<string>();

      //if (members.Count >= 5)
      //{
        List<MemberModel> quizmasters = GetRoles(members, NumberOfMeetings, "quiz");
        foreach (var s in quizmasters)
        {
          quiznames.Add(s.Name);
        }
     // }
      List<string> videonames = new List<string>();

      //if (members.Count >= 5)
      //{
        List<MemberModel> video = GetRoles(members, NumberOfMeetings, "video");
        foreach (var s in video)
        {
          videonames.Add(s.Name);
        }
      //}

      var t = snames.Count ;
      
      t = enames.Count;
      t = gnames.Count;
      t = tnames.Count;
      t = hnames.Count;
      t = ttnames.Count;
      t = grnames.Count;
      t = timernames.Count;
      t = ahnames.Count;
      t = quiznames.Count;
      t = videonames.Count;
      t = 0;
      //one.GenerateMeeting(members);
      //one.DayOfMeeting = new DateTime(2018, 11, 7);

      //MeetingModel two = new MeetingModel();
      //two.GenerateMeeting(members);
      //two.DayOfMeeting = new DateTime(2018, 11, 14);

      //MeetingModel three = new MeetingModel();
      //three.GenerateMeeting(members);
      //three.DayOfMeeting = new DateTime(2018, 11, 28);

      //MeetingModel four = new MeetingModel();
      //four.GenerateMeeting(members);
      //four.DayOfMeeting = new DateTime(2018, 11, 30);
      //one.Save();
      //MeetingModel four = new MeetingModel();
      //four.GenerateMeeting(members);
      //four.DayOfMeeting = new DateTime(2018, 10, 26);

      //MeetingModel five = new MeetingModel();
      //five.GenerateMeeting(members);
      //five.DayOfMeeting = new DateTime(2018, 10, 31);

      if (File.Exists("C:\\Users\\mike\\Documents\\TI\\MeetingsNext.csv"))
      {
        File.Delete("C:\\Users\\mike\\Documents\\TI\\MeetingsNext.csv");
      }

      using (StreamWriter file = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\MeetingsNext.csv"))
      {
        string dates = "Role, Oct 2, Oct 9, Oct 16, Oct 23, Oct 25, Oct 30";
        file.WriteLine(dates);
        string row1 = "Toastmaster," + tnames[0] + "," + tnames[1] + "," + tnames[2] + "," + tnames[3] + "," + tnames[4] + "," + tnames[5];
        file.WriteLine(row1);
        row1 = "Speaker 1," + snames[0] + "," + snames[2] + "," + snames[4] + "," + snames[6] + "," + snames[8] + "," + snames[10];
        file.WriteLine(row1);
        row1 = "Speaker 2," + snames[1] + "," + snames[3] + "," + snames[5] + "," + snames[7] + "," + snames[9] + "," + snames[11];
        file.WriteLine(row1);
        row1 = "GE," + gnames[0] + "," + gnames[1] + "," + gnames[2] + "," + gnames[3] + "," + gnames[4] + "," + gnames[5];
        file.WriteLine(row1);
        row1 = "Eval 1," + enames[0] + "," + enames[2] + "," + enames[4] + "," + enames[6] + "," + enames[8] + "," + enames[10];
        file.WriteLine(row1);
        row1 = "Eval 2," + enames[1] + "," + enames[3] + "," + enames[5] + "," + enames[7] + "," + enames[9] + "," + enames[11];
        file.WriteLine(row1);
        row1 = "TT," + ttnames[0] + "," + ttnames[1] + "," + ttnames[2] + "," + ttnames[3] + "," + tnames[4] + "," + tnames[5];
        file.WriteLine(row1);
        row1 = "Ah ," + ahnames[0] + "," + ahnames[1] + "," + ahnames[2] + "," + ahnames[3] + "," + ahnames[4] + "," + ahnames[5];
        file.WriteLine(row1);
        row1 = "Timer," + timernames[0] + "," + timernames[1] + "," + timernames[2] + "," + timernames[3] + "," + timernames[4] + "," + timernames[5];
        file.WriteLine(row1);
        row1 = "Gram," + grnames[0] + "," + grnames[1] + "," + grnames[2] + "," + grnames[3] + "," + grnames[4] + "," + grnames[5];
        file.WriteLine(row1);
        row1 = "Quiz," + quiznames[0] + "," + quiznames[1] + "," + quiznames[2] + "," + quiznames[3] + "," + quiznames[4] + "," + quiznames[5];
        file.WriteLine(row1);
        row1 = "Video," + videonames[0] + "," + videonames[1] + "," + videonames[2] + "," + videonames[3] + "," + videonames[4] + "," + videonames[5];
        file.WriteLine(row1);
        row1 = "HS," + hnames[0] + "," + hnames[1] + "," + hnames[2] + "," + hnames[3] + "," + hnames[4] + "," + hnames[5];
        file.WriteLine(row1);

      }
    }

    List<MemberModel> GetRoles(List<MemberModel> members, int numberOfMeetings, string role)
    {
      List<MemberModel> roles = new List<MemberModel>();
      //var speaker1 = members.OrderBy(a => a.Speaker).First();
      int i = 1;
      if (role == "speaker")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedSpeakersToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedSpeakersToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedSpeakersToBeAddedLater)
          {
            members.Remove(t);
          }


          var s = members.OrderBy(a => a.Speaker).First();
          roles.Add(s);
          members.Remove(s);
          s = members.OrderBy(a => a.Speaker).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedSpeakersToBeAddedLater)
          {
            members.Add(n);

          }

          i++;
        }

      }
      else if (role == "evaluator")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedEvaluatorsToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedEvaluatorsToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedEvaluatorsToBeAddedLater)
          {
            members.Remove(t);
          }

          var s = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
          roles.Add(s);
          members.Remove(s);
          s = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedEvaluatorsToBeAddedLater)
          {
            members.Add(n);

          }

          i++;
        }
      }
      else if (role == "generalevaluator")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedEvaluatorsToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedEvaluatorsToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedEvaluatorsToBeAddedLater)
          {
            members.Remove(t);
          }

          var s = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.GeneralEvaluator).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedEvaluatorsToBeAddedLater)
          {
            members.Add(n);
          }
          i++;
        }
      }
      else if (role == "toastmaster")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedEvaluatorsToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedEvaluatorsToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedEvaluatorsToBeAddedLater)
          {
            members.Remove(t);
          }

          var s = members.Where(a => a.CanBeToastmaster == true).OrderBy(a => a.Toastmaster).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedEvaluatorsToBeAddedLater)
          {
            members.Add(n);
          }
          i++;
        }
      }
      else if (role == "hotseat")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedEvaluatorsToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedEvaluatorsToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedEvaluatorsToBeAddedLater)
          {
            members.Remove(t);
          }

          var s = members.OrderBy(a => a.HotSeat).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedEvaluatorsToBeAddedLater)
          {
            members.Add(n);
          }
          i++;
        }
      }
      else if (role == "tabletopics")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedEvaluatorsToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedEvaluatorsToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedEvaluatorsToBeAddedLater)
          {
            members.Remove(t);
          }

          var s = members.OrderBy(a => a.TT).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedEvaluatorsToBeAddedLater)
          {
            members.Add(n);
          }
          i++;
        }
      }
      else if (role == "grammarian")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedEvaluatorsToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedEvaluatorsToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedEvaluatorsToBeAddedLater)
          {
            members.Remove(t);
          }

          var s = members.OrderBy(a => a.Gram).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedEvaluatorsToBeAddedLater)
          {
            members.Add(n);
          }
          i++;
        }
      }
      else if (role == "timer")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedEvaluatorsToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedEvaluatorsToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedEvaluatorsToBeAddedLater)
          {
            members.Remove(t);
          }

          var s = members.OrderBy(a => a.Timer).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedEvaluatorsToBeAddedLater)
          {
            members.Add(n);
          }
          i++;
        }
      }
      else if (role == "ah")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedEvaluatorsToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedEvaluatorsToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedEvaluatorsToBeAddedLater)
          {
            members.Remove(t);
          }

          var s = members.OrderBy(a => a.Ah).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedEvaluatorsToBeAddedLater)
          {
            members.Add(n);
          }
          i++;
        }
      }
      else if (role == "quiz")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedEvaluatorsToBeAddedLater = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedEvaluatorsToBeAddedLater.Add(member);
            }
          }

          foreach (var t in removedEvaluatorsToBeAddedLater)
          {
            members.Remove(t);
          }

          var s = members.OrderBy(a => a.Quiz).First();
          roles.Add(s);
          members.Remove(s);

          foreach (var n in removedEvaluatorsToBeAddedLater)
          {
            members.Add(n);
          }
          i++;
        }
      }
      else if (role == "video")
      {
        while (i <= numberOfMeetings)
        {
          List<MemberModel> removedVideo = new List<MemberModel>();
          foreach (var member in members)
          {
            if (member.MeetingsOut.Contains(i))
            {
              removedVideo.Add(member);
            }
          }

          foreach (var t in removedVideo)
            members.Remove(t);

          var v = members.OrderBy(a => a.Video).First();
          roles.Add(v);
          members.Remove(v);

          foreach (var n in removedVideo)
          {
            members.Add(n);
          }
          i++;
        }
      }
      return roles;
    }
    List<DateTime> GetMonthlyMeetings(DateTime startDate)
    {
      // assume startDate is a wednesday
      DateTime firstWednesday = startDate;
      DateTime secondWednesday = startDate.AddDays(7);
      DateTime thirdWednesday = startDate.AddDays(14);
      DateTime fourthWednesday = startDate.AddDays(21);
      DateTime fifthWednesday = startDate.AddDays(28);
      var daysinmonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
      DateTime lastDayOfMonth = new DateTime(2019, startDate.Month, daysinmonth);

      DateTime g = lastDayOfMonth;
      while (g.DayOfWeek != DayOfWeek.Friday)
      {
        g = g.AddDays(-1);
      }
      DateTime fridayMeeting = g;
      List<DateTime> meetings = new List<DateTime>();

      var month = startDate.Month;
      // handle months with holidays differently - November, December, January, July, etc
      if (month == 11) //november
      {
        // account for Thanksgiving
      }
      
      if (month == 12) //december
      {
        // account for Christmas
        meetings.Add(firstWednesday);
        meetings.Add(secondWednesday);
        meetings.Add(thirdWednesday);
        return meetings;
      }

      meetings.Add(firstWednesday);
      meetings.Add(secondWednesday);
      meetings.Add(thirdWednesday);
      if (fridayMeeting > thirdWednesday && fridayMeeting < fourthWednesday)
      {
        meetings.Add(fridayMeeting);
        meetings.Add(fourthWednesday);
      }
      else if (fridayMeeting > fourthWednesday && fridayMeeting < fifthWednesday)
      {
        meetings.Add(fourthWednesday);
        meetings.Add(fridayMeeting);
        if (fifthWednesday <= lastDayOfMonth)
          meetings.Add(fifthWednesday);
      }
      else if (fridayMeeting > fourthWednesday && fridayMeeting > fifthWednesday)
      {
        meetings.Add(fourthWednesday);
        meetings.Add(fifthWednesday);
        meetings.Add(fridayMeeting);
      }
      return meetings;
    }
    List<MemberModel> GetEvaluators(List<MemberModel> members, int numberOfMeetings)
    {
      List<MemberModel> evaluators = new List<MemberModel>();
      return evaluators;
    }

    List<MemberModel> GetSpeakers(List<MemberModel> members, DateTime startDate)
    {
      //// assume startDate is a wednesday
      //DateTime firstWednesday = startDate;
      //DateTime secondWednesday = startDate.AddDays(7);
      //DateTime thirdWednesday = startDate.AddDays(14);
      //DateTime fourthWednesday = startDate.AddDays(21); 
      //DateTime fifthWednesday = startDate.AddDays(28);
      //var daysinmonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
      //DateTime c = new DateTime(2018, 11, daysinmonth);
      //DayOfWeek r = c.DayOfWeek;
      //if (fifthWednesday >= c)
      //{
      //  string t = "don't add to array";
      //}

      //DateTime g = c;
      //while (g.DayOfWeek != DayOfWeek.Friday)
      //{
      //  g = g.AddDays(-1);
      //}

      //var month = startDate.Month;
      //// handle months with holidays differently - November, December, January, July, etc
      //if (month == 11)
      //{ }


      List<MemberModel> speakers = new List<MemberModel>();
      var speaker1 = members.OrderBy(a => a.Speaker).First();
      members.Remove(speaker1);

      //Speaker1 = members.OrderBy(a => a.Speaker).First();
      //members.Remove(Speaker1);
      //Speaker2 = members.OrderBy(a => a.Speaker).First();
      //members.Remove(Speaker2);
      return speakers;
    }
    static DateTime GetNextMissedMeeting(int meeting, int currentYear, int currentMonth)
    {
      DateTime a = new DateTime(currentYear, currentMonth, 1);
      DayOfWeek e = a.DayOfWeek;
      int daysUntilWednesday = ((int)DayOfWeek.Wednesday - (int)a.DayOfWeek + 7) % 7;
      DateTime b = a.AddDays(daysUntilWednesday);



      //DateTime month = new DateTime(currentYear, currentMonth, 1);
      //DateTime result = DateTime.Now.AddDays(1);
      ////while (result.DayOfWeek != day)
      ////  result = result.AddDays(1);
      return b;
    }
  }
}
