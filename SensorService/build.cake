var target = Argument("target", "Report");

#addin nuget:?package=Cake.Coverlet&version=2.1.2
#tool nuget:?package=ReportGenerator&version=4.0.4

var testProjectsRelativePaths = new string[]
{
  "../SensorServiceTests/SensorServiceTests.csproj"
};

var parentDirectory = Directory(".");
var coverageDirectory = parentDirectory + Directory("code_coverage");
var coberturaFileName = "results";
var coberturaFileExtension = ".cobertura.xml";
var reportTypes = "HtmlInline_AzurePipelines"; // Use "Html" value locally for performance and files' size.
var coverageFilePath = coverageDirectory + File(coberturaFileName + coberturaFileExtension);
var jsonFilePath = coverageDirectory + File(coberturaFileName + ".json");

Task("Clean")
  .Does(() =>
{
  if (!DirectoryExists(coverageDirectory))
  {
    CreateDirectory(coverageDirectory);
  }
  else
  {
    CleanDirectory(coverageDirectory);
  }
});

Task("Test")
  .IsDependentOn("Clean")
  .Does(() =>
{
  var testSettings = new DotNetCoreTestSettings
  {
    ArgumentCustomization = args => args.Append($"--logger trx")
  };

  var coverletSettings = new CoverletSettings
  {
    CollectCoverage = true,
    CoverletOutputDirectory = coverageDirectory,
    CoverletOutputName = coberturaFileName
  };

  if (testProjectsRelativePaths.Length == 1)
  {
    coverletSettings.CoverletOutputFormat = CoverletOutputFormat.cobertura;
    DotNetCoreTest(testProjectsRelativePaths[0], testSettings, coverletSettings);
  }
  else
  {
    DotNetCoreTest(testProjectsRelativePaths[0], testSettings, coverletSettings);

    coverletSettings.MergeWithFile = jsonFilePath;
    for (int i = 1; i < testProjectsRelativePaths.Length; i++)
    {
      if (i == testProjectsRelativePaths.Length - 1)
      {
        coverletSettings.CoverletOutputFormat  = CoverletOutputFormat.cobertura;
      }

      DotNetCoreTest(testProjectsRelativePaths[i], testSettings, coverletSettings);
    }
  }
});

Task("Report")
  .IsDependentOn("Test")
  .Does(() =>
{
  var reportSettings = new ReportGeneratorSettings
  {
    ArgumentCustomization = args => args.Append($"--reportTypes:{reportTypes}")
  };
  ReportGenerator(coverageFilePath, coverageDirectory, reportSettings);
});

RunTarget(target);

