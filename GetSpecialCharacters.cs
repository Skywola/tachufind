using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;


public static class GetSpecialCharacters
{
	#region "Modifiers"
	// input to this fn is getAndSelectLetterBehindCursor()
	public static string ChangeVowelToGrave(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "a":
				letter = "à";
				// ÀÈÌÒÙàèìòù
				break;
			case "à":
				letter = "à";
				break;
			case "ä":
				letter = "à";
				break;
			case "ă":
				letter = "à";
				break;
			case "á":
				letter = "à";
				break;
			case "â":
				letter = "à";
				break;
			case "ā":
				letter = "à";

				break;
			case "A":
				letter = "À";
				break;
			case "À":
				letter = "À";
				break;
			case "Ä":
				letter = "À";
				break;
			case "Ă":
				letter = "À";
				break;
			case "Á":
				letter = "À";
				break;
			case "Â":
				letter = "À";
				break;
			case "Ā":
				letter = "À";
				break;

			case "e":
				letter = "è";
				break;
			case "è":
				letter = "è";
				break;
			case "ë":
				letter = "è";
				break;
			case "ĕ":
				letter = "è";
				break;
			case "é":
				letter = "è";
				break;
			case "ê":
				letter = "è";
				break;
			case "ē":
				letter = "è";
				break;
			case "E":
				letter = "È";
				break;
			case "È":
				letter = "È";
				break;
			case "Ë":
				letter = "È";
				break;
			case "Ĕ":
				letter = "È";
				break;
			case "É":
				letter = "È";
				break;
			case "Ê":
				letter = "È";
				break;
			case "Ē":
				letter = "È";
				break;

			case "i":
				letter = "ì";
				break;
			case "ì":
				letter = "ì";
				break;
			case "ï":
				letter = "ì";
				break;
			case "ĭ":
				letter = "ì";
				break;
			case "í":
				letter = "ì";
				break;
			case "î":
				letter = "ì";
				break;
			case "ī":
				letter = "ì";

				break;
			case "I":
				letter = "Ì";
				break;
			case "Ì":
				letter = "Ì";
				break;
			case "Ï":
				letter = "Ì";
				break;
			case "Ĭ":
				letter = "Ì";
				break;
			case "Í":
				letter = "Ì";
				break;
			case "Î":
				letter = "Ì";
				break;
			case "Ī":
				letter = "Ì";
				break;

			case "o":
				letter = "ò";
				break;
			case "ò":
				letter = "ò";
				break;
			case "ö":
				letter = "ò";
				break;
			case "ŏ":
				letter = "ò";
				break;
			case "ó":
				letter = "ò";
				break;
			case "ô":
				letter = "ò";
				break;
			case "ō":
				letter = "ò";
				break;
			case "O":
				letter = "Ò";
				break;
			case "Ò":
				letter = "Ò";
				break;
			case "Ö":
				letter = "Ò";
				break;
			case "Ŏ":
				letter = "Ò";
				break;
			case "Ó":
				letter = "Ò";
				break;
			case "Ô":
				letter = "Ò";
				break;
			case "Ō":
				letter = "Ò";
				break;

			case "u":
				letter = "ù";
				break;
			case "ù":
				letter = "ù";
				break;
			case "ü":
				letter = "ù";
				break;
			case "ŭ":
				letter = "ù";
				break;
			case "ú":
				letter = "ù";
				break;
			case "û":
				letter = "ù";
				break;
			case "ū":
				letter = "ù";
				break;
			case "U":
				letter = "Ù";
				break;
			case "Ù":
				letter = "Ù";
				break;
			case "Ü":
				letter = "Ù";
				break;
			case "Ŭ":
				letter = "Ù";
				break;
			case "Ú":
				letter = "Ù";
				break;
			case "Û":
				letter = "Ù";
				break;
			case "Ū":
				letter = "Ù";
				break;

			// GREEK
			case "α":
				letter = "ὰ";
				break;
			case "ὰ":
				letter = "ὰ";
				break;
			case "ἀ":
				letter = "ἂ";
				break;
			case "ἁ":
				letter = "ἃ";
				break;
			case "ἄ":
				letter = "ἂ";
				break;
			case "ἅ":
				letter = "ἃ";
				break;
			case "ά":
				letter = "ὰ";
				break;
			case "ᾰ":
				letter = "ὰ";
				break;
			case "ᾱ":
				letter = "ὰ";
				break;
			case "ᾶ":
				letter = "ὰ";
				break;
			case "ᾳ":
				letter = "ᾲ";
				break;
			case "ᾀ":
				letter = "ᾂ";
				break;
			case "ᾁ":
				letter = "ᾃ";
				break;
			case "ᾅ":
				letter = "ᾃ";
				break;
			case "ᾴ":
				letter = "ᾲ";

				break;
			case "Α":
				letter = "Ὰ";
				break;
			case "Ἀ":
				letter = "Ἂ";
				break;
			case "Ἁ":
				letter = "Ἃ";
				break;
			case "Ἄ":
				letter = "Ἂ";
				break;
			case "Ἅ":
				letter = "Ἃ";
				break;
			case "Ά":
				letter = "Ὰ";
				break;
			case "Ᾰ":
				letter = "Ὰ";
				break;
			case "Ᾱ":
				letter = "Ὰ";
				break;

			case "ᾉ":
				letter = "ᾋ";
				break;
			case "ᾈ":
				letter = "ᾊ";
				break;
			case "ᾍ":
				letter = "ᾋ";
				break;
			case "ᾌ":
				letter = "ᾊ";
				break;

			case "ε":
				letter = "ὲ";
				break;
			case "έ":
				letter = "ὲ";
				break;
			case "ἐ":
				letter = "ἒ";
				break;
			case "ἑ":
				letter = "ἓ";
				break;
			case "ἔ":
				letter = "ἒ";
				break;
			case "ἕ":
				letter = "ἓ";
				break;
			case "Ε":
				letter = "Ὲ";
				break;
			case "Ἐ":
				letter = "Ἒ";
				break;
			case "Ἑ":
				letter = "Ἓ";
				break;
			case "Ἔ":
				letter = "Ἒ";
				break;
			case "Ἕ":
				letter = "Ἓ";
				break;
			case "Έ":
				letter = "Ὲ";
				break;

			case "η":
				letter = "ὴ";
				break;
			case "ὴ":
				letter = "ὴ";
				break;
			case "ἠ":
				letter = "ἢ";
				break;
			case "ἡ":
				letter = "ἣ";
				break;
			case "ἤ":
				letter = "ἢ";
				break;
			case "ἥ":
				letter = "ἣ";
				break;

			case "ῃ":
				letter = "ῂ";
				break;
			case "ῂ":
				letter = "ῂ";
				break;
			case "ᾐ":
				letter = "ᾒ";
				break;
			case "ᾑ":
				letter = "ᾓ";
				break;
			case "ᾔ":
				letter = "ᾒ";
				break;
			case "ᾕ":
				letter = "ᾓ";
				break;
			case "ή":
				letter = "ὴ";
				break;
			case "ῆ":
				letter = "ὴ";
				break;

			case "Η":
				letter = "Ὴ";
				break;
			case "Ὴ":
				letter = "Ἢ";
				break;
			case "Ἡ":
				letter = "Ἣ";
				break;
			case "ᾘ":
				letter = "ᾚ";
				break;
			case "ᾙ":
				letter = "ᾚ";
				break;
			case "Ἤ":
				letter = "ᾚ";
				break;
			case "Ἥ":
				letter = "ᾚ";
				break;

			case "ι":
				letter = "ὶ";
				break;
			case "ὶ":
				letter = "ὶ";
				break;
			case "ῖ":
				letter = "ὶ";
				break;
			case "ί":
				letter = "ὶ";
				break;
			case "ἰ":
				letter = "ἲ";
				break;
			case "ἱ":
				letter = "ἳ";
				break;
			case "ἴ":
				letter = "ἲ";
				break;
			case "ἵ":
				letter = "ἳ";
				break;
			case "ῐ":
				letter = "ὶ";
				break;
			case "ῑ":
				letter = "ὶ";
				break;
			case "ἲ":
				letter = "ἲ";
				break;
			case "ἳ":
				letter = "ἳ";
				break;
			case "ϊ":
				letter = "ῒ";
				break;
			case "ΐ":
				letter = "ῒ";   
				break;
			case "ί":
				letter = "ῒ";
				break;

			case "Ι":
				letter = "Ὶ";
				break;
			case "Ὶ":
				letter = "Ὶ";
				break;
			case "Ἰ":
				letter = "Ἲ";
				break;
			case "Ἱ":
				letter = "Ἳ";
				break;
			case "Ἴ":
				letter = "Ἲ";
				break;
			case "Ἵ":
				letter = "Ἳ";
				break;
			case "Ῐ":
				letter = "Ὶ";
				break;
			case "Ῑ":
				letter = "Ὶ";
				break;
			case "Ἲ":
				letter = "Ἲ";
				break;
			case "Ἳ":
				letter = "Ἳ";
				break;

			case "ο":
				letter = "ὸ";
				break;
			case "ό":
				letter = "ὸ";
				break;
			case "ὀ":
				letter = "ὂ";
				break;
			case "ὁ":
				letter = "ὃ";
				break;
			case "ὄ":
				letter = "ὂ";
				break;
			case "ὅ":
				letter = "ὃ";
				break;

			case "Ο":
				letter = "Ὸ";
				break;
			case "Ό":
				letter = "Ὸ";
				break;
			case "Ὀ":
				letter = "Ὂ";
				break;
			case "Ὁ":
				letter = "Ὃ";
				break;
			case "Ὄ":
				letter = "Ὂ";
				break;
			case "Ὅ":
				letter = "Ὃ";
				break;

			case "υ":
				letter = "ὺ";
				break;
			case "ύ":
				letter = "ὺ";
				break;
			case "ῦ":
				letter = "ὺ";
				break;
			case "ὐ":
				letter = "ὒ";
				break;
			case "ὑ":
				letter = "ὓ";
				break;
			case "ὔ":
				letter = "ὒ";
				break;
			case "ὕ":
				letter = "ὓ";
				break;
			case "ῠ":
				letter = "ὺ";
				break;
			case "ῡ":
				letter = "ὺ";    
				break;
			case "ϋ":
				letter = "ῢ";
				break;
			case "ΰ":
				letter = "ῢ";
				break;

			case "Υ":
				letter = "Ὺ";
				break;
			case "Ύ":
				letter = "Ὺ";
				break;
			case "Ὑ":
				letter = "Ὓ";
				break;
			case "Ῠ":
				letter = "Ὺ";
				break;
			case "Ῡ":
				letter = "Ὺ";
				break;
			case "Ὕ":
				letter = "Ὓ";
				break;

			case "ω":
				letter = "ὼ";
				break;
			case "ὼ":
				letter = "ὼ";
				break;
			case "ώ":
				letter = "ὼ";
				break;
			case "ὠ":
				letter = "ὢ";
				break;
			case "ὡ":
				letter = "ὣ";
				break;
			case "ᾤ":
				letter = "ὢ";
				break;
			case "ᾥ":
				letter = "ὣ";
				break;
			case "ᾠ":
				letter = "ὢ";
				break;
			case "ᾡ":
				letter = "ᾣ";
				break;
			case "ῳ":
				letter = "ῲ";
				break;
			case "Ω":
				letter = "Ὼ";
				break;
			case "Ὠ":
				letter = "Ὢ";
				break;
			case "Ὡ":
				letter = "Ὣ";
				break;
			case "ᾨ":
				letter = "ᾪ";
				break;
			case "ᾩ":
				letter = "ᾫ";
				break;
			case "Ὤ":
				letter = "ᾪ";
				break;
			case "Ὥ":
				letter = "ᾫ";
				break;
			// END GREEK


			// RUSSIAN
			// ÀÈÌÒÙàèìòù
			case "а":
				// This is not redundant, it is the Russian a!
				letter = "à";
				break;
			case "А":
				letter = "À";
				break;
			case "е":
				letter = "è";
				break;
			case "ё":
				// This is not redundant, it is the Russian ё!
				letter = "è";
				break;
			case "Е":
				letter = "È";
				break;
			case "Ё":
				// This is not redundant, it is the Russian Ё!
				letter = "È";
				break;
			case "о":
				letter = "ò";
				break;
			case "О":
				letter = "Ò";
				break;
			// END RUSSIAN


			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}

