using System.Reflection;
using System.Xml.Serialization;

namespace TechDebtAttributes.Report.Schema
{
    [XmlRoot("item")]
    public class DebtElement
    {
        private readonly MemberInfo _type;
        private readonly CleanCodeAttribute _attribute;

        public DebtElement()
        {
            
        }

        public DebtElement(MemberInfo type, CleanCodeAttribute attribute)
        {
            _type = type;
            _attribute = attribute;
        }

        [XmlAttribute("issue")]
        public string IssueType
        {
            get { return _attribute.IssueType.ToString(); }
            set { }

        }

        [XmlAttribute("benefit")]
        public double Benefit
        {
            get { return _attribute.RelativeBenefitToFix; }
            set
            {
                
            }
        }

        [XmlAttribute("severity")]
        public string Severity
        {
            get { return _attribute.Severity.ToString(); }
            set { }
        }

        [XmlAttribute("effort")]
        public string Effort
        {
            get { return _attribute.LevelOfEffort.ToString(); }
            set { }
        }

        [XmlElement("code")]
        public string Code
        {
            get
            {
                var s = (_type.DeclaringType == null ? string.Empty : _type.DeclaringType.FullName+".");
                return s + _type.Name;
            }
            set
            {
                
            }
        }

        [XmlElement("description")]
        public string Description
        {
            get { return _attribute.Description; }
            set { }
        }

        [XmlAttribute("reviewer")]
        public string Reviewer
        {
            get { return _attribute.Reviewer; }
            set { }
        }

        [XmlAttribute("backlogId")]
        public string BacklogId
        {
            get { return _attribute.BacklogId; }
            set { }
        }


    }
}
