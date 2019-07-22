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
    private ObservableCollection<MemberModel> _members = new ObservableCollection<MemberModel>();

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

    public MeetingModelRegularVM(MeetingModelBase meetingModel)
    {
      this.meetingModel = meetingModel;
    }

    
    public MeetingModelRegularVM(string meetingDate, string meetingTemplate, ObservableCollection<MemberModel> members)
    {
      DayOfMeeting = meetingDate;
      _template = meetingTemplate;
      _members = members;
      meetingModel = new MeetingModelRegular();
    }
    
    public void Generate()
    {
      
      Speaker1 = _members.OrderBy(a => a.Speaker).First().ToString();
      _members.Remove(Speaker1);
      Speaker2 = _members.OrderBy(a => a.Speaker).First();
      _members.Remove(Speaker2);
      Evaluator1 = _members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
      _members.Remove(Evaluator1);
      Evaluator2 = _members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
      _members.Remove(Evaluator2);
      Toastmaster = _members.Where(a => a.CanBeToastmaster == true).OrderBy(a => a.Toastmaster).First();
      _members.Remove(Toastmaster);
      GeneralEvaluator = _members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.GeneralEvaluator).First();
      _members.Remove(GeneralEvaluator);
      TT = _members.OrderBy(a => a.TT).First();
      _members.Remove(TT);
      HotSeat = _members.OrderBy(a => a.HotSeat).First();
      _members.Remove(HotSeat);
      Gram = _members.OrderBy(a => a.Gram).First();
      _members.Remove(Gram);
      Ah = _members.OrderBy(a => a.Ah).First();
      _members.Remove(Ah);
      Quiz = _members.OrderBy(a => a.Quiz).First();
      _members.Remove(Quiz);
      Timer = _members.OrderBy(a => a.Timer).First();
      _members.Remove(Timer);
      Video = _members.OrderBy(a => a.Video).First();
      _members.Remove(Video);
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
