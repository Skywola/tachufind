using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    public partial class MyUserSettings : ApplicationSettingsBase
    {
        // StrSetUserAgreed
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("")]
        public String StrSetUserAgreed
        {
            get { return (String)(this[nameof(StrSetUserAgreed)]); }
            set { this[nameof(StrSetUserAgreed)] = value; }
        }

        #region GeneralSettings
        [UserScopedSetting()]
        [DefaultSettingValue("true")] // Default is set to 'true' to use default colors
        public bool UseDefaultColors
        {
            get { return (bool)this[nameof(UseDefaultColors)]; }
            set { this[nameof(UseDefaultColors)] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")] // Default is set to 'true' to use default colors
        public bool LoopTTSPlayback
        {
            get { return (bool)this[nameof(LoopTTSPlayback)]; }
            set { this[nameof(LoopTTSPlayback)] = value; }
        }


        // Text to speech speed setting
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("-4")]
        public int TTS_Speed
        {
            get { return (int)(this[nameof(TTS_Speed)]); }
            set { this[nameof(TTS_Speed)] = value; }
        }

        // Text to speech speed setting
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("80")]
        public int TTS_Volume
        {
            get { return (int)(this[nameof(TTS_Volume)]); }
            set { this[nameof(TTS_Volume)] = value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("100")]
        public int TTS_Synth_Volume
        {
            get
            {
                object raw = this[nameof(TTS_Synth_Volume)];

                if (raw is int i)
                    return i;

                if (raw is string s && int.TryParse(s, out int parsed))
                    return parsed;

                return 0;
            }
            set
            {
                this[nameof(TTS_WFVoice2_Volume)] = value;
            }
        }


        // TTS_Voice
        [UserScopedSettingAttribute()]
        [DefaultSettingValue("Microsoft David Desktop")]
        public String TTS_Voice
        {
            get { return ((String)this[nameof(TTS_Voice)]); }
            set { this[nameof(TTS_Voice)] = (String)value; }
        }

        // FilePath
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public String FilePath
        {
            get { return ((String)this[nameof(FilePath)]); }
            set { this[nameof(FilePath)] = (String)value; }
        }

        // FilePath01
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("")]
        public String FilePath01
        {
            get { return (String)(this[nameof(FilePath01)]); }
            set { this[nameof(FilePath01)] = value; }
        }

        #endregion //  GeneralSettings



        #region FORM SETTINGS
        #region FrmMain

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public Boolean ToggleTTS_SpecChar // if false, on Main form, display all special character buttons instead on start-up
        {
            get { return (Boolean)(this[nameof(ToggleTTS_SpecChar)]); }
            set { this[nameof(ToggleTTS_SpecChar)] = value; }
        }

        // FrmMainLocation
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("600,100")]
        public Point FrmMainLocation
        {
            get { return (Point)(this[nameof(FrmMainLocation)]); }
            set { this[nameof(FrmMainLocation)] = value; }
        }

        // FrmMainSize
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("950, 800")]
        public Size FrmMainSize
        {
            get { return (Size)this[nameof(FrmMainSize)]; }
            set { this[nameof(FrmMainSize)] = value; }
        }

        // RTBMainBackColor
        [UserScopedSetting()]
        [DefaultSettingValue("DarkGray")]
        public Color RTBMainBackColor  // This might be orphaned
        {
            get { return ((Color)this[nameof(RTBMainBackColor)]); }
            set { this[nameof(RTBMainBackColor)] = (Color)value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public Boolean ChkPreserveHighlighting // if false, on Main form, display all special character buttons instead on start-up
        {
            get { return (Boolean)(this["chkPreserveHighlighting"]); }
            set { this["chkPreserveHighlighting"] = value; }
        }

        // CursorPosition
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("0")]
        public int CursorPosition
        {
            get { return (int)(this[nameof(CursorPosition)]); }
            set { this[nameof(CursorPosition)] = value; }
        }

        // FrmMainWordWrap
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public Boolean FrmMainWordWrap
        {
            get { return (Boolean)(this[nameof(FrmMainWordWrap)]); }
            set { this[nameof(FrmMainWordWrap)] = value; }
        }

        #endregion // FrmMain


        #region FrmColor
        // Search time limit for FrmColor
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("20")]
        public int SearchTimeLimit
        {
            get { return (int)(this[nameof(SearchTimeLimit)]); }
            set { this[nameof(SearchTimeLimit)] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("900,120")]
        public Point FrmColorLocation
        {
            get { return (Point)(this[nameof(FrmColorLocation)]); }
            set { this[nameof(FrmColorLocation)] = value; }
        }

        // FrmColor Mode:  Toggles between Replace Color = true   Replace Text = false
        [UserScopedSetting()]
        [DefaultSettingValue("true")] // Default is set to 'true' to use default colors
        public bool FrmColor_ReplaceMode
        {
            get { return (bool)this[nameof(FrmColor_ReplaceMode)]; }
            set { this[nameof(FrmColor_ReplaceMode)] = value; }
        }


        // Colors
        [UserScopedSetting()]
        [DefaultSettingValue("#0000FF")] // Blue
        public Color Color1
        {
            get { return ((Color)this[nameof(Color1)]); }
            set { this[nameof(Color1)] = (Color)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("#00FF00")]  // Green
        public Color Color2
        {
            get { return ((Color)this[nameof(Color2)]); }
            set { this[nameof(Color2)] = (Color)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("#FF0000")]  // Red
        public Color Color3
        {
            get { return ((Color)this[nameof(Color3)]); }
            set { this[nameof(Color3)] = (Color)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Crimson")]
        public Color Color4
        {
            get { return ((Color)this[nameof(Color4)]); }
            set { this[nameof(Color4)] = (Color)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("DarkViolet")] // 
        public Color Color5
        {
            get { return ((Color)this[nameof(Color5)]); }
            set { this[nameof(Color5)] = (Color)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("SaddleBrown")]
        public Color Color6
        {
            get { return ((Color)this[nameof(Color6)]); }
            set { this[nameof(Color6)] = (Color)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("SlateGray")]
        public Color Color7
        {
            get { return ((Color)this[nameof(Color7)]); }
            set { this[nameof(Color7)] = (Color)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("DodgerBlue")]
        public Color Color8
        {
            get { return ((Color)this[nameof(Color8)]); }
            set { this[nameof(Color8)] = (Color)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Olive")]
        public Color Color9
        {
            get { return ((Color)this[nameof(Color9)]); }
            set { this[nameof(Color9)] = (Color)value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Yellow")]
        public Color Color10
        {
            get { return ((Color)this[nameof(Color10)]); }
            set { this[nameof(Color10)] = (Color)value; }
        }
        #endregion // FrmColor


        #region FrmOCR

        // FrmOCRLocation
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("200,200")]
        public Point FrmOCRLocation
        {
            get { return (Point)(this[nameof(FrmOCRLocation)]); }
            set { this[nameof(FrmOCRLocation)] = value; }
        }

        #endregion // FrmOCR

        #region FrmQuiz 

        // FrmQuizLocation
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("200,600")]
        public Point FrmQuizLocation
        {
            get { return (Point)(this[nameof(FrmQuizLocation)]); }
            set { this[nameof(FrmQuizLocation)] = value; }
        }

        // FrmQuizSize
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("1502, 858")]  //1521, 858
        public Size FrmQuizSize
        {
            get { return (Size)this[nameof(FrmQuizSize)]; }
            set { this[nameof(FrmQuizSize)] = value; }
        }

        // Font for FrmQuiz's RTBQuestion and RTBAnswer
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("Times New Roman")]
        public string FrmQuizFontName
        {
            get { return (string)(this[nameof(FrmQuizFontName)]); }
            set { this[nameof(FrmQuizFontName)] = value; }
        }

        // Font Size for FrmQuiz's RTBQuestion and RTBAnswer
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("22")]
        public int FrmQuizFontSize
        {
            get { return (int)(this[nameof(FrmQuizFontSize)]); }
            set { this[nameof(FrmQuizFontSize)] = Convert.ToInt32(value); }
        }

        // quizListBoxSelection
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute(" ")]
        public string quizListBoxSelection
        {
            get { return (string)(this["quizListBoxSelection"]); }
            set { this["quizListBoxSelection"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public Boolean chkAddClear
        {
            get { return (Boolean)(this["chkAddClear"]); }
            set { this["chkAddClear"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public Boolean chkShuffle
        {
            get { return (Boolean)(this["chkShuffle"]); }
            set { this["chkShuffle"] = value; }
        }
        #endregion // FrmQuiz

        #region FrmGetQuizes

        // FrmGetQuizesLocation
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("200,600")]
        public Point FrmGetQuizesLocation
        {
            get { return (Point)(this[nameof(FrmGetQuizesLocation)]); }
            set { this[nameof(FrmGetQuizesLocation)] = value; }
        }
        #endregion // FrmGetQuizes

        #region FrmGetSearches

        // FrmGetSearchesLocation
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("200,600")]
        public Point FrmGetSearchesLocation
        {
            get { return (Point)(this[nameof(FrmGetSearchesLocation)]); }
            set { this[nameof(FrmGetSearchesLocation)] = value; }
        }

        // SearchListBoxSelection
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute(" ")]
        public string SearchListBoxSelection
        {
            get { return (string)(this[nameof(SearchListBoxSelection)]); }
            set { this[nameof(SearchListBoxSelection)] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("All")]
        public string CboSearchFilterSelection
        {
            get { return (string)(this[nameof(CboSearchFilterSelection)]); }
            set { this[nameof(CboSearchFilterSelection)] = value; }
        }
        #endregion // FrmGetSearches

        #region FrmInterlinear

        // FrmInterlinearLocation
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("200,200")]
        public Point FrmInterlinearLocation
        {
            get { return (Point)(this[nameof(FrmInterlinearLocation)]); }
            set { this[nameof(FrmInterlinearLocation)] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public String FrmInterlinearLastLocationDir
        {
            get { return ((String)this[nameof(FrmInterlinearLastLocationDir)]); }
            set { this[nameof(FrmInterlinearLastLocationDir)] = (String)value; }
        }
        #endregion // FrmInterlinear

        #region FrmOptions

        // FrmOptionsLocation
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("200,600")]
        public Point FrmOptionsLocation
        {
            get { return (Point)(this[nameof(FrmOptionsLocation)]); }
            set { this[nameof(FrmOptionsLocation)] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public Boolean FrmOptionsOpenFromLastLocation
        {
            get { return (Boolean)(this[nameof(FrmOptionsOpenFromLastLocation)]); }
            set { this[nameof(FrmOptionsOpenFromLastLocation)] = value; }
        }
        #endregion // FrmOptions

        #region FrmSection
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("960,160")]
        public Point FrmSectionLocation
        {
            get { return (Point)(this[nameof(FrmSectionLocation)]); }
            set { this[nameof(FrmSectionLocation)] = value; }
        }

        // FrmSectionSize
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("1521, 858")]  //1521, 858
        public Size FrmSectionSize
        {
            get { return (Size)this[nameof(FrmSectionSize)]; }
            set { this[nameof(FrmSectionSize)] = value; }
        }

        #endregion // FORM SETTINGS

        #region FrmOpticalCharacterRecognition

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("English")]
        public String OCR_Language1
        {
            get { return ((String)this[nameof(OCR_Language1)]); }
            set { this[nameof(OCR_Language1)] = (String)value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("Greek")]
        public String OCR_Language2
        {
            get { return ((String)this[nameof(OCR_Language2)]); }
            set { this[nameof(OCR_Language2)] = (String)value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("English")]
        public String OCR_Language3
        {
            get { return ((String)this[nameof(OCR_Language3)]); }
            set { this[nameof(OCR_Language3)] = (String)value; }
        }

        // state of OCR Marker
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public string StateOfOCRTTSBrackets
        {
            get { return (string)(this[nameof(StateOfOCRTTSBrackets)]); }
            set { this[nameof(StateOfOCRTTSBrackets)] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("70")]
        public int LineLengthForInterlinears
        {
            get { return (int)(this[nameof(LineLengthForInterlinears)]); }
            set
            {
                this[nameof(LineLengthForInterlinears)] = Convert.ToInt32(value);
            }
        }

        // lastFolderPathToOCRImage
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("English")]
        public string LastFolderPathToOCRImageTop
        {
            get { return (string)(this[nameof(LastFolderPathToOCRImageTop)]); }
            set { this[nameof(LastFolderPathToOCRImageTop)] = value; }
        }

        // lastFolderPathToOCRImage
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("English")]
        public string LastFolderPathToOCRImageBottom
        {
            get { return (string)(this[nameof(LastFolderPathToOCRImageBottom)]); }
            set { this[nameof(LastFolderPathToOCRImageBottom)] = value; }
        }

        #endregion // FrmOpticalCharacterRecognition

        #region FrmKBShortcuts

        // FrmKBShortcutsLocation
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("200,600")]
        public Point FrmKBShortcutsLocation
        {
            get { return (Point)(this[nameof(FrmKBShortcutsLocation)]); }
            set { this[nameof(FrmKBShortcutsLocation)] = value; }
        }

        // FrmKBShortcutsSize
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("1521, 858")]  //1521, 858
        public Size FrmKBShortcutsSize
        {
            get { return (Size)this[nameof(FrmKBShortcutsSize)]; }
            set { this[nameof(FrmKBShortcutsSize)] = value; }
        }
        #endregion // FrmKBShortcuts

        #region FrmWordFrequency
        // TTS_WFVoice1
        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public String TTS_WFVoice1
        {
            get { return ((String)this[nameof(TTS_WFVoice1)]); }
            set { this[nameof(TTS_WFVoice1)] = (String)value; }
        }

        // TTS_WFVoice2
        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public String TTS_WFVoice2
        {
            get { return ((String)this[nameof(TTS_WFVoice2)]); }
            set { this[nameof(TTS_WFVoice2)] = (String)value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("100")]
        public int TTS_WFVoice1_Volume
        {
            get
            {
                object raw = this[nameof(TTS_WFVoice1_Volume)];

                if (raw is int i)
                    return i;

                if (raw is string s && int.TryParse(s, out int parsed))
                    return parsed;

                return 0;
            }
            set
            {
                this[nameof(TTS_WFVoice1_Volume)] = value;
            }
        }


        [UserScopedSetting]
        [DefaultSettingValue("100")]
        public int TTS_WFVoice2_Volume
        {
            get
            {
                object raw = this[nameof(TTS_WFVoice2_Volume)];

                if (raw is int i)
                    return i;

                if (raw is string s && int.TryParse(s, out int parsed))
                    return parsed;

                return 0;
            }
            set
            {
                this[nameof(TTS_WFVoice2_Volume)] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("-2")]
        public int TTS_WFVoice1_Rate1
        {
            get
            {
                object raw = this[nameof(TTS_WFVoice1_Rate1)];

                if (raw is int i)
                    return i;

                if (raw is string s && int.TryParse(s, out int parsed))
                    return parsed;

                return 0;
            }
            set
            {
                this[nameof(TTS_WFVoice1_Rate1)] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int TTS_WFVoice1_Rate2
        {
            get
            {
                object raw = this[nameof(TTS_WFVoice1_Rate2)];

                if (raw is int i)
                    return i;

                if (raw is string s && int.TryParse(s, out int parsed))
                    return parsed;

                return 0;
            }
            set
            {
                this[nameof(TTS_WFVoice1_Rate2)] = value;
            }
        }



        #endregion FrmWordFrequency





        #endregion // Main
    }
}