	// input to this fn is getAndSelectLetterBehindCursor()
	public static string ChangeVowelToAcute(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "a":
				letter = "á";
				// ÁÉÍÓÚáéíóú
				break;
			case "á":
				letter = "á";
				break;
			case "à":
				letter = "á";
				break;
			case "ä":
				letter = "á";
				break;
			case "ă":
				letter = "á";
				break;
			case "â":
				letter = "á";
				break;
			case "ā":
				letter = "á";
				break;
			case "A":
				letter = "Á";
				break;
			case "Á":
				letter = "Á";
				break;
			case "À":
				letter = "Á";
				break;
			case "Ä":
				letter = "Á";
				break;
			case "Ă":
				letter = "Á";
				break;
			case "Â":
				letter = "Á";
				break;
			case "Ā":
				letter = "Á";
				break;
			case "e":
				letter = "é";
				break;
			case "é":
				letter = "é";
				break;
			case "è":
				letter = "é";
				break;
			case "ë":
				letter = "é";
				break;
			case "ĕ":
				letter = "é";
				break;
			case "ê":
				letter = "é";
				break;
			case "ē":
				letter = "é";
				break;
			case "E":
				letter = "É";
				break;
			case "É":
				letter = "É";
				break;
			case "È":
				letter = "É";
				break;
			case "Ë":
				letter = "É";
				break;
			case "Ĕ":
				letter = "É";
				break;
			case "Ê":
				letter = "É";
				break;
			case "Ē":
				letter = "É";

				break;
			case "i":
				letter = "í";
				break;
			case "í":
				letter = "í";
				break;
			case "ì":
				letter = "í";
				break;
			case "ï":
				letter = "í";
				break;
			case "ĭ":
				letter = "í";
				break;
			case "î":
				letter = "í";
				break;
			case "ī":
				letter = "í";
				break;

			case "I":
				letter = "Í";
				break;
			case "Í":
				letter = "Í";
				break;
			case "Ì":
				letter = "Í";
				break;
			case "Ï":
				letter = "Í";
				break;
			case "Ĭ":
				letter = "Í";
				break;
			case "Î":
				letter = "Í";
				break;
			case "Ī":
				letter = "Í";
				break;
			case "o":
				letter = "ó";
				break;
			case "ó":
				letter = "ó";
				break;
			case "ò":
				letter = "ó";
				break;
			case "ö":
				letter = "ó";
				break;
			case "ŏ":
				letter = "ó";
				break;
			case "ô":
				letter = "ó";
				break;
			case "ō":
				letter = "ó";
				break;
			case "O":
				letter = "Ó";
				break;
			case "Ó":
				letter = "Ó";
				break;
			case "Ò":
				letter = "Ó";
				break;
			case "Ö":
				letter = "Ó";
				break;
			case "Ŏ":
				letter = "Ó";
				break;
			case "Ô":
				letter = "Ó";
				break;
			case "Ō":
				letter = "Ó";
				break;
			case "u":
				letter = "ú";
				break;
			case "ú":
				letter = "ú";
				break;
			case "ù":
				letter = "ú";
				break;
			case "ü":
				letter = "ú";
				break;
			case "ŭ":
				letter = "ú";
				break;
			case "û":
				letter = "ú";
				break;
			case "ū":
				letter = "ú";
				break;
            case "ϋ":
                letter = "ΰ";
                break;
            case "ΰ":
                letter = "ΰ";
                break;

            case "U":
				letter = "Ú";
				break;
			case "Ú":
				letter = "Ú";
				break;
			case "Ù":
				letter = "Ú";
				break;
			case "Ü":
				letter = "Ú";
				break;
			case "Ŭ":
				letter = "Ú";
				break;
			case "Û":
				letter = "Ú";
				break;
			case "Ū":
				letter = "Ú";
				break;

			// GREEK
			case "α":
				letter = "ά";
				break;
			case "ά":
				letter = "ά";
				break;
			case "ὰ":
				letter = "ά";
				break;
			case "ἁ":
				letter = "ἅ";
				break;
			case "ἀ":
				letter = "ἄ";
				break;
			case "ἂ":
				letter = "ἅ";
				break;
			case "ἃ":
				letter = "ἄ";
				break;
			case "ᾰ":
				letter = "ά";
				break;
			case "ᾱ":
				letter = "ά";
				break;
			case "ᾶ":
				letter = "ά";
				break;
			case "ᾳ":
				letter = "ᾴ";
				break;
			case "ᾀ":
				letter = "ᾄ";
				break;
			case "ᾁ":
				letter = "ᾅ";
				break;
			case "ᾂ":
				letter = "ᾄ";
				break;
			case "ᾃ":
				letter = "ᾅ";
				break;
			case "ᾲ":
				letter = "ᾴ";
				break;
			case "ᾷ":
				letter = "ᾴ";
				break;

			case "Α":
				letter = "Ά";
				break;
			case "Ά":
				letter = "Ά";
				break;
			case "Ὰ":
				letter = "Ά";
				break;
			case "Ἁ":
				letter = "Ἅ";
				break;
			case "Ἀ":
				letter = "Ἄ";
				break;
			case "Ἂ":
				letter = "Ἄ";
				break;
			case "Ἃ":
				letter = "Ἅ";
				break;
			case "Ᾰ":
				letter = "Ά";
				break;
			case "Ᾱ":
				letter = "Ά";
				break;
			case "ᾈ":
				letter = "ᾌ";
				break;
			case "ᾉ":
				letter = "ᾍ";
				break;
			case "ᾊ":
				letter = "ᾌ";
				break;
			case "ᾋ":
				letter = "ᾍ";
				break;

			case "ε":
				letter = "έ";
				break;
			case "ὲ":
				letter = "έ";
				break;
			case "ἐ":
				letter = "ἔ";
				break;
			case "ἑ":
				letter = "ἕ";
				break;
			case "ἒ":
				letter = "ἔ";
				break;
			case "ἓ":
				letter = "ἕ";
				break;

			case "Ε":
				letter = "Έ";
				break;
			case "Ἐ":
				letter = "Ἔ";
				break;
			case "Ἑ":
				letter = "Ἕ";
				break;
			case "Ἒ":
				letter = "Ἔ";
				break;
			case "Ἓ":
				letter = "Ἕ";
				break;

			case "η":
				letter = "ή";
				break;
			case "ἠ":
				letter = "ἤ";
				break;
			case "ἡ":
				letter = "ἥ";
				break;
			case "ἢ":
				letter = "ἤ";
				break;
			case "ἣ":
				letter = "ἥ";
				break;
			case "ῆ":
				letter = "ή";
				break;
			case "ὴ":
				letter = "ή";
				break;

			case "ῃ":
				letter = "ῄ";
				break;
			case "ῄ":
				letter = "ῄ";
				break;
			case "ᾐ":
				letter = "ᾔ";
				break;
			case "ᾑ":
				letter = "ᾕ";
				break;
			case "ᾒ":
				letter = "ᾔ";
				break;
			case "ᾓ":
				letter = "ᾕ";
				break;
			case "ῂ":
				letter = "ῄ";
				break;
			case "ῇ":
				letter = "ῄ";
				break;

			case "Η":
				letter = "Ή";
				break;
			case "Ὴ":
				letter = "Ἤ";
				break;
			case "Ἡ":
				letter = "Ἥ";
				break;
			case "Ἢ":
				letter = "Ἤ";
				break;
			case "Ἣ":
				letter = "Ἥ";
				break;
			case "ᾘ":
				letter = "ᾜ";
				break;
			case "ᾙ":
				letter = "ᾝ";
				break;

			case "ι":
				letter = "ί";
				break;
			case "ί":
				letter = "ί";
				break;
			case "ὶ":
				letter = "ί";
				break;
			case "ἰ":
				letter = "ἴ";
				break;
			case "ἱ":
				letter = "ἵ";
				break;
			case "ἲ":
				letter = "ἴ";
				break;
			case "ἳ":
				letter = "ἵ";
				break;
			case "ῐ":
				letter = "ί";
				break;
			case "ῑ":
				letter = "ί";
				break;
			case "ῖ":
				letter = "ί";    
				break;
			case "ΐ":
				letter = "ΐ";
				break;
			case "ϊ":
				letter = "ΐ";    
				break;
			case "ί":
				letter = "ΐ";
				break;
			case "ῒ":
				letter = "ΐ";
				break;

			case "Ι":
				letter = "Ί";
				break;
			case "Ἰ":
				letter = "Ἴ";
				break;
			case "Ἱ":
				letter = "Ἵ";
				break;
			case "Ἲ":
				letter = "Ἴ";
				break;
			case "Ἳ":
				letter = "Ἵ";
				break;
			case "Ῐ":
				letter = "Ί";
				break;
			case "Ῑ":
				letter = "Ί";
				break;

			case "ο":
				letter = "ό";
				break;
			case "ὸ":
				letter = "ό";
				break;
			case "ὀ":
				letter = "ὄ";
				break;
			case "ὁ":
				letter = "ὅ";
				break;
			case "ὂ":
				letter = "ὄ";
				break;
			case "ὃ":
				letter = "ὅ";
				break;
			case "Ο":
				letter = "Ό";
				break;
			case "Ὸ":
				letter = "Ό";
				break;
			case "Ὀ":
				letter = "Ὄ";
				break;
			case "Ὁ":
				letter = "Ὅ";
				break;
			case "Ὂ":
				letter = "Ὄ";
				break;
			case "Ὃ":
				letter = "Ὅ";
				break;

			case "υ":
				letter = "ύ";
				break;
			case "ὺ":
				letter = "ύ";
				break;
			case "ῦ":
				letter = "ύ";
				break;
			case "ὐ":
				letter = "ὔ";
				break;
			case "ὑ":
				letter = "ὕ";
				break;
			case "ὒ":
				letter = "ὔ";
				break;
			case "ὓ":
				letter = "ὕ";
				break;
			case "ῠ":
				letter = "ύ";
				break;
			case "ῡ":
				letter = "ύ";
				break;

			case "Υ":
				letter = "Ύ";
				break;
			case "Ὺ":
				letter = "Ύ";
				break;
			case "Ὑ":
				letter = "Ὕ";
				break;
			case "Ῠ":
				letter = "Ύ";
				break;
			case "Ῡ":
				letter = "Ύ";
				break;

			case "ω":
				letter = "ώ";
				break;
			case "ώ":
				letter = "ώ";
				break;
			case "ὠ":
				letter = "ὤ";
				break;
			case "ὡ":
				letter = "ὥ";
				break;
			case "ὢ":
				letter = "ὤ";
				break;
			case "ὣ":
				letter = "ὥ";
				break;
			case "ὼ":
				letter = "ώ";
				break;
			case "ῶ":
				letter = "ώ";
				break;

			case "ῳ":
				letter = "ῴ";
				break;
			case "ᾠ":
				letter = "ᾤ";
				break;
			case "ᾡ":
				letter = "ᾥ";
				break;
			case "ᾢ":
				letter = "ᾤ";
				break;
			case "ᾣ":
				letter = "ᾥ";
				break;
			case "Ω":
				letter = "Ώ";
				break;
			case "Ὠ":
				letter = "Ὤ";
				break;
			case "Ὡ":
				letter = "Ὥ";
				break;
			case "ᾪ":
				letter = "Ὤ";
				break;
			case "ᾫ":
				letter = "Ὥ";
				break;
			case "Ὼ":
				letter = "Ώ";

				break;
			case "ᾨ":
				letter = "ᾬ";
				break;
			case "ᾩ":
				letter = "ᾫ";
				break;
			// END GREEK

			// RUSSIAN
			// ÁÉÍÓÚáéíóú
			case "а":
				// This is not redundant, it is the Russian a!
				letter = "á";
				break;
			case "А":
				// This is not redundant, it is the Russian a!
				letter = "Á";
				break;
			case "е":
				// This is not redundant, it is the Russian e!
				letter = "é";
				break;
			case "ё":
				// This is not redundant, it is the Russian ё!
				letter = "é";
				break;
			case "Е":
				// This is not redundant, it is the Russian E!
				letter = "É";
				break;
			case "Ё":
				// This is not redundant, it is the Russian Ё!
				letter = "É";
				break;
			case "о":
				// This is not redundant, it is the Russian E!
				letter = "ó";
				break;
			case "О":
				// This is not redundant, it is the Russian E!
				letter = "Ó";
				break;
			// END RUSSIAN

			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}

