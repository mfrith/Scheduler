using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SchedulerUI;

namespace ScheduleUI
{
  public class MeetingTypeSelector : DataTemplateSelector
  {
    public DataTemplate StandardMeetingTemplate { get; set; }
    public DataTemplate ThreeSpeakerMeetingTemplate { get; set; }
    public DataTemplate SpeakathonMeetingTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      return base.SelectTemplate(item, container);
    }
  }

  public class MeetingModelRegularVM : ViewModelBase
  {
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
    #region privates
    private static readonly KeyValuePair<string, string>[] meetingTemplates =
    {
        new KeyValuePair<string, string>("regularTemplate","Regular Meeting"),
        new KeyValuePair<string, string>("threeSpeakerTemplate", "Three Speaker Meeting"),
        new KeyValuePair<string, string>("speakathonTemplate", "Speakathon")
    };
    private string _template = string.Empty;
    private MeetingModelBase meetingModel;
    //private DateTime DayOfMeeting { get; set; }
    //private MemberModel Toastmaster { get; set; }
    //private MemberModel Speaker1 { get; set; }
    //private MemberModel Speaker2 { get; set; }
    //private MemberModel GeneralEvaluator { get; set; }
    //private MemberModel Evaluator1 { get; set; }
    //private MemberModel Evaluator2 { get; set; }
    //private MemberModel TT { get; set; }
    //private MemberModel Ah { get; set; }
    //private MemberModel Gram { get; set; }
    //private MemberModel Timer { get; set; }
    //private MemberModel Quiz { get; set; }
    //private MemberModel Video { get; set; }
    //private MemberModel HotSeat { get; set; }
    //private List<int> Attendees { get; set; }
    //private MemberModel TTWinner { get; set; }
    //private List<int> TTContestants { get; set; }
    private List<MemberModel> _members = new List<MemberModel>();

    #endregion
    public string MeetingType { get; set; }
    public string ID { get; set; }
    public string DayOfMeeting { get; set; }
    public string Toastmaster { get; set; }
    public string Speaker1 { get; set; }
    public string Speaker2 { get; set; }
    public string Speaker3 { get; set; }
    public string Speaker4 { get; set; }
    public string Speaker5 { get; set; }
    public string GeneralEvaluator { get; set; }
    public string Evaluator1 { get; set; }
    public string Evaluator2 { get; set; }
    public string Evaluator3 { get; set; }
    public string Evaluator4 { get; set; }
    public string Evaluator5 { get; set; }
    public string AhCounter { get; set; }
    public string Grammarian { get; set; }
    public string Timer { get; set; }
    public string QuizMaster { get; set; }
    public string Video { get; set; }
    public string HotSeat { get; set; }
    public List<string> Attendees { get; set; }
    public string Resolved { get; set; }
    public string TableTopics { get; set; }


    public MeetingModelRegularVM()
    { }

    public MeetingModelRegularVM(List<MemberModel> members)
    {
      _members = members;
    }
    public MeetingModelRegularVM(MeetingModelBase meetingModel)
    {
      this.meetingModel = meetingModel;
    }


    public MeetingModelRegularVM(string meetingDate, string meetingTemplate, List<MemberModel> members)
    {
      DayOfMeeting = meetingDate;
      _template = meetingTemplate;
      _members = members;
      meetingModel = new MeetingModelRegular();
    }

    public void Generate()
    {
      //List<MemberModel> members = new List<MemberModel>(_members);
      // need flag for monthly grouping
      // base selection for the month, default setting
      //if (bMonthly)
      //{

      //}
      //var speaker1 = _members.OrderBy(a => a.Speaker).First();
      //_members.Remove(speaker1);
      //var speaker2 = _members.OrderBy(a => a.Speaker).First();
      //_members.Remove(speaker2);
      //var evaluator1 = _members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
      //_members.Remove(evaluator1);
      //var evaluator2 = _members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
      //_members.Remove(evaluator2);
      //var toastmaster = _members.Where(a => a.CanBeToastmaster == true).OrderBy(a => a.Toastmaster).First();
      //_members.Remove(toastmaster);
      //var generalEvaluator = _members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.GeneralEvaluator).First();
      //_members.Remove(generalEvaluator);
      //var tt = _members.OrderBy(a => a.TT).First();
      //_members.Remove(tt);
      //var hotSeat = _members.OrderBy(a => a.HotSeat).First();
      //_members.Remove(hotSeat);
      //var gram = _members.OrderBy(a => a.Gram).First();
      //_members.Remove(gram);
      //var ah = _members.OrderBy(a => a.Ah).First();
      //_members.Remove(ah);
      //var quiz = _members.OrderBy(a => a.Quiz).First();
      //_members.Remove(quiz);
      //var timer = _members.OrderBy(a => a.Timer).First();
      //_members.Remove(timer);
      //var video = _members.OrderBy(a => a.Video).First();
      //_members.Remove(video);

      //GetMembers(ref members);
      List<DateTime> theMeetings = GetMonthlyMeetings(new DateTime(2020, 1, 8), true);
      int NumberOfMeetings = 6;//  meetings.Count;
                               // List<string> speakers =
      GetRolesPerMonth(theMeetings);
      GetRolesPerMeeting(theMeetings);//, "speaker");
      //List<string> evaluators = GetRoles(members, theMeetings, "evaluator");

      ////List<MemberModel> speakers = GetSpeakers(members, new DateTime(2018, 11, 7));
      //List<MemberModel> evaluators = GetRoles(members, theMeetings, "evaluator");
      //List<string> enames = new List<string>();
      //foreach (var s in evaluators)
      //{
      //  enames.Add(s.Name);
      //}

      //List<MemberModel> generalEvaluators = GetRoles(members, theMeetings, "generalevaluator");
      //List<string> gnames = new List<string>();
      //foreach (var s in generalEvaluators)
      //{
      //  gnames.Add(s.Name);
      //}

      //List<MemberModel> toastmasters = GetRoles(members, theMeetings, "toastmaster");
      //List<string> tnames = new List<string>();
      //foreach (var s in toastmasters)
      //{
      //  tnames.Add(s.Name);
      //}
      //List<MemberModel> hotseat = GetRoles(members, theMeetings, "hotseat");
      //List<string> hnames = new List<string>();
      //foreach (var s in hotseat)
      //{
      //  hnames.Add(s.Name);
      //}

      ////if (members.Count < theMeetings.Count())
      ////{
      ////  members.Clear();
      ////  GetMembers(ref members);

      ////}
      //List<MemberModel> tableTopics = GetRoles(members, theMeetings, "tabletopics");
      //List<string> ttnames = new List<string>();
      //foreach (var s in tableTopics)
      //{
      //  ttnames.Add(s.Name);
      //}

      ////if (members.Count < theMeetings.Count())
      ////{
      ////  members.Clear();
      ////  GetMembers(ref members);
      ////}
      //// }
      //List<MemberModel> grammarians = GetRoles(members, theMeetings, "grammarian");
      //List<string> grnames = new List<string>();
      //foreach (var s in grammarians)
      //{
      //  grnames.Add(s.Name);
      //}
      ////if (members.Count < NumberOfMeetings)
      ////{
      ////  members.Clear();
      ////  GetMembers(ref members);
      ////}
      //List<string> timernames = new List<string>();
      ////if (members.Count >= 5)
      ////{
      //List<MemberModel> timers = GetRoles(members, theMeetings, "timer");
      //foreach (var s in timers)
      //{
      //  timernames.Add(s.Name);
      //}
      //// }
      ////if (members.Count < theMeetings.Count())
      ////{
      ////  members.Clear();
      ////  GetMembers(ref members);
      ////}
      //List<string> ahnames = new List<string>();

      ////if (members.Count >= 5)
      ////{
      //List<MemberModel> ahcounters = GetRoles(members, theMeetings, "ah");
      //foreach (var s in ahcounters)
      //{
      //  ahnames.Add(s.Name);
      //}
      ////   }
      //List<string> quiznames = new List<string>();

      ////if (members.Count >= 5)
      ////{
      //List<MemberModel> quizmasters = GetRoles(members, theMeetings, "quiz");
      //foreach (var s in quizmasters)
      //{
      //  quiznames.Add(s.Name);
      //}
      //// }
      //List<string> videonames = new List<string>();

      ////if (members.Count >= 5)
      ////{
      //List<MemberModel> video = GetRoles(members, theMeetings, "video");
      //foreach (var s in video)
      //{
      //  videonames.Add(s.Name);
      //}
      ////}

      //var t = snames.Count;

      //t = enames.Count;
      //t = gnames.Count;
      //t = tnames.Count;
      //t = hnames.Count;
      //t = ttnames.Count;
      //t = grnames.Count;
      //t = timernames.Count;
      //t = ahnames.Count;
      //t = quiznames.Count;
      //t = videonames.Count;

      //t = 0;

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

      //if (File.Exists("C:\\Users\\mike\\Documents\\TI\\MeetingsNext.csv"))
      //{
      //  File.Delete("C:\\Users\\mike\\Documents\\TI\\MeetingsNext.csv");
      //}

      //using (StreamWriter file = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\MeetingsNext.csv"))
      //{
      //  string dates = "Role, Oct 2, Oct 9, Oct 16, Oct 23, Oct 25, Oct 30";
      //  file.WriteLine(dates);
      //  string row1 = "Toastmaster," + tnames[0] + "," + tnames[1] + "," + tnames[2] + "," + tnames[3] + "," + tnames[4] + "," + tnames[5];
      //  file.WriteLine(row1);
      //  row1 = "Speaker 1," + snames[0] + "," + snames[2] + "," + snames[4] + "," + snames[6] + "," + snames[8] + "," + snames[10];
      //  file.WriteLine(row1);
      //  row1 = "Speaker 2," + snames[1] + "," + snames[3] + "," + snames[5] + "," + snames[7] + "," + snames[9] + "," + snames[11];
      //  file.WriteLine(row1);
      //  row1 = "GE," + gnames[0] + "," + gnames[1] + "," + gnames[2] + "," + gnames[3] + "," + gnames[4] + "," + gnames[5];
      //  file.WriteLine(row1);
      //  row1 = "Eval 1," + enames[0] + "," + enames[2] + "," + enames[4] + "," + enames[6] + "," + enames[8] + "," + enames[10];
      //  file.WriteLine(row1);
      //  row1 = "Eval 2," + enames[1] + "," + enames[3] + "," + enames[5] + "," + enames[7] + "," + enames[9] + "," + enames[11];
      //  file.WriteLine(row1);
      //  row1 = "TT," + ttnames[0] + "," + ttnames[1] + "," + ttnames[2] + "," + ttnames[3] + "," + tnames[4] + "," + tnames[5];
      //  file.WriteLine(row1);
      //  row1 = "Ah ," + ahnames[0] + "," + ahnames[1] + "," + ahnames[2] + "," + ahnames[3] + "," + ahnames[4] + "," + ahnames[5];
      //  file.WriteLine(row1);
      //  row1 = "Timer," + timernames[0] + "," + timernames[1] + "," + timernames[2] + "," + timernames[3] + "," + timernames[4] + "," + timernames[5];
      //  file.WriteLine(row1);
      //  row1 = "Gram," + grnames[0] + "," + grnames[1] + "," + grnames[2] + "," + grnames[3] + "," + grnames[4] + "," + grnames[5];
      //  file.WriteLine(row1);
      //  row1 = "Quiz," + quiznames[0] + "," + quiznames[1] + "," + quiznames[2] + "," + quiznames[3] + "," + quiznames[4] + "," + quiznames[5];
      //  file.WriteLine(row1);
      //  row1 = "Video," + videonames[0] + "," + videonames[1] + "," + videonames[2] + "," + videonames[3] + "," + videonames[4] + "," + videonames[5];
      //  file.WriteLine(row1);
      //  row1 = "HS," + hnames[0] + "," + hnames[1] + "," + hnames[2] + "," + hnames[3] + "," + hnames[4] + "," + hnames[5];
      //  file.WriteLine(row1);

      //}
    }

    List<DateTime> GetMonthlyMeetings(DateTime startDate, bool lastFriday = true)
    {
      // assume startDate is a wednesday
      DateTime firstWednesday = startDate;
      DateTime secondWednesday = startDate.AddDays(7);
      DateTime thirdWednesday = startDate.AddDays(14);
      DateTime fourthWednesday = startDate.AddDays(21);
      DateTime fifthWednesday = startDate.AddDays(28);
      var daysinmonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
      DateTime lastDayOfMonth = new DateTime(2020, startDate.Month, daysinmonth);

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
        //meetings.Add(firstWednesday);
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

      if (lastFriday == false)
        meetings.Remove(fridayMeeting);
      return meetings;
    }

    void GetRolesPerMonth(List<DateTime> meetingDates)
    {
      List<MemberModel> members = new List<MemberModel>(_members.Where(m => m.Name != "Mike Frith").ToList());

      //List<MemberModel> localMembers = new List<MemberModel>(members);
      //members.CopyTo(localMembers);
      //HashSet<string> firstrole = new HashSet<string>();
      int i = 0;
      HashSet<string> meeting1 = new HashSet<string>();
      HashSet<string> meeting2 = new HashSet<string>();
      HashSet<string> meeting3 = new HashSet<string>();
      HashSet<string> meeting4 = new HashSet<string>();
      HashSet<string> meeting5 = new HashSet<string>();

      List<HashSet<string>> meetings = new List<HashSet<string>>();
      foreach (var t in meetingDates)
      {
        meetings.Add(new HashSet<string>());
      }

      //"speaker")
      List<string> snames = new List<string>();
      List<string> enames = new List<string>();
      List<string> tnames = new List<string>();
      List<string> gnames = new List<string>();
      List<string> ttnames = new List<string>();
      List<string> ahnames = new List<string>();
      List<string> grnames = new List<string>();
      List<string> timernames = new List<string>();
      List<string> quiznames = new List<string>();
      List<string> videonames = new List<string>();
      List<string> hnames = new List<string>();

      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);

        var speaker = members.OrderBy(a => a.Speaker).First();
        //firstrole.Add(speaker.Name);
        snames.Add(speaker.Name);
        members.Remove(speaker);
        speaker.Speaker = meetingDates[i];
        speaker = members.OrderBy(a => a.Speaker).First();
        //firstrole.Add(speaker.Name);
        snames.Add(speaker.Name);
        members.Remove(speaker);

        speaker.Speaker = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }

      ////"evaluator")
      i = 0;
      //List<MemberModel> temp = new List<MemberModel>();
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);
        var evaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
        //while (!firstrole.Add(evaluator.Name))
        //{
        //  members.Remove(evaluator);
        //  temp.Add(evaluator);
        //  evaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();

        //}
        enames.Add(evaluator.Name);
        evaluator.Evaluator = meetingDates[i];
        members.Remove(evaluator);
        evaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
        //while (!firstrole.Add(evaluator.Name))
        //{
        //  members.Remove(evaluator);
        //  temp.Add(evaluator);
        //  evaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();

        //}
        enames.Add(evaluator.Name);
        members.Remove(evaluator);

        evaluator.Evaluator = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }

      //foreach (var e in temp)
      //  members.Add(e);

      //temp.Clear();
      //else if (role == "generalevaluator")
      i = 0;
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);
        var evaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.GeneralEvaluator).First();
        //while (!firstrole.Add(evaluator.Name))
        //{
        //  members.Remove(evaluator);
        //  temp.Add(evaluator);
        //  evaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();

        //}
        members.Remove(evaluator);

        gnames.Add(evaluator.Name);
        evaluator.GeneralEvaluator = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }
      //foreach (var e in temp)
      //  members.Add(e);
      //temp.Clear();

      ////else if (role == "toastmaster")
      i = 0;
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);
        var toastmaster = members.Where(a => a.CanBeToastmaster == true).OrderBy(a => a.Toastmaster).First();
        //while (!firstrole.Add(toastmaster.Name))
        //{
        //  members.Remove(toastmaster);
        //  temp.Add(toastmaster);
        //  toastmaster = members.Where(a => a.CanBeToastmaster == true).OrderBy(a => a.Toastmaster).First();

        //}
        tnames.Add(toastmaster.Name);
        members.Remove(toastmaster);

        toastmaster.Toastmaster = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }

      //foreach (var e in temp)
      //  members.Add(e);
      //temp.Clear();

      ////else if (role == "hotseat")
      i = 0;
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);
        var hotseat = members.OrderBy(a => a.HotSeat).First();
        //while (!firstrole.Add(hotseat.Name))
        //{
        //  members.Remove(hotseat);
        //  temp.Add(hotseat);
        //  hotseat = members.OrderBy(a => a.HotSeat).First();

        //}
        hnames.Add(hotseat.Name);
        members.Remove(hotseat);

        hotseat.HotSeat = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }

      //foreach (var e in temp)
      //  members.Add(e);
      //temp.Clear();

      ////}
      ////else if (role == "tabletopics")
      i = 0;
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);
        var tt = members.OrderBy(a => a.TT).First();
        //while (!firstrole.Add(tt.Name))
        //{
        //  members.Remove(tt);
        //  temp.Add(tt);
        //  tt = members.OrderBy(a => a.TT).First();

        //}
        ttnames.Add(tt.Name);
        members.Remove(tt);

        tt.TT = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }

      //foreach (var e in temp)
      //  members.Add(e);
      //temp.Clear();

      ////}
      ////else if (role == "grammarian")
      i = 0;
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        if (members.Count == 0)
          members = new List<MemberModel>(_members.Where(m => m.Name != "Mike Frith").ToList());
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);

        if (members.Count == 0)
        {
          members = new List<MemberModel>(_members.Where(m => m.Name != "Mike Frith").ToList());
          foreach (var im in iterationMembers)
            members.Remove(im);
          //iterationMembers.Clear();
        }
        var gram = members.OrderBy(a => a.Gram).First();
        //while (!firstrole.Add(tt.Name))
        //{
        //  members.Remove(tt);
        //  temp.Add(tt);
        //  tt = members.OrderBy(a => a.TT).First();

        //}
        grnames.Add(gram.Name);
        members.Remove(gram);

        gram.Gram = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }
      ////else if (role == "timer")

      //members.Clear();

      //members = new List<MemberModel>(localMembers);

      i = 0;
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);
        var timer = members.OrderBy(a => a.Timer).First();
        //while (!firstrole.Add(tt.Name))
        //{
        //  members.Remove(tt);
        //  temp.Add(tt);
        //  tt = members.OrderBy(a => a.TT).First();

        //}
        timernames.Add(timer.Name);
        members.Remove(timer);

        timer.Timer = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }
      ////else if (role == "ah")
      i = 0;
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);
        var ah = members.OrderBy(a => a.Ah).First();
        //while (!firstrole.Add(tt.Name))
        //{
        //  members.Remove(tt);
        //  temp.Add(tt);
        //  tt = members.OrderBy(a => a.TT).First();

        //}
        ahnames.Add(ah.Name);
        members.Remove(ah);
        ah.Ah = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }
      ////else if (role == "quiz")
      i = 0;
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);
        var quiz = members.OrderBy(a => a.Quiz).First();
        //while (!firstrole.Add(tt.Name))
        //{
        //  members.Remove(tt);
        //  temp.Add(tt);
        //  tt = members.OrderBy(a => a.TT).First();

        //}
        quiznames.Add(quiz.Name);
        members.Remove(quiz);
        quiz.Quiz = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }

      ////else if (role == "video")
      i = 0;
      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);
        var video = members.OrderBy(a => a.Video).First();
        //while (!firstrole.Add(tt.Name))
        //{
        //  members.Remove(tt);
        //  temp.Add(tt);
        //  tt = members.OrderBy(a => a.TT).First();

        //}
        videonames.Add(video.Name);
        members.Remove(video);
        video.Video = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);
      }

      if (File.Exists("C:\\Users\\mike\\Documents\\TI\\MeetingsPerMonthNext.csv"))
      {
        File.Delete("C:\\Users\\mike\\Documents\\TI\\MeetingsPerMonthNext.csv");
      }

      using (StreamWriter file = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\MeetingsPerMonthNext.csv"))
      {
        string dates = "Role ";
        foreach (var meeting in meetingDates)
        {
          dates += ", " + meeting.ToString("MMMM dd");
        }
        foreach (var role in regularTemplate)
        {

        }
        //string dates = "Role, " + meetingDates[0].ToString("MMMM dd") 
        file.WriteLine(dates);
        string row1 = "Toastmaster," + tnames[0] + "," + tnames[1] + "," + tnames[2] + "," + tnames[3] + "," + tnames[4]; //+ "," + tnames[5];
        file.WriteLine(row1);
        row1 = "Speaker 1," + snames[0] + "," + snames[2] + "," + snames[4] + "," + snames[6] + "," + snames[8];// + "," + snames[10];
        file.WriteLine(row1);
        row1 = "Speaker 2," + snames[1] + "," + snames[3] + "," + snames[5] + "," + snames[7] + "," + snames[9];// + "," + snames[11];
        file.WriteLine(row1);
        row1 = "GE," + gnames[0] + "," + gnames[1] + "," + gnames[2] + "," + gnames[3] + "," + gnames[4];// + "," + gnames[5];
        file.WriteLine(row1);
        row1 = "Eval 1," + enames[0] + "," + enames[2] + "," + enames[4] + "," + enames[6] + "," + enames[8];// + "," + enames[10];
        file.WriteLine(row1);
        row1 = "Eval 2," + enames[1] + "," + enames[3] + "," + enames[5] + "," + enames[7] + "," + enames[9];// + "," + enames[11];
        file.WriteLine(row1);
        row1 = "TT," + ttnames[0] + "," + ttnames[1] + "," + ttnames[2] + "," + ttnames[3] + "," + tnames[4];// + "," + tnames[5];
        file.WriteLine(row1);
        row1 = "Ah ," + ahnames[0] + "," + ahnames[1] + "," + ahnames[2] + "," + ahnames[3] + "," + ahnames[4];// + "," + ahnames[5];
        file.WriteLine(row1);
        row1 = "Timer," + timernames[0] + "," + timernames[1] + "," + timernames[2] + "," + timernames[3] + "," + timernames[4];// + "," + timernames[5];
        file.WriteLine(row1);
        row1 = "Gram," + grnames[0] + "," + grnames[1] + "," + grnames[2] + "," + grnames[3] + "," + grnames[4];// + "," + grnames[5];
        file.WriteLine(row1);
        row1 = "Quiz," + quiznames[0] + "," + quiznames[1] + "," + quiznames[2] + "," + quiznames[3] + "," + quiznames[4];// + "," + quiznames[5];
        file.WriteLine(row1);
        row1 = "Video," + videonames[0] + "," + videonames[1] + "," + videonames[2] + "," + videonames[3] + "," + videonames[4];// + "," + videonames[5];
        file.WriteLine(row1);
        row1 = "HS," + hnames[0] + "," + hnames[1] + "," + hnames[2] + "," + hnames[3] + "," + hnames[4];// + "," + hnames[5];
        file.WriteLine(row1);

      }
    }
    void GetRolesPerMeeting(List<DateTime> meetingDates)
    {
      //List<MemberModel> localMembers = new List<MemberModel>(members);
      //members.CopyTo(localMembers);
      //HashSet<string> firstrole = new HashSet<string>();
      int i = 0;
      HashSet<string> meeting1 = new HashSet<string>();
      HashSet<string> meeting2 = new HashSet<string>();
      HashSet<string> meeting3 = new HashSet<string>();
      HashSet<string> meeting4 = new HashSet<string>();
      HashSet<string> meeting5 = new HashSet<string>();

      List<HashSet<string>> meetings = new List<HashSet<string>>();
      foreach (var t in meetingDates)
      {
        meetings.Add(new HashSet<string>());
      }

      //"speaker")
      List<string> snames = new List<string>();
      List<string> enames = new List<string>();
      List<string> tnames = new List<string>();
      List<string> gnames = new List<string>();
      List<string> ttnames = new List<string>();
      List<string> ahnames = new List<string>();
      List<string> grnames = new List<string>();
      List<string> timernames = new List<string>();
      List<string> quiznames = new List<string>();
      List<string> videonames = new List<string>();
      List<string> hnames = new List<string>();

      List<MemberModel> members = new List<MemberModel>(_members.Where(m => m.Name != "Mike Frith").ToList());

      while (i <= meetingDates.Count() - 1)
      {
        int meetingsout = i + 1;
        var iterationMembers = members.Where(it => it.MeetingsOut.Contains(meetingsout)).ToList();
        foreach (var im in iterationMembers)
          members.Remove(im);

        var speaker = members.OrderBy(a => a.Speaker).First();
        snames.Add(speaker.Name);
        members.Remove(speaker);
        speaker.Speaker = meetingDates[i];
        speaker = members.OrderBy(a => a.Speaker).First();
        snames.Add(speaker.Name);
        members.Remove(speaker);

        speaker.Speaker = meetingDates[i];

        var evaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();

        enames.Add(evaluator.Name);
        evaluator.Evaluator = meetingDates[i];
        members.Remove(evaluator);
        evaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();

        enames.Add(evaluator.Name);
        members.Remove(evaluator);

        evaluator.Evaluator = meetingDates[i];

        var genevaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.GeneralEvaluator).First();

        members.Remove(genevaluator);

        gnames.Add(genevaluator.Name);
        genevaluator.GeneralEvaluator = meetingDates[i];

        var toastmaster = members.Where(a => a.CanBeToastmaster == true).OrderBy(a => a.Toastmaster).First();

        tnames.Add(toastmaster.Name);
        members.Remove(toastmaster);

        toastmaster.Toastmaster = meetingDates[i];

        var hotseat = members.OrderBy(a => a.HotSeat).First();

        hnames.Add(hotseat.Name);
        members.Remove(hotseat);

        hotseat.HotSeat = meetingDates[i];

        var tt = members.OrderBy(a => a.TT).First();

        ttnames.Add(tt.Name);
        members.Remove(tt);

        tt.TT = meetingDates[i];

        var gram = members.OrderBy(a => a.Gram).First();

        grnames.Add(gram.Name);
        members.Remove(gram);

        gram.Gram = meetingDates[i];

        var timer = members.OrderBy(a => a.Timer).First();

        timernames.Add(timer.Name);
        members.Remove(timer);

        timer.Timer = meetingDates[i];

        var ah = members.OrderBy(a => a.Ah).First();

        ahnames.Add(ah.Name);
        members.Remove(ah);
        ah.Ah = meetingDates[i];

        var quiz = members.OrderBy(a => a.Quiz).First();

        quiznames.Add(quiz.Name);
        members.Remove(quiz);
        quiz.Quiz = meetingDates[i];

        var video = members.OrderBy(a => a.Video).First();

        videonames.Add(video.Name);
        members.Remove(video);
        video.Video = meetingDates[i];
        i++;
        foreach (var im in iterationMembers)
          members.Add(im);

        if (members.Count < 12)
          members = new List<MemberModel>(_members);
      }

      if (File.Exists("C:\\Users\\mike\\Documents\\TI\\MeetingsNext.csv"))
      {
        File.Delete("C:\\Users\\mike\\Documents\\TI\\MeetingsNext.csv");
      }

      using (StreamWriter file = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\MeetingsNext.csv"))
      {
        string dates = "Role ";
        foreach (var meeting in meetingDates)
        {
          dates += ", " + meeting.ToString("MMMM dd");
        }
        foreach (var role in regularTemplate)
        {

        }
        //string dates = "Role, " + meetingDates[0].ToString("MMMM dd") 
        file.WriteLine(dates);
        string row1 = "Toastmaster," + tnames[0] + "," + tnames[1] + "," + tnames[2] + "," + tnames[3] + "," + tnames[4]; //+ "," + tnames[5];
        file.WriteLine(row1);
        row1 = "Speaker 1," + snames[0] + "," + snames[2] + "," + snames[4] + "," + snames[6] + "," + snames[8];// + "," + snames[10];
        file.WriteLine(row1);
        row1 = "Speaker 2," + snames[1] + "," + snames[3] + "," + snames[5] + "," + snames[7] + "," + snames[9];// + "," + snames[11];
        file.WriteLine(row1);
        row1 = "GE," + gnames[0] + "," + gnames[1] + "," + gnames[2] + "," + gnames[3] + "," + gnames[4];// + "," + gnames[5];
        file.WriteLine(row1);
        row1 = "Eval 1," + enames[0] + "," + enames[2] + "," + enames[4] + "," + enames[6] + "," + enames[8];// + "," + enames[10];
        file.WriteLine(row1);
        row1 = "Eval 2," + enames[1] + "," + enames[3] + "," + enames[5] + "," + enames[7] + "," + enames[9];// + "," + enames[11];
        file.WriteLine(row1);
        row1 = "TT," + ttnames[0] + "," + ttnames[1] + "," + ttnames[2] + "," + ttnames[3] + "," + tnames[4];// + "," + tnames[5];
        file.WriteLine(row1);
        row1 = "Ah ," + ahnames[0] + "," + ahnames[1] + "," + ahnames[2] + "," + ahnames[3] + "," + ahnames[4];// + "," + ahnames[5];
        file.WriteLine(row1);
        row1 = "Timer," + timernames[0] + "," + timernames[1] + "," + timernames[2] + "," + timernames[3] + "," + timernames[4];// + "," + timernames[5];
        file.WriteLine(row1);
        row1 = "Gram," + grnames[0] + "," + grnames[1] + "," + grnames[2] + "," + grnames[3] + "," + grnames[4];// + "," + grnames[5];
        file.WriteLine(row1);
        row1 = "Quiz," + quiznames[0] + "," + quiznames[1] + "," + quiznames[2] + "," + quiznames[3] + "," + quiznames[4];// + "," + quiznames[5];
        file.WriteLine(row1);
        row1 = "Video," + videonames[0] + "," + videonames[1] + "," + videonames[2] + "," + videonames[3] + "," + videonames[4];// + "," + videonames[5];
        file.WriteLine(row1);
        row1 = "HS," + hnames[0] + "," + hnames[1] + "," + hnames[2] + "," + hnames[3] + "," + hnames[4];// + "," + hnames[5];
        file.WriteLine(row1);

      }
    }

    public void Reset()
    {

    }

    public List<string> ToList()
    {
      List<string> list = new List<string>();
      list.Add(Toastmaster);
      list.Add(Speaker1);
      list.Add(Speaker2);
      list.Add(GeneralEvaluator);
      list.Add(Evaluator1);
      list.Add(Evaluator2);
      list.Add(TableTopics);
      list.Add(AhCounter);
      list.Add(Timer);
      list.Add(Grammarian);
      list.Add(QuizMaster);
      list.Add(Video);
      list.Add(HotSeat);
      return list;
    }

    public void Save(int meetingID)
    {
      //System.IO.FileStream fileStream = new FileStream("C:\\Users\\mike\\Documents\\TI\\Meetings.dat", FileMode.Append, FileAccess.Write);
      //StreamWriter strmWriter = new StreamWriter(fileStream);
      //strmWriter.Write(this.ToFile());
      meetingModel.DayOfMeeting = DayOfMeeting;//.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
      meetingModel.Toastmaster = Toastmaster;
      meetingModel.Speaker1 = Speaker1;
      meetingModel.Speaker2 = Speaker2;
      meetingModel.GeneralEvaluator = GeneralEvaluator;
      meetingModel.Evaluator1 = Evaluator1;
      meetingModel.Evaluator2 = Evaluator2;
      //meetingModel. = TT;
      //meetingModel.Gram = Gram;
      //meetingModel.Ah = Ah;
      //meetingModel.Timer = Timer;
      //meetingModel.Quiz = Quiz;
      //meetingModel.Video = Video;
      //meetingModel.HotSeat = HotSeat;
      //meetingModel.Save(meetingID);

    }
  }
}
