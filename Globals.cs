using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.IO;
using Tachufind;
using Tesseract;

public static class Globals
{
	public static MyUserSettings User_Settings = new MyUserSettings();

	public static string AppName = "Tachufind";
	public static string CommonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
	public static string Data_Folder = CommonAppData + "\\" + AppName + "\\";
	public static string FindReplaceFileExtension = ".fdta";
	public static string QuizesFileExtension = ".qdta";
	public static bool Greek = false;
    public static string ProjectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

    // START Constants
    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    public static String TESSDATA_PREFIX = Path.Combine(ProjectRoot, "tessdata");
    public static String SPLITTERTAG = "!#!";
	public static int SPLITTERSIZE = 3;
	public static int NUM_OF_ELEMENTS = 6; // Actual usage at this point is 5 separate digits
	public static String BORDER = "   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%   ";
	public static int[] HeaderArray = { 0, 0, 0, 0, 0, 0, }; // Same as NUM_OF_ELEMENTS
															 // Charset UTF_8 = Charset.forName("UTF-8");
															 // END Constants    

	// END Constants
	//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

	// KEYBOARD
	public static bool ShiftKey = false;
	//  public static bool altKey = false;   Alt is not detected


	// Important Properties . . . 
	public static RichTextBox Current_RTB_withFocus;
	public static int SelStart = 0;
	public static string CurrentLanguage;
	public static RichTextBox Main_Backup = new RichTextBox();
	public static bool ShowFrmKeyboardShorcutsPopWindow = false;

    // Used for Text-To-Speech
    public static bool FrmTTSExists = false;
	public static string Voice;
	public static int currentTTSLineNumber = 0;

	public static int ScrollPosition;
	public static bool OmitTextInSqBrackets = true;  // Uses REGEX to disregard text in []
	public static string VoiceEnabled;
    public static SpeechSynthesizer Synth = new SpeechSynthesizer();
    public static Dictionary<int, string> LineDictionary = new Dictionary<int, string>();
	// For un-highlighting text
	public static int OldLineStart = 0;
    public static int OldLineEnd = 0;
    public static int OldLineIndex = -1;
	public static bool FirstPass = true;
	public static bool SpeechSynthClosing = false;

    // TESSERACT
	public static EngineCacheManager EngineCache = new EngineCacheManager();
	public static TesseractEngine Engine;
    public static bool FrmOCRExists = false;
    public static DataObject DataObject = new DataObject();
	public static string TessText = string.Empty;

    // Quizes
    public const string QUESTION_TAG = "\n<|Q]>";
    public const string ANSWER_TAG = "\n<|A]>";
	    public static List<string> ListOfQuizTitles; // This is for titles ONLY 
    // For building quizes:
	public static string QuizName = "";
    public static string AppendQuizName = "";
    public static bool QuestionSaved = true;
	public static bool QuizSaved = true;
	public static bool SaveEditBtnClicked = false;
    public static bool AddBtnClicked = false;
    public static List<string> QuestionAnswer_Pair = new List<string>();
	public static List<string> TempList = new List<string>();
	public static Dictionary<int, string> OriginalRTFDictionary = new Dictionary<int, string>();
	public static Dictionary<int, string> RunningRTFDictionary = new Dictionary<int, string>();
    public static bool ReorderDictionaryNeeded = false;
    public static Queue<int> QueueKeyContainer = new Queue<int>();
	public static int CurrentQueueKey = 0;
    public static bool KeyQueueLoaded = false;
    public static Dictionary<int, string> AppendRTFDictionary1 = new Dictionary<int, string>();
    public static Dictionary<int, string> AppendRTFDictionary2 = new Dictionary<int, string>();
    public static bool Shuffle = false;
    public static bool QuizNameClicked = false;
	public static bool QuizDeleted = false;

    public static int KeyOfCurrentDictionary = 0;  // Current running question key
    public static List<string> DualQAList = new List<string>();
	public static bool InitializeCounts = true;
	public static bool AddQuizTitle = false;
		// AI confirmed after being fed the procedures for this that there is no danger of missingKeys becoming negative.
	public static bool AppendStep1 = false;
    public static bool AppendStep2 = false;
	public static bool QuizRetrieved = false;
	public static bool QuizTitleClicked = false;
    public static bool RevertQuiz = false;

    public static RichTextBox Question = new RichTextBox();
    public static RichTextBox Answer = new RichTextBox();
    public static int MAX_NUMBER_OF_ENTRIES = 99999;  // Not used

    public static bool BoolRTBQuestionModified = false;
	public static bool BoolRTBAnswerModified = false;
	public static bool Invert = false;
	public static Font QuizFont;
	public static int QuizFontSize = 28;