	// input to this fn is getAndSelectLetterBehindCursor()
	public static string ChangeVowelToUmlaut(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "a":
				letter = "ä";
				// ÄËÏÖÜäëïöü
				break;
			case "ä":
				letter = "ä";
				break;
			case "à":
				letter = "ä";
				break;
			case "ă":
				letter = "ä";
				break;
			case "á":
				letter = "ä";
				break;
			case "â":
				letter = "ä";
				break;
			case "ā":
				letter = "ä";
				break;
			case "A":
				letter = "Ä";
				break;
			case "Ä":
				letter = "Ä";
				break;
			case "À":
				letter = "Ä";
				break;
			case "Ă":
				letter = "Ä";
				break;
			case "Á":
				letter = "Ä";
				break;
			case "Â":
				letter = "Ä";
				break;
			case "Ā":
				letter = "Ä";
				break;
			case "e":
				letter = "ë";
				break;
			case "ë":
				letter = "ë";
				break;
			case "è":
				letter = "ë";
				break;
			case "ĕ":
				letter = "ë";
				break;
			case "é":
				letter = "ë";
				break;
			case "ê":
				letter = "ë";
				break;
			case "ē":
				letter = "ë";
				break;
			case "E":
				letter = "Ë";
				break;
			case "Ë":
				letter = "Ë";
				break;
			case "È":
				letter = "Ë";
				break;
			case "Ĕ":
				letter = "Ë";
				break;
			case "É":
				letter = "Ë";
				break;
			case "Ê":
				letter = "Ë";
				break;
			case "Ē":
				letter = "Ë";
				break;
			case "i":
				letter = "ï";
				break;
			case "ï":
				letter = "ï";
				break;
			case "ì":
				letter = "ï";
				break;
			case "ĭ":
				letter = "ï";
				break;
			case "í":
				letter = "ï";
				break;
			case "î":
				letter = "ï";
				break;
			case "ī":
				letter = "ï";
				break;
			case "I":
				letter = "Ï";
				break;
			case "Ï":
				letter = "Ï";
				break;
			case "Ì":
				letter = "Ï";
				break;
			case "Ĭ":
				letter = "Ï";
				break;
			case "Í":
				letter = "Ï";
				break;
			case "Î":
				letter = "Ï";
				break;
			case "Ī":
				letter = "Ï";

				break;
			case "o":
				letter = "ö";
				break;
			case "ö":
				letter = "ö";
				break;
			case "ò":
				letter = "ö";
				break;
			case "ŏ":
				letter = "ö";
				break;
			case "ó":
				letter = "ö";
				break;
			case "ô":
				letter = "ö";
				break;
			case "ō":
				letter = "ö";
				break;
			case "O":
				letter = "Ö";
				break;
			case "Ö":
				letter = "Ö";
				break;
			case "Ò":
				letter = "Ö";
				break;
			case "Ŏ":
				letter = "Ö";
				break;
			case "Ó":
				letter = "Ö";
				break;
			case "Ô":
				letter = "Ö";
				break;
			case "Ō":
				letter = "Ö";
				break;
			case "u":
				letter = "ü";
				break;
			case "ü":
				letter = "ü";
				break;
			case "ù":
				letter = "ü";
				break;
			case "ŭ":
				letter = "ü";
				break;
			case "ú":
				letter = "ü";
				break;
			case "û":
				letter = "ü";
				break;
			case "ū":
				letter = "ü";
				break;
			case "U":
				letter = "Ü";
				break;
			case "Ü":
				letter = "Ü";
				break;
			case "Ù":
				letter = "Ü";
				break;
			case "Ŭ":
				letter = "Ü";
				break;
			case "Ú":
				letter = "Ü";
				break;
			case "Û":
				letter = "Ü";
				break;
			case "Ū":
				letter = "Ü";
				break;

			// GRΕΕΚ
			case "ι":
				letter = "ϊ";       
				break;
			case "ί":
				letter = "ΐ";
				break;
			case "ὶ":
				letter = "ῒ";
				break;
            case "Ι":
				letter = "Ϊ";
				break;
			case "υ":
				letter = "ϋ";
				break;
            case "ύ":
                letter = "ΰ";
                break;
            case "ὺ":
                letter = "ῢ";
                break;
            case "Υ":
				letter = "Ϋ";
				break;

			// RUSSIAN
			// ÄËÏÖÜäëïöü
			case "а":
				// This is not redundant, it is the Russian a!
				letter = "ä";
				break;
			case "А":
				// This is not redundant, it is the Russian a!
				letter = "Ä";
				break;
			case "е":
				// This is not redundant, it is the Russian e!
				letter = "ë";
				break;
			case "ё":
				// This is not redundant, it is the Russian ё!
				letter = "ë";
				break;
			case "Е":
				// This is not redundant, it is the Russian E!
				letter = "Ë";
				break;
			case "Ё":
				// This is not redundant, it is the Russian Ё!
				letter = "Ë";
				break;
			case "о":
				// This is not redundant, it is the Russian E!
				letter = "ö";
				break;
			case "О":
				// This is not redundant, it is the Russian E!
				letter = "Ö";
				break;
			// END RUSSIAN


			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}

