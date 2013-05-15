using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FluentValidation
{
    public static partial class Rules
    {
        //===============================================================
        // Regex taken from http://stackoverflow.com/questions/123559/a-comprehensive-regex-for-phone-number-validation. Matches 10-digit phone numbers like 123-456-7890
        public static Rule<String> IsPhoneNumber =
            new Rule<string>(
                x =>
                Regex.IsMatch(x,
                              @"(?:(?:(\s*\(?([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*)|([2-9]1[02-9]|[2‌​-9][02-8]1|[2-9][02-8][02-9]))\)?\s*(?:[.-]\s*)?)([2-9]1[02-9]|[2-9][02-9]1|[2-9]‌​[02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})"),
                x => String.Format("{0} is not a valid phone number.", x));
        //===============================================================
        // Regex taken from http://regexlib.com/Search.aspx?k=ssn&AspxAutoDetectCookieSupport=1. Matches hyphenated SSN like 123-45-6789
        public static Rule<String> IsSocialSecurityNumber = new Rule<string>(x => Regex.IsMatch(x, @"^\d{3}-\d{2}-\d{4}$"), x => String.Format("{0} is not a valid social security number.", x));
        //===============================================================
        // Taken from http://stackoverflow.com/questions/2577236/regex-for-zip-code
        public static Rule<String> IsZipCode = new Rule<string>(x => Regex.IsMatch(x, @"^\d{5}(?:[-\s]\d{4})?$"), x => String.Format("{0} is not a valid zip code.", x));
        //===============================================================
        public static Rule<String> IsNotNullOrWhitespace = new Rule<string>(x => !String.IsNullOrWhiteSpace(x), x => String.Format("{0} cannot be null or whitespace."));
        //===============================================================
        public static Rule<decimal> IsGreaterThan(decimal val)
        {
            return new Rule<decimal>(x => x > val, x => String.Format("{0} is not greater than {1}.", x, val));
        }
        //===============================================================
        public static Rule<decimal> IsGreaterThanOrEqualTo(decimal val)
        {
            return new Rule<decimal>(x => x >= val, x => String.Format("{0} is not greater than or equal to {1}.", x, val));
        }
        //===============================================================
        public static Rule<decimal> IsLessThan(decimal val)
        {
            return new Rule<decimal>(x => x < val, x => String.Format("{0} is not less than {1}.", x, val));
        }
        //===============================================================
        public static Rule<decimal> IsLessThanOrEqualTo(decimal val)
        {
            return new Rule<decimal>(x => x <= val, x => String.Format("{0} is not less than or equal to {1}.", x, val));
        }
        //===============================================================
    }
}
