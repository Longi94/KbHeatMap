using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KbHeatMap.Razer;

namespace KbHeatMap.Utils
{
    public static class Mapping
    {
        private static Dictionary<string, Constants.Key> KeyMapping = new Dictionary<string, Constants.Key>
        {
            {"Escape", Constants.Key.Escape},
            {"F1", Constants.Key.F1},
            {"F2", Constants.Key.F2},
            {"F3", Constants.Key.F3},
            {"F4", Constants.Key.F4},
            {"F5", Constants.Key.F5},
            {"F6", Constants.Key.F6},
            {"F7", Constants.Key.F7},
            {"F8", Constants.Key.F8},
            {"F9", Constants.Key.F9},
            {"F10", Constants.Key.F10},
            {"F11", Constants.Key.F11},
            {"F12", Constants.Key.F12},
            {"PrintScreen", Constants.Key.PrintScreen},
            {"Scroll", Constants.Key.Scroll},
            {"Pause", Constants.Key.Pause},
            {"Oemtilde", Constants.Key.OemTilde},
            {"D1", Constants.Key.D1},
            {"D2", Constants.Key.D2},
            {"D3", Constants.Key.D3},
            {"D4", Constants.Key.D4},
            {"D5", Constants.Key.D5},
            {"D6", Constants.Key.D6},
            {"D7", Constants.Key.D7},
            {"D8", Constants.Key.D8},
            {"D9", Constants.Key.D9},
            {"D0", Constants.Key.D0},
            {"OemMinus", Constants.Key.OemMinus},
            {"Oemplus", Constants.Key.OemEquals},
            {"Back", Constants.Key.Backspace},
            {"Insert", Constants.Key.Insert},
            {"Home", Constants.Key.Home},
            {"PageUp", Constants.Key.PageUp},
            {"NumLock", Constants.Key.NumLock},
            {"Divide", Constants.Key.NumDivide},
            {"Multiply", Constants.Key.NumMultiply},
            {"Subtract", Constants.Key.NumSubtract},
            {"Tab", Constants.Key.Tab},
            {"Q", Constants.Key.Q},
            {"W", Constants.Key.W},
            {"E", Constants.Key.E},
            {"R", Constants.Key.R},
            {"T", Constants.Key.T},
            {"Y", Constants.Key.Y},
            {"U", Constants.Key.U},
            {"I", Constants.Key.I},
            {"O", Constants.Key.O},
            {"P", Constants.Key.P},
            {"OemOpenBrackets", Constants.Key.OemLeftBracket},
            {"OemCloseBrackets", Constants.Key.OemRightBracket},
            {"Oem6", Constants.Key.OemRightBracket},
            {"Oem5", Constants.Key.OemBackslash},
            {"Delete", Constants.Key.Delete},
            {"End", Constants.Key.End},
            {"PageDown", Constants.Key.PageDown},
            {"NumPad7", Constants.Key.Num7},
            {"NumPad8", Constants.Key.Num8},
            {"NumPad9", Constants.Key.Num9},
            {"Add", Constants.Key.NumAdd},
            {"CapsLock", Constants.Key.CapsLock},
            {"Capital", Constants.Key.CapsLock},
            {"A", Constants.Key.A},
            {"S", Constants.Key.S},
            {"D", Constants.Key.D},
            {"F", Constants.Key.F},
            {"G", Constants.Key.G},
            {"H", Constants.Key.H},
            {"J", Constants.Key.J},
            {"K", Constants.Key.K},
            {"L", Constants.Key.L},
            {"OemSemicolon", Constants.Key.OemSemicolon},
            {"Oem1", Constants.Key.OemSemicolon},
            {"OemQuotes", Constants.Key.OemApostrophe},
            {"Oem7", Constants.Key.OemApostrophe},
            {"Enter", Constants.Key.Enter},
            {"NumPad4", Constants.Key.Num4},
            {"NumPad5", Constants.Key.Num5},
            {"Clear", Constants.Key.Num5},
            {"NumPad6", Constants.Key.Num6},
            {"LShiftKey", Constants.Key.LeftShift},
            {"Z", Constants.Key.Z},
            {"X", Constants.Key.X},
            {"C", Constants.Key.C},
            {"V", Constants.Key.V},
            {"B", Constants.Key.B},
            {"N", Constants.Key.N},
            {"M", Constants.Key.M},
            {"Oemcomma", Constants.Key.OemComma},
            {"OemPeriod", Constants.Key.OemPeriod},
            {"Oem2", Constants.Key.OemSlash},
            {"OemQuestion", Constants.Key.OemSlash},
            {"RShiftKey", Constants.Key.RightShift},
            {"Up", Constants.Key.Up},
            {"NumPad1", Constants.Key.Num1},
            {"NumPad2", Constants.Key.Num2},
            {"NumPad3", Constants.Key.Num3},
            {"Next", Constants.Key.Num3},
            {"Return", Constants.Key.NumEnter},
            {"LControlKey", Constants.Key.LeftControl},
            {"LWin", Constants.Key.LeftWindows},
            {"LMenu", Constants.Key.LeftAlt},
            {"Space", Constants.Key.Space},
            {"RMenu", Constants.Key.RightAlt},
            {"Apps", Constants.Key.RightMenu},
            {"RControlKey", Constants.Key.RightControl},
            {"Left", Constants.Key.Left},
            {"Down", Constants.Key.Down},
            {"Right", Constants.Key.Right},
            {"NumPad0", Constants.Key.Num0},
            {"Decimal", Constants.Key.NumDecimal}
        };

        public static Constants.Key GetRazerKey(Keys key)
        {
            try
            {
                return KeyMapping[key.ToString()];
            }
            catch (KeyNotFoundException)
            {
                return Constants.Key.Invalid;
            }
        }
    }
}