	// input to this fn is getAndSelectLetterBehindCursor()
	public static string ChangeVowelToCircumflex(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "a":
				letter = "â";
				// ÂÊÎÔÛâêîĵôû
				break;
			case "â":
				letter = "â";
				break;
			case "à":
				letter = "â";
				break;
			case "ä":
				letter = "â";
				break;
			case "ă":
				letter = "â";
				break;
			case "á":
				letter = "â";
				break;
			case "ā":
				letter = "â";

				break;
			case "A":
				letter = "Â";
				break;
			case "Â":
				letter = "Â";
				break;
			case "À":
				letter = "Â";
				break;
			case "Ä":
				letter = "Â";
				break;
			case "Ă":
				letter = "Â";
				break;
			case "Á":
				letter = "Â";
				break;
			case "Ā":
				letter = "Â";

				break;
			case "e":
				letter = "ê";
				break;
			case "ê":
				letter = "ê";
				break;
			case "è":
				letter = "ê";
				break;
			case "ë":
				letter = "ê";
				break;
			case "ĕ":
				letter = "ê";
				break;
			case "é":
				letter = "ê";
				break;
			case "ē":
				letter = "ê";

				break;
			case "E":
				letter = "Ê";
				break;
			case "Ê":
				letter = "Ê";
				break;
			case "È":
				letter = "Ê";
				break;
			case "Ë":
				letter = "Ê";
				break;
			case "Ĕ":
				letter = "Ê";
				break;
			case "É":
				letter = "Ê";
				break;
			case "Ē":
				letter = "Ê";

				break;
			case "i":
				letter = "î";
				break;
			case "î":
				letter = "î";
				break;
			case "ì":
				letter = "î";
				break;
			case "ï":
				letter = "î";
				break;
			case "ĭ":
				letter = "î";
				break;
			case "í":
				letter = "î";
				break;
			case "ī":
				letter = "î";

				break;
			case "I":
				letter = "Î";
				break;
			case "Î":
				letter = "Î";
				break;
			case "Ì":
				letter = "Î";
				break;
			case "Ï":
				letter = "Î";
				break;
			case "Ĭ":
				letter = "Î";
				break;
			case "Í":
				letter = "Î";
				break;
			case "Ī":
				letter = "Î";
				break;
			case "j":
				letter = "ĵ";
				break;

			case "o":
				letter = "ô";
				break;
			case "ô":
				letter = "ô";
				break;
			case "ò":
				letter = "ô";
				break;
			case "ö":
				letter = "ô";
				break;
			case "ŏ":
				letter = "ô";
				break;
			case "ó":
				letter = "ô";
				break;
			case "ō":
				letter = "ô";

				break;
			case "O":
				letter = "Ô";
				break;
			case "Ô":
				letter = "Ô";
				break;
			case "Ò":
				letter = "Ô";
				break;
			case "Ö":
				letter = "Ô";
				break;
			case "Ŏ":
				letter = "Ô";
				break;
			case "Ó":
				letter = "Ô";
				break;
			case "Ō":
				letter = "Ô";

				break;
			case "u":
				letter = "û";
				break;
			case "û":
				letter = "û";
				break;
			case "ù":
				letter = "û";
				break;
			case "ü":
				letter = "û";
				break;
			case "ŭ":
				letter = "û";
				break;
			case "ú":
				letter = "û";
				break;
			case "ū":
				letter = "û";

				break;
			case "U":
				letter = "Û";
				break;
			case "Û":
				letter = "Û";
				break;
			case "Ù":
				letter = "Û";
				break;
			case "Ü":
				letter = "Û";
				break;
			case "Ŭ":
				letter = "Û";
				break;
			case "Ú":
				letter = "Û";
				break;
			case "Ū":
				letter = "Û";
				break;  
			case "Y":
				letter = "Ŷ";
				break;
			case "y":
				letter = "ŷ";
				break;


			// GREEK
			case "α":
				letter = "ᾶ";
				break;
			case "ᾶ":
				letter = "ᾶ";
				break;
			case "ἀ":
				letter = "ἆ";
				break;
			case "ἁ":
				letter = "ἇ";
				break;
			case "ᾳ":
				letter = "ᾷ";
				break;
			case "ᾀ":
				letter = "ᾆ";
				break;
			case "ᾁ":
				letter = "ᾇ";
				break;
			case "ᾰ":
				letter = "ᾶ";
				break;
			case "ᾱ":
				letter = "ᾶ";
				break;
			case "ὰ":
				letter = "ᾶ";
				break;
			case "ά":
				letter = "ᾶ";
				break;
			case "Ἀ":
				letter = "Ἆ";
				break;
			case "Ἁ":
				letter = "Ἇ";
				break;
			case "ᾈ":
				letter = "ᾎ";
				break;
			case "ᾉ":
				letter = "ᾏ";
				break;


			case "ι":
				letter = "ῖ";
				break;
			case "ἰ":
				letter = "ἶ";
				break;
			case "ἱ":
				letter = "ἷ";
				break;
			case "ῐ":
				letter = "ῖ";
				break;
			case "ῑ":
				letter = "ῖ";
				break;
			case "ὶ":
				letter = "ῖ";
				break;
			case "ί":
				letter = "ῖ";
				break;
			case "ϊ":
				letter = "ῗ";
				break;

			case "Ἰ":
				letter = "Ἶ";
				break;
			case "Ἱ":
				letter = "Ἷ";

				break;
			case "η":
				letter = "ῆ";
				break;
			case "ὴ":
				letter = "ῆ";
				break;
			case "ή":
				letter = "ῆ";
				break;
			case "ῆ":
				letter = "ῆ";
				break;
			case "ἠ":
				letter = "ἦ";
				break;
			case "ἡ":
				letter = "ἧ";
				break;
			case "ῃ":
				letter = "ῇ";
				break;
			case "ᾐ":
				letter = "ᾖ";
				break;
			case "ᾑ":
				letter = "ᾗ";

				break;
			case "Ἠ":
				letter = "Ἦ";
				break;
			case "Ἡ":
				letter = "Ἧ";
				break;
			case "ᾘ":
				letter = "ᾞ";
				break;
			case "ᾙ":
				letter = "ᾟ";

				break;
			case "υ":
				letter = "ῦ";  
				break;
			case "ῦ":
				letter = "ῦ";
				break;
			case "ὺ":
				letter = "ῦ";  
				break;
			case "ύ":
				letter = "ῦ"; 
				break;
			case "ὓ":
				letter = "ῦ";
				break;
			case "ὔ":
				letter = "ῦ";
				break;

			case "ὐ":
				letter = "ὖ";
				break;
			case "ὑ":
				letter = "ὗ";
				break;
			case "ῠ":
				letter = "ῦ";
				break;
			case "ῡ":
				letter = "ῦ";
				break;
			case "ϋ":
				letter = "ῧ";
				break;

			case "Ὑ":
				letter = "Ὗ";

				break;

			case "ω":
				letter = "ῶ";
				// Ω trema does not exist
				break;
			case "ῶ":
				letter = "ῶ";
				break;
			case "ὠ":
				letter = "ὦ";
				break;
			case "ὡ":
				letter = "ὧ";
				break;
			case "ὼ":
				letter = "ῶ";
				break;
			case "ώ":
				letter = "ῶ";
				break;
			case "ὤ":
				letter = "ῶ";
				break;

			case "ῳ":
				letter = "ῷ";
				break;
			case "ᾠ":
				letter = "ᾦ";
				break;
			case "ᾡ":
				letter = "ᾧ";

				break;
			case "Ὠ":
				letter = "Ὦ";
				break;
			case "Ὡ":
				letter = "Ὧ";
				break;
			case "ᾨ":
				letter = "ᾮ";
				break;
			case "ᾩ":
				letter = "ᾯ";
				break;


			// RUSSIAN
			// ÂÊÎÔÛâêîôû
			case "а":
				// This is not redundant, it is the Russian a!
				letter = "â";
				break;
			case "А":
				// This is not redundant, it is the Russian a!
				letter = "Â";
				break;
			case "е":
				// This is not redundant, it is the Russian e!
				letter = "ê";
				break;
			case "ё":
				// This is not redundant, it is the Russian ё!
				letter = "ê";
				break;
			case "Е":
				// This is not redundant, it is the Russian E!
				letter = "Ê";
				break;
			case "Ё":
				// This is not redundant, it is the Russian Ё!
				letter = "Ê";
				break;
			case "о":
				// This is not redundant, it is the Russian E!
				letter = "ô";
				break;
			case "О":
				// This is not redundant, it is the Russian E!
				letter = "Ô";
				break;
			// END RUSSIAN


			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}

	// input to this fn is getAndSelectLetterBehindCursor()
	public static string ChangeVowelToCedilla(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "c":
				letter = "ç";
				break;
			case "ç":
				letter = "ç";
				break;
			case "C":
				letter = "Ç";
				break;
			case "Ç":
				letter = "Ç";
				break;

			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}

	// input to this fn is getAndSelectLetterBehindCursor()
	public static string ChangeVowelToLong(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "a":
				letter = "ā";
				break;
			case "ā":
				letter = "ā";
				break;
			case "à":
				letter = "ā";
				break;
			case "ä":
				letter = "ā";
				break;
			case "ă":
				letter = "ā";
				break;
			case "á":
				letter = "ā";
				break;
			case "â":
				letter = "ā";
				break;

			case "A":
				letter = "Ā";
				break;
			case "Ā":
				letter = "Ā";
				break;
			case "À":
				letter = "Ā";
				break;
			case "Ä":
				letter = "Ā";
				break;
			case "Ă":
				letter = "Ā";
				break;
			case "Á":
				letter = "Ā";
				break;
			case "Â":
				letter = "Ā";
				break;

			case "e":
				letter = "ē";
				break;
			case "ē":
				letter = "ē";
				break;
			case "è":
				letter = "ē";
				break;
			case "ë":
				letter = "ē";
				break;
			case "ĕ":
				letter = "ē";
				break;
			case "é":
				letter = "ē";
				break;
			case "ê":
				letter = "ē";
				break;
			case "E":
				letter = "Ē";
				break;
			case "Ē":
				letter = "Ē";
				break;
			case "È":
				letter = "Ē";
				break;
			case "Ë":
				letter = "Ē";
				break;
			case "Ĕ":
				letter = "Ē";
				break;
			case "É":
				letter = "Ē";
				break;
			case "Ê":
				letter = "Ē";
				break;
			case "i":
				letter = "ī";
				break;
			case "ī":
				letter = "ī";
				break;
			case "ì":
				letter = "ī";
				break;
			case "ï":
				letter = "ī";
				break;
			case "ĭ":
				letter = "ī";
				break;
			case "í":
				letter = "ī";
				break;
			case "î":
				letter = "ī";
				break;
			case "I":
				letter = "Ī";
				break;
			case "Ī":
				letter = "Ī";
				break;
			case "Ì":
				letter = "Ī";
				break;
			case "Ï":
				letter = "Ī";
				break;
			case "Ĭ":
				letter = "Ī";
				break;
			case "Í":
				letter = "Ī";
				break;
			case "Î":
				letter = "Ī";
				break;
			case "o":
				letter = "ō";
				break;
			case "ō":
				letter = "ō";
				break;
			case "ò":
				letter = "ō";
				break;
			case "ö":
				letter = "ō";
				break;
			case "ŏ":
				letter = "ō";
				break;
			case "ó":
				letter = "ō";
				break;
			case "ô":
				letter = "ō";
				break;

			case "O":
				letter = "Ō";
				break;
			case "Ō":
				letter = "Ō";
				break;
			case "Ò":
				letter = "Ō";
				break;
			case "Ö":
				letter = "Ō";
				break;
			case "Ŏ":
				letter = "Ō";
				break;
			case "Ó":
				letter = "Ō";
				break;
			case "Ô":
				letter = "Ō";
				break;
			case "u":
				letter = "ū";
				break;
			case "ū":
				letter = "ū";
				break;
			case "ù":
				letter = "ū";
				break;
			case "ü":
				letter = "ū";
				break;
			case "ŭ":
				letter = "ū";
				break;
			case "ú":
				letter = "ū";
				break;
			case "û":
				letter = "ū";
				break;
			case "U":
				letter = "Ū";
				break;
			case "Ū":
				letter = "Ū";
				break;
			case "Ù":
				letter = "Ū";
				break;
			case "Ü":
				letter = "Ū";
				break;
			case "Ŭ":
				letter = "Ū";
				break;
			case "Ú":
				letter = "Ū";
				break;
			case "Û":
				letter = "Ū";
				break;
			case "x":
				letter = "x̄";
				break;
			case "y":
				letter = "y̅";
				break;  
			case "z":
				letter = "z̄";
				break;  


			// GREEK
			case "α":
				letter = "ᾱ";
				break;
			case "ᾱ":
				letter = "ᾱ";
				break;
			case "ᾰ":
				letter = "ᾱ";
				break;
			case "ὰ":
				letter = "ᾱ";  
				break;
			case "ἀ":
				letter = "ᾱ";  
				break;
			case "ά":
				letter = "ᾱ";
				break;
			case "ἁ":
				letter = "ᾱ";
				break;     
			case "ἂ":
				letter = "ᾱ";
				break;
			case "ἅ":
				letter = "ᾱ";
				break;
			case "ἄ":
				letter = "ᾱ";
				break;
			case "ἃ":
				letter = "ᾱ";
				break; 
			case "ᾶ":
				letter = "ᾱ";  
				break;
			case "ᾳ":
				letter = "ᾱ";
				break;

			case "Α":
				letter = "Ᾱ";
				break;
			case "Ᾱ":
				letter = "Ᾱ";
				break;
			case "Ᾰ":
				letter = "Ᾱ";
				break;
			case "Ὰ":
				letter = "Ᾱ";
				break;
			case "Ἀ":
				letter = "Ᾱ";
				break;
			case "Ά":
				letter = "Ᾱ";
				break;
			case "Ἁ":
				letter = "Ᾱ";
				break;
			case "ε":
				letter = "η";
				break;
			case "Ε":
				letter = "Η";

				break;
			case "ι":
				letter = "ῑ"; 
				break;
			case "ῑ":
				letter = "ῑ"; 
				break;
			case "ῖ":
				letter = "ῑ";
				break;
			case "ϊ":            
				letter = "ῑ";
				break;
			case "ῐ":
				letter = "ῑ";
				break;
			case "ὶ":
				letter = "ῑ";
				break;
			case "ἰ":
				letter = "ῑ";  
				break;
			case "ί":
				letter = "ῑ";  
				break;
			case "ἴ":
				letter = "ῑ";

				break;

			case "Ι":
				letter = "Ῑ";
				break;
			case "Ῑ":
				letter = "Ῑ";
				break;
			case "Ῐ":
				letter = "Ῑ";
				break;
			case "Ὶ":
				letter = "Ῑ";
				break;
			case "Ἰ":
				letter = "Ῑ";
				break;
			case "Ί":
				letter = "Ῑ";

				break;

			case "υ":
				letter = "ῡ";
				break;
			case "ῡ":
				letter = "ῡ";
				break;
			case "ῠ":
				letter = "ῡ";
				break;
			case "ύ":
				letter = "ῡ";  
				break;
			case "ύ":
				letter = "ῡ"; 
				break;
			case "ῦ":
				letter = "ῡ"; 
				break;
			case "ὺ":
				letter = "ῡ";
				break;
			case "ὑ":
				letter = "ῡ";
				break;
			case "ὒ":
				letter = "ῡ";
				break;
			case "ὓ":
				letter = "ῡ";
				break;
			case "ὔ":
				letter = "ῡ";
				break;
			case "ὕ":
				letter = "ῡ";
				break;
			case "ὖ":
				letter = "ῡ";
				break;
			case "ὗ":
				letter = "ῡ";
				break;
			case "ὐ":
				letter = "ῡ";

				break;
			case "Υ":
				letter = "Ῡ";
				break;
			case "Ῡ":
				letter = "Ῡ";
				break;
			case "Ῠ":
				letter = "Ῡ";
				break;
			case "Ύ":
				letter = "Ῡ";
				break;
			case "Ὺ":
				letter = "Ῡ";
				break;
			case "Ὑ":
				letter = "Ῡ";
				break;
			case "Ὓ":
				letter = "Ῡ";
				break;
			case "Ὕ":
				letter = "Ῡ";
				break;
			case "Ὗ":
				letter = "Ῡ";

				break;
			case "ο":
				letter = "ω";

				break;
			case "Ο":
				letter = "Ω";

				break;
			// END GREEK


			// RUSSIAN
			case "а":
				// This is not redundant, it is the Russian a!
				letter = "ā";
				break;
			case "А":
				// This is not redundant, it is the Russian a!
				letter = "Ā";
				break;
			case "е":
				// This is not redundant, it is the Russian e!
				letter = "ē";
				break;
			case "ё":
				// This is not redundant, it is the Russian ё!
				letter = "ē";
				break;
			case "Е":
				// This is not redundant, it is the Russian E!
				letter = "Ē";
				break;
			case "Ё":
				// This is not redundant, it is the Russian Ё!
				letter = "Ē";
				break;
			case "о":
				// This is not redundant, it is the Russian o!
				letter = "ō";
				break;
			case "О":
				// This is not redundant, it is the Russian E!
				letter = "Ō";
				break;

			// END RUSSIAN



			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}

