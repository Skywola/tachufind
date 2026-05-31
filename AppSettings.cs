using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    public static class AppSettings
    {
        internal static string CommonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        internal static string Data_Folder = CommonAppData + "\\" + AppConstants.AppName + "\\";
        internal static string currentDirectory = Directory.GetCurrentDirectory();
        //internal static DirectoryInfo? parentDirectory = Directory.GetParent(currentDirectory);
        //internal static DirectoryInfo? grandparentDirectory = parentDirectory?.Parent;
        internal static string FindReplaceFileExtension = ".fdta";
        internal static string QuizesFileExtension = ".qdta";
        internal static string FileMenuFileHistoryTextFile = Data_Folder + "recentFiles.txt";

        internal static bool UserHasAgreed = false;
        internal static string commandLinePath = string.Empty;
        internal static string lastUsedDirectory = string.Empty;
        internal static string filePath = "";

        internal static int testvariable = 0;  // For debugging


        // Interlinear Creator
        internal static TimeSpan MinTimeGap = TimeSpan.FromMilliseconds(2);  // Minimum time gap between subtitles
        // MaxTimeDuration of any line.  This sets lines with no immediate following dialogue
        // to a longer Duration if possible to give the viewer a bit more time to study the line.
        internal static TimeSpan MaxTimeDuration = TimeSpan.FromSeconds(18);


        // From FrmColor Searches, to be accessable to FrmMain
        // to run the searches set up in FrmColor. Once the searches
        // are set up in FrmColor, they are actually run in FrmMain.
        internal static bool SearchJustRetrieved = false;
        internal static string SearchName = string.Empty;
        internal static int CurrentCursorLocation = 0;
        internal static Affix AffixType { get; set; } = Affix.All;
        internal static Mode SearchMode { get; set; } = Mode.Text;
        internal static bool SearchInProgress { get; set; } = false;

        // internal static bool RbEditColor = true;
        internal static bool ChkAutoFindNext = true;
        internal static bool MatchCase = false;
        internal static bool WordOnly = false;

        //internal static bool FrmMainInit = true;  // To prevent form location change from overwriting init values from INI file
        internal static bool FileNotFound = false;
        internal static bool RtbModified = false;  // Rich Text Box Main, on FrmMain
        internal static int SelStart = 0;
        //internal static bool FileOpenCanceled = false;
        internal static bool NewFileNameAlreadyExistsButHasBeenSaved = true;

        // UNDO 
        internal static int currentCursorPosition = 0;
        internal static Stack<string> undoStack = new();
        internal static int UndoCount = 0;
        internal static Queue<int> UndoQueue = new();

        // FONTS
        public static string SelectedFontFamily { get; set; } = "Times New Roman"; 
        public static float SelectedFontSize { get; set; } = 12.0f;

        // GENERAL FRMMAIN
        internal static bool isExiting = false;
        internal static bool loadfirst = true;

        internal static bool ShowFrmKeyboardShorcutsPopWindow = false;

        // TEXT-TO-SPEECH
        internal static string Voice = string.Empty;
        internal static bool isSpeaking = false;
        internal static int startCharacter = 0;
        internal static int lengthTTS = 0;   
        internal static bool OmitTextInSqBrackets = true;  // Uses REGEX to disregard text in []
        internal static bool IsSynthActive = false;
        internal static int timeLeft = 0;
        internal static int EOF;  // End of file
        public static bool userRequestedStop = false;

        // POSITION MEMORY
        internal static int clickNum = 1;
        internal static int ReturnButtonMemoryPositions = 30;
        internal static int initialMouseClickPosition = 0;
        internal static bool isMouseDown = false;


        // Sections
        internal static int SectionNumber = 0;
        internal static int FrmSectionWidth = 900;
        internal static int FrmSectionHeight = 600;

        // TOOLTIPS
        internal static string ToolTip1 = "      Color selected text, if shift is pressed, change backcolor";
        internal static string ToolTip2 = "      Color selected text, if shift is pressed, change backcolor";
        internal static string ToolTip3 = "      Color selected text, if shift is pressed, change backcolor";
        internal static string ToolTip4 = "      Color selected text, if shift is pressed, change backcolor";
        internal static string ToolTip5 = "      Color selected text, if shift is pressed, change backcolor";
        internal static string ToolTip6 = "      Color selected text, if shift is pressed, change backcolor";
        internal static string ToolTip7 = "      Color selected text, if shift is pressed, change backcolor";
        internal static string ToolTip8 = "      Color selected text, if shift is pressed, change backcolor";
        internal static string ToolTip9 = "      Color selected text, if shift is pressed, change backcolor";
        internal static string ToolTip10 = "      Color selected text, if shift is pressed, change backcolor";


    }
}
