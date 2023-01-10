// borrowed from https://github.com/encse/adventofcode
using System;
using System.Linq;
using System.Text;
using AdventOfCode.Model;

namespace AdventOfCode.Generator;

class SplashScreenGenerator {
    public string Generate(Calendar calendar) {
        string calendarPrinter = CalendarPrinter(calendar);
        return $@"
            |using System;
            |
            |namespace AdventOfCode.Y{calendar.Year};
            |
            |class SplashScreenImpl : SplashScreen {{
            |
            |    public void Show() {{
            |
            |        var color = Console.ForegroundColor;
            |        {calendarPrinter.Indent(12)}
            |        Console.ForegroundColor = color;
            |        Console.WriteLine();
            |    }}
            |
            |   private static void Write(int rgb, bool bold, string text){{
            |       Console.Write($""\u001b[38;2;{{(rgb>>16)&255}};{{(rgb>>8)&255}};{{rgb&255}}{{(bold ? "";1"" : """")}}m{{text}}"");
            |   }}
            |}}
            |".StripMargin();
    }

    private string CalendarPrinter(Calendar calendar) {

        var lines = calendar.Lines.Select(line =>
            new[] { new CalendarToken { Text = "           " } }.Concat(line)).ToList();

        var bw = new BufferWriter();
        foreach (var line in lines) {
            foreach (var token in line) {
                bw.Write(token.ConsoleColor, token.Text, token.Bold);
            }

            bw.Write(-1, "\n", false);
        }
        return bw.GetContent();
    }

    bool Matches(string[] selector, object x){
        return true;
    }

}