	// input to this fn is getAndSelectLetterBehindCursor()
	public static string ChangeVowelToShort(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "a":
				letter = "ă";
				break;
			case "ă":
				letter = "ă";
				break;
			case "à":
				letter = "ă";
				break;
			case "ä":
				letter = "ă";
				break;
			case "á":
				letter = "ă";
				break;
			case "â":
				letter = "ă";
				break;
			case "ā":
				letter = "ă";

				break;
			case "A":
				letter = "Ă";
				break;
			case "Ă":
				letter = "Ă";
				break;
			case "À":
				letter = "Ă";
				break;
			case "Ä":
				letter = "Ă";
				break;
			case "Á":
				letter = "Ă";
				break;
			case "Â":
				letter = "Ă";
				break;
			case "Ā":
				letter = "Ă";

				break;
			case "e":
				letter = "ĕ";
				break;
			case "ĕ":
				letter = "ĕ";
				break;
			case "è":
				letter = "ĕ";
				break;
			case "ë":
				letter = "ĕ";
				break;
			case "é":
				letter = "ĕ";
				break;
			case "ê":
				letter = "ĕ";
				break;
			case "ē":
				letter = "ĕ";

				break;
			case "E":
				letter = "Ĕ";
				break;
			case "Ĕ":
				letter = "Ĕ";
				break;
			case "È":
				letter = "Ĕ";
				break;
			case "Ë":
				letter = "Ĕ";
				break;
			case "É":
				letter = "Ĕ";
				break;
			case "Ê":
				letter = "Ĕ";
				break;
			case "Ē":
				letter = "Ĕ";

				break;
			case "i":
				letter = "ĭ";
				break;
			case "ĭ":
				letter = "ĭ";
				break;
			case "ì":
				letter = "ĭ";
				break;
			case "ï":
				letter = "ĭ";
				break;
			case "í":
				letter = "ĭ";
				break;
			case "î":
				letter = "ĭ";
				break;
			case "ī":
				letter = "ĭ";

				break;
			case "I":
				letter = "Ĭ";
				break;
			case "Ĭ":
				letter = "Ĭ";
				break;
			case "Ì":
				letter = "Ĭ";
				break;
			case "Ï":
				letter = "Ĭ";
				break;
			case "Í":
				letter = "Ĭ";
				break;
			case "Î":
				letter = "Ĭ";
				break;
			case "Ī":
				letter = "Ĭ";

				break;
			case "o":
				letter = "ŏ";
				break;
			case "ŏ":
				letter = "ŏ";
				break;
			case "ò":
				letter = "ŏ";
				break;
			case "ö":
				letter = "ŏ";
				break;
			case "ó":
				letter = "ŏ";
				break;
			case "ô":
				letter = "ŏ";
				break;
			case "ō":
				letter = "ŏ";

				break;
			case "O":
				letter = "Ŏ";
				break;
			case "Ŏ":
				letter = "Ŏ";
				break;
			case "Ò":
				letter = "Ŏ";
				break;
			case "Ö":
				letter = "Ŏ";
				break;
			case "Ó":
				letter = "Ŏ";
				break;
			case "Ô":
				letter = "Ŏ";
				break;
			case "Ō":
				letter = "Ŏ";

				break;
			case "u":
				letter = "ŭ";
				break;
			case "ŭ":
				letter = "ŭ";
				break;
			case "ù":
				letter = "ŭ";
				break;
			case "ü":
				letter = "ŭ";
				break;
			case "ú":
				letter = "ŭ";
				break;
			case "û":
				letter = "ŭ";
				break;
			case "ū":
				letter = "ŭ";

				break;
			case "U":
				letter = "Ŭ";
				break;
			case "Ŭ":
				letter = "Ŭ";
				break;
			case "Ù":
				letter = "Ŭ";
				break;
			case "Ü":
				letter = "Ŭ";
				break;
			case "Ú":
				letter = "Ŭ";
				break;
			case "Û":
				letter = "Ŭ";
				break;
			case "Ū":
				letter = "Ŭ";

				break;

			// GREEK
			case "α":
				letter = "ᾰ";
				break;
			case "ᾰ":
				letter = "ᾰ";
				break;
			case "ᾱ":
				letter = "ᾰ";
				break;
			case "ᾶ":
				letter = "ᾰ";
				break;
			case "ἁ":
				letter = "ᾰ";
				break;
			case "ἀ":
				letter = "ᾰ";
				break;
			case "ά":
				letter = "ᾰ";
				break;
			case "ὰ":
				letter = "ᾰ";
				break;
			case "ᾳ":
				letter = "ᾰ";

				break;
			case "Α":
				letter = "Ᾰ";
				break;
			case "Ᾰ":
				letter = "Ᾰ";
				break;
			case "Ᾱ":
				letter = "Ᾰ";
				break;
			case "Ἁ":
				letter = "Ᾰ";
				break;
			case "Ἀ":
				letter = "Ᾰ";
				break;
			case "Ά":
				letter = "Ᾰ";
				break;
			case "Ὰ":
				letter = "Ᾰ";

				break;
			case "η":
				letter = "ε";
				break;
			case "ή":
				letter = "ε";
				break;
			case "ὴ":
				letter = "ε";
				break;
			case "ἡ":
				letter = "ε";
				break;
			case "ῆ":
				letter = "ε";

				break;
			case "ῃ":
				letter = "ε";
				break;
			case "ῄ":
				letter = "ε";
				break;
			case "ῂ":
				letter = "ε";

				break;
			case "Η":
				letter = "Ε";
				break;
			case "Ή":
				letter = "Ε";
				break;
			case "Ὴ":
				letter = "Ε";
				break;
			case "Ἡ":
				letter = "Ε";
				break;
			case "ῌ":
				letter = "Ε";
				break;
			case "ι":
				letter = "ῐ";
				break;
			case "ῐ":
				letter = "ῐ";
				break;
			case "ῑ":
				letter = "ῐ";
				break;
			case "ῖ":
				letter = "ῐ";
				break;
			case "ὶ":
				letter = "ῐ";
				break;
			case "ί":
				letter = "ῐ";
				break;
			case "ἰ":
				letter = "ῐ";

				break;

			case "Ι":
				letter = "Ῐ";
				break;
			case "Ῐ":
				letter = "Ῐ";
				break;
			case "Ῑ":
				letter = "Ῐ";
				break;
			case "Ὶ":
				letter = "Ῐ";
				break;
			case "Ί":
				letter = "Ῐ";
				break;
			case "Ἰ":
				letter = "Ῐ";

				break;
			case "υ":
				letter = "ῠ";
				break;
			case "ῠ":
				letter = "ῠ";
				break;
			case "ῡ":
				letter = "ῠ";
				break;
			case "ύ":
				letter = "ῠ";
				break;
			case "ὺ":
				letter = "ῠ";
				break;
			case "ῦ":
				letter = "ῠ";
				break;
			case "ὑ":
				letter = "ῠ";
				break;
			case "ὒ":
				letter = "ῠ";
				break;
			case "ὓ":
				letter = "ῠ";
				break;
			case "ὔ":
				letter = "ῠ";
				break;
			case "ὕ":
				letter = "ῠ";
				break;
			case "ὖ":
				letter = "ῠ";
				break;
			case "ὗ":
				letter = "ῠ";
				break;
			case "ὐ":
				letter = "ῠ";

				break;

			case "Υ":
				letter = "Ῠ";
				break;
			case "Ῠ":
				letter = "Ῠ";
				break;
			case "Ῡ":
				letter = "Ῠ";
				break;
			case "Ύ":
				letter = "Ῠ";
				break;
			case "Ὺ":
				letter = "Ῠ";
				break;
			case "Ὑ":
				letter = "Ῠ";
				break;
			case "Ὓ":
				letter = "Ῠ";
				break;
			case "Ὕ":
				letter = "Ῠ";
				break;
			case "Ὗ":
				letter = "Ῠ";

				break;

			case "ω":
				letter = "ο";
				break;
			case "ὠ":
				letter = "ο";
				break;
			case "ὡ":
				letter = "ο";
				break;
			case "ῶ":
				letter = "ο";
				break;
			case "ώ":
				letter = "ο";
				break;
			case "ῳ":
				letter = "ο";
				break;
			case "ῲ":
				letter = "ο";

				break;

			case "Ω":
				letter = "Ο";
				break;
			case "Ὠ":
				letter = "Ο";
				break;
			case "Ὡ":
				letter = "Ο";
				break;
			case "Ώ":
				letter = "Ο";
				break;
			case "ῼ":
				letter = "Ο";

				break;
			// END GREEK


			// RUSSIAN
			case "а":
				// This is not redundant, it is the Russian a!
				letter = "ă";
				break;
			case "А":
				// This is not redundant, it is the Russian A!
				letter = "Ă";
				break;
			case "е":
				// This is not redundant, it is the Russian e!
				letter = "ĕ";
				break;
			case "ё":
				// This is not redundant, it is the Russian ё!
				letter = "ĕ";
				break;
			case "Е":
				// This is not redundant, it is the Russian E!
				letter = "Ĕ";
				break;
			case "Ё":
				// This is not redundant, it is the Russian Ё!
				letter = "Ĕ";
				break;
			case "о":
				// This is not redundant, it is the Russian о!
				letter = "ŏ";
				break;
			case "О":
				// This is not redundant, it is the Russian о!
				letter = "Ŏ";
				break;
			// END RUSSIAN


			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}