	public static string P_RTFMain_RTF = string.Empty;
	public static string P_RTFMain_TXT = string.Empty;
	public static string P_FrmMainTextUndo;
	public static bool BoolDestinationIsRTB;
	public static QuestionTracker QuestionTracker = new QuestionTracker();


	// FrmColor
	public static bool FrmColorExists = false;
	public static Font ColorFont;
	public static int SearchTimeLimit = 22; // time in seconds
	public static DateTime StartTime = DateTime.Now;
	public static DateTime CurrentTime = DateTime.Now;
	public static int DisplayTime = 0;

	public static bool SearchInProgress = false;
	public static int FindStart = 0;  // keeps track of boundary marker initial positions
	public static int FindStopOrEOF;  // keeps track of boundary marker final positions
	public static int BoundryMarkerStart;
	public static int BoundryMarkerEnd;
	public static int SearchRoundCount = 0;
	public static int BtnPasteIntoFontSize = 22;
	
    // FrmSaveSearch
    public static bool SearchChanged = false;
	public static int LastBackUpCount = 0;
	public static int RTBSearchCursorPos = 0;



	// ReportError
	public static RichTextBox ReportError;  // This may be removable 

	public static bool BoolPrintPagesMode;
	public static bool LanguageJustChanged = false;

	public static int ReplaceAll_BMEndPoint = 0;  // BM = Boundary Marker
	public static bool BoolCursorState = false;
	// For marking when searches have been saved.
	public static bool BoolFrmColorSearchSaved = true;
	public static bool AlwaysUseWhiteBackgroundColorForPrinting = true;
	public static bool BoolNewFileNameAlreadyExistsButHasBeenSaved = true;


	// This allows FileIO (FIO) to cancel if user hit cancel button instead of selecting a file to open
	public static bool BoolFileOpenCancelled = false;
	public static bool BoolImportingTitles = false;

	public static bool BoolSectionSearchInProgress = false;

	public static IDataObject ClipboardDataObject;  // This has to do with Clipboard




	// For BtnBOFSearchClick and BtnEofSearchClick
	public static string BOFSearchText = null;
	public static int BOFPass;
	public static int BOFSearchStrLength;
	public static int BOFIndexToText;
	public static int BOFSearchStart;
	public static int BOFSearchEnd;
	public static int BOFLastCursorLocation;

	public static string EOFSearchText = null;
	public static int EOFPass;
	public static int EOFSearchStrLength;
	public static int EOFIndexToText;
	public static int EOFSearchStart;
	public static int EOFSearchEnd;
	public static int EOFLastCursorLocation;
	

	// For BtnFindFwdFrmCursorClick and BtnFindRevFrmCursorClick 
	public static int Start;

	// For RTBMainMouseDown
	public static int MouseDownX;
	public static int MouseDownY;
	public static bool CursorPositionChanged;

	// COLOR 
	//
	// For FrmColor btnFind and btnReplace Only
	public static bool BtnFindSearchChanged = false;  // For FrmColor btnFind, RTBMain cursor position changed
	public static int BtnFindStart = 0;  // For FrmColor btnFind, RTBMain cursor position
	public static int BtnFindSearchRoundCount = 0;
	public static Color BtnFindCurrentColor = Color.Black;
	// END For CrmColor btnFind and btnReplace Only
	public static bool FrmGetSearchClosing = false;



	// Used for Searches History, but not saved
	public static List<string> ListOfSearchTitles = new List<string>();
	public static string SearchFileContents;
	//public static Queue queueSearchItems = new Queue();
	public static bool SearchesEmpty;  // Currently not used

	public static string ToolTip01 = "Color selected text";
	public static string ToolTip02 = "Color selected text";
	public static string ToolTip03 = "Color selected text";
	public static string ToolTip04 = "Color selected text";
	public static string ToolTip05 = "Color selected text";
	public static string ToolTip06 = "Color selected text";
	public static string ToolTip07 = "Color selected text";
	public static string ToolTip08 = "Color selected text";
	public static string ToolTip09 = "Color selected text";
	public static string ToolTip10 = "Color selected text";


	// Three buttons exist. This tells what type of search is occurring
	public static bool Suffix = false;
	public static bool Prefix = false;
	public static bool ReplaceAll = false;


