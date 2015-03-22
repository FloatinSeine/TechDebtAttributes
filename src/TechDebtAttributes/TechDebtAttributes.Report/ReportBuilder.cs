
using System.Collections.Generic;
using System.Reflection;
using TechDebtAttributes.Report.Schema;

namespace TechDebtAttributes.Report
{
    public class ReportBuilder
    {

        public CleanCodeReport BuildReport(IEnumerable<MemberInfo> typesWithTechDebt)
        {
            return GenerateReport(typesWithTechDebt);
        }

        private static CleanCodeReport GenerateReport(IEnumerable<MemberInfo> typesWithTechDebt)
        {
            var rpt = new CleanCodeReport();

            foreach (var type in typesWithTechDebt)
            {
                var techDebtAttribute =
                   (CleanCodeAttribute)type.GetCustomAttributes(typeof(CleanCodeAttribute), inherit: false)[0];

                var elem = new DebtElement(type, techDebtAttribute);
                rpt.Elements.Add(elem);
            }

            return rpt;
        }
    }
}
