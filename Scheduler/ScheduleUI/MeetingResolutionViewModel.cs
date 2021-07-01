using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using SchedulerUI;

namespace ScheduleUI
{
  public class MeetingResolutionViewModel : ViewModelBase
  {
    List<string> regularTemplate = new List<string>(new string[] {"DayOfMeeting","Toastmaster","Speaker 1","Speaker 2","General Evaluator",
                                                                  "Evaluator 1", "Evaluator 2", "Table Topics", "Ah Counter",
                                                                  "Timer", "Grammarian", "Quiz Master", "Video", "Hot Seat" });

    List<string> regularTemplateOutput = new List<string>(new string[] {"DayOfMeeting","Toastmaster","Speaker1","Speaker2","GeneralEvaluator",
                                                                  "Evaluator1", "Evaluator2", "TableTopics", "AhCounter",
                                                                  "Timer", "Grammarian", "QuizMaster", "Video", "HotSeat" });
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
    private List<MemberModel> _members;

    #endregion
    public string MeetingType { get; set; }
    public string ID { get; set; }
    private string _toastmaster;
    public string DayOfMeeting { get; set; }
    public string Toastmaster
    {
      get { return _toastmaster; }
      set { _toastmaster = value; }
    }
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

    public string Month { get; set; }
    public string Year { get; set; }
    public string WOTD { get; set; }
    public string Theme { get; set; }
    public MeetingResolutionViewModel()
    { }

    public List<string> SpeakerEvaluatorLessList
    {
      get
      {

        return new List<string>();
      }
    }

    public List<string> MembersList
    {
      get
      {
        return _members.Select(iterator => iterator.Name).ToList();
      }
    }

    private MeetingModelRegular _meeting;

    public MeetingResolutionViewModel(MeetingModelRegular meetingModel, List<MemberModel> members)
    {
      _meeting = meetingModel;
      _members = members;
      Toastmaster = _meeting.Toastmaster;
      Speaker1 = _meeting.Speaker1;
      Speaker2 = _meeting.Speaker2;
      GeneralEvaluator = _meeting.GeneralEvaluator;
      Evaluator1 = _meeting.Evaluator1;
      Evaluator2 = _meeting.Evaluator2;
      TableTopics = (_meeting as MeetingModelRegular).TableTopics;
      AhCounter = _meeting.AhCounter;
      Grammarian = _meeting.Grammarian;
      Timer = _meeting.Timer;
      QuizMaster = _meeting.QuizMaster;
      Video = _meeting.Video;
      HotSeat = _meeting.HotSeat;
      WOTD = _meeting.WOTD;
      Theme = _meeting.Theme;
      DayOfMeeting = _meeting.DayOfMeeting;
    }

    public void Reset()
    {

    }

    //public List<string> ToList()
    //{
    //  List<string> list = new List<string>();
    //  list.Add(Toastmaster);
    //  list.Add(Speaker1);
    //  list.Add(Speaker2);
    //  list.Add(GeneralEvaluator);
    //  list.Add(Evaluator1);
    //  list.Add(Evaluator2);
    //  list.Add(TableTopics);
    //  list.Add(AhCounter);
    //  list.Add(Timer);
    //  list.Add(Grammarian);
    //  list.Add(QuizMaster);
    //  list.Add(Video);
    //  list.Add(HotSeat);
    //  return list;
    //}

    public void Save(int meetingID)
    {
      //System.IO.FileStream fileStream = new FileStream("C:\\Users\\mike\\Documents\\TI\\Meetings.dat", FileMode.Append, FileAccess.Write);
      //StreamWriter strmWriter = new StreamWriter(fileStream);
      //strmWriter.Write(this.ToFile());
      _meeting.Toastmaster = Toastmaster;
      _meeting.Speaker1 = Speaker1;
      _meeting.Speaker2 = Speaker2;
      _meeting.GeneralEvaluator = GeneralEvaluator;
      _meeting.Evaluator1 = Evaluator1;
      _meeting.Evaluator2 = Evaluator2;
      (_meeting as MeetingModelRegular).TableTopics = TableTopics;
      _meeting.Grammarian = Grammarian;
      _meeting.AhCounter = AhCounter;
      _meeting.Timer = Timer;
      _meeting.QuizMaster = QuizMaster;
      _meeting.Video = Video;
      _meeting.HotSeat = HotSeat;


    }

  }
}
