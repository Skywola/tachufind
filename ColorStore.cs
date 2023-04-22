using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    public class ColorStore
    {
        private readonly Dictionary<string, Color> _colors = new Dictionary<string, Color>
        {
        {"C0", Color.White},
        {"C1", Color.FromArgb(255, 0, 0, 255)}, // Blue
        {"C2", Color.FromArgb(255, 0, 255, 0)},  // Green
        {"C3", Color.FromArgb(255, 255, 0, 0)},  // Red
        {"C4", Color.FromArgb(255, 0, 255, 255)},  // Yellow
        {"C5", Color.FromArgb(255, 148, 0, 211)},   // Dark Violet    5
        {"C6", Color.FromArgb(255, 139, 69, 19)},   // Saddle Brown
        {"C7", Color.FromArgb(255, 112, 128, 144)},   // Slate Gray
        {"C8", Color.FromArgb(255, 30, 144, 255)},   // Dodger Blue
        {"C9", Color.FromArgb(255, 220, 20, 60)},  //  Crimson
        {"C10", Color.FromArgb(255, 128, 128, 0)}, // Olive
        {"C11", Color.Black}
        };

        public void AddColor(string name, string colorName)
        {
            _colors[name] = ParseColorFromName(colorName);
        }

        public void AddColor(string name, Color color)
        {
            if (!_colors.ContainsKey(name)) throw new KeyNotFoundException($"Color '{name}' not found");

            _colors[name] = color;
        }

        public Color GetColor(string name)
        {
            if (!_colors.TryGetValue(name, out var color))
            {
                throw new ArgumentException($"Color '{name}' not found");
            }
            return color;
        }

        private static Color ParseColorFromName(string colorName)
        {
            // Try to parse the color using the known color names
            if (Enum.TryParse(colorName, true, out KnownColor knownColor))
            {
                return Color.FromKnownColor(knownColor);
            }

            // Try to parse the color using the #RRGGBB format
            if (colorName.StartsWith("#") && colorName.Length == 7)
            {
                var r = byte.Parse(colorName.Substring(1, 2), NumberStyles.HexNumber);
                var g = byte.Parse(colorName.Substring(3, 2), NumberStyles.HexNumber);
                var b = byte.Parse(colorName.Substring(5, 2), NumberStyles.HexNumber);
                return Color.FromArgb(255, r, g, b);
            }

            // Throw an exception if the color name cannot be parsed
            throw new ArgumentException($"Invalid color name: {colorName}");
        }


    }
}
