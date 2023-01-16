using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace AdventOfCode.Y2018.Day21;

public class Helper {
    public IEnumerable<int> Run(string name, string fileInput) {
        var run = Run<int[], IEnumerable<int[]>>(name, fileInput, new int[]{28});

        var visited = new List<int>();
        foreach(var r in run(new int[] { 0, 0, 0, 0, 0, 0 })){
            if (visited.Contains(r[3])) {
                break;
            }
            visited.Add(r[3]);
            yield return r[3];
        }
    }

    public Func<A, B> Run<A, B>(string name, string fileInput, int[] breakpoints) {
        var code = Code(fileInput, breakpoints);
        var syntaxTree = SyntaxFactory.ParseSyntaxTree(code);
        var location = typeof(object).GetTypeInfo().Assembly.Location;
        var sysRef = MetadataReference.CreateFromFile(location);
        var compilation = CSharpCompilation.Create(name)
            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .AddReferences(sysRef)
            .AddSyntaxTrees(syntaxTree);

        var ms = new MemoryStream();
        EmitResult compilationResult = compilation.Emit(ms);
        if (compilationResult.Success) {
            ms.Seek(0, SeekOrigin.Begin);
            var asm = AssemblyLoadContext.Default.LoadFromStream(ms);
            var m = asm.GetType("RoslynCore.Helper").GetMethod("Run");
            return (Func<A, B>)Delegate.CreateDelegate(typeof(Func<A, B>), null, m);
        } else {
            foreach (Diagnostic codeIssue in compilationResult.Diagnostics) {
                string issue = $"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()}, Location: { codeIssue.Location.GetLineSpan()}, Severity: { codeIssue.Severity}";
                Console.WriteLine(issue);
            }

            throw new Exception();
        }
    }

    public string Code(string fileInput, int[] breakpoints) {
        var ipRegistry = int.Parse(fileInput.Split("\n").First().Substring("#ip ".Length));
        var fi = fileInput.Split("\n").Skip(1).ToArray();

        var sb = new StringBuilder();

        for (var ip = 0; ip < fi.Length; ip++) {
            var line = fi[ip];
            var lineParts = line.Split(";")[0].Trim().Split(" ");
            var stm = lineParts.Skip(1).Select(int.Parse).ToArray();

            // because time travel is a dangerous activity, the activation system begins with a few instructions which verify 
            // that bitwise AND (via bani) does a numeric operation and not an operation as if the inputs were interpreted as strings. 
            // If the test fails, it enters an infinite loop re-running the test instead of allowing the program to execute normally. 
            // If the test passes, the program continues, and assumes that all other bitwise operations (banr, bori, and borr) also interpret their inputs as numbers.
            //  (Clearly, the Elves who wrote this system were worried that someone might introduce a bug while trying to emulate this system with a scripting language.)
            var compiledStm = lineParts[0] switch {
                "addr" => $"r[{stm[2]}] = r[{stm[0]}] + r[{stm[1]}]",
                "addi" => $"r[{stm[2]}] = r[{stm[0]}] + {stm[1]}",
                "mulr" => $"r[{stm[2]}] = r[{stm[0]}] * r[{stm[1]}]",
                "muli" => $"r[{stm[2]}] = r[{stm[0]}] * {stm[1]}",
                "banr" => $"r[{stm[2]}] = r[{stm[0]}] & r[{stm[1]}]",
                "bani" => $"r[{stm[2]}] = r[{stm[0]}] & {stm[1]}",
                "borr" => $"r[{stm[2]}] = r[{stm[0]}] | r[{stm[1]}]",
                "bori" => $"r[{stm[2]}] = r[{stm[0]}] | {stm[1]}",
                "setr" => $"r[{stm[2]}] = r[{stm[0]}]",
                "seti" => $"r[{stm[2]}] = {stm[0]}",
                "gtir" => $"r[{stm[2]}] = {stm[0]} > r[{stm[1]}] ? 1 : 0",
                "gtri" => $"r[{stm[2]}] = r[{stm[0]}] > {stm[1]} ? 1 : 0",
                "gtrr" => $"r[{stm[2]}] = r[{stm[0]}] > r[{stm[1]}] ? 1 : 0",
                "eqir" => $"r[{stm[2]}] = {stm[0]} == r[{stm[1]}] ? 1 : 0",
                "eqri" => $"r[{stm[2]}] = r[{stm[0]}] == {stm[1]} ? 1 : 0",
                "eqrr" => $"r[{stm[2]}] = r[{stm[0]}] == r[{stm[1]}] ? 1 : 0",
                _ => throw new ArgumentException()
            };
            var brk = breakpoints.Contains(ip) ? "yield return r;" : "";
            sb.AppendLine($"\t\tcase {ip}: {brk} {compiledStm}; r[{ipRegistry}]++; break;");
        }

        return $@"
            using System;
            using System.Collections.Generic;
            namespace RoslynCore
            {{
                public static class Helper
                {{
                    public static IEnumerable<int[]> Run(int[] r) {{
                        while(true) {{
                            switch (r[{ipRegistry}]) {{
                                {sb.ToString()}
                            }}
                        }}
                    }}
                }}
            }}
        ";
    }

}