# Track technical debt at the source code level

Decorating your code with attributes to markup where technical debt exists gives an oppertunity to create live documentation directly from source code and compliments standard static analysis tools.

In this fork have added a new atttribute called CleanCode with a different set of properties for highlighting issues within your codebase.

A modified Reporter is added to create a Tab delimited output which can be imported into a spreadsheet or translated and imported into other tools such as SonarQube

##New Parameters are:

Issue Type - The principles being broken or a generic flag for Technical Debt

Severity - Flags relatively how much does this hurt the developer

Level of Effort - Flags relative size of the effort to fix

Reviewer - Who flagged the code

BacklogId - Add a Jira, or similar too, Id to the code to highlight the flagged code has been/submitted for prioritisation


#Usage
The [CleanCode] Attribute has a [Conditional("Debug")] attribute applied so the release code will not be decorate with atttribtues that have no value in the release build.


Usage is similar to the original [TechDebt] attribute but [CleanCode] is provided as an alternative to be more descriptive.


```
[CleanCode(IssueType.Yagni, Severity.Mild, LevelOfEffort.Minor, Description = "Pointless interface you wont need."]
public interface ISomeDumbInterface
{     
}
```


The BacklogId and Reviewer fields will allow you to reference back to a User Story or Bug in the tool you use to manage your product backlog.

```
[CleanCode(IssueType.Yagni, Severity.Mild, LevelOfEffort.Minor, Description = "Pointless interface you wont need.", BacklogId="1000", Reviewer="Steve"]
public interface ISomeDumbInterface
{     
}
```


## Original Project
The original project from Jason Roberts is available here:

https://github.com/jason-roberts/TechDebtAttributes/




--------

## Step 1: Capture

Add attributes to your production code where you find technical debt that you can not currently fix:

Install NuGet Package: TechDebtAttributes into your production assembly(s)


Use [TechDebt] attributes to when you find technical debt that you can't fix right away:

```
[TechDebt(10, 44, description = "This is dumb, we should remove it")]
public interface ISomeDumbInterface
{     
}
```

In the example above, this debt has a pain value of 10 and an effort to fix of 44.

You decide what the relative values mean for these ints.

## Step 2 Report

Install NuGet Package: TechDebtAttributes into your test project

Add a test in your test project to output a report of all tech debt:

```
public class WhatsTheTechDebt
{
	[Fact]
	public void ReportOnTechDebtButNeverFailATest()
	{
		var assemblyContainingTechDebt = Assembly.GetAssembly(typeof (SomeThing));

		var report = TechDebtReporter.GenerateReport(assemblyContainingTechDebt);

		Console.WriteLine(report);
	}
}	
```	
	
Run the test and get the report:	

```	
Start of Tech Debt Report - finding all [TechDebt] attribute usages
Benefit to fix: 1666.7  Void .ctor() Pain:5000 Effort to fix:3
Benefit to fix: 5 Quick fix to stop stupid stuff happening sometimes Void SomeMethod() Pain:5 Effort to fix:1
Benefit to fix: 2  ExampleUsage.SillyEnum Tomato Pain:47 Effort to fix:23
Benefit to fix: 0.3 This really is silly ExampleUsage.SillyEnum Pain:2 Effort to fix:6
Benefit to fix: 0.2 This is dumb, we should remove it ExampleUsage.ISomeDumbInterface Pain:10 Effort to fix:44
Benefit to fix: 0.1 This should be moved to it's own interface Void Y() Pain:10 Effort to fix:100
Benefit to fix: 0 There's a lot of work to fix this whole class for not much gain ExampleUsage.SomeThing Pain:1 Effort to fix:200
End of Tech Debt Report.
```

## Step 3: Fail tests if too much tech debt exists (optional)

```
// This test will fail because there is more than total of 10 pain in all tech debt
[Fact]
public void ReportOnTechDebtAndFailTestIfTotalPainExceeded()
{
	var assemblyContainingTechDebt = Assembly.GetAssembly(typeof(SomeThing));

	const int maximumPainInCodebaseThatWereWillingToLiveWith = 10;

	TechDebtReporter.AssertMaxPainNotExceeded(assemblyContainingTechDebt, maximumPainInCodebaseThatWereWillingToLiveWith);            
}
```

## More examples:

https://github.com/jason-roberts/TechDebtAttributes/blob/master/src/TechDebtAttributes/ExampleUsage.Tests/UsingTechDebtAttributesInTests.cs

https://github.com/jason-roberts/TechDebtAttributes/blob/master/src/TechDebtAttributes/TechDebtAttributes.Tests/TechDebtReporterTests.cs


--------

## About Jason Roberts

Jason Roberts is a Microsoft MVP, [Pluralsight course author](http://bit.ly/psjasonroberts) and Journeyman Software Developer with over 12 years experience.

He is the author of the books [Keeping Software Soft](http://keepingsoftwaresoft.com)) and [C# Tips](http://bit.ly/sharpbook) and writes at his blog [DontCodeTired.com](http://dontcodetired.com).