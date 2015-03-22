using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using TechDebtAttributes.Report;
using TechDebtAttributes.Report.Configuration;
using TechDebtAttributes.Report.Schema;

namespace TechDebtReporting
{
    class Program
    {
        static void Main(string[] args)
        {
            var report = new ReportBuilder();
            var analyser = new AssemblyAnalyser(Configuration.ReportConfiguration.FolderToScan);

            var rpt = report.BuildReport(analyser.Analyse());

            var xmlFile = Configuration.ReportConfiguration.ReportFolder + @"\cleancode_rpeort.xml";
            //SaveXmlReport(xmlFile, rpt);

            PrintReport(rpt);

            Console.In.ReadLine();
        }

        private static CleanCodeConfigurationSection Configuration
        {
            get
            {
                return ConfigurationManager.GetSection("cleanCode") as CleanCodeConfigurationSection;
            }
        }

        private static void PrintReport(CleanCodeReport report)
        {
            var serializerObj = new XmlSerializer(typeof(CleanCodeReport));
            serializerObj.Serialize(Console.OpenStandardOutput(), report);
        }

        private static void SaveXmlReport(string path, CleanCodeReport report)
        {
            var serializerObj = new XmlSerializer(typeof(CleanCodeReport));
            using(var writer = new IndentedTextWriter(new StreamWriter(path))) {
                serializerObj.Serialize(writer, report);
            }
        }
    }
}
