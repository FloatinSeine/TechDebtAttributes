
using System.Configuration;

namespace TechDebtAttributes.Report.Configuration
{
    public class ReportConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("binaries", IsRequired = true)]
        public string FolderToScan
        {
            get { return (string)this["binaries"]; }
            set { this["binaries"] = value; }
        }

        [ConfigurationProperty("report", IsRequired = true)]
        public string ReportFolder
        {
            get { return (string)this["report"]; }
            set { this["report"] = value; }
        }
    }
}
