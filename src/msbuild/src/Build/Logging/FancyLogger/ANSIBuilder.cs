﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Microsoft.Build.Logging.FancyLogger
{
    internal static class ANSIBuilder
    {
        public static string ANSIRemove(string text)
        {
            return Regex.Replace(text, "\\x1b(?:[@-Z\\-_]|\\[[0-?]*[ -\\/]*[@-~])", "");
        }

        public static class Alignment
        {
            public static string Center(string text)
            {
                string result = String.Empty;
                string noFormatString = ANSIRemove(text);
                if (noFormatString.Length > Console.BufferWidth) return text;
                int space = (Console.BufferWidth - noFormatString.Length) / 2;
                result += new string(' ', space);
                result += text;
                result += new string(' ', space);
                return result;
            }

            public static string Right(string text)
            {
                string result = String.Empty;
                string noFormatString = ANSIRemove(text);
                if (noFormatString.Length > Console.BufferWidth) return text;
                int space = Console.BufferWidth - noFormatString.Length;
                result += new string(' ', space);
                result += text;
                return result;
            }

            public static string Left(string text)
            {
                string result = String.Empty;
                string noFormatString = ANSIRemove(text);
                if (noFormatString.Length > Console.BufferWidth) return text;
                int space = Console.BufferWidth - noFormatString.Length;
                result += text;
                result += new string(' ', space);
                return result;
            }

            public static string SpaceBetween(string leftText, string rightText, int width)
            {
                string result = String.Empty;
                string leftNoFormatString = ANSIRemove(leftText);
                string rightNoFormatString = ANSIRemove(rightText);
                if (leftNoFormatString.Length + rightNoFormatString.Length > Console.BufferWidth) return leftText + rightText;
                int space = Console.BufferWidth - (leftNoFormatString.Length + rightNoFormatString.Length) - 1;
                result += leftText;
                result += new string(' ', space);
                result += rightText;
                return result;
            }
        }

        public static class Formatting
        {
            public enum ForegroundColor
            {
                Black = 30,
                Red = 31,
                Green = 32,
                Yellow = 33,
                Blue = 34,
                Magenta = 35,
                Cyan = 36,
                White = 37,
                Default = 39
            };

            public enum BackgroundColor
            {
                Black = 40,
                Red = 41,
                Green = 42,
                Yellow = 43,
                Blue = 44,
                Magenta = 45,
                Cyan = 46,
                White = 47,
                Default = 49
            }

            public static string Color(string text, ForegroundColor color)
            {
                return String.Format("\x1b[{0}m{1}\x1b[0m", (int)color, text);
            }

            public static string Color(string text, BackgroundColor color)
            {
                return String.Format("\x1b[{0}m{1}\x1b[0m", (int)color, text);
            }

            public static string Color(string text, BackgroundColor backgrdoundColor, ForegroundColor foregroundColor)
            {
                return String.Format("\x1b[{0};{1}m{2}\x1b[0m", (int)backgrdoundColor, (int)foregroundColor, text);
            }

            public static string Bold(string text)
            {
                return String.Format("\x1b[1m{0}\x1b[22m", text);
            }

            public static string Dim(string text)
            {
                return String.Format("\x1b[2m{0}\x1b[22m", text);
            }

            public static string Italic(string text)
            {
                return String.Format("\x1b[3m{0}\x1b[23m", text);
            }

            public static string Underlined(string text)
            {
                return String.Format("\x1b[4m{0}\x1b[24m", text);
            }

            public static string DoubleUnderlined(string text)
            {
                return String.Format("\x1b[21m{0}\x1b[24m", text);
            }

            public static string Blinking(string text)
            {
                return String.Format("\x1b[5m{0}\x1b[25m", text);
            }

            public static string Inverse(string text)
            {
                return String.Format("\x1b[7m{0}\x1b[27m", text);
            }

            public static string Invisible(string text)
            {
                return String.Format("\x1b[8m{0}\x1b[28m", text);
            }

            public static string CrossedOut(string text)
            {
                return String.Format("\x1b[9m{0}\x1b[29m", text);
            }

            public static string Overlined(string text)
            {
                return String.Format("\x1b[53m{0}\x1b[55m", text);
            }

            // TODO: Right now only replaces \ with /. Needs review to make sure it works on all or most terminal emulators.
            public static string Hyperlink(string text, string url)
            {
                // return String.Format("\x1b[]8;;{0}\x1b\\{1}\x1b[]8;\x1b\\", text, url);
                return url.Replace("\\", "/");
            }

            public static string DECLineDrawing(string text)
            {
                return String.Format("\x1b(0{0}\x1b(B", text);
            }
        }

        public static class Cursor
        {
            public enum CursorStyle
            {
                Default = 0,
                BlockBlinking = 1,
                BlockSteady = 2,
                UnderlineBlinking = 3,
                UnderlineSteady = 4,
                BarBlinking = 5,
                BarSteady = 6,
            }

            public static string Style(CursorStyle style)
            {
                return String.Format("\x1b[{0} q", (int)style);
            }

            public static string Up(int n = 1)
            {
                return String.Format("\x1b[{0}A", n);
            }

            public static string UpAndScroll(int n)
            {
                string result = "";
                for (int i = 0; i < n; i++) {
                    result += "\x1bM";
                }
                return result;
            }

            public static string Down(int n = 1)
            {
                return String.Format("\x1b[{0}B", n);
            }

            public static string Forward(int n = 1)
            {
                return String.Format("\x1b[{0}C", n);
            }

            public static string Backward(int n = 1)
            {
                return String.Format("\x1b[{0}D", n);
            }

            public static string Home()
            {
                return String.Format("\x1b[H");
            }

            public static string Position(int row, int column)
            {
                return String.Format("\x1b[{0};{1}H", row, column);
            }

            public static string SavePosition()
            {
                return String.Format("\x1b[s");
            }

            public static string RestorePosition() {
                return String.Format("\x1b[u");
            }
        }

        public static class Tabulator
        {
            public static string SetStop()
            {
                return String.Format("\x1bH");
            }

            public static string ForwardTab(int n)
            {
                if (n == 0) return "";
                return String.Format("\x1b[{0}I", n);
            }

            public static string BackwardTab(int n)
            {
                return String.Format("\x1b[{0}Z", n);
            }

            public static string UnsetStop()
            {
                return String.Format("\x1b[0g");
            }

            public static string UnserAlStops()
            {
                return String.Format("\x1b[3g");
            }
        }

        public static class Viewport
        {
            public static string ScrollDown(int n)
            {
                return String.Format("\x1b[{0}T", n);
            }

            public static string ScrollUp(int n)
            {
                return String.Format("\x1b[{0}S", n);
            }

            public static string SetScrollingRegion(int start, int end)
            {
                return String.Format("\x1b[{0};{1}r", start, end);
            }

            public static string PrependLines(int n)
            {
                return String.Format("\x1b[{0}L", n);
            }

            public static string DeleteLines(int n)
            {
                return String.Format("\x1b[{0}M", n);
            }
        }

        public static class Eraser
        {
            public static string DisplayCursorToEnd()
            {
                return String.Format("\x1b[0J");
            }

            public static string DisplayStartToCursor()
            {
                return String.Format("\x1b[1J");
            }

            public static string Display()
            {
                return String.Format("\x1b[2J");
            }

            public static string LineCursorToEnd()
            {
                return String.Format("\x1b[0K");
            }

            public static string LineStartToCursor()
            {
                return String.Format("\x1b[1K");
            }

            public static string Line()
            {
                return String.Format("\x1b[2k");
            }
        }

        public static class Graphics
        {
            private static int spinnerCounter = 0;
            public static string Spinner()
            {
                return Spinner(spinnerCounter++);
            }

            public static string Spinner(int n)
            {
                char[] chars = { '\\', '|', '/', '-'};
                return chars[n % (chars.Length - 1)].ToString();
            }

            public static string ProgressBar(float percentage, int width = 10, char completedChar = '█', char remainingChar = '░')
            {
                string result = String.Empty;
                for (int i = 0; i < (int)Math.Floor(width * percentage); i++)
                {
                    result += completedChar;
                }
                for (int i = (int)Math.Floor(width * percentage); i < width; i++)
                {
                    result += remainingChar;
                }
                return result;
            }

            public static string Bell()
            {
                return String.Format("\x07");
            }
        }

        public static class Buffer
        {
            public static string Fill()
            {
                return String.Format("\x1b#8");
            }

            public static string UseAlternateBuffer()
            {
                return "\x1b[?1049h";
            }

            public static string UseMainBuffer()
            {
                return "\x1b[?1049l";
            }
        }
    }
}
