using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerUI
{
  public class MemberModel
  {
    public int MemberID { get; set; }
    public string Name { get; set; }
    public DateTime Toastmaster { get; set; }
    public DateTime Speaker { get; set; }
    public DateTime GeneralEvaluator { get; set; }
    public DateTime Evaluator { get; set; }
    public DateTime TT { get; set; }
    public DateTime Ah { get; set; }
    public DateTime Gram { get; set; }
    public DateTime Timer { get; set; }
    public DateTime Quiz { get; set; }
    public DateTime Video { get; set; }
    public DateTime HotSeat { get; set; }
    public bool HasBeenOfficer { get; set; }
    public bool CanBeToastmaster { get; set; }
    public bool CanBeEvaluator { get; set; }
    public List<int> MeetingsOut { get; set; }
    public List<string> Mentors { get; set; }

    public MemberModel()
    {

    }

    public MemberModel(string[] record)
    {
      MemberID = System.Int32.Parse(record[1]);
      Name = record[2];
      if (!string.IsNullOrEmpty(record[3]))
        Toastmaster = System.DateTime.Parse(record[3]);
      if (!string.IsNullOrEmpty(record[4])) Speaker = System.DateTime.Parse(record[4]);
      if (!string.IsNullOrEmpty(record[5]))
        GeneralEvaluator = System.DateTime.Parse(record[5]);
      if (!string.IsNullOrEmpty(record[6])) Evaluator = System.DateTime.Parse(record[6]);
      if (!string.IsNullOrEmpty(record[7])) TT = System.DateTime.Parse(record[7]);
      if (!string.IsNullOrEmpty(record[8])) Ah = System.DateTime.Parse(record[8]);
      if (!string.IsNullOrEmpty(record[9])) Gram = System.DateTime.Parse(record[9]);
      if (!string.IsNullOrEmpty(record[10])) Timer = System.DateTime.Parse(record[10]);
      if (!string.IsNullOrEmpty(record[11])) Quiz = System.DateTime.Parse(record[11]);
      if (!string.IsNullOrEmpty(record[12])) Video = System.DateTime.Parse(record[12]);
      if (!string.IsNullOrEmpty(record[13]))  HotSeat = System.DateTime.Parse(record[13]);
      if (!string.IsNullOrEmpty(record[14]))
        HasBeenOfficer = System.Boolean.Parse(record[14]);
      else
        HasBeenOfficer = false;


      if (!string.IsNullOrEmpty(record[15]))
        CanBeToastmaster = System.Boolean.Parse(record[15]);
      else
        CanBeToastmaster = false;

      if (!string.IsNullOrEmpty(record[16]))
        CanBeEvaluator = System.Boolean.Parse(record[16]);
      else
        CanBeEvaluator = false;

      if (!string.IsNullOrWhiteSpace(record[17]))
      {
        string m = record[17];
        char[] delims = new char[] { ';' };
        List<string> t = m.Split(delims, StringSplitOptions.None).ToList();
        MeetingsOut = t.Select(it => System.Int32.Parse(it)).ToList();
      }

      if (!string.IsNullOrEmpty(record[18]))
      {
        string m = record[18];
        char[] delims = new char[] { ';' };
        Mentors = m.Split(delims, StringSplitOptions.None).ToList();

      }
    }

  }
}

