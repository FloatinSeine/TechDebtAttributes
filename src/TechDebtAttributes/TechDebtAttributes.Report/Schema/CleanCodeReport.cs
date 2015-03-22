using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TechDebtAttributes.Report.Schema
{
    [XmlRoot("report")]
    public class CleanCodeReport
    {
        public CleanCodeReport()
        {
            Elements = new List<DebtElement>();
            Date = DateTime.UtcNow;
        }

        [XmlElement("date")]
        public DateTime Date { get; set; }

        [XmlArrayItem("item", typeof(DebtElement))]
        [XmlArray("codeToClean")]
        public List<DebtElement> Elements { get; set; } 
    }
}
