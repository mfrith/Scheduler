using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchedulerUI
{
  public class MeetingModelBase
  {
    public string ID { get; set; }
    public string DayOfMeeting { get; set; }
    public string Toastmaster { get; set; }
    public string Speaker1 { get; set; }
    public string Speaker2 { get; set; }
    public string GeneralEvaluator { get; set; }
    public string Evaluator1 { get; set; }
    public string Evaluator2 { get; set; }

    public string AhCounter { get; set; }
    public string Grammarian { get; set; }
    public string Timer { get; set; }
    public string QuizMaster { get; set; }
    public string Video { get; set; }
    public string HotSeat { get; set; }
    public List<string> Attendees { get; set; }

    public string Resolved { get; set; }
    public string MeetingType { get; set; }
  }

  public class MeetingModelRegular : MeetingModelBase
  {
    public string TableTopics { get; set; }
    public string TTWinner { get; set; }
    public List<string> TTContestants { get; set; }

    public string Serialize(MeetingModelRegular value)
    {
      var options = new JsonWriterOptions
      {
        Indented = true
      };

      using (var stream = new System.IO.MemoryStream())
      {
        using (var writer = new System.Text.Json.Utf8JsonWriter(stream, options))
        {
          writer.WriteStartObject();
          writer.WriteString("MeetingType", MeetingType);
          writer.WriteString("ID", ID);
          writer.WriteString("DayOfMeeting", DayOfMeeting);
          writer.WriteString("Toastmaster", Toastmaster);
          writer.WriteString("Speaker1", Speaker1);
          writer.WriteString("Speaker2", Speaker2);
          writer.WriteString("GeneralEvaluator", GeneralEvaluator);
          writer.WriteString("Evaluator1", Evaluator1);
          writer.WriteString("Evaluator2", Evaluator2);
          writer.WriteString("TableTopics", TableTopics);
          writer.WriteString("AhCounter", AhCounter);
          writer.WriteString("Grammarian", Grammarian);
          writer.WriteString("Timer", Timer);
          writer.WriteString("QuizMaster", QuizMaster);
          writer.WriteString("Video", Video);
          writer.WriteString("HotSeat", HotSeat);
          writer.WriteString("Attendees", Attendees?.ToString());
          writer.WriteString("Resolved", Resolved);
          writer.WriteEndObject();
        }
        return Encoding.UTF8.GetString(stream.ToArray());
      }

     // return JsonSerializer.ToString<MeetingModelRegular>(value, options);
    }

    public MeetingModelBase Deserialize(string json)
    {
      byte[] data = Encoding.UTF8.GetBytes(json);
      var t = new MeetingModelRegular();

      return t;
    }
  }

  public class MeetingModel3Speaker : MeetingModelBase
  {
    public string Speaker3;
    public string Evaluator3;
  }

  public class MeetingModel5Speaker : MeetingModelBase
  {
    public MeetingModel5Speaker()
    { }

    public string Speaker3 { get; set; }
    public string Speaker4 { get; set; }
    public string Speaker5 { get; set; }
    public string Evaluator3 { get; set; }
    public string Evaluator4 { get; set; }
    public string Evaluator5 { get; set; }
  }
  public class MeetingModel
  {
    public DateTime DayOfMeeting { get; set; }
    public MemberModel Toastmaster { get; set; }
    public MemberModel Speaker1 { get; set; }
    public MemberModel Speaker2 { get; set; }
    public MemberModel GeneralEvaluator { get; set; }
    public MemberModel Evaluator1 { get; set; }
    public MemberModel Evaluator2 { get; set; }
    public MemberModel TT { get; set; }
    public MemberModel Ah { get; set; }
    public MemberModel Gram { get; set; }
    public MemberModel Timer { get; set; }
    public MemberModel Quiz { get; set; }
    public MemberModel Video { get; set; }
    public MemberModel HotSeat { get; set; }
    public List<int> Attendees { get; set; }
    public MemberModel TTWinner { get; set; }
    public List<int> TTContestants { get; set; }
    public bool Resolved { get; set; }
    public int MeetingType { get; set; }
    public MeetingModel()
    {

    }
  
    public string DayOfMeetingS
    {
      get { return DayOfMeeting.ToString(); }
    }


    public MeetingModel(string[] record, ref ObservableCollection<MemberModel> members)
    {
      if (!string.IsNullOrEmpty(record[1])) DayOfMeeting = DateTime.Parse(record[1]);
      if (!string.IsNullOrEmpty(record[2])) Toastmaster = members.Where(it => it.MemberID == Int32.Parse(record[2])).FirstOrDefault();


      if (!string.IsNullOrEmpty(record[3])) Speaker1 = members.Where(it => it.MemberID == Int32.Parse(record[3])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[4])) Speaker2 = members.Where(it => it.MemberID == Int32.Parse(record[4])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[5])) GeneralEvaluator = members.Where(it => it.MemberID == Int32.Parse(record[5])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[6])) Evaluator1 = members.Where(it => it.MemberID == Int32.Parse(record[6])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[7])) Evaluator2 = members.Where(it => it.MemberID == Int32.Parse(record[7])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[8])) TT = members.Where(it => it.MemberID == Int32.Parse(record[8])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[9])) Ah = members.Where(it => it.MemberID == Int32.Parse(record[9])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[10])) Gram = members.Where(it => it.MemberID == Int32.Parse(record[10])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[11])) Timer = members.Where(it => it.MemberID == Int32.Parse(record[11])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[12])) Quiz = members.Where(it => it.MemberID == Int32.Parse(record[12])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[13])) Video = members.Where(it => it.MemberID == Int32.Parse(record[13])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[14])) HotSeat = members.Where(it => it.MemberID == Int32.Parse(record[14])).FirstOrDefault();
      if (!string.IsNullOrEmpty(record[15]))
      {
        string m = record[15];
        char[] delims = new char[] { ';' };
        List<string> t = m.Split(delims, StringSplitOptions.None).ToList();
        Attendees = t.Select(it => Int32.Parse(it)).ToList();
      }
      if (!string.IsNullOrEmpty(record[16])) TTWinner = members.Where(it => it.MemberID == Int32.Parse(record[16])).FirstOrDefault();

      if (!string.IsNullOrEmpty(record[17]))
      {
        string m = record[17];
        char[] delims = new char[] { ';' };
        List<string> t = m.Split(delims, StringSplitOptions.None).ToList();
        TTContestants = t.Select(it => Int32.Parse(it)).ToList();
      }

      if (!string.IsNullOrEmpty(record[18]))
        Resolved = Int32.Parse(record[18]) == 0 ? false : true;

      if (!string.IsNullOrEmpty(record[19]))
        MeetingType = Int32.Parse(record[19]);
    }

    string Serialize(MeetingModel value)
    {
      var options = new JsonSerializerOptions
      {
        WriteIndented = true
      };
      return JsonSerializer.ToString<MeetingModel>(value, options);
    }
    public void Save(int meetingID)
    {
      // save meeting to file, append to existing file
      //System.IO.FileStream fileStream = new FileStream("C:\\Users\\mike\\Documents\\TI\\Meetings.dat", FileMode.Append, FileAccess.Write);
      //StreamWriter strmWriter = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\Meetings.dat", true);
      //strmWriter.WriteLine(ToFile(meetingID));
      //strmWriter.Close();
      var json = JsonSerializer.ToString<MeetingModel>(this);
      StreamWriter strmWriter = new StreamWriter("C:\\Users\\mike\\Documents\\TI\\Meetings.json", true);
      strmWriter.WriteLine(json);
      strmWriter.Close();
      //string firstLine = strmReader.ReadLine();
      //string line;
      //char[] delims = new char[] { ',' };
      //List<MeetingModel> theList = new List<MeetingModel>();
      //while ((line = strmReader.ReadLine()) != null)
      //{
      //  string[] pole = line.Split(delims, StringSplitOptions.None);
      //  MeetingModel rcd = new MeetingModel(pole, ref _members);

      //  theList.Add(rcd);
      //}

      //theList.Sort((x, y) => DateTime.Compare(x.DayOfMeeting, y.DayOfMeeting));
      //theList.Reverse();
      //_meetings = new ObservableCollection<MeetingModel>(theList);
      // Resolve meeting in file? How to write to middle of file.
      //return JsonSerializer.ToString();
      // persist info for the meeting to file meeting.dat?
      //SqlConnection sqlConn = new SqlConnection();
      //SqlCommand sqlComm = new SqlCommand();
      //sqlConn.ConnectionString = @"Server=.\SQLEXPRESS;Database=Scheduler;Integrated Security=true;";
      //sqlConn.Open();
      //sqlComm = sqlConn.CreateCommand();
      //string strTM = "UPDATE Testmembers SET Toastmaster=@Toastmaster WHERE MemberID=@MemberID";
      //sqlComm.CommandText = strTM;
      ////DateTime thedate = new DateTime(2018, 5, 18);
      //sqlComm.Parameters.AddWithValue("@Toastmaster", DayOfMeeting);
      //sqlComm.Parameters.AddWithValue("@MemberID", Toastmaster.MemberID);
      //sqlComm.ExecuteNonQuery();

      //SqlCommand spkr = new SqlCommand();
      //spkr = sqlConn.CreateCommand();
      //spkr.Parameters.AddWithValue("@Speaker", DayOfMeeting);
      //spkr.Parameters.AddWithValue("@MemberID", Speaker1.MemberID);
      //string queryspkr = "UPDATE Testmembers SET Speaker=@Speaker WHERE MemberID=@MemberID";
      //spkr.CommandText = queryspkr;
      //spkr.ExecuteNonQuery();

      //SqlCommand eval = new SqlCommand();
      //eval = sqlConn.CreateCommand();
      //eval.Parameters.AddWithValue("@Evaluator1", DayOfMeeting);
      //eval.Parameters.AddWithValue("@MemberID", Evaluator1.MemberID);
      //string evalstr = "UPDATE Testmembers SET Evaluator1=@Evaluator1 WHERE MemberID=@MemberID";
      //eval.CommandText = evalstr;
      //eval.ExecuteNonQuery();

      //sqlConn.Close();
    }

    public string ToFile(int meetingID)
    {
      StringBuilder s = new StringBuilder(meetingID.ToString());
      s.Append(",");
      s.Append(DayOfMeeting.ToString("MM/dd/yyyy"));
      s.Append(",");
      s.Append(Toastmaster.MemberID.ToString());
      s.Append(",");

      s.Append(Speaker1.MemberID.ToString());
      s.Append(",");
      s.Append(Speaker2.MemberID.ToString());
      s.Append(",");
      s.Append(GeneralEvaluator.MemberID.ToString());
      s.Append(",");
      s.Append(Evaluator1.MemberID.ToString());
      s.Append(",");
      s.Append(Evaluator2.MemberID.ToString());
      s.Append(",");
      s.Append(TT.MemberID.ToString());
      s.Append(",");
      s.Append(Ah.MemberID.ToString());
      s.Append(",");
      s.Append(Gram.MemberID.ToString());
      s.Append(",");
      s.Append(Timer.MemberID.ToString());
      s.Append(",");
      s.Append(Quiz.MemberID.ToString());
      s.Append(",");
      s.Append(Video.MemberID.ToString());
      s.Append(",");
      s.Append(HotSeat.MemberID.ToString());

      //if (!string.IsNullOrEmpty(record[15]))
      //{
      //  string m = record[15];
      //  char[] delims = new char[] { ';' };
      //  List<string> t = m.Split(delims, StringSplitOptions.None).ToList();
      //  Attendees = t.Select(it => Int32.Parse(it)).ToList();
      //}
      //if (!string.IsNullOrEmpty(record[16])) TTWinner = members.Where(it => it.MemberID == Int32.Parse(record[16])).FirstOrDefault();

      //if (!string.IsNullOrEmpty(record[17]))
      //{
      //  string m = record[17];
      //  char[] delims = new char[] { ';' };
      //  List<string> t = m.Split(delims, StringSplitOptions.None).ToList();
      //  TTContestants = t.Select(it => Int32.Parse(it)).ToList();
      //}

      //if (!string.IsNullOrEmpty(record[18]))
      //  Resolved = Int32.Parse(record[18]) == 0 ? false : true;

      //if (!string.IsNullOrEmpty(record[19]))
      //  MeetingType = Int32.Parse(record[19]);


      return s.ToString();
     
    }
    public void GenerateMeeting(List<MemberModel> members)
    {
      
      Speaker1 = members.OrderBy(a => a.Speaker).First();
      members.Remove(Speaker1);
      Speaker2 = members.OrderBy(a => a.Speaker).First();
      members.Remove(Speaker2);
      Evaluator1 = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
      members.Remove(Evaluator1);
      Evaluator2 = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.Evaluator).First();
      members.Remove(Evaluator2);
      Toastmaster = members.Where(a => a.CanBeToastmaster == true).OrderBy(a => a.Toastmaster).First();
      members.Remove(Toastmaster);
      GeneralEvaluator = members.Where(a => a.CanBeEvaluator == true).OrderBy(a => a.GeneralEvaluator).First();
      members.Remove(GeneralEvaluator);
      TT = members.OrderBy(a => a.TT).First();
      members.Remove(TT);
      HotSeat = members.OrderBy(a => a.HotSeat).First();
      members.Remove(HotSeat);
      Gram = members.OrderBy(a => a.Gram).First();
      members.Remove(Gram);
      Ah = members.OrderBy(a => a.Ah).First();
      members.Remove(Ah);
      Quiz = members.OrderBy(a => a.Quiz).First();
      members.Remove(Quiz);
      Timer = members.OrderBy(a => a.Timer).First();
      members.Remove(Timer);
      Video = members.OrderBy(a => a.Video).First();
      members.Remove(Video);
    }

    public void Generate(string role, DateTime thedate)
    {
      SqlConnection sqlConn = new SqlConnection();
      SqlCommand sql2 = new SqlCommand();
      sql2 = sqlConn.CreateCommand();
      sql2.Parameters.AddWithValue("@Speaker", thedate);
      sql2.Parameters.AddWithValue("@MemberID", Speaker1.MemberID);
      string querystr = "UPDATE Testmembers SET Speaker=@Speaker WHERE MemberID=@MemberID";
      sql2.CommandText = querystr;
    }

  }

  public class MeetingModel3 : MeetingModel
  {
    public MeetingModel3()
    { }

    public MemberModel Speaker3 { get; set; }
    public MemberModel Evaluator3 { get; set; }
  }


}
