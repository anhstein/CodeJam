using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Anhstein.CodeJam
{
    public abstract class CodeJamBase
    {
        protected readonly ITestOutputHelper Output;

        public CodeJamBase(ITestOutputHelper output)
        {
            this.Output = output;
        }

        public string ProblemName
        {
            get
            {
                return this.GetType().Name;
            }
        }
        
        [Theory]
        [InlineData("sample.in")]
        public void Run(string inputFile)
        {
            inputFile = Path.Combine(Directory.GetCurrentDirectory(), ProblemName, inputFile);
            Output.WriteLine(inputFile);
            Assert.False(string.IsNullOrEmpty(inputFile));
            Assert.True(File.Exists(inputFile));

            int t;
            FileStream fileStream = new FileStream(inputFile, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            using (StreamWriter writer = File.CreateText(inputFile.Replace(".in", ".out")))
            {
                t = int.Parse(reader.ReadLine());
                for (int caseNum = 1; caseNum <= t; caseNum++)
                {
                    writer.WriteLine("Case #{0}: {1}", caseNum, Solve(reader));
                }
            }
        }

        public abstract object Solve(StreamReader reader);
    }
}
