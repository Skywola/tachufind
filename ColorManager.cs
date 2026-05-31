using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Tachufind
{

    public static class ColorManager
    {
        private static Dictionary<string, Color> colorDictionary;
        private static Color color1 = Color.DodgerBlue;
        private static Color color2 = Color.FromArgb(245, 237, 6);  // This makes Yellow, avoids getting it to close to White
        private static Color color3 = Color.Green;
        private static Color color4 = Color.Orange;
        private static Color color5 = Color.Purple;
        private static Color color6 = Color.Red; 
        private static Color color7 = Color.Brown;
        private static Color color8 = Color.Violet;
        private static Color color9 = Color.Indigo;   
        private static Color color10 = Color.LightGreen;

        // NEW: Static constructor to initialize colorDictionary
        static ColorManager()
        {
            colorDictionary = new Dictionary<string, Color>()
        {
            {"C1", color1},
            {"C2", color2},
            {"C3", color3},
            {"C4", color4},
            {"C5", color5},
            {"C6", color6},
            {"C7", color7},
            {"C8", color8},
            {"C9", color9},
            {"C10", color10}
        };
        }

        public static Color GetDefaultColor(string key)
        {
            switch (key)
            {
                case "C1":
                    return color1;
                case "C2":
                    return color2;
                case "C3":
                    return color3;
                case "C4": 
                    return color4;
                case "C5": 
                    return color5;
                case "C6": 
                    return color6;
                case "C7": 
                    return color7;
                case "C8": 
                    return color8;
                case "C9": 
                    return color9;
                case "C10":
                    return color10;
                default:   
                    return Color.Black; // Default fallback color
            }
        }

        public static Color GetColorFromUserSettings(int index) 
        {
               Color color = GetColorByIndex(index);
               return color;
        }

        public static Color GetColorByIndex(int index)
        {
            if (index < 1 || index > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 1 and 10.");
            }

            string propertyName = $"Color{index}";
            var colorProperty = typeof(MyUserSettings).GetProperty(propertyName);

            if (colorProperty != null)
            {
                var colorValue = colorProperty.GetValue(Globals.User_Settings);
                if (colorValue != null)
                {
                    return (Color)colorValue;
                }
            }

            return Color.Black; // Default color if something goes wrong
        }

        // Set color in MyUserSettings AKA UserSettings
        public static void SetColor(string key, string colorInput)
        {
            if (string.IsNullOrWhiteSpace(colorInput))
            {
                MessageBox.Show("Error: Input must not be null or empty.");
                return;
            }

            // Remove "Color [" and "]" from the colorInput if present
            if (colorInput.StartsWith("Color [") && colorInput.EndsWith("]"))
            {
                colorInput = colorInput.Substring(7, colorInput.Length - 8);
            }

            try
            {
                // Attempt to parse the input as a color value
                Color color = ColorTranslator.FromHtml(colorInput);
                SetColor(key, color);
            }
            catch
            {
                try
                {
                    Color parsedColor = ParseColorFromName(colorInput);
                    SetColor(key, parsedColor);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Invalid color input: {colorInput}. Error: {ex.Message}");
                }
            }
        }

        public static void SetColor(string key, Color color)
        {
            if (colorDictionary.ContainsKey(key))
            {
                colorDictionary[key] = color;
            }
            else
            {
                MessageBox.Show($"Invalid color key: {key}.");
            }
        }

        public static bool IsValidColor(Color colorInput)
        {
            try
            {
                // Attempt to convert the color to HTML to check validity
                var colorString = ColorTranslator.ToHtml(colorInput);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Color ParseColorFromName(string colorName)
        {
            if (Enum.TryParse(colorName, true, out KnownColor knownColor))
            {
                return Color.FromKnownColor(knownColor);
            }

            if (colorName.StartsWith("#") && colorName.Length == 7)
            {
                var r = byte.Parse(colorName.Substring(1, 2), NumberStyles.HexNumber);
                var g = byte.Parse(colorName.Substring(3, 2), NumberStyles.HexNumber);
                var b = byte.Parse(colorName.Substring(5, 2), NumberStyles.HexNumber);
                return Color.FromArgb(255, r, g, b);
            }
            MessageBox.Show($"Invalid color name: {colorName}");
            return Color.Black;
        }

        public static Color GetColor(string colorName)
        {
            if (colorDictionary.TryGetValue(colorName, out Color color))
            {
                return color;
            }
            else
            {
                MessageBox.Show("Invalid color name.");
                return Color.Empty;
            }
        }
    }
}