	public static Boolean[] BoolForms = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};

	// For public class History
	//public static int FrmMainXLocation = 600;
	//public static int FrmMainYLocation = 100;    // L3
	//public static int FrmMainWidth = 950;
	//public static int FrmMainHeight = 800;
	//public static int FrmColorXLocation = 900;
	//public static int FrmColorYLocation = 120;
	//public static int FrmQuizXLocation = 200;
	//public static int FrmQuizYLocation = 600;
	public static int FrmSectionXLocation = 960;
	public static int FrmSectionYLocation = 160;
	public static int FrmSectionWidth = 200;
	public static int FrmSectionHeight = 300;
	public static int FrmGetSearchXLocation = 890;  // L12
	public static int FrmGetSearchYLocation = 600;
	public static int FrmSaveSearchesXLocation = 700;
	public static int FrmSaveSearchesYLocation = 80;
	public static int FrmTTSXLocation = 700;
	public static int FrmTTSYLocation = 120;
	public static bool FrmMainInit = true;  // To prevent form location change from overwriting init values from INI file
	public static bool FrmColorInit = true;
	public static bool FrmQuizInit = true;
	public static bool FrmSectionInit = true;


	// FrmMain
	 public static bool Italics = false;

	public static Color FrmMainTextForecolor = Color.Black;
	public static Color BackColorStd = System.Drawing.Color.FromArgb(169, 169, 169);  // Global Standard BackColor for all RichTextBoxes
	

	// FrmColor
	public static bool PrintCanceled = false;
	public static bool PBoolRTBModified = false;
	public static int Element = 0;
	public static int SectionOrChartNumber = 0;
	public static int StartSectionOrChart = 0;
	public static int EndSectionOrChart = 0;
    

    // Keyboard Shortcuts Information
    public static string EnglishShortcuts = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset161 Times New Roman;}{\\f1\\fnil\\fcharset0 Times New Roman;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;\\red169\\green169\\blue169;\\red0\\green128\\blue0;\\red148\\green0\\blue211;\\red224\\green255\\blue255;}\r\n{\\*\\generator Riched20 10.0.22000}{\\*\\mmathPr\\mdispDef1\\mwrapIndent1440 }\\viewkind4\\uc1 \r\n\\pard\\cf1\\highlight2\\b\\f0\\fs44\\lang1032\\tab\\tab\\tab\\tab ENGLISH KEYBOARD SHORTCUTS\\par\r\n\\par\r\n In Windows 11, use the following keyboard shortcuts to insert special \\par\r\n characters when using the \\cf3 English (United States) \\i ENG\\cf1\\i0  language \\par\r\n keyboard layout.  Letters in \\cf4 parenthesis\\cf1  are possible characters.\\par\r\n\\par\r\n Type the \\cf4 letter\\cf1 , then the key combination:\\tab\\tab\\tab\\tab Result\\par\r\n Acute accent, \\cf4 (aeiou)\\cf1 , \\f1\\lang1033  press Alt + Right Arrow\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e1\\'e9\\'ed\\'f3\\'fa)\\par\r\n\\f0\\lang1032  Grave accent, \\cf4 (aeiou)\\cf1 , \\f1\\lang1033  press Alt + Left Arrow\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e0\\'e8\\'ec\\'f2\\'f9)\\par\r\n Diaeresis \\cf4 (aeiou)\\cf1 , \\tab\\tab press Alt + ;\\tab\\tab\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e4\\'eb\\'ef\\'f6\\'fc)\\par\r\n\\par\r\n To use these keyboard shortcuts, you will need to have the \\cf3 English \\par\r\n language keyboard\\cf1  layout selected.\\par\r\n\\highlight5\\par\r\n}\r\n\0";
    public static string AncientGreekShortcuts = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset161 Times New Roman;}{\\f1\\fnil\\fcharset0 Times New Roman;}{\\f2\\fnil\\fcharset1 Cambria Math;}{\\f3\\fnil Times New Roman;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;\\red169\\green169\\blue169;\\red0\\green128\\blue0;\\red148\\green0\\blue211;\\red139\\green69\\blue19;\\red192\\green192\\blue192;}\r\n{\\*\\generator Riched20 10.0.22000}{\\*\\mmathPr\\mmathFont2\\mdispDef1\\mwrapIndent1440 }\\viewkind4\\uc1 \r\n\\pard\\cf1\\highlight2\\b\\f0\\fs44\\lang1032      \\tab\\tab ANCIENT GREEK KEYBOARD SHORTCUTS\\par\r\n \\par\r\n In Windows 11, use the following keyboard shortcuts to insert special \\par\r\n characters when using the \\cf3 Greek \\i\\'c5\\'cb\\cf1\\i0  language. \\par\r\n\\par\r\n Note :\\b0  \\b Letters in (\\cf4 parenthesis)\\cf1  are possible characters.\\b0\\par\r\n\\b  Character\\tab\\tab\\tab\\tab\\tab Key Combination\\tab\\tab       Result\\b0\\par\r\n Short \\tab\\tab\\tab\\cf4 (\\b\\'e1\\'e9\\'f5)\\cf1\\b0\\tab\\tab\\b\\f1\\lang1033 press \\f0\\lang1032 Alt + =\\b0  \\cf4\\tab\\tab\\cf1\\b\\tab\\tab\\u8112?\\u8144?\\u8160?\\b0\\par\r\n Macron\\tab\\tab\\tab\\cf4 (\\b\\'e1\\'e9\\'f5)\\cf1\\b0\\tab\\tab\\b\\f1\\lang1033 press \\f0\\lang1032 Alt + -\\b0\\tab\\tab\\tab\\tab\\b\\u8113?\\u8145?\\u8161?\\b0\\par\r\n\\b  \\'e1\\'e9\\'f5\\b0\\tab  \\f2\\u8594?\\b\\f3\\lang1033   \\b0\\f0\\lang1032 (\\i The doubtful vowels, called such because they can be long or short.\\i0 )\\par\r\n\\b  Note: \\'e5, \\'ef\\b0  is always short, \\b\\'e7, \\'f9\\b0  always long.   So \\b o\\b0  long = \\b\\'f9\\b0 ,  \\b\\'e5\\b0  long = \\b\\'e7\\b0 .\\b\\par\r\n\\par\r\n Type the \\cf4 letter\\cf1 , then the key combination:\\par\r\n Character\\tab\\tab\\tab\\tab\\tab Key Combination                 Result\\par\r\n Acute accent \\tab\\cf4\\b0 (\\b\\'e1\\'e5\\'e9\\'ef\\'f5\\'e7\\'f9\\b0 ) \\cf1\\b  \\f1\\lang1033  press Alt + Right Arrow\\tab\\b0\\f0\\lang1032 (\\b\\u8049?\\u8051?\\u8055?\\u8057?\\u8059?\\u8053?\\u8061?\\b0 )\\b\\f1\\lang1033\\par\r\n\\f0\\lang1032  Grave accent \\tab\\cf4\\b0 (\\b\\'e1\\'e5\\'e9\\'ef\\'f5\\'e7\\'f9\\b0 ) \\cf1\\b  \\f1\\lang1033  press Alt + Left Arrow\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u8048?\\u8050?\\u8054?\\u8056?\\u8058?\\u8052?\\u8060?\\b0 )\\b\\f1\\lang1033\\par\r\n\\f0\\lang1032  Circumflex \\tab\\cf4\\b0 (\\b\\'e1\\'e9\\'f5\\'e7\\'f9\\b0 ) \\cf1\\b  \\f1\\lang1033  \\tab press Alt + `\\tab\\tab\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u8118?\\u8150?\\u8166?\\u8134?\\u8182?\\b0 )\\par\r\n\\b  Rough \\tab\\tab\\cf4\\b0 (\\b\\'e1\\'e5\\'e9\\'ef\\'f5\\'e7\\'f9\\b0 ) \\cf1\\b\\tab\\f1\\lang1033 press Alt + (\\tab\\tab\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u7937?\\u7953?\\u7985?\\u8001?\\u8017?\\u7969?\\u8033?\\b0 )\\par\r\n  \\cf5 Then:\\cf1\\b\\par\r\n\\b0   + acute \\tab\\tab\\cf4 (\\b\\'e1\\'e5\\'e9\\'ef\\'f5\\'e7\\'f9\\b0 )\\cf1  \\tab\\b\\f1\\lang1033 press Alt + \\tab Right Arrow\\tab\\b0\\f0\\lang1032 (\\b\\u7941?\\u7957?\\u7989?\\u8005?\\u8021?\\u7973?\\u8037?\\b0 )\\par\r\n  + grave\\tab\\tab\\cf4 (\\b\\'e1\\'e5\\'e9\\'ef\\'f5\\'e7\\'f9\\b0 )\\cf1  \\tab\\b\\f1\\lang1033 press Alt + \\tab Left Arrow\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u7939?\\u7955?\\u7987?\\u8003?\\u8019?\\u7971?\\u8035?\\b0 )\\par\r\n  + circumflex \\tab\\cf4 (\\b\\'e1\\'e9\\'f5\\'e7\\'f9\\b0 )\\cf1\\tab\\b\\f1\\lang1033 press Alt + \\tab `\\tab\\tab\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u7937?\\u7985?\\u8017?\\u7969?\\u8033?\\b0 )\\par\r\n\\par\r\n\\b  Smooth \\tab\\tab\\cf4\\b0 (\\b\\'e1\\'e5\\'e9\\'ef\\'f5\\'e7\\'f9\\b0 ) \\cf1\\b\\tab\\f1\\lang1033 press Alt + \\tab )\\tab\\tab\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u7936?\\u7952?\\u7984?\\u8000?\\u8016?\\u7968?\\u8032?\\b0 )\\par\r\n  \\cf5 Then:\\cf1\\b\\par\r\n\\b0   + acute \\tab\\tab\\cf4 (\\b\\'e1\\'e5\\'e9\\'ef\\'f5\\'e7\\'f9\\b0 )\\cf1  \\tab\\b\\f1\\lang1033 press Alt + \\tab Right Arrow\\tab\\b0\\f0\\lang1032 (\\b\\u7940?\\u7956?\\u7988?\\u8004?\\u8020?\\u7972?\\u8036?\\b0 )\\par\r\n  + grave\\tab\\tab\\cf4 (\\b\\'e1\\'e5\\'e9\\'ef\\'f5\\'e7\\'f9\\b0 )\\cf1  \\tab\\b\\f1\\lang1033 press Alt + \\tab Left Arrow\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u7938?\\u7954?\\u7986?\\u8002?\\u8018?\\u7970?\\u8034?\\b0 )\\par\r\n  + circumflex \\tab\\cf4 (\\b\\'e1\\'e9\\'f5\\'e7\\'f9\\b0 )\\cf1\\tab\\b\\f1\\lang1033 press Alt + \\tab `\\tab\\tab\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u7942?\\u7990?\\u8022?\\u7974?\\u8038?\\b0 )\\par\r\n\\par\r\n \\b Iota Subscript\\b0\\tab\\cf4 (\\b\\'e1\\'e5\\'e9\\'ef\\'f5\\'e7\\'f9\\b0 )\\cf1\\b\\tab\\f1\\lang1033 press Alt + '\\tab\\tab\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u8115?\\u8131?\\u8179?\\b0 )\\par\r\n  \\cf5 Then:\\par\r\n\\cf1   + smooth\\tab\\tab\\cf4 (\\b\\'e1\\'e7\\'f9\\b0 )\\cf1  \\tab\\tab\\b\\f1\\lang1033 press Alt + \\tab )\\tab\\tab\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u8064?\\u8080?\\u8096?\\b0 )\\par\r\n  + rough\\tab\\tab\\cf4 (\\b\\'e1\\'e7\\'f9\\b0 )\\cf1  \\tab\\tab\\b\\f1\\lang1033 press Alt + (\\tab\\tab\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u8065?\\u8081?\\u8097?\\b0 )\\par\r\n  + acute \\tab\\tab\\cf4 (\\b\\'e1\\'e7\\'f9\\b0 )\\cf1  \\tab\\tab\\b\\f1\\lang1033 press Alt + \\tab Right Arrow\\tab\\b0\\f0\\lang1032 (\\b\\u8116?\\u8132?\\u8180?\\b0 )\\par\r\n  + grave\\tab\\tab\\cf4 (\\b\\'e1\\'e7\\'f9\\b0 )\\cf1  \\tab\\tab\\b\\f1\\lang1033 press Alt + \\tab Left Arrow\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u8114?\\u8130?\\u8178?\\b0 )\\par\r\n  + circumflex \\tab\\cf4 (\\b\\'e1\\'e7\\'f9\\b0 )\\cf1\\tab\\tab\\b\\f1\\lang1033 press Alt + \\tab `\\tab\\tab\\tab\\tab\\b0\\f0\\lang1032 (\\b\\u8119?\\u8135?\\u8183?\\b0 )\\par\r\n\\b\\par\r\n\\b0  \\b Diaresis \\b0\\tab\\tab\\cf4\\b (\\'e9\\'f5)\\cf1\\b0\\tab\\tab\\tab\\b\\f1\\lang1033 press Alt + ; \\f0\\lang1032\\tab    \\tab\\tab     \\tab  (\\'fa\\'fb)\\par\r\n\\b0   \\cf5 Then:\\cf1\\b\\par\r\n    \\b0 + acute  \\tab\\tab\\cf4\\b (\\'e9\\'f5)\\cf1\\b0\\tab\\tab\\tab\\b\\f1\\lang1033 press Alt + Right Arrow\\f0\\lang1032\\tab    \\u8147?\\u8163?\\par\r\n\\b0     + grave   \\tab\\tab\\cf4\\b (\\'e9\\'f5)\\cf1\\b0\\tab\\tab\\tab\\b\\f1\\lang1033 press Alt + Left Arrow\\b0\\f0\\lang1032\\tab    \\tab    \\b\\u8146?\\u8162?\\par\r\n\\par\r\n\\b0  \\b\\f1\\lang1033 Question Mark  \\b0\\f0\\lang1032  \\tab\\tab\\tab\\b\\f1\\lang1033 press \\f0\\lang1032 q + Space Bar  \\tab\\tab     ;\\par\r\n\\f1\\lang1033\\par\r\n Period\\b0\\f0\\lang1032\\tab\\tab\\tab\\tab\\tab\\tab\\b press Alt + \\b0 .\\tab\\tab     \\tab\\tab     \\b\\u903?\\par\r\n\\highlight6\\par\r\n}\r\n";
    public static string FrenchShortcuts = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset161 Times New Roman;}{\\f1\\fnil\\fcharset0 Times New Roman;}{\\f2\\fnil Times New Roman;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;\\red192\\green192\\blue192;\\red0\\green128\\blue0;\\red148\\green0\\blue211;}\r\n{\\*\\generator Riched20 10.0.22000}{\\*\\mmathPr\\mdispDef1\\mwrapIndent1440 }\\viewkind4\\uc1 \r\n\\pard\\cf1\\highlight2\\b\\f0\\fs44\\lang1032\\tab\\tab\\tab\\tab FRENCH KEYBOARD SHORTCUTS\\par\r\n\\par\r\n In Windows 11, you can use the following keyboard shortcuts to insert \\par\r\n special characters when using the \\cf3 French France \\i FRA\\cf1\\i0  language \\par\r\n keyboard layout.  Letters in \\cf4 parenthesis\\cf1  are possible characters.\\par\r\n\\par\r\n Type the \\cf4 letter\\cf1 , then the key combination:\\tab\\tab\\tab\\tab Result\\par\r\n Acute accent, \\tab\\cf4 (aeiou)\\cf1 , \\f1\\lang1033  press Alt + Right Arrow\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e1\\'e9\\'ed\\'f3\\'fa)\\par\r\n\\f0\\lang1032  Grave accent, \\tab\\cf4 (aeiou)\\cf1 , \\f1\\lang1033  press Alt + Left Arrow\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e0\\'e8\\'ec\\'f2\\'f9)\\par\r\n Circumflex \\tab\\cf4\\f0\\lang1032 (aeiou)\\cf1 ,\\f1\\lang1033   press Alt + '  \\tab\\tab\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e2\\'ea\\'ee\\'f4\\'fb)\\par\r\n Diaresis\\tab\\tab\\cf4\\f0\\lang1032 (aeiou)\\cf1 ,\\f1\\lang1033   press Alt + ] \\tab\\tab\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e4\\'eb\\'ef\\'f6\\'fc)\\par\r\n Cedilla\\tab\\tab\\tab\\cf4 (c)\\cf1  \\tab         press 9\\tab\\tab\\tab\\tab\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e7)\\par\r\n\\par\r\n To use these keyboard shortcuts, you will need to have the French \\par\r\n language keyboard layout selected.  \\par\r\n Note: French normally uses \\cf3 CONTROL W\\cf1  for undo.\\par\r\n\\highlight0\\b0\\f2\\par\r\n}\r\n\0";
    public static string GermanShortcuts = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset161 Times New Roman;}{\\f1\\fnil\\fcharset0 Times New Roman;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;\\red192\\green192\\blue192;\\red0\\green128\\blue0;\\red148\\green0\\blue211;\\red169\\green169\\blue169;}\r\n{\\*\\generator Riched20 10.0.22000}{\\*\\mmathPr\\mdispDef1\\mwrapIndent1440 }\\viewkind4\\uc1 \r\n\\pard\\cf1\\highlight2\\b\\f0\\fs44\\lang1032\\tab\\tab\\tab\\tab GERMAN KEYBOARD SHORTCUTS\\par\r\n\\par\r\n In Windows 11, you can use the following keyboard shortcuts to insert \\par\r\n special characters when using the \\cf3 German (Germany) \\i DEU\\cf1\\i0  language \\par\r\n keyboard layout.  Letters in \\cf4 parenthesis\\cf1  are possible characters.\\par\r\n\\par\r\n Type the \\cf4 letter\\cf1 , then the key combination:\\tab\\tab\\tab Result\\par\r\n Umlaut\\f1\\lang1033 , \\cf4 (aeiou)\\cf1 , \\tab press Alt + [\\tab\\tab\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e4\\'eb\\'ef\\'f6\\'fc)\\par\r\n Scharfes \\'df, \\tab\\tab press -\\tab\\tab\\tab\\tab\\tab\\tab\\tab      \\'df\\par\r\n\\par\r\n To use these keyboard shortcuts, you will need to have the German \\par\r\n language keyboard layout selected. \\par\r\n Note: German normally uses \\cf3 CONTROL Y\\cf1  for undo.\\par\r\n\\highlight5\\par\r\n}\r\n\0";
    public static string ItalianShortcuts = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset161 Times New Roman;}{\\f1\\fnil\\fcharset0 Times New Roman;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;\\red192\\green192\\blue192;\\red0\\green128\\blue0;\\red148\\green0\\blue211;}\r\n{\\*\\generator Riched20 10.0.22000}{\\*\\mmathPr\\mdispDef1\\mwrapIndent1440 }\\viewkind4\\uc1 \r\n\\pard\\cf1\\highlight2\\b\\f0\\fs44\\lang1032\\tab\\tab\\tab\\tab ITALIAN KEYBOARD SHORTCUTS\\par\r\n\\par\r\n In Windows 11, use the following keyboard shortcuts to insert special \\par\r\n characters when using the \\cf3 Italian (Italy) \\i ITA\\cf1\\i0  language \\par\r\n keyboard layout.  Letters in \\cf4 parenthesis\\cf1  are possible characters.\\par\r\n\\par\r\n Type the \\cf4 letter\\cf1 , then the key combination:\\tab\\tab\\tab\\tab Result\\par\r\n Acute accent, \\cf4 (aeiou)\\cf1 , \\f1\\lang1033  press Alt + Right Arrow\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e1\\'e9\\'ed\\'f3\\'fa)\\par\r\n\\f0\\lang1032  Grave accent, \\cf4 (aeiou)\\cf1 , \\f1\\lang1033  press Alt + Left Arrow\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e0\\'e8\\'ec\\'f2\\'f9)\\par\r\n\\par\r\n To use these keyboard shortcuts, you will need to have the \\cf3\\f0\\lang1032 Italian\\f1\\lang1033  \\par\r\n language keyboard\\cf1  layout selected.\\par\r\n}\r\n\0";
	public static string SpanishShortcuts = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset161 Times New Roman;}{\\f1\\fnil\\fcharset0 Times New Roman;}{\\f2\\fnil Times New Roman;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;\\red192\\green192\\blue192;\\red0\\green128\\blue0;\\red148\\green0\\blue211;}\r\n{\\*\\generator Riched20 10.0.22000}{\\*\\mmathPr\\mdispDef1\\mwrapIndent1440 }\\viewkind4\\uc1 \r\n\\pard\\ltrpar\\cf1\\highlight2\\b\\f0\\fs44\\lang1032\\tab\\tab\\tab\\tab SPANISH KEYBOARD SHORTCUTS\\par\r\n\\par\r\n In Windows 11, use the following keyboard shortcuts to insert special \\par\r\n characters when using  the \\cf3 Spanish (Spain)\\cf1 , \\cf3\\i ESP\\cf1\\i0  language keyboard\\par\r\n layout.  Letters in \\cf4 parenthesis\\cf1  are possible characters.\\par\r\n\\par\r\n Type the \\cf4 letter\\cf1 , then the key combination:\\tab\\tab\\tab Result\\par\r\n \\tab Acute accent, \\tab (aeiou), \\f1\\lang1033  Alt + Right Arrow\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e1\\'e9\\'ed\\'f3\\'fa)\\par\r\n\\f0\\lang1032  \\tab Grave accent, \\tab (aeiou), \\f1\\lang1033  Alt + Left Arrow\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e0\\'e8\\'ec\\'f2\\'f9)\\par\r\n \\tab Diaeresis, \\tab\\tab (aeiou),  Alt + [ \\tab\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e4\\'eb\\'ef\\'f6\\'fc)\\par\r\n \\tab Circumflex, \\tab (aeiou),  Alt + ;\\tab\\tab\\tab\\tab\\tab\\f0\\lang1032 (\\f1\\lang1033\\'e2\\'ea\\'ee\\'f4\\'fb)\\par\r\n \\par\r\n Individual Letters:  \\par\r\n \\tab Key(s)\\par\r\n \\tab ; \\tab\\tab\\tab\\tab\\tab\\'f1\\par\r\n       Shift + ; \\tab\\tab\\tab\\'d1\\par\r\n \\tab = \\tab\\tab\\tab\\tab\\tab\\'a1\\par\r\n \\tab Shift + = \\tab\\tab\\tab\\'bf\\par\r\n\\par\r\n To use these keyboard shortcuts, the \\cf3 Spanish language keyboard\\cf1  \\par\r\n layout must be selected.\\par\r\n\\highlight0\\b0\\f2\\par\r\n}\r\n\0";
	public static string RussianShortcuts = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset161 Times New Roman;}{\\f1\\fnil\\fcharset204 Times New Roman;}{\\f2\\fnil\\fcharset0 Times New Roman;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;\\red192\\green192\\blue192;\\red0\\green128\\blue0;\\red148\\green0\\blue211;\\red169\\green169\\blue169;}\r\n{\\*\\generator Riched20 10.0.22000}{\\*\\mmathPr\\mdispDef1\\mwrapIndent1440 }\\viewkind4\\uc1 \r\n\\pard\\cf1\\highlight2\\b\\f0\\fs44\\lang1032\\tab\\tab\\tab\\tab RUSSIAN KEYBOARD SHORTCUTS\\par\r\n\\par\r\n In Windows 11, use the following keyboard shortcuts to insert special \\par\r\n characters when using the \\cf3 Russian \\i\\f1\\lang1049\\'d0\\'d3\\'d1\\cf1\\i0  language keyboard layout.\\par\r\n Note that \\i modern Russian has no diacritic characters\\i0 , although it is\\par\r\n still possible to use diacritics when in Russian.  \\par\r\n Letters in \\cf4 parenthesis\\cf1  are possible characters.\\par\r\n\\par\r\n Type the \\cf4 letter\\cf1 , then the key combination:\\tab\\tab\\tab\\tab Result\\par\r\n Acute accent, \\cf4 (aeo)\\cf1 , \\f2\\lang1033  press Alt + Right Arrow\\tab\\tab\\tab\\f0\\lang1032 (\\f2\\lang1033\\'e1\\'e9\\'f3)\\par\r\n\\f0\\lang1032  Grave accent, \\cf4 (aeo)\\cf1 , \\f2\\lang1033  press Alt + Left Arrow\\tab\\tab\\tab\\f0\\lang1032 (\\f2\\lang1033\\'e0\\'e8\\'f2)\\par\r\n Diaeresis \\cf4 (aeo)\\cf1 , \\tab\\tab press Alt + ;\\tab\\tab\\tab\\tab\\tab\\f0\\lang1032 (\\f2\\lang1033\\'e4\\'eb\\'f6)\\par\r\n\\par\r\n To use these keyboard shortcuts, you will need to have the \\cf3\\f0\\lang1032 Russian\\f2\\lang1033  \\par\r\n language keyboard\\cf1  layout selected.\\lang1040\\par\r\n\\highlight5\\par\r\n}\r\n\0";
	public static string ArabicShortcuts = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset161 Times New Roman;}{\\f1\\fnil\\fcharset178 Times New Roman;}{\\f2\\fnil\\fcharset204 Times New Roman;}{\\f3\\fnil\\fcharset0 Times New Roman;}{\\f4\\fnil Times New Roman;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;\\red192\\green192\\blue192;\\red0\\green128\\blue0;}\r\n{\\*\\generator Riched20 10.0.22000}{\\*\\mmathPr\\mdispDef1\\mwrapIndent1440 }\\viewkind4\\uc1 \r\n\\pard\\ltrpar\\cf1\\highlight2\\b\\f0\\fs44\\lang1032\\tab\\tab\\tab\\tab ARABIC KEYBOARD SHORTCUTS\\par\r\n\\par\r\n In Windows 11, use the following keyboard shortcuts to insert special \\par\r\n characters when using the \\cf3 Arabic (Egypt) \\f1\\rtlch\\lang3073\\'da \\cf1\\f2\\ltrch\\lang1049 language keyboard layout.\\f0\\lang1032\\par\r\n\\par\r\n\\tab  Fatha,\\f3\\lang1033  \\tab\\tab Shift + A\\tab\\tab\\par\r\n\\tab  Kasra,\\tab\\tab Shift + I\\par\r\n\\tab  Damma,  \\tab Shift + U\\par\r\n\\tab  Sukun, \\tab Shift + X\\par\r\n\\tab  Shadda, \\tab Shift + W\\par\r\n\\tab  Tanween, \\tab Shift + N, Shift + M, Shift + , \\par\r\n\\tab  Hamza, \\tab\\f0\\lang1032 Shift + '\\par\r\n \\par\r\n\\f3\\lang1033\\par\r\n To use these keyboard shortcuts, you will need to have the \\cf3\\f0\\lang1032 Arabic\\f3\\lang1033  \\par\r\n language keyboard\\cf1  layout selected.   These could vary based on\\par\r\n your PC, and so far have not been thoroughly tested.\\lang1040\\par\r\n\\cf0\\highlight0\\b0\\f4\\lang1033\\par\r\n}\r\n\0";


    // Options from FrmOptions
    public static string StrSetUserAgreed = ""; 
	public static bool UserHasAgreed = false;
	public static bool FrmOptionsOpenFromLastLocation = true;
	public static bool SetfocusOnTextboxOnMouseover = false;
	public static bool FrmOptionsOptOutOfFutureChangeFontWarnings = false;

	public static string StrBuildDate = DateTime.Now.ToString("MM/dd/yyyy");  // "2/24/2023";   // For INI file
    public static string BuildVersion = "7.0";

}

