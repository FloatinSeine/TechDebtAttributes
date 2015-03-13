using System;

namespace TechDebtAttributes
{
    public enum IssueType
    {
        CodeSmell,
        Dry,
        Idempotence,
        Kiss,
        LawOfDementer,
        Solid,
        TellDontAsk,
        TechnicalDebt,
        Yagni
    }

    public enum LevelOfEffort
    {
        Trivial = 1,
        Minor = 2,
        Medium = 3,
        Large = 4,
        Massive = 5
    }

    public enum Severity
    {
        Annoying = 1,
        Mild = 2,
        Painful = 3,
        Chronic = 4,
        Disabling = 5
    }

    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Delegate | AttributeTargets.Enum |
        AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.GenericParameter | AttributeTargets.Interface |
        AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct,
        AllowMultiple = false)]
    public class CleanCodeAttribute : Attribute
    {
        private readonly IssueType _issueType;
        private readonly Severity _severity;
        private readonly LevelOfEffort _effort;

        public CleanCodeAttribute(IssueType type, Severity severity, LevelOfEffort effort)
        {
            _issueType = type;
            _severity = severity;
            _effort = effort;
        }

        public IssueType IssueType
        {
            get { return _issueType; }
        }

        public Severity Severity
        {
            get { return _severity;  }
        }
        public LevelOfEffort LevelOfEffort
        {
            get { return _effort; }
        }

        public string Description { get; set; }
        public string Reviewer { get; set; }
        public string BacklogId { get; set; }
        public int Pain { get; set; }
        public int EffortToFix { get; set; }


        public double RelativeBenefitToFix
        {
            get
            {
                var s = Convert.ToInt32(_severity);
                var e = Convert.ToInt32(_effort);
                return (double) s / e;
            }
        }
    }
}
