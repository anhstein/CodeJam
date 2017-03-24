using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
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
        
        public string ContestName
        {
            get
            {
                return this.GetType().GetTypeInfo().GetCustomAttribute<DescriptionAttribute>().Description;
            }
        }

        public string InputFolder
        {
            get
            {
                //System.AppContext.BaseDirectory returns [BaseDirectory]\bin\Debug\netcoreapp1.1
                return Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, @"..\..\..", ContestName, ProblemName));
            }
        }

        [Theory]
        [InlineData("sample.in")]
        [InlineData("A-small-attempt0.in")]
        [InlineData("A-large.in")]
        public void Run(string inputFile)
        {
            inputFile = Path.Combine(InputFolder, inputFile);
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
