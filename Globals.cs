using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;


namespace Tachufind
{
    internal static class Globals
    {
        #region Important Properties
        public static RichTextBox Current_RTB_withFocus = new();
        public static MyUserSettings User_Settings = new();
        public static Color BackColorStd = System.Drawing.Color.FromArgb(169, 169, 169);  // Global Standard BackColor for all RichTextBoxes

        #endregion // Important Properties


        #region Form Settings

        #region Globals for FrmMain

        public static int BMCount = 0;
        // An Affix is a Prefix or Suffix, but in this uncluding an option for normal search
        //public static SearchAffix searchAffix = SearchAffix.None;
        //public static bool ValidAffixDetected = false;
        //public static int foundIndex = 0;
        public enum SearchAffix
        {
            Prefix,
            Suffix,
            None,
        }
        public static object dictEntries = new Dictionary<string, string> { };

        // public static int newCharIndex = 1;  // 12-29-2025 This may be unnecesary

        #endregion // Globals for FrmMain

        #region FrmOpticalCharacterRecognition

        public static string storedSteps = string.Empty;

        public static Image? cachedImageTop;
        public static Image? cachedImageBottom;

        public static Stack<string> undoTopStack = new();
        public static Stack<string> undoBottomStack = new();

        // splitSentenceQueue holds
        public static Queue<string> TopSentencesQueue = new();
        public static Queue<string> BottomSentencesQueue = new();

        // These are used to make sentence lengths of interlinear texts the same.
        public static Queue<string> PaddedTopQueue = new();
        public static Queue<string> PaddedBottomQueue = new();

        public static Queue<string> linesQueueTop = new();
        public static Queue<string> linesQueueBottom = new();

        #endregion // FrmOpticalCharacterRecognition


        #region FrmColor
        public static int FindStart = 0;  // keeps track of boundary marker initial positions
        public static DateTime StartTime = DateTime.Now;
        public static DateTime StartBMTime = DateTime.Now;
        public static bool TimeExpired = false;
        public static int DisplayTime = 0;
        public static int BtnPasteIntoFontSize = 22;

        public static Search search = new();
        internal static Queue<Search> searchQueue = new();  // FrmColor/FrmMain Search - Replace operations
        #endregion  // FrmColor


        #region FrmQuiz
        // Quizes
        public static bool FrmQuizInit = true;

        public static List<string> ListOfQuizTitles = [];
        // This is for titles ONLY 
        //                                             // For building quizes:
        public static string QuizName = "";
        public static string AppendQuizName = "";
        public static bool QuestionSaved = true;
        public static bool QuizSaved = true;
        public static bool SaveEditBtnClicked = false;
        public static bool AddBtnClicked = false;
        public static bool questionOrAnswerModified = false;
        public static List<string> QuestionAnswer_Pair = new List<string>();
        public static Dictionary<int, string> OriginalRTFDictionary = [];
        public static Dictionary<int, string> RunningRTFDictionary = [];
        public static Dictionary<int, string> AppendRTFDictionary = [];
        //public static bool ReorderDictionaryNeeded = false;
        public static Queue<int> QueueKeyContainer = new();
        //public static int CurrentQueueKey = 0;
        public static bool KeyQueueLoaded = false;
        public static bool Shuffle = false;
        public static bool QuizNameClicked = false;
        public static bool QuizDeleted = false;

        
        public static List<string> DualQAList = [];
        public static bool InitializeCounts = true;
        //// AI confirmed after being fed the procedures for this that there is no danger of missingKeys becoming negative.
        public static bool GetAppendFile = false;
        //public static bool AppendStep2 = false;
        public static bool QuizRetrieved = false;
        public static bool RevertQuiz = false;

        public static bool BoolRTBQuestionModified = false;
        public static bool BoolRTBAnswerModified = false;
        //public static bool Invert = false;
        //public static Font RTBMainFont = new Font("Times New Roman", 22);
        public static Font QuizFont = new("Times New Roman", 22);
        //public static Font StandardFont = new Font("Times New Roman", 22);

        public static QuestionTracker QuestionTracker = new();

        #endregion // FrmQuiz


        // FOR FRMInterlinear
        public static string f1Path = string.Empty;
        public static string f2Path = string.Empty;
        public static string output = string.Empty;
        public static bool firstPass = true;
        public static string lineNumber = string.Empty;
        public static string sectionBottomText = string.Empty;
        public static string currentTimeRange = string.Empty;
        public static string previousTimeRange = string.Empty;
        public static string timeBottom = string.Empty;
        public static string subtitleTop = string.Empty;
        public static string subtitleBottom = string.Empty;


        // FrmSaveSearch
        public static bool SearchChanged = false;
        // For marking when searches have been saved.
        public static bool BoolFrmColorSearchSaved = true;


        // FrmSection
        public static bool FrmSectionInit = true;

        public static string DictEntries { get => DictEntries; set => DictEntries = value; }
        
        #endregion // Form Settings
        //
    }
}
