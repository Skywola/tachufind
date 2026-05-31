using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    public static class ScreenUtility
    {
        private static bool locationChanging = false;
        private static bool sizeChanging = false;

        // Initialize form with adjustable size
        public static void InitializeForm(Form form, Point savedLocation, Size savedSize)
        {
            // Ensure saved location is within bounds of any screen
            Screen screen = Screen.FromPoint(savedLocation);
            Rectangle workingArea = screen.WorkingArea;
            int left = Math.Max(savedLocation.X, workingArea.Left);
            int top = Math.Max(savedLocation.Y, workingArea.Top);
            savedLocation = new Point(left, top);

            form.Size = savedSize;
            form.Location = savedLocation;
        }

        // Initialize form with fixed size
        public static void InitializeForm(Form form, Point savedLocation)
        {
            // Ensure saved location is within bounds of any screen
            Screen screen = Screen.FromPoint(savedLocation);
            Rectangle workingArea = screen.WorkingArea;
            int left = Math.Max(savedLocation.X, workingArea.Left);
            int top = Math.Max(savedLocation.Y, workingArea.Top);
            savedLocation = new Point(left, top);

            form.Location = savedLocation;
        }

        // Handle location change
        public static void HandleLocationChanged(Form form, Action<Point> saveLocationAction)
        {
            if (locationChanging) return;

            try
            {
                locationChanging = true;

                Screen screen = Screen.FromControl(form);
                Rectangle workingArea = screen.WorkingArea;
                int left = Math.Max(form.Left, workingArea.Left);
                int top = Math.Max(form.Top, workingArea.Top);

                // Ensure location is within the bounds of the screen
                form.Location = new Point(left, top);

                // Call the provided action to save the location
                saveLocationAction(form.Location);
            }
            finally
            {
                locationChanging = false;
            }
        }

        // Handle size change
        public static void HandleSizeChanged(Form form, Action<Size> saveSizeAction)
        {
            if (sizeChanging) return;

            try
            {
                sizeChanging = true;

                // Call the provided action to save the size
                saveSizeAction(form.Size);
            }
            finally
            {
                sizeChanging = false;
            }
        }
    }



}
