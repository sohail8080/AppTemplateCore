using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Validation_DA
{
    public class Regular_Expression
    {
        //  \d  one digit from 0 to 9


        //============================================
        //Regex r = new Regex(@"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}");  
        //^ sting starting
        //  \+? zeor or one times + character repeated
        // \d{0,2} 0 or 1 or 2 repetion of digiits, we do not use + or ? or * for repetiion, but range
        // \-? zero or one - character
        // \d{4,5} 4 or 5 repetion of digiits, we do not use + or ? or * for repetiion, but range
        // \-? zero or one - character
        // \d{5,6} 5 or 6 repetion of digiits, we do not use + or ? or * for repetiion, but range
        //{ "+91-9678967101", "9678967101", "+91-9678-967101", "+91-96789-67101", "+919678967101" };
        //================================================



        //[RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5)]


        //phone:  @"^[1-9]\d{2}-[1-9]\d{2}-\d{4}$"
        //==========================================
        // ^ Start of sting 
        // [1-9] one digitis in the range 1-9
        // d{2} two legth digit
        // - fixed dash
        // [1-9] one digitis in the range 1-9
        // d{2} two legth digit
        // - fixed dash
        // d{2} four legth digit
        // $ string end
        // e.g 532-222-3333


        //zipCode: @"^\d{5}$"
        //============================
        // ^ start sting
        // \d{5} five length digit
        // $ string end


        //state , @"^([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$"
        //====================================================
        // ^ string start
        // ( bracket for or operator
        // [a-zA-Z]+  one or more character in the given range
        // | or operator
        // [a-zA-Z]+  one or more character in the given range
        // \s one space only
        // [a-zA-Z]+  one or more character in the given range
        // $ string end



        //address, @"^[0-9]+\s+([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$"
        //============================================================
        // ^ string start 
        // [0-9]+ one or more digit int the given range
        //\s+ one or more space
        // ( bracket for or operator
        // [a-zA-Z]+  one or more characters in the given range
        // | or operator
        // [a-zA-Z]+  one or more character in the given range
        //  //\s one space
        // [a-zA-Z]+  one or more characters in the given range



        // \d+[\+-x\*]\d+
        //======================================
        //  \d+    one or more digits, + sign is not for concatination
        // [\+-x\*] only one character from + or - or x or *, we do not use + or ? or *  or range for repetiion, 
        //  that means one. \ is used for + and * special charaters 
        // \d+      one or more digits, + sign is not for concatination
        // above reg ex match these stings "2+2" and "3*9" in "(2+2) * 3*9"


        //[RegularExpression(@"^\d{5}(-\d{4})?$"
        //=============================================
        // ^ start of sting
        // \d{5} five length digit
        // ( start of expression
        // - hard coded dash
        // \d{4} four length digit
        // ) end of expression
        // ? zero or one time string is reapated
        // (-\d{4})? zero or one time 4 length digit string is repeated
        // examples 12345, 12345-1234


        //[RegularExpression(@"^([0-9]{10})$",
        //========================================
        // start ^
        // [0-9]{10} 10 length digit in the range
        // $ end of string
        // e.g 3333333333


        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
        //==========================================================================
        // start ^
        // \(?              zero or one starting braces
        // ([0-9]{3})       three length digit in the given range
        // \)?               zero or one ending braces
        // [-. ]?           zeor or one characters from set, - or . (any character) or space
        // ([0-9]{3})       three digit number from given range
        // [-. ]?           zeor or one characters from set, - or . (any character) or space
        // ([0-9]{4})       four digit number from given range

        //[RegularExpression(@"^[0-9]*$",
        //===============================
        // ^ starting
        // [0-9]*  zero or more times repetion of digit from given range


        // [RegularExpression("^[0-9]{8}$")]
        //==================================
        // ^ starting of string
        // [0-9]{8}  eignt length digit from given range
        // $ end of string


        //[RegularExpression(@"^\d{3}-\d{3}-\d{4}$")] string phone)
        //========================================================
        // starting of string ^
        // \d{3} three length digit
        // - dash
        // \d{3} three length digit
        // - dash
        // \d{4}  four length digit


        // [RegularExpression(@"^[a-zA-Z]+$"
        //=============================================
        // starting string ^
        // [a-zA-Z]+ one or more character from given range
        // $ end of string


        //[RegularExpression(@"^[a-z]{8,16}$",
        //============================================
        // starting string ^
        // [a-z]{8,16} lower case characters of length between 8 to 16


        //[RegularExpression("([1-9][0-9]*)")]
        //============================================
        //[1-9] one digit in the given range
        //[0-9]* zero or more digits in given range



        //[RegularExpression("([0-9]+)"
        //====================================
        // one or more digit in the given range


        //[RegularExpression(@"^[0-9]*$"
        //=====================================
        // starting string
        //[0-9]* zero or more digits in given range 



        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
        //============================================================
        // ([a-zA-Z0-9\s_\\.\-:])+  one or more characters from given range
        // (.png|.jpg|.gif)         only one string from given 3 strings


        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]

        //[RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        //[RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers of First Name")]
        //[RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        //[RegularExpression("([A-Za-z])+( [A-Za-z]+)", ErrorMessage = "Enter valid full name.")]
        //[RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]



        // [] means only one charater from the given character set/range
        // []*  means zero or more characters repeated from the given charater set/range
        // []+  means one or more characters repeated from the given charater set/range
        // []?  means zero or one characters repeated from the given charater set/range
        // []{min, max}  means atleast min to max repetition from the given charater set/range


        // [a-z] means only one characters repeated from a-z range
        // [a-z]* means zero or more characters repeated from a-z range
        // [a-z]+ means one or more characters repeated from a-z range
        // [a-z]? means zero or one characters repeated from a-z range
        // [a-z]{3,5} means 3 or 4 or 5 characters repeated from a-z range
        // [a-z]{3,} means min 3 or max any characters repeated from a-z range
    }
}
