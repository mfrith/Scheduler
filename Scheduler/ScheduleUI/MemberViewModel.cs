using SchedulerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleUI
{
  class MemberInfoViewModel
  {
    private MemberModel _member;
    private bool _modified;

    public MemberInfoViewModel(MemberModel member)
    {
      _member = member;
    }

    public string Name
    {
      get { return _member.Name; }
    }

    public bool IsCurrent
    {
      get { return _member.IsCurrentMember; }
    }

    public bool CanBeToastmaster
    {
      get { return _member.CanBeToastmaster; }
    }

    public bool CanBeEvaluator
    {
      get { return _member.CanBeEvaluator; }
    }
  }
}
