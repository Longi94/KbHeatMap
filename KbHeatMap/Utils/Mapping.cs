﻿using System.Collections.Generic;
using System.Windows.Forms;
using Colore.Effects.Keyboard;

namespace KbHeatMap.Utils
{
    public static class Mapping
    {
        private static Dictionary<string, Key> KeyMapping = new Dictionary<string, Key>
        {
            {"Escape", Key.Escape},
            {"F1", Key.F1},
            {"F2", Key.F2},
            {"F3", Key.F3},
            {"F4", Key.F4},
            {"F5", Key.F5},
            {"F6", Key.F6},
            {"F7", Key.F7},
            {"F8", Key.F8},
            {"F9", Key.F9},
            {"F10", Key.F10},
            {"F11", Key.F11},
            {"F12", Key.F12},
            {"PrintScreen", Key.PrintScreen},
            {"Scroll", Key.Scroll},
            {"Pause", Key.Pause},
            {"Oemtilde", Key.OemTilde},
            {"D1", Key.D1},
            {"D2", Key.D2},
            {"D3", Key.D3},
            {"D4", Key.D4},
            {"D5", Key.D5},
            {"D6", Key.D6},
            {"D7", Key.D7},
            {"D8", Key.D8},
            {"D9", Key.D9},
            {"D0", Key.D0},
            {"OemMinus", Key.OemMinus},
            {"Oemplus", Key.OemEquals},
            {"Back", Key.Backspace},
            {"Insert", Key.Insert},
            {"Home", Key.Home},
            {"PageUp", Key.PageUp},
            {"NumLock", Key.NumLock},
            {"Divide", Key.NumDivide},
            {"Multiply", Key.NumMultiply},
            {"Subtract", Key.NumSubtract},
            {"Tab", Key.Tab},
            {"Q", Key.Q},
            {"W", Key.W},
            {"E", Key.E},
            {"R", Key.R},
            {"T", Key.T},
            {"Y", Key.Y},
            {"U", Key.U},
            {"I", Key.I},
            {"O", Key.O},
            {"P", Key.P},
            {"OemOpenBrackets", Key.OemLeftBracket},
            {"OemCloseBrackets", Key.OemRightBracket},
            {"Oem6", Key.OemRightBracket},
            {"Oem5", Key.OemBackslash},
            {"Delete", Key.Delete},
            {"End", Key.End},
            {"PageDown", Key.PageDown},
            {"NumPad7", Key.Num7},
            {"NumPad8", Key.Num8},
            {"NumPad9", Key.Num9},
            {"Add", Key.NumAdd},
            {"CapsLock", Key.CapsLock},
            {"Capital", Key.CapsLock},
            {"A", Key.A},
            {"S", Key.S},
            {"D", Key.D},
            {"F", Key.F},
            {"G", Key.G},
            {"H", Key.H},
            {"J", Key.J},
            {"K", Key.K},
            {"L", Key.L},
            {"OemSemicolon", Key.OemSemicolon},
            {"Oem1", Key.OemSemicolon},
            {"OemQuotes", Key.OemApostrophe},
            {"Oem7", Key.OemApostrophe},
            {"Enter", Key.Enter},
            {"NumPad4", Key.Num4},
            {"NumPad5", Key.Num5},
            {"Clear", Key.Num5},
            {"NumPad6", Key.Num6},
            {"LShiftKey", Key.LeftShift},
            {"Z", Key.Z},
            {"X", Key.X},
            {"C", Key.C},
            {"V", Key.V},
            {"B", Key.B},
            {"N", Key.N},
            {"M", Key.M},
            {"Oemcomma", Key.OemComma},
            {"OemPeriod", Key.OemPeriod},
            {"Oem2", Key.OemSlash},
            {"OemQuestion", Key.OemSlash},
            {"RShiftKey", Key.RightShift},
            {"Up", Key.Up},
            {"NumPad1", Key.Num1},
            {"NumPad2", Key.Num2},
            {"NumPad3", Key.Num3},
            {"Next", Key.Num3},
            {"Return", Key.Enter}, // FIXME
            //{"Return", Key.NumEnter},
            {"LControlKey", Key.LeftControl},
            {"LWin", Key.LeftWindows},
            {"LMenu", Key.LeftAlt},
            {"Space", Key.Space},
            {"RMenu", Key.RightAlt},
            {"Apps", Key.RightMenu},
            {"RControlKey", Key.RightControl},
            {"Left", Key.Left},
            {"Down", Key.Down},
            {"Right", Key.Right},
            {"NumPad0", Key.Num0},
            {"Decimal", Key.NumDecimal}
        };

        public static Key GetRazerKey(Keys key)
        {
            try
            {
                return KeyMapping[key.ToString()];
            }
            catch (KeyNotFoundException)
            {
                return Key.Invalid;
            }
        }
    }
}
