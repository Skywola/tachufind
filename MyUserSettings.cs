using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

public class MyUserSettings : ApplicationSettingsBase
{
    [UserScopedSetting()]
    [DefaultSettingValue("DarkGray")]
    public Color RTBMainBackColor
    {
        get  { return ((Color)this["RTBMainBackColor"]);}
        set  {this["RTBMainBackColor"] = (Color)value;}
    }

    [UserScopedSetting()]
    [DefaultSettingValue("White")]
    public Color Color00
    {
        get { return ((Color)this["Color00"]); }
        set { this["Color00"] = (Color)value; }
    }

    // string col = Color.DarkViolet.ToString();  // gets names

    [UserScopedSetting()]
    [DefaultSettingValue("Blue")]
    public Color Color01
    {
        get { return ((Color)this["Color01"]); }
        set { this["Color01"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("Green")]
    public Color Color02
    {
        get { return ((Color)this["Color02"]); }
        set { this["Color02"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("Red")]
    public Color Color03
    {
        get { return ((Color)this["Color03"]); }
        set { this["Color03"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("Crimson")]
    public Color Color04
    {
        get { return ((Color)this["Color04"]); }
        set { this["Color04"] = (Color)value; }
    }
    
    [UserScopedSetting()]
    [DefaultSettingValue("DarkViolet")] // 
    public Color Color05
    {
        get { return ((Color)this["Color05"]); }
        set { this["Color05"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("SaddleBrown")]
    public Color Color06
    {
        get { return ((Color)this["Color06"]); }
        set { this["Color06"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("SlateGray")]
    public Color Color07
    {
        get { return ((Color)this["Color07"]); }
        set { this["Color07"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("DodgerBlue")]
    public Color Color08  
    {
        get { return ((Color)this["Color08"]); }
        set { this["Color08"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("Olive")]
    public Color Color09
    {
        get { return ((Color)this["Color09"]); }
        set { this["Color09"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("Yellow")]
    public Color Color10
    {
        get { return ((Color)this["Color10"]); }
        set { this["Color10"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("Black")]
    public Color Color11
    {
        get { return ((Color)this["Color11"]); }
        set { this["Color11"] = (Color)value; }
    }

    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public String FilePath
    {
        get { return ((String)this["FilePath"]); }
        set { this["FilePath"] = (String)value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("600,100")]
    public Point FrmMainLocation
    {
        get { return (Point)(this["FrmMainLocation"]); }
        set { this["FrmMainLocation"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("950, 800")]
    public Size FrmMainSize
    {
        get { return (Size)this["FrmMainSize"]; }
        set { this["FrmMainSize"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("900,120")]
    public Point FrmColorLocation
    {
        get { return (Point)(this["FrmColorLocation"]); }
        set { this["FrmColorLocation"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("200,600")]
    public Point FrmQuizLocation
    {
        get { return (Point)(this["FrmQuizLocation"]); }
        set { this["FrmQuizLocation"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("1521, 858")]  //1521, 858
    public Size FrmQuizSize
    {
        get { return (Size)this["FrmQuizSize"]; }
        set { this["FrmQuizSize"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValue("")]
    public String TTS_Voice
    {
        get { return ((String)this["TTS_Voice"]); }
        set { this["TTS_Voice"] = (String)value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValue("English")]
    public String OCR_Language
    {
        get { return ((String)this["OCR_Language"]); }
        set { this["OCR_Language"] = (String)value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("960,160")]
    public Point FrmSectionLocation
    {
        get { return (Point)(this["FrmSectionLocation"]); }
        set { this["FrmSectionLocation"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("200, 300")]
    public Size FrmSectionSize
    {
        get { return (Size)this["FrmSectionSize"]; }
        set { this["FrmSectionSize"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("700,120")]
    public Point FrmTTSLocation
    {
        get { return (Point)(this["FrmTTSLocation"]); }
        set { this["FrmTTSLocation"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("true")]
    public Boolean FrmMainInit
    {
        get { return (Boolean)(this["FrmMainInit"]); }
        set { this["FrmMainInit"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("true")]
    public Boolean FrmColorInit
    {
        get { return (Boolean)(this["FrmColorInit"]); }
        set { this["FrmColorInit"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("true")]
    public Boolean FrmQuizInit
    {
        get { return (Boolean)(this["FrmQuizInit"]); }
        set { this["FrmQuizInit"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("true")]
    public Boolean FrmSectionInit
    {
        get { return (Boolean)(this["FrmSectionInit"]); }
        set { this["FrmSectionInit"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("0")]
    public int CursorPosition
    {
        get { return (int)(this["CursorPosition"]); }
        set { this["CursorPosition"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("true")]
    public Boolean FrmMainWordWrap
    {
        get { return (Boolean)(this["FrmMainWordWrap"]); }
        set { this["FrmMainWordWrap"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("true")]
    public Boolean Italics
    {
        get { return (Boolean)(this["Italics"]); }
        set { this["Italics"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("Black")]
    public Color FrmMainTextForecolor
    {
        get { return (Color)(this["FrmMainTextForecolor"]); }
        set { this["FrmMainTextForecolor"] = value; }
    }

    [UserScopedSettingAttribute()]   // Global Standard BackColor for all RichTextBoxes
    [DefaultSettingValueAttribute("Color.FromArgb(169, 169, 169)")]
    public Color backColorStd
    {
        get { return (Color)(this["backColorStd"]); }
        set { this["backColorStd"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("false")]
    public Boolean PrintCanceled
    {
        get { return (Boolean)(this["PrintCanceled"]); }
        set { this["PrintCanceled"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("false")]
    public Boolean BoolRTBModified
    {
        get { return (Boolean)(this["BoolRTBModified"]); }
        set { this["BoolRTBModified"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String StrSetUserAgreed
    {
        get { return (String)(this["StrSetUserAgreed"]); }
        set { this["StrSetUserAgreed"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("false")]
    public Boolean UserHasAgreed
    {
        get { return (Boolean)(this["UserHasAgreed"]); }
        set { this["UserHasAgreed"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("true")]
    public Boolean FrmOptionsOpenFromLastLocation
    {
        get { return (Boolean)(this["FrmOptionsOpenFromLastLocation"]); }
        set { this["FrmOptionsOpenFromLastLocation"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("false")]
    public Boolean FrmOptionsOptOutOfFutureChangeFontWarnings
    {
        get { return (Boolean)(this["FrmOptionsOptOutOfFutureChangeFontWarnings"]); }
        set { this["FrmOptionsOptOutOfFutureChangeFontWarnings"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("04/24/2022")]
    public String StrBuildDate
    {
        get { return (String)(this["StrBuildDate"]); }
        set { this["StrBuildDate"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("7.0")]
    public String BuildVersion
    {
        get { return (String)(this["BuildVersion"]); }
        set { this["BuildVersion"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath01
    {
        get { return (String)(this["FilePath01"]); }
        set { this["FilePath01"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath02
    {
        get { return (String)(this["FilePath02"]); }
        set { this["FilePath02"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath03
    {
        get { return (String)(this["FilePath03"]); }
        set { this["FilePath03"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath04
    {
        get { return (String)(this["FilePath04"]); }
        set { this["FilePath04"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath05
    {
        get { return (String)(this["FilePath05"]); }
        set { this["FilePath05"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath06
    {
        get { return (String)(this["FilePath06"]); }
        set { this["FilePath06"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath07
    {
        get { return (String)(this["FilePath07"]); }
        set { this["FilePath07"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath08
    {
        get { return (String)(this["FilePath08"]); }
        set { this["FilePath08"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath09
    {
        get { return (String)(this["FilePath09"]); }
        set { this["FilePath09"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath10
    {
        get { return (String)(this["FilePath10"]); }
        set { this["FilePath10"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath11
    {
        get { return (String)(this["FilePath11"]); }
        set { this["FilePath11"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath12
    {
        get { return (String)(this["FilePath12"]); }
        set { this["FilePath12"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath13
    {
        get { return (String)(this["FilePath13"]); }
        set { this["FilePath13"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath14
    {
        get { return (String)(this["FilePath14"]); }
        set { this["FilePath14"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath15
    {
        get { return (String)(this["FilePath15"]); }
        set { this["FilePath15"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath16
    {
        get { return (String)(this["FilePath16"]); }
        set { this["FilePath16"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath17
    {
        get { return (String)(this["FilePath17"]); }
        set { this["FilePath17"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath18
    {
        get { return (String)(this["FilePath18"]); }
        set { this["FilePath18"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath19
    {
        get { return (String)(this["FilePath19"]); }
        set { this["FilePath19"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath20
    {
        get { return (String)(this["FilePath20"]); }
        set { this["FilePath20"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("")]
    public String FilePath21
    {
        get { return (String)(this["FilePath21"]); }
        set { this["FilePath21"] = value; }
    }

    // Text to speech setting
    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("-4")]
    public int TTS_Speed
    {
        get { return (int)(this["TTS_Speed"]); }
        set { this["TTS_Speed"] = value; }
    }

    // Search time limit for FrmColor
    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("20")]
    public int searchTimeLimit
    {
        get { return (int)(this["searchTimeLimit"]); }
        set { this["searchTimeLimit"] = value; }
    }

    // Font for RTBMain
    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("Times New Roman")]
    public string RTBMainFontName
    {
        get { return (string)(this["RTBMainFontName"]); }
        set { this["RTBMainFontName"] = value; }
    }

    // Font Size for RTBMain
    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("22")]
    public int RTBMainFontSize
    {
        get { return (int)(this["RTBMainFontSize"]); }
        set { this["RTBMainFontSize"] = value; }
    }

    // Font for FrmQuiz's RTBQuestion and RTBAnswer
    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("Times New Roman")]
    public string FrmQuizFontName
    {
        get { return (string)(this["FrmQuizFontName"]); }
        set { this["FrmQuizFontName"] = value; }
    }

    // Font Size for FrmQuiz's RTBQuestion and RTBAnswer
    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("22")]
    public int FrmQuizFontSize
    {
        get { return (int)(this["FrmQuizFontSize"]); }
        set { this["FrmQuizFontSize"] = Convert.ToInt32(value); }
    }

    // For preserving Combo Box cmbLanguage setting in FrmOCR
    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("English")]
    public string cmbLanguageForFrmOCR
    {
        get { return (string)(this["cmbLanguageForFrmOCR"]); }
        set { this["cmbLanguageForFrmOCR"] = value; }
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
    public Boolean chkClearQuizTitle
    {
        get { return (Boolean)(this["chkClearQuizTitle"]); }
        set { this["chkClearQuizTitle"] = value; }
    }

    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("true")]
    public Boolean chkShuffle
    {
        get { return (Boolean)(this["chkShuffle"]); }
        set { this["chkShuffle"] = value; }
    }

    // lastFolderPathToOCRImage
    [UserScopedSettingAttribute()]
    [DefaultSettingValueAttribute("English")]
    public string lastFolderPathToOCRImage
    {
        get { return (string)(this["lastFolderPathToOCRImage"]); }
        set { this["lastFolderPathToOCRImage"] = value; }
    }

}

// //public static Font font = new Font("Times New Roman", 18, FontStyle.Regular);