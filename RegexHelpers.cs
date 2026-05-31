using System.Text.RegularExpressions;

namespace Tachufind
{
    public static partial class RegexHelpers
    { 
        // HTML REGEXES
        [GeneratedRegex(@"( )+")]
        public static partial Regex MultipleSpacesRegex();

        [GeneratedRegex(@"<( )*head([^>])*>", RegexOptions.IgnoreCase)]
        public static partial Regex HeadTagOpenRegex();

        [GeneratedRegex(@"(<( )*(/)( )*head( )*>)", RegexOptions.IgnoreCase)]
        public static partial Regex HeadTagCloseRegex();

        [GeneratedRegex(@"(<head>).*(</head>)", RegexOptions.IgnoreCase)]
        public static partial Regex HeadContentRegex();

        [GeneratedRegex(@"<( )*script([^>])*>", RegexOptions.IgnoreCase)]
        public static partial Regex ScriptTagOpenRegex();

        [GeneratedRegex(@"(<( )*(/)( )*script( )*>)", RegexOptions.IgnoreCase)]
        public static partial Regex ScriptTagCloseRegex();

        [GeneratedRegex(@"(<script>).*(</script>)", RegexOptions.IgnoreCase)]
        public static partial Regex ScriptContentRegex();

        [GeneratedRegex(@"<( )*style([^>])*>", RegexOptions.IgnoreCase)]
        public static partial Regex StyleTagOpenRegex();

        [GeneratedRegex(@"(<( )*(/)( )*style( )*>)", RegexOptions.IgnoreCase)]
        public static partial Regex StyleTagCloseRegex();

        [GeneratedRegex(@"(<style>).*(</style>)", RegexOptions.IgnoreCase)]
        public static partial Regex StyleContentRegex();

        [GeneratedRegex(@"<( )*td([^>])*>", RegexOptions.IgnoreCase)]
        public static partial Regex TdTagOpenRegex();

        [GeneratedRegex(@"<( )*br( )*>", RegexOptions.IgnoreCase)]
        public static partial Regex BrTagRegex();

        [GeneratedRegex(@"<( )*li( )*>", RegexOptions.IgnoreCase)]
        public static partial Regex LiTagRegex();

        [GeneratedRegex(@"<( )*div([^>])*>", RegexOptions.IgnoreCase)]
        public static partial Regex DivTagOpenRegex();

        [GeneratedRegex(@"<( )*tr([^>])*>", RegexOptions.IgnoreCase)]
        public static partial Regex TrTagOpenRegex();

        [GeneratedRegex(@"<( )*p([^>])*>", RegexOptions.IgnoreCase)]
        public static partial Regex PTagOpenRegex();

        [GeneratedRegex(@"<[^>]*>", RegexOptions.IgnoreCase)]
        public static partial Regex AllTagsRegex();

        [GeneratedRegex(@"&nbsp;", RegexOptions.IgnoreCase)]
        public static partial Regex NbspRegex();

        [GeneratedRegex(@"&bull;", RegexOptions.IgnoreCase)]
        public static partial Regex BullRegex();

        [GeneratedRegex(@"&lsaquo;", RegexOptions.IgnoreCase)]
        public static partial Regex LsaquoRegex();

        [GeneratedRegex(@"&rsaquo;", RegexOptions.IgnoreCase)]
        public static partial Regex RsaquoRegex();

        [GeneratedRegex(@"&trade;", RegexOptions.IgnoreCase)]
        public static partial Regex TradeRegex();

        [GeneratedRegex(@"&frasl;", RegexOptions.IgnoreCase)]
        public static partial Regex FraslRegex();

        [GeneratedRegex(@"&copy;", RegexOptions.IgnoreCase)]
        public static partial Regex CopyRegex();

        [GeneratedRegex(@"&reg;", RegexOptions.IgnoreCase)]
        public static partial Regex RegRegex();

        [GeneratedRegex(@"&(.{2,6});", RegexOptions.IgnoreCase)]
        public static partial Regex SpecialCharsRegex();

        [GeneratedRegex(@"(\r)( )+(\r)", RegexOptions.IgnoreCase)]
        public static partial Regex RedundantLineBreaksRegex();

        [GeneratedRegex(@"(\t)( )+(\t)", RegexOptions.IgnoreCase)]
        public static partial Regex RedundantTabsRegex();

        [GeneratedRegex(@"(\t)( )+(\r)", RegexOptions.IgnoreCase)]
        public static partial Regex TabsAndLineBreaksRegex();

        [GeneratedRegex(@"(\r)( )+(\t)", RegexOptions.IgnoreCase)]
        public static partial Regex LineBreaksAndTabsRegex();

        [GeneratedRegex(@"(\r)(\t)+(\r)", RegexOptions.IgnoreCase)]
        public static partial Regex MultipleTabsBetweenLineBreaksRegex();

        [GeneratedRegex(@"(\r)(\t)+", RegexOptions.IgnoreCase)]
        public static partial Regex TabsAfterLineBreakRegex();
        // HTML REGEXES


        [GeneratedRegex(@"^§\d+$", RegexOptions.IgnoreCase)]
        public static partial Regex GetSectionRegex();

        [GeneratedRegex(@"\p{L}+", RegexOptions.IgnoreCase)]
        public static partial Regex GetWordRegex();

        [GeneratedRegex(@"\r\n?|\n", RegexOptions.IgnoreCase)]
        public static partial Regex RemoveNewLineCharsRegex();

        [GeneratedRegex(@"\\fs\d+", RegexOptions.IgnoreCase)] 
        public static partial Regex GetFontSizeRegex();


        [GeneratedRegex(@"¦¦¦", RegexOptions.IgnoreCase)]
        public static partial Regex GetBoundaryMarkerRegex();


        // "\r\n?|\n", "
    }
}
