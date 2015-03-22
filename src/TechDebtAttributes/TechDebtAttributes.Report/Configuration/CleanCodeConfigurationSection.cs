using System.Configuration;

namespace TechDebtAttributes.Report.Configuration
{
    public class CleanCodeConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("folders", IsRequired = true)]
        public ReportConfigurationElement ReportConfiguration
        {
            get { return (ReportConfigurationElement)this["folders"]; }
        }
    }
}
