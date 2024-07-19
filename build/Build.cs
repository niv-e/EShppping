using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.ReportGenerator;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Publish);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath TestResultsDirectory => RootDirectory / "test-results";
    AbsolutePath CoverageResultsDirectory => RootDirectory / "coverage-results";
    AbsolutePath CoverageReport => CoverageResultsDirectory / "coverage-report";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            DotNetClean(s => s
                .SetProject(Solution)
                .SetConfiguration(Configuration));
            EnsureCleanDirectory(TestResultsDirectory);
            EnsureCleanDirectory(CoverageResultsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            if (IsLocalBuild)
            {
                System.Diagnostics.Debugger.Launch();
            }
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .EnableCollectCoverage()
                .SetCoverletOutputFormat(CoverletOutputFormat.cobertura)
                .SetCoverletOutput(CoverageResultsDirectory / "coverage.cobertura.xml")
                .SetResultsDirectory(TestResultsDirectory));
        });

    Target Coverage => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            if (IsLocalBuild)
            {
                System.Diagnostics.Debugger.Launch();
            }

            ReportGenerator(s => s
                .SetReports(CoverageResultsDirectory / "*.xml")
                .SetTargetDirectory(CoverageReport)
                .SetReportTypes(ReportTypes.HtmlInline));
        });

    Target Publish => _ => _
        .DependsOn(Coverage)
        .OnlyWhenStatic(() => Configuration.Equals(Configuration.Release))
        .Executes(() =>
        {
            DotNetPublish(s => s
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });
}