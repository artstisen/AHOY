using System;
using System.IO;
using Xunit;

namespace AhoyBrowser.Tests
{
    [Collection("IO")]
    public sealed class CrashReporterTests : IDisposable
    {
        private readonly string root;

        public CrashReporterTests()
        {
            root = Path.Combine(Path.GetTempPath(), "AhoyTests-" + Guid.NewGuid().ToString("N"));
            IO.DataRootOverride = root;
        }

        public void Dispose()
        {
            IO.DataRootOverride = null;
            try { Directory.Delete(root, true); } catch { /* best effort */ }
        }

        [Fact]
        public void WriteLog_AppendsErrorToDataFolder()
        {
            Exception error;
            try { throw new InvalidOperationException("boom"); }
            catch (Exception ex) { error = ex; }

            string path = CrashReporter.WriteLog(error);
            Assert.Equal(Path.Combine(root, IO.ProgramFolderName, CrashReporter.LogFileName), path);

            CrashReporter.WriteLog(new Exception("second"));
            string log = File.ReadAllText(path);
            Assert.Contains("InvalidOperationException: boom", log);
            Assert.Contains("second", log);
            Assert.Contains("Version: ", log);
            Assert.Contains(nameof(WriteLog_AppendsErrorToDataFolder), log); // stack trace included
        }
    }
}
