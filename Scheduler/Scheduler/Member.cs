using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
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
    //public List<DateTime> MeetingsOut { get; set; }
    public List<int> MeetingsOut { get; set; }
  }
}
