using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scheduler
{
  /// <summary>
  /// Interaction logic for Home.xaml
  /// </summary>
  public partial class Home : Page
  {
    public Home()
    {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      SqlConnection conn = new SqlConnection();
      conn.ConnectionString = @"Server=.\SQLEXPRESS;Database=Scheduler;Integrated Security=true;";
      List<MemberModel> members = new List<MemberModel>();
      MeetingModel one = new MeetingModel();
      DataSet dsMembers = new DataSet();
      SqlDataAdapter daMembers = new SqlDataAdapter("select * from Members where memberID > 0", conn);
      daMembers.Fill(dsMembers);
      DataTable dtMembers = dsMembers.Tables["Table"];


      // this builds an array of the wednesdays in the month.
      // need to work on adding the last friday to the array.
      // then I can just pull the meetings missed in DateTime format

      //DateTime today = DateTime.Today;
      DateTime a = new DateTime(2018, 11, 1);
      int daysUntilFirstWednesday = ((int)DayOfWeek.Wednesday - (int)a.DayOfWeek + 7) % 7;
      DateTime b = a.AddDays(daysUntilFirstWednesday);
      DateTime d = a.AddDays(daysUntilFirstWednesday + 7);
      DateTime f = a.AddDays(daysUntilFirstWednesday + 14);
      DateTime g = a.AddDays(daysUntilFirstWednesday + 21);
      DateTime h = a.AddDays(daysUntilFirstWednesday + 28);
      var daysinmonth = DateTime.DaysInMonth(2018, 11);
      DateTime c = new DateTime(2018, 11, daysinmonth);
      DayOfWeek r = c.DayOfWeek;
      if (h >= c)
      {
        string t = "don't add to array";
      }


      foreach (DataRow rowMember in dtMembers.Rows)
      {
        MemberModel theMember = new MemberModel();
        theMember.MemberID = (int)rowMember.ItemArray[1];
        theMember.Name = rowMember.ItemArray[2].ToString();
        if (theMember.Name == "Mike Frith" || theMember.MemberID == 0)// || theMember.Name == "Abhijit Roy" || theMember.Name == "Gene Huang" || theMember.Name == "Andy Green" 
                                                                      //|| theMember.Name == "Mary Mozingo" || theMember.Name == "Carmen Payne" || theMember.Name == "Deng Ding" || theMember.Name == "Christine Ma" || theMember.Name == "Emma Rodas")
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
            members.Add(theMember);
            continue;
          }
          string[] days = daysStr.Trim().Split(',');
          List<int> daysMissing = days.Select(int.Parse).ToList();
          theMember.MeetingsOut = daysMissing;

        }
        else
          theMember.MeetingsOut = new List<int>(0);
        members.Add(theMember);
      }

      List<DateTime> meetings = GetMonthlyMeetings(new DateTime(2018, 12, 5));
      int NumberOfMeetings = meetings.Count;
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
      List<MemberModel> tableTopics = GetRoles(members, NumberOfMeetings, "tabletopics");
      List<string> ttnames = new List<string>();
      foreach (var s in tableTopics)
      {
        ttnames.Add(s.Name);
      }
      List<MemberModel> grammarians = GetRoles(members, NumberOfMeetings, "grammarian");
      List<string> grnames = new List<string>();
      foreach (var s in grammarians)
      {
        grnames.Add(s.Name);
      }
      List<string> timernames = new List<string>();
      if (members.Count >= 5)
      {
        List<MemberModel> timers = GetRoles(members, NumberOfMeetings, "timer");
        foreach (var s in timers)
        {
          timernames.Add(s.Name);
        }
      }
      List<string> ahnames = new List<string>();

      if (members.Count >= 5)
      {
        List<MemberModel> ahcounters = GetRoles(members, NumberOfMeetings, "ah");
        foreach (var s in ahcounters)
        {
          ahnames.Add(s.Name);
        }
      }
      List<string> quiznames = new List<string>();

      if (members.Count >= 5)
      {
        List<MemberModel> quizmasters = GetRoles(members, NumberOfMeetings, "quiz");
        foreach (var s in quizmasters)
        {
          quiznames.Add(s.Name);
        }
      }
      List<string> videonames = new List<string>();

      if (members.Count >= 5)
      {
        List<MemberModel> video = GetRoles(members, NumberOfMeetings, "video");
        foreach (var s in video)
        {
          videonames.Add(s.Name);
        }
      }


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
      DateTime lastDayOfMonth = new DateTime(2018, startDate.Month, daysinmonth);

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
