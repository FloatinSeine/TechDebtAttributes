using System.Reflection;
using ApprovalTests;
using ApprovalTests.Reporters;
using ExampleUsage;
using Xunit;

namespace TechDebtAttributes.Tests
{
    public class CleanCodeReportTests
    {
        [Fact]
        [UseReporter(typeof(NotepadLauncher))]
        public void ShouldRenderReport()
        {
            var assemblyToReportOn = Assembly.GetAssembly(typeof(SomeThingToClean));

            Approvals.Verify(CleanCodeReporter.GenerateReport(assemblyToReportOn));
        }
    }
}
