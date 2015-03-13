using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TechDebtAttributes
{
    public static class CleanCodeReporter
    {
        public static string GenerateReport(params Assembly[] assemblies)
        {
            var typesWithTechDebt = FindTypesWithCleanCodeAttributes(assemblies);

            var reportLines = GenerateReportLines(typesWithTechDebt);

            return RenderReportLinesToTextReport(reportLines);
        }

        private static IEnumerable<MemberInfo> FindTypesWithCleanCodeAttributes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(FindAllTheTypesThatHaveCleanCodeAttributes);
        }

        private static IEnumerable<MemberInfo> FindAllTheTypesThatHaveCleanCodeAttributes(Assembly assembly)
        {
            return assembly.GetTypes()
                .SelectMany(type => type.GetMembers())
                .Union(assembly.GetTypes())
                .Where(type => Attribute.IsDefined(type, typeof(CleanCodeAttribute)));
        }

        private static IEnumerable<ReportLine> GenerateReportLines(IEnumerable<MemberInfo> typesWithTechDebt)
        {
            var reportItems = new List<ReportLine>();

            foreach (var type in typesWithTechDebt)
            {
                var techDebtAttribute =
                    (CleanCodeAttribute)type.GetCustomAttributes(typeof(CleanCodeAttribute), inherit: false)[0];

                

                reportItems.Add(new ReportLine
                {
                    Attribute = techDebtAttribute,
                    TypeOrMemberName = type.ToString(),
                    DeclaringType = type.DeclaringType!=null ? type.DeclaringType.ToString() : string.Empty
                });
            }
            return reportItems;
        }

        private static string RenderReportLinesToTextReport(IEnumerable<ReportLine> reportLines)
        {
            var sb = new StringBuilder();

            sb.AppendLine("***Start of Tech Debt Report - finding all [TechDebt] attribute usages");

            sb.AppendLine();
            sb.AppendLine("Type\tBeneft\tSeverity\tEffort\tCode\tDescription\tReviewer\tBacklogId");

            foreach (var item in reportLines.OrderByDescending(x=>x.Attribute.IssueType).ThenByDescending(x => x.Attribute.RelativeBenefitToFix))
            {
                sb.AppendLine(item.ToString());
            }

            sb.AppendLine();
            sb.AppendLine("***End.");

            return sb.ToString();
        }


        private class ReportLine
        {
            public string TypeOrMemberName { get; set; }
            public string DeclaringType { get; set; }
            public CleanCodeAttribute Attribute { get; set; }

            public override string ToString()
            {
                return string.Format("{0}\t{1:0.#}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                    Attribute.IssueType,
                    Attribute.RelativeBenefitToFix, Attribute.Severity, Attribute.LevelOfEffort,
                    FullName(), Attribute.Description,
                    Attribute.Reviewer,Attribute.BacklogId);
            }

            private string FullName()
            {
                return (string.IsNullOrEmpty(DeclaringType) ? TypeOrMemberName : DeclaringType + "." + TypeOrMemberName);

            }
        }
    }
}
