using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day10;

public static class MessageExtension
{
    public static MessageString Msg(this string st) 
    {
        return new MessageString(st);
    }
}