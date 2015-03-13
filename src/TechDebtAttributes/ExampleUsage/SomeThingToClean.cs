using TechDebtAttributes;

namespace ExampleUsage
{
    [CleanCode(IssueType.Solid, Severity.Mild, LevelOfEffort.Medium, Description = "Lacks an interface.",BacklogId = "1000", Reviewer = "Steve")]
    public class SomeThingToClean
    {
        [CleanCode(IssueType.Solid, Severity.Painful, LevelOfEffort.Minor, Description = "Breaks OCP")]
        public void SomeMethod()
        {
        }

        [CleanCode(IssueType.Yagni, Severity.Mild, LevelOfEffort.Medium, Description = "Pointless method you wont need.")]
        public void SomeMethod2()
        {
        }
    }
}