	public static void ShowSpecialCharactersShortcutsBox()
	{         // LOCATION Keyboard shortcuts display
		Globals.ShowFrmKeyboardShorcutsPopWindow = true;
	}

	// input to this fn is getAndSelectLetterBehindCursor()
	public static string ChangeVowelToAE(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "a":
				letter = "æ";
				// Ææ
				break;
			case "A":
				letter = "Æ";
				break;
			default:

				break;
		}
		return letter;
	}

	// input to this fn is getAndSelectLetterBehindCursor()
	public static string ChangeVowelToOE(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "o":
				letter = "œ";
				// Œœ
				break;
			case "O":
				letter = "Œ";
				break;
			default:

				break;
		}
		return letter;
	}



	public static string ChangeVowelToIotaSubscript(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "α":
				letter = "ᾳ";
				break;
			case "ᾳ":
				letter = "ᾳ";
				break;
			case "ἀ":
				letter = "ᾀ";
				break;
			case "ἁ":
				letter = "ᾁ";
				break;
			case "ἂ":
				letter = "ᾂ";
				break;
			case "ἃ":
				letter = "ᾃ";
				break;
			case "ἄ":
				letter = "ᾄ";
				break;
			case "ἅ":
				letter = "ᾅ";
				break;
			case "ἆ":
				letter = "ᾆ";
				break;
			case "ἇ":
				letter = "ᾇ";
				break;
			case "ὰ":
				letter = "ᾲ";
				break;
			case "ά":
				letter = "ᾴ";
				break;
			case "ᾶ":
				letter = "ᾷ";
				break;
			case "ᾰ":
				letter = "ᾳ";
				break;
			case "ᾱ":
				letter = "ᾳ";

				break;
			case "Α":
				letter = "ᾼ";
				break;
			case "ᾼ":
				letter = "ᾼ";
				break;
			case "Ἀ":
				letter = "ᾈ";
				break;
			case "Ἁ":
				letter = "ᾉ";
				break;
			case "Ἂ":
				letter = "ᾊ";
				break;
			case "Ἄ":
				letter = "ᾌ";
				break;
			case "Ἅ":
				letter = "ᾍ";
				break;
			case "Ἃ":
				letter = "ᾋ";
				break;
			case "Ἆ":
				letter = "ᾎ";
				break;
			case "Ἇ":
				letter = "ᾏ";
				break;
			case "Ᾰ":
				letter = "ᾼ";
				break;
			case "Ᾱ":
				letter = "ᾼ";

				break;

			case "η":
				letter = "ῃ";
				break;
			case "ῃ":
				letter = "ῃ";
				break;
			case "ῆ":
				letter = "ῇ";
				break;
			case "ἠ":
				letter = "ᾐ";
				break;
			case "ἡ":
				letter = "ᾑ";
				break;
			case "ἢ":
				letter = "ᾒ";
				break;
			case "ἣ":
				letter = "ᾓ";
				break;
			case "ἤ":
				letter = "ᾔ";
				break;
			case "ἥ":
				letter = "ᾕ";
				break;
			case "ἦ":
				letter = "ᾖ";
				break;
			case "ἧ":
				letter = "ᾗ";
				break;
			case "ὴ":
				letter = "ῂ";
				break;
			case "ή":
				letter = "ῄ";
				break;
			case "Η":
				letter = "ῌ";
				break;
			case "ῌ":
				letter = "ῌ";
				break;
			case "Ἠ":
				letter = "ᾘ";
				break;
			case "Ἡ":
				letter = "ᾙ";
				break;
			case "Ἢ":
				letter = "ᾚ";
				break;
			case "Ἣ":
				letter = "ᾛ";
				break;
			case "Ἤ":
				letter = "ᾜ";
				break;
			case "Ἥ":
				letter = "ᾝ";
				break;
			case "Ἦ":
				letter = "ᾞ";
				break;
			case "Ἧ":
				letter = "ᾟ";

				break;
			case "ω":
				letter = "ῳ";
				break;
			case "ῳ":
				letter = "ῳ";
				break;
			case "ὠ":
				letter = "ᾠ";
				break;
			case "ὡ":
				letter = "ᾡ";
				break;
			case "ὢ":
				letter = "ᾢ";
				break;
			case "ὤ":
				letter = "ᾤ";
				break;
			case "ὣ":
				letter = "ᾣ";
				break;
			case "ὥ":
				letter = "ᾥ";
				break;
			case "ὦ":
				letter = "ᾦ";
				break;
			case "ὧ":
				letter = "ᾧ";
				break;
			case "ῶ":
				letter = "ῷ";
				break;
			case "ὼ":
				letter = "ῲ";
				break;
			case "ώ":
				letter = "ῴ";

				break;
			case "Ω":
				letter = "ῼ";
				break;
			case "ῼ":
				letter = "ῼ";
				break;
			case "Ὠ":
				letter = "ᾨ";
				break;
			case "Ὡ":
				letter = "ᾩ";
				break;
			case "Ὢ":
				letter = "ᾪ";
				break;
			case "Ὣ":
				letter = "ᾫ";
				break;
			case "Ὤ":
				letter = "ᾬ";
				break;
			case "Ὥ":
				letter = "ᾭ";
				break;
			case "Ὦ":
				letter = "ᾮ";
				break;
			case "Ὧ":
				letter = "ᾯ";

				break;

			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}

	public static string ChangeVowelToTrema(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "α":
				letter = "ᾶ";
				break;
			case "ᾶ":
				letter = "ᾶ";
				break;
			case "ἀ":
				letter = "ἆ";
				break;
			case "ἁ":
				letter = "ἇ";
				break;
			case "ᾳ":
				letter = "ᾷ";
				break;
			case "ᾀ":
				letter = "ᾆ";
				break;
			case "ᾁ":
				letter = "ᾇ";
				break;
			case "ᾰ":
				letter = "ᾶ";
				break;
			case "ᾱ":
				letter = "ᾶ";
				break;
			case "ὰ":
				letter = "ᾶ";
				break;
			case "ά":
				letter = "ᾶ";
				break;
			case "Ἀ":
				letter = "Ἆ";
				break;
			case "Ἁ":
				letter = "Ἇ";
				break;
			case "ᾈ":
				letter = "ᾎ";
				break;
			case "ᾉ":
				letter = "ᾏ";
				break;


			case "ι":
				letter = "ῖ";
				break;
			case "ῖ":
				letter = "ῖ";
				break;
			case "ἰ":
				letter = "ἶ";
				break;
			case "ἱ":
				letter = "ἷ";
				break;
			case "ῐ":
				letter = "ῖ";
				break;
			case "ῑ":
				letter = "ῖ";
				break;
			case "ὶ":
				letter = "ῖ";
				break;
			case "ί":
				letter = "ῖ";  
				break;
			case "ϊ":
				letter = "ῗ";
				break;
			case "ἴ":
				letter = "ῖ";
				break;

			case "Ἰ":
				letter = "Ἶ";
				break;
			case "Ἱ":
				letter = "Ἷ";

				break;
			case "η":
				letter = "ῆ";
				break;
			case "ὴ":
				letter = "ῆ";
				break;
			case "ή":
				letter = "ῆ";
				break;
			case "ῆ":
				letter = "ῆ";
				break;
			case "ἠ":
				letter = "ἦ";
				break;
			case "ἡ":
				letter = "ἧ";
				break;
			case "ῃ":
				letter = "ῇ";
				break;
			case "ᾐ":
				letter = "ᾖ";
				break;
			case "ᾑ":
				letter = "ᾗ";

				break;
			case "Ἠ":
				letter = "Ἦ";
				break;
			case "Ἡ":
				letter = "Ἧ";
				break;
			case "ᾘ":
				letter = "ᾞ";
				break;
			case "ᾙ":
				letter = "ᾟ";

				break;
			case "υ":
				letter = "ῦ";
				break;
			case "ὺ":
				letter = "ῦ";
				break;
			case "ύ":
				letter = "ῦ";
				break;
			case "ὐ":
				letter = "ὖ";
				break;
			case "ὑ":
				letter = "ὗ";  
				break;
			case "ῠ":
				letter = "ῦ"; 
				break;
			case "ῡ":
				letter = "ῦ";
				break;
			case "ὓ":
				letter = "ῦ";
				break;
			case "ϋ":
				letter = "ῧ";
				break;

			case "Ὑ":
				letter = "Ὗ";

				break;

			case "ω":
				letter = "ῶ";
				// Ω trema does not exist
				break;
			case "ῶ":
				letter = "ῶ";
				break;
			case "ὠ":
				letter = "ὦ";
				break;
			case "ὡ":
				letter = "ὧ";
				break;
			case "ὼ":
				letter = "ῶ";
				break;
			case "ώ":
				letter = "ῶ";

				break;

			case "ῳ":
				letter = "ῷ";
				break;
			case "ᾠ":
				letter = "ᾦ";
				break;
			case "ᾡ":
				letter = "ᾧ";

				break;
			case "Ὠ":
				letter = "Ὦ";
				break;
			case "Ὡ":
				letter = "Ὧ";
				break;
			case "ᾨ":
				letter = "ᾮ";
				break;
			case "ᾩ":
				letter = "ᾯ";
				break;

			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}

	public static string ChangeVowelToRough(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "α":
				letter = "ἁ";
				break;
			case "ἀ":
				letter = "ἁ";
				break;
			case "ἁ":
				letter = "ἁ";
				break;
			case "ὰ":
				letter = "ἃ";
				break;
			case "ά":
				letter = "ἅ";
				break;
			case "ἂ":
				letter = "ἃ";
				break;
			case "ἄ":
				letter = "ἅ";
				break;
			case "ἅ":
				letter = "ἅ";
				break;
			case "ᾶ":
				letter = "ἇ";
				break;
			case "ᾰ":
				letter = "ἁ";
				break;
			case "ᾱ":
				letter = "ἁ";
				break;
			case "ᾳ":
				letter = "ᾁ";
				break;
			case "ᾁ":
				letter = "ᾁ";
				break;
			case "ᾷ":
				letter = "ᾇ";
				break;
			case "ᾇ":
				letter = "ᾇ";
				break;
			case "ἆ":
				letter = "ἇ";
				break;

			case "Α":
				letter = "Ἁ";
				break;
			case "Ἁ":
				letter = "Ἁ";
				break;
			case "Ὰ":
				letter = "Ἃ";
				break;
			case "Ἃ":
				letter = "Ἃ";
				break;
			case "Ά":
				letter = "Ἅ";
				break;
			case "Ἅ":
				letter = "Ἅ";
				break;
			case "Ἀ":
				letter = "Ἁ";
				break;
			case "Ἂ":
				letter = "Ἃ";
				break;
			case "Ἄ":
				letter = "Ἅ";
				break;
			case "ᾼ":
				letter = "ᾉ";
				break;
			case "Ἆ":
				letter = "Ἇ";
				break;
			case "Ἇ":
				letter = "Ἇ";

				break;
			case "ᾈ":
				letter = "ᾉ";
				break;
			case "ᾉ":
				letter = "ᾉ";
				break;
			case "ᾊ":
				letter = "ᾋ";
				break;
			case "ᾋ":
				letter = "ᾋ";
				break;
			case "ᾎ":
				letter = "ᾏ";
				break;
			case "ᾏ":
				letter = "ᾏ";
				break;
			case "Ᾰ":
				letter = "Ἁ";
				break;
			case "Ᾱ":
				letter = "Ἁ";

				break;

			case "ε":
				letter = "ἑ";
				break;
			case "ἑ":
				letter = "ἑ";
				break;
			case "έ":
				letter = "ἕ";
				break;
			case "ὲ":
				letter = "ἓ";
				break;
			case "ἓ":
				letter = "ἓ";
				break;
			case "ἐ":
				letter = "ἑ";
				break;
			case "ἒ":
				letter = "ἓ";
				break;
			case "ἔ":
				letter = "ἕ";
				break;
			case "ἕ":
				letter = "ἕ";
				break;
			//Case "ὲ"
			//    letter = "ἑ"
			//Case "έ"
			//    letter = "ἑ"

			case "Ε":
				letter = "Ἑ";
				break;
			case "Ἑ":
				letter = "Ἑ";
				break;
			case "Έ":
				letter = "Ἕ";
				break;
			case "Ἕ":
				letter = "Ἕ";
				break;
			case "Ὲ":
				letter = "Ἓ";
				break;
			case "Ἓ":
				letter = "Ἓ";
				break;
			case "Ἐ":
				letter = "Ἑ";
				break;
			case "Ἒ":
				letter = "Ἓ";

				break;
			case "η":
				letter = "ἡ";
				break;
			case "ἡ":
				letter = "ἡ";
				break;
			case "ὴ":
				letter = "ἣ";
				break;
			case "ἣ":
				letter = "ἣ";
				break;
			case "ή":
				letter = "ἥ";
				break;
			case "ἥ":
				letter = "ἥ";
				break;
			case "ῃ":
				letter = "ᾑ";
				break;
			case "ἠ":
				letter = "ἡ";
				break;
			case "ἢ":
				letter = "ἣ";
				break;
			case "ἤ":
				letter = "ἥ";
				break;
			case "ἦ":
				letter = "ἧ";
				break;
			case "ἧ":
				letter = "ἧ";

				break;
			case "ᾐ":
				letter = "ᾑ";
				break;
			case "ᾑ":
				letter = "ᾑ";
				break;
			case "ᾒ":
				letter = "ᾓ";
				break;
			case "ᾓ":
				letter = "ᾓ";
				break;
			case "ᾔ":
				letter = "ᾕ";
				break;
			case "ᾕ":
				letter = "ᾕ";
				break;
			case "ᾖ":
				letter = "ᾗ";
				break;
			case "ᾗ":
				letter = "ᾗ";
				break;
			case "ῂ":
				letter = "ᾑ";
				break;
			case "ῄ":
				letter = "ᾑ";
				break;
			case "ῆ":
				letter = "ἡ";

				break;
			case "Η":
				letter = "Ἡ";
				break;
			case "Ἡ":
				letter = "Ἡ";
				break;
			case "Ὴ":
				letter = "Ἣ";
				break;
			case "Ἣ":
				letter = "Ἣ";
				break;
			case "Ή":
				letter = "Ἥ";
				break;
			case "Ἥ":
				letter = "Ἥ";
				break;
			case "Ἠ":
				letter = "Ἡ";
				break;
			case "Ἢ":
				letter = "Ἣ";
				break;
			case "Ἤ":
				letter = "Ἥ";
				break;
			case "Ἦ":
				letter = "Ἧ";
				break;
			case "Ἧ":
				letter = "Ἧ";

				break;
			case "ῌ":
				letter = "ᾙ";
				break;
			case "ᾙ":
				letter = "ᾙ";
				break;
			case "ᾘ":
				letter = "ᾙ";
				break;
			case "ᾚ":
				letter = "ᾛ";
				break;
			case "ᾛ":
				letter = "ᾛ";
				break;
			case "ᾜ":
				letter = "ᾝ";
				break;
			case "ᾝ":
				letter = "ᾝ";
				break;
			case "ᾞ":
				letter = "ᾟ";
				break;
			case "ᾟ":
				letter = "ᾟ";
				break;
			//Case "Ὴ"
			//    letter = "Ἡ"
			//Case "Ή"
			//    letter = "Ἡ"

			case "ι":
				letter = "ἱ";
				break;
			case "ἱ":
				letter = "ἱ";
				break;
			case "ὶ":
				letter = "ἳ"; 
				break;
			case "ἳ":
				letter = "ἳ";
				break;
			case "ί":
				letter = "ἵ";
				break;
			case "ἵ":
				letter = "ἵ";  
				break;
			case "ἴ":
				letter = "ἵ";
				break;
			case "ῖ":
				letter = "ἷ";
				break;
			case "ἷ":
				letter = "ἷ";
				break;
			case "ἰ":
				letter = "ἱ";
				break;
			case "ἲ":
				letter = "ἳ";
				break;
			case "ἶ":
				letter = "ἷ";
				break;
			case "ῐ":
				letter = "ἱ";
				break;
			case "Ι":
				letter = "Ἱ";
				break;
			case "Ἱ":
				letter = "Ἱ";
				break;
			case "Ὶ":
				letter = "Ἳ";
				break;
			case "Ἳ":
				letter = "Ἳ";
				break;
			case "Ί":
				letter = "Ἵ";
				break;
			case "Ἵ":
				letter = "Ἵ";
				break;
			case "Ἰ":
				letter = "Ἱ";
				break;
			case "Ἲ":
				letter = "Ἳ";
				break;
			case "Ἶ":
				letter = "Ἷ";
				break;
			case "Ἷ":
				letter = "Ἷ";
				break;
			case "Ῐ":
				letter = "Ἱ";
				break;
			case "Ῑ":
				letter = "Ἱ";
				break;
			case "Ἴ":
				letter = "Ἵ";

				break;

			case "ο":
				letter = "ὁ";
				break;
			case "ὁ":
				letter = "ὁ";
				break;
			case "ὸ":
				letter = "ὃ";
				break;
			case "ὃ":
				letter = "ὃ";
				break;
			case "ό":
				letter = "ὅ";
				break;
			case "ὅ":
				letter = "ὅ";
				break;
			case "ὀ":
				letter = "ὁ";
				break;
			case "ὂ":
				letter = "ὃ";
				break;
			case "ὄ":
				letter = "ὅ";

				break;
			case "Ο":
				letter = "Ὁ";
				break;
			case "Ὁ":
				letter = "Ὁ";
				break;
			case "Ὸ":
				letter = "Ὃ";
				break;
			case "Ὃ":
				letter = "Ὃ";
				break;
			case "Ό":
				letter = "Ὅ";
				break;
			case "Ὅ":
				letter = "Ὅ";
				break;
			case "Ὀ":
				letter = "Ὁ";
				break;
			case "Ὂ":
				letter = "Ὃ";
				break;
			case "Ὄ":
				letter = "Ὅ";

				break;

			case "υ":
				letter = "ὑ";
				break;
			case "ὑ":
				letter = "ὑ";
				break;
			case "ὺ":
				letter = "ὓ";
				break;
			case "ὓ":
				letter = "ὓ";
				break;
			case "ύ":
				letter = "ὕ";
				break;
			case "ὕ":
				letter = "ὕ";
				break;
			case "ὐ":
				letter = "ὑ";
				break;
			case "ὒ":
				letter = "ὓ";
				break;
			case "ὔ":
				letter = "ὕ";
				break;
			case "ὖ":
				letter = "ὗ";
				break;
			case "ὗ":
				letter = "ὗ";
				break;
			case "ῠ":
				letter = "ὑ";
				break;
			case "ῡ":
				letter = "ὑ";
				break;
			case "ῦ":
				letter = "ὗ";

				break;

			case "Υ":
				letter = "Ὑ";
				break;
			case "Ὑ":
				letter = "Ὑ";
				break;
			case "Ὺ":
				letter = "Ὓ";
				break;
			case "Ὓ":
				letter = "Ὓ";
				break;
			case "Ύ":
				letter = "Ὕ";
				break;
			case "Ὕ":
				letter = "Ὕ";
				break;
			case "Ῠ":
				letter = "Ὑ";
				break;
			case "Ῡ":
				letter = "Ὑ";

				break;


			case "ω":
				letter = "ὡ";
				break;
			case "ὡ":
				letter = "ὡ";
				break;
			case "ὼ":
				letter = "ὣ";
				break;
			case "ὣ":
				letter = "ὣ";
				break;
			case "ώ":
				letter = "ὥ";
				break;
			case "ὥ":
				letter = "ὥ";
				break;
			case "ῶ":
				letter = "ὧ";
				break;
			case "ὦ":
				letter = "ὧ";
				break;
			case "ὠ":
				letter = "ὡ";
				break;
			case "ὢ":
				letter = "ὣ";
				break;
			case "ὤ":
				letter = "ὥ";

				break;
			case "ῳ":
				letter = "ᾡ";
				break;
			case "ᾡ":
				letter = "ᾡ";
				break;
			case "ᾠ":
				letter = "ᾡ";
				break;
			case "ᾢ":
				letter = "ᾣ";
				break;
			case "ᾣ":
				letter = "ᾣ";
				break;
			case "ᾤ":
				letter = "ᾥ";
				break;
			case "ᾥ":
				letter = "ᾥ";
				break;
			case "ᾦ":
				letter = "ᾧ";
				break;
			case "ᾧ":
				letter = "ᾧ";
				break;
			case "ῴ":
				letter = "ᾥ";

				break;

			case "Ω":
				letter = "Ὡ";
				break;
			case "Ὡ":
				letter = "Ὡ";
				break;
			case "Ὼ":
				letter = "Ὣ";
				break;
			case "Ὣ":
				letter = "Ὣ";
				break;
			case "Ώ":
				letter = "Ὥ";
				break;
			case "Ὥ":
				letter = "Ὥ";
				break;
			case "ῼ":
				letter = "ᾩ";
				break;
			case "ᾩ":
				letter = "ᾩ";
				break;
			case "ᾫ":
				letter = "ᾫ";
				break;
			case "ᾭ":
				letter = "ᾭ";

				break;
			case "ρ":
				letter = "ῥ";
				break;
			case "ῥ":
				letter = "ῥ";
				break;
			case "ῤ":
				letter = "ῥ";
				break;
			case "Ρ":
				letter = "Ῥ";
				break;
			case "Ῥ":
				letter = "Ῥ";
				break;

			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}

	public static string ChangeVowelToSmooth(string selectedLetterBehindCursor)
	{
		string letter = selectedLetterBehindCursor;
		switch (selectedLetterBehindCursor)
		{
			case "α":
				letter = "ἀ";
				break;
			case "ἀ":
				letter = "ἀ";
				break;
			case "ὰ":
				letter = "ἂ";
				break;
			case "ά":
				letter = "ἄ";
				break;
			case "ἁ":
				letter = "ἀ";
				break;
			case "ἃ":
				letter = "ἂ";
				break;
			case "ἅ":
				letter = "ἄ";
				break;
			case "ἄ":
				letter = "ἄ";
				break;
			case "ἇ":
				letter = "ἆ";
				break;
			case "ᾁ":
				letter = "ᾀ";
				break;
			case "ᾃ":
				letter = "ᾂ";
				break;
			case "ᾅ":
				letter = "ᾄ";
				break;
			case "ᾇ":
				letter = "ᾆ";
				break;
			case "ἆ":
				letter = "ἆ";
				break;
			case "ᾱ":
				letter = "ἀ";
				break;
			case "ᾰ":
				letter = "ἀ";
				break;
			case "ᾶ":
				letter = "ἆ";
				break;
			case "ᾳ":
				letter = "ᾁ";
				break;
			case "ᾲ":
				letter = "ᾂ";
				break;
			case "ᾴ":
				letter = "ᾄ";
				break;
			case "Α":
				letter = "Ἀ";
				break;
			case "A":
				letter = "Ἀ";
				break;
			case "Ὰ":
				letter = "Ἂ";
				break;
			case "Ά":
				letter = "Ἄ";
				break;
			case "Ἁ":
				letter = "Ἀ";
				break;
			case "Ἃ":
				letter = "Ἂ";
				break;
			case "Ἅ":
				letter = "Ἄ";
				break;
			case "Ἇ":
				letter = "Ἆ";
				break;
			case "ᾼ":
				letter = "ᾈ";
				break;
			case "ᾉ":
				letter = "ᾈ";
				break;
			case "ᾋ":
				letter = "ᾊ";
				break;
			case "ᾍ":
				letter = "ᾌ";
				break;
			case "ᾏ":
				letter = "ᾎ";
				break;

			case "ε":
				letter = "ἐ";
				break;
			case "έ":
				letter = "ἔ";
				break;
			case "ὲ":
				letter = "ἒ";
				break;
			case "ἑ":
				letter = "ἐ";
				break;
			case "ἓ":
				letter = "ἒ";
				break;
			case "ἕ":
				letter = "ἔ";
				break;

			// Special cases  δʼ τʼ θʼ ρʼ etc.
			case "Β":
				letter = "Βʼ";
				break;
			case "β":
				letter = "βʼ";
				break;
			case "Γ":
				letter = "Γʼ";
				break;
			case "γ":
				letter = "γʼ";
				break;
			case "Δ":
				letter = "Δʼ";
				break;
			case "δ":
				letter = "δʼ";
				break;
			case "Ζ":
				letter = "Ζʼ";
				break;
			case "ζ":
				letter = "ζʼ";
				break;
			case "Θ":
				letter = "Θʼ";
				break;
			case "θ":
				letter = "θʼ";
				break;
			case "Κ":
				letter = "Κʼ";
				break;
			case "κ":
				letter = "κʼ";
				break;
			case "Λ":
				letter = "Λʼ";
				break;
			case "λ":
				letter = "λʼ";
				break;
			case "Μ":
				letter = "Μʼ";
				break;
			case "μ":
				letter = "μʼ";
				break;
			case "Ν":
				letter = "Νʼ";
				break;
			case "ν":
				letter = "νʼ";
				break;
			case "Π":
				letter = "Πʼ";
				break;
			case "π":
				letter = "πʼ";
				break;
			case "Ρ":
				letter = "Ρʼ";
				break;
			case "ρ":
				letter = "ρʼ";
				break;
			case "Σ":
				letter = "Σʼ";
				break;
			case "σ":
				letter = "σʼ";
				break;
			case "Τ":
				letter = "Τʼ";
				break;
			case "τ":
				letter = "τʼ";
				break;
			case "Φ":
				letter = "Φʼ";
				break;
			case "φ":
				letter = "φʼ";
				break;
			case "Χ":
				letter = "Χʼ";
				break;
			case "χ":
				letter = "Χʼ";
				break;
			case "Ψ":
				letter = "Ψʼ";
				break;
			case "ψ":
				letter = "ψʼ";
				break;

			case "Ε":
				letter = "Ἐ";
				break;
			case "Ὲ":
				letter = "Ἓ";
				break;
			case "Έ":
				letter = "Ἔ";
				break;
			case "Ἑ":
				letter = "Ἐ";
				break;
			case "Ἓ":
				letter = "Ἒ";
				break;
			case "Ἕ":
				letter = "Ἔ";
				break;

			case "η":
				letter = "ἠ";
				break;
			case "ἠ":
				letter = "ἠ";
				break;
			case "ὴ":
				letter = "ἢ";
				break;
			case "ή":
				letter = "ἤ";   
				break;
			case "ἡ":
				letter = "ἠ";
				break;
			case "ἣ":
				letter = "ἢ";
				break;
			case "ἥ":
				letter = "ἤ";
				break;
			case "ἤ":
				letter = "ἤ";
				break;
			case "ῆ":
				letter = "ἦ";
				break;
			case "ἧ":
				letter = "ἦ";

				break;
			case "ῃ":
				letter = "ᾐ";
				break;
			case "ῂ":
				letter = "ᾒ";
				break;
			case "ῄ":
				letter = "ᾔ";
				break;
			case "ᾑ":
				letter = "ᾐ";
				break;
			case "ᾓ":
				letter = "ᾒ";
				break;
			case "ᾕ":
				letter = "ᾔ";
				break;
			case "ᾗ":
				letter = "ᾖ";

				break;
			case "Η":
				letter = "Ἠ";
				break;
			case "Ὴ":
				letter = "Ἢ";
				break;
			case "Ἠ":
				letter = "Ἢ";
				break;
			case "Ή":    
				letter = "Ἤ";
				break;
			case "Ἡ":
				letter = "Ἠ";  
				break;
			case "Ἣ":
				letter = "Ἢ";
				break;
			case "Ἥ":
				letter = "Ἤ";
				break;
			case "Ἧ":
				letter = "Ἦ";

				break;
			case "ᾙ":
				letter = "ᾘ";
				break;
			case "ᾛ":
				letter = "ᾚ";
				break;
			case "ᾝ":
				letter = "ᾜ";
				break;
			case "ᾟ":
				letter = "ᾞ";

				break;
			case "ι":
				letter = "ἰ";
				break;
			case "ἰ":
				letter = "ἰ";
				break;
			case "ὶ":
				letter = "ἲ";
				break;
			case "ί":
				letter = "ἴ";   
				break;
			case "ἵ":      
				letter = "ἴ";    
				break;
			case "ἴ":
				letter = "ἴ";
				break;
			case "ἲ":
				letter = "ἴ";
				break;
			case "ῖ":
				letter = "ἶ";
				break;
			case "ἱ":
				letter = "ἰ";
				break;
			case "ἳ":
				letter = "ἲ";
				break;
			case "ἷ":
				letter = "ἶ";
				break;
			case "ῐ":
				letter = "ἰ";
				break;
			case "ῑ":
				letter = "ἰ";
				break;
			case "Ι":
				letter = "Ἰ";
				break;
			case "Ἰ":
				letter = "Ἰ";
				break;
			case "Ὶ":
				letter = "Ἲ";
				break;
			case "Ί":
				letter = "Ἴ";
				break;
			case "Ἱ":
				letter = "Ἰ";
				break;
			case "Ἳ":
				letter = "Ἲ";
				break;
			case "Ἵ":
				letter = "Ἴ";
				break;
			case "Ἷ":
				letter = "Ἶ";
				break;
			case "Ῐ":
				letter = "Ἰ";
				break;
			case "Ῑ":
				letter = "Ἰ";
				break;
			case "Ἲ":
				letter = "Ἴ";
				break;
			case "ο":
				letter = "ὀ";
				break;
			case "ὸ":
				letter = "ὂ";
				break;
			case "ό":
				letter = "ὄ";
				break;
			case "ὁ":
				letter = "ὀ";
				break;
			case "ὃ":
				letter = "ὂ";
				break;
			case "ὅ":
				letter = "ὄ";

				break;
			case "Ο":
				letter = "Ὀ";
				break;
			case "Ὸ":
				letter = "Ὂ";
				break;
			case "Ό":
				letter = "Ὄ";
				break;
			case "Ὁ":
				letter = "Ὀ";
				break;
			case "Ὃ":
				letter = "Ὂ";
				break;
			case "Ὅ":
				letter = "Ὄ";
				break;

			case "υ":
				letter = "ὐ";
				break;
			case "ὺ":
				letter = "ὒ";
				break;
			case "ύ":
				letter = "ὔ";
				break;
			case "ῦ":
				letter = "ὖ";
				break;
			case "ὑ":
				letter = "ὐ";
				break;
			case "ὓ":
				letter = "ὒ";
				break;
			case "ὕ":
				letter = "ὔ";
				break;
			case "ὗ":
				letter = "ὖ";

				break;
			case "ω":
				letter = "ὠ";
				break;
			case "ὠ":
				letter = "ὠ";
				break;
			case "ὼ":
				letter = "ὤ";
				break;
			case "ώ":
				letter = "ὤ";
				break;
			case "ῶ":
				letter = "ὦ";
				break;
			case "ῳ":
				letter = "ᾠ";
				break;
			case "ὡ":
				letter = "ὠ";
				break;
			case "ὣ":
				letter = "ὢ";
				break;
			case "ὥ":
				letter = "ὤ";
				break;
			case "ὧ":
				letter = "ὦ";

				break;
			case "ᾡ":
				letter = "ᾠ";
				break;
			case "ᾣ":
				letter = "ᾢ";
				break;
			case "ᾥ":
				letter = "ᾤ";
				break;
			case "ῲ":
				letter = "ᾢ";
				break;
			case "ῴ":
				letter = "ᾤ";
				break;
			case "ᾧ":
				letter = "ᾦ";
				break;
			case "Ω":
				letter = "Ὠ";
				break;
			case "Ὼ":
				letter = "Ὢ";
				break;
			case "Ώ":
				letter = "Ὤ";
				break;
			case "ῼ":
				letter = "ᾨ";
				break;
			case "Ὡ":
				letter = "Ὠ";
				break;
			case "Ὣ":
				letter = "Ὢ";
				break;
			case "Ὥ":
				letter = "Ὤ";
				break;
			case "Ὧ":
				letter = "Ὦ";

				break;
			case "ᾩ":
				letter = "ᾨ";
				break;
			case "ᾫ":
				letter = "ᾪ";
				break;
			case "ᾭ":
				letter = "ᾬ";
				break;
			case "ᾯ":
				letter = "ᾮ";
				break;
			case "ῥ":
				letter = "ῤ";

				break;
			default:
				ShowSpecialCharactersShortcutsBox();
				break;
		}
		return letter;
	}


	#endregion


}


/*
System.Windows.Forms.InputLanguage cultureInfo = null;
cultureInfo = System.Windows.Forms.InputLanguage.CurrentInputLanguage;
var language = cultureInfo.Culture.EnglishName;
switch (language)
{ 
	case "Greek (Greece)":
		var result = MessageBox.Show("\n\rGreek\n\r", "  ", MessageBoxButtons.OK);
		break;
	default:
		var result2 = MessageBox.Show("\n\rF1 - Get\n\r", "", MessageBoxButtons.OK);
		break;
} */

// Greek (Greece)
// THESE ARE NOT NEEDED AS THERE IS NO KNOWN VARIATION AMONG THESE
// They all  fall under default 
// Case "English (United States)"   
// Case "Spanish (Spain, International Sort)"  
// Case "French (France)"
// Case "German (Germany)"
// Case "Italian (Italy)"