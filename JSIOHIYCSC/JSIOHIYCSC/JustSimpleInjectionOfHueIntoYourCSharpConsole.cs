using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace JSIOHIYCSC {

    /// <summary> Enum of diferent color options, they are used to draw hues with fancy colors </summary>
    public enum HueColorMode {
        /// <summary> Hues will be drawn using default ConsoleColor </summary>
        NoColor,
        /// <summary> Eatch hue will have diferent color </summary>
        OneTypeOfHueColor,
        /// <summary> NSFL </summary>
        TotalHue
    }

    /// <summary> Main class of JSIOHIYCSC library, contains a large abount of hue and some code. I am little scared of this class and I am just gland it is static ... </summary>
    public static class JustSimpleInjectionOfHueIntoYourCSharpConsole {

        private static bool IsHueInjected = false;
        private static bool ShowInformationMessages = true;
        private static int HueTimerMinValue = 250;
        private static int HueTimerMaxValue = 3000;
        private static HueColorMode HueColorSettings;
        private static int HueShowedCounter = 0;

        private static Random HueRandomizer = new Random();
        private static Timer HueTimer = new Timer();

        private static string HueString = "hue";

        /// <summary> Returns true is if hue is injected into your assembly </summary>
        public static bool IsInjected() {
            return IsHueInjected;
        }

        /// <summary> Returns how many hues was drawn from start of injection</summary>
        public static int GetHueCounterValue() {
            return HueShowedCounter;
        }

        /// <summary> Sets minimal and maximal value of randomizer for timer that triggers hue drawing, value is in miliseconds </summary>
        /// <param name="_setHueIntervalMinValue">Sets the minimal value of hue randomizer</param>
        /// <param name="_setHueIntervalMaxValue">Sets the maximal value of hue randomizer</param>
        public static void SetTimerInterval(int _setHueIntervalMinValue, int _setHueIntervalMaxValue) {
            HueTimerMinValue = _setHueIntervalMinValue;
            HueTimerMaxValue = _setHueIntervalMaxValue;

            if(ShowInformationMessages) {
                WriteTextColoredAt(Console.CursorLeft, Console.CursorTop, "HueTimer has been set to values: Min=" + HueTimerMinValue + ", Max=" + HueTimerMaxValue, ConsoleColor.DarkGray);
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
            }
        }

        /// <summary> Sets color mode for drawing hues. Fancy colored hues ... How you can you even want more than this? </summary>
        /// <param name="_ColorSettings">Drawing mode</param>
        public static void SetDrawingColorMode(HueColorMode _ColorSettings) {
            HueColorSettings = _ColorSettings;

            if(ShowInformationMessages) {
                WriteTextColoredAt(Console.CursorLeft, Console.CursorTop, "HueColorMode has been set to value: " + Enum.GetName(typeof(HueColorMode), _ColorSettings), ConsoleColor.DarkGray);
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
            }
        }

        /// <summary> When set to true, this library will write some 'hepful' information messages right into your Console! </summary>
        /// <param name="_showInformationMessages">Determinates if this library should show information messages</param>
        public static void SetInformationMessages(bool _showInformationMessages) {
            ShowInformationMessages = _showInformationMessages;
        }

        /// <summary> Inects hue into your console. Be aware, this is a injection, this process can not be revested! </summary>
        public static void InjectHue() {
            if(IsHueInjected == false) {
                IsHueInjected = true;

                HueTimer.Interval = HueRandomizer.Next(HueTimerMinValue, HueTimerMaxValue);
                HueTimer.Elapsed += HueTimerTick;
                HueTimer.Enabled = true;
                HueTimer.Start();

                if(ShowInformationMessages) {
                    WriteTextColoredAt(Console.CursorLeft, Console.CursorTop, "Hue was succesfully injected into your assembly!", ConsoleColor.Green);
                    WriteTextColoredAt(Console.CursorLeft, Console.CursorTop + 1, "Warning: Be aware that this assembly has more than 268% of hue! Yeeey :P", ConsoleColor.DarkGray);
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);
                }
            } else {
                if(ShowInformationMessages) {
                    WriteTextColoredAt(Console.CursorLeft, Console.CursorTop, "Can't inject more hue, becouse hue is already injected!", ConsoleColor.Red);
                    WriteTextColoredAt(Console.CursorLeft, Console.CursorTop + 1, "Warning: This assembly can't handle more than 269% of hue!", ConsoleColor.DarkGray);
                    WriteTextColoredAt(Console.CursorLeft, Console.CursorTop + 2, "Do you want the universe to explode?", ConsoleColor.DarkGray);
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 3);
                }
            }
        }

        private static void HueTimerTick(object sender, ElapsedEventArgs e) {
            HueTimer.Interval = HueRandomizer.Next(HueTimerMinValue, HueTimerMaxValue);
            switch(HueColorSettings) {
                case HueColorMode.NoColor:
                    WriteTextAt(GetRandomCoordForConsole('x'), GetRandomCoordForConsole('y'), GenerateHue());
                    break;

                case HueColorMode.OneTypeOfHueColor:
                    WriteTextColoredAt(GetRandomCoordForConsole('x'), GetRandomCoordForConsole('y'), GenerateHue(), GetRandomConsoleColor());
                    break;

                case HueColorMode.TotalHue:
                    string hue = GenerateHue();
                    ConsoleColor[] colors = new ConsoleColor[hue.Length];

                    for(int i = 0; i < hue.Length; i++) {
                        colors[i] = GetRandomConsoleColor();
                    }

                    WriteTextMulticoloredAt(GetRandomCoordForConsole('x'), GetRandomCoordForConsole('y'), hue, colors);
                    break;
            }

            HueShowedCounter++;
        }

        private static ConsoleColor GetRandomConsoleColor() {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(HueRandomizer.Next(consoleColors.Length));
        }

        private static string GenerateHue() {
            string HueData = "";
            char[] HueArray;

            if(HueRandomizer.Next(1, 10) >= 8) {
                for(int i = 0; i < HueRandomizer.Next(1, 4); i++) {
                    HueData += HueString;
                }
            } else {
                HueData = HueString;
            }

            HueArray = HueData.ToArray();

            for(int i = 0; i < HueArray.Length; i++ ) {
                if(HueRandomizer.Next(0, 2) == 1) HueArray[i] = char.ToUpper(HueArray[i]);
            }

            return string.Join("", HueArray);
        }

        private static int GetRandomCoordForConsole(char _dimentsion) {
            switch(_dimentsion) {
                case 'x':
                    return HueRandomizer.Next(0, Console.WindowWidth);

                case 'y':
                    return HueRandomizer.Next(0, Console.WindowHeight);

                default:
                    return 0;
            }
        }

        private static void WriteTextAt(int _x, int _y, string _text) {
            int LastCursorPosX = Console.CursorLeft;
            int LastCursorPosY = Console.CursorTop;

            Console.SetCursorPosition(_x, _y);
            Console.Write(_text);

            Console.SetCursorPosition(LastCursorPosX, LastCursorPosY);
        }

        private static void WriteTextColoredAt(int _x, int _y, string _text, ConsoleColor _color) {
            int LastCursorPosX = Console.CursorLeft;
            int LastCursorPosY = Console.CursorTop;
            ConsoleColor DefaultColor = Console.ForegroundColor;

            Console.ForegroundColor = _color;
            Console.SetCursorPosition(_x, _y);
            Console.Write(_text);

            Console.ForegroundColor = DefaultColor;
            Console.SetCursorPosition(LastCursorPosX, LastCursorPosY);
        }

        private static void WriteTextMulticoloredAt(int _x, int _y, string _text, ConsoleColor[] _color) {
            int LastCursorPosX = Console.CursorLeft;
            int LastCursorPosY = Console.CursorTop;
            ConsoleColor DefaultColor = Console.ForegroundColor;

            char[] textArray = _text.ToArray();

            for(int i = 0; i < textArray.Length; i++) {
                if(_x + textArray.Length > Console.WindowWidth) break;
                Console.ForegroundColor = _color[i];
                Console.SetCursorPosition(_x + i, _y);
                Console.Write(textArray[i]);
            }
            
            Console.ForegroundColor = DefaultColor;
            Console.SetCursorPosition(LastCursorPosX, LastCursorPosY);
        }

    }
}
