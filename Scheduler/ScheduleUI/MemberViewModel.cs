using SchedulerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleUI
{
  class MemberInfoViewModel : PropertyChangedBase
  {
    private MemberModel _member;
    private bool _modified;

    public MemberInfoViewModel(MemberModel member)
    {
      _member = member;
      _name = member.Name;
      _iscurrent = member.IsCurrentMember;
      _canBeEvaluator = member.CanBeEvaluator;
      _canBeToastmaster = member.CanBeToastmaster;
      _mentors = member.Mentors;
      _id = member.MemberID;
    }

    private string _name;
    public string Name
    {
      get { return _name; }
      set { SetProperty(ref _name, value, () => Name); }
    }

    private bool _iscurrent;
    public bool IsCurrent
    {
      get { return _iscurrent; }
      set { SetProperty(ref _iscurrent, value, () => IsCurrent); }

    }

    private bool _canBeToastmaster;
    public bool CanBeToastmaster
    {
      get { return _canBeToastmaster; }
      set { SetProperty(ref _canBeToastmaster, value, () => CanBeToastmaster); }
    }

    private bool _canBeEvaluator;
    public bool CanBeEvaluator
    {
      get { return _canBeEvaluator; }
      set { SetProperty(ref _canBeEvaluator, value, () => CanBeEvaluator); }
    }

    private int _id;
    public int ID
    {
      get { return _id; }
      set { SetProperty(ref _id, value, () => ID); }
    }
    private List<string> _mentors;
    public List<string> Mentors
    {
      get { return _mentors; }
      set { SetProperty(ref _mentors, value, () => Mentors); }
    }
  }
}
