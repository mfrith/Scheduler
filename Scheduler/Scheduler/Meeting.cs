using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{

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

    public void Save()
    {
      
      // persist info for the meeting
      SqlConnection sqlConn = new SqlConnection();
      SqlCommand sqlComm = new SqlCommand();
      sqlConn.ConnectionString = @"Server=.\SQLEXPRESS;Database=Scheduler;Integrated Security=true;";
      sqlConn.Open();
      sqlComm = sqlConn.CreateCommand();
      string strTM = "UPDATE Testmembers SET Toastmaster=@Toastmaster WHERE MemberID=@MemberID";
      sqlComm.CommandText = strTM;
      //DateTime thedate = new DateTime(2018, 5, 18);
      sqlComm.Parameters.AddWithValue("@Toastmaster", DayOfMeeting);
      sqlComm.Parameters.AddWithValue("@MemberID", Toastmaster.MemberID);
      sqlComm.ExecuteNonQuery();

      SqlCommand spkr = new SqlCommand();
      spkr = sqlConn.CreateCommand();
      spkr.Parameters.AddWithValue("@Speaker", DayOfMeeting);
      spkr.Parameters.AddWithValue("@MemberID", Speaker1.MemberID);
      string queryspkr = "UPDATE Testmembers SET Speaker=@Speaker WHERE MemberID=@MemberID";
      spkr.CommandText = queryspkr;
      spkr.ExecuteNonQuery();

      SqlCommand eval = new SqlCommand();
      eval = sqlConn.CreateCommand();
      eval.Parameters.AddWithValue("@Evaluator1", DayOfMeeting);
      eval.Parameters.AddWithValue("@MemberID", Evaluator1.MemberID);
      string evalstr = "UPDATE Testmembers SET Evaluator1=@Evaluator1 WHERE MemberID=@MemberID";
      eval.CommandText = evalstr;
      eval.ExecuteNonQuery();

      sqlConn.Close();
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
}
