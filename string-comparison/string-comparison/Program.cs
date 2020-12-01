using System;
using System.Globalization;
using System.Threading;

namespace string_comparison
{
    class Program
    {
        static void Main(string[] args)
        {
            StringEqualityRules_LinguisticVsOrdinal();
        }

        static void StringComparisonRules_LinguisticVsOrdinal()
        {
            string str1 = "asta";
            string str2 = "ăsta";
            string str3 = "basta";

            // linguistic comparison: a, ă, b
            ComparisonTest(str1, str2, StringComparison.CurrentCulture);
            ComparisonTest(str2, str3, StringComparison.CurrentCulture);

            PrintSeparator();

            // ordinal comparison: a, b, ă
            ComparisonTest(str1, str2, StringComparison.Ordinal);
            ComparisonTest(str2, str3, StringComparison.Ordinal);
        }

        static void StringComparisonRules_UppercaseVsLowercase()
        {
            string str1 = "asta";
            string str2 = "ASTA";

            // linguistic comparison: uppercase > lowercase
            ComparisonTest(str1, str2, StringComparison.CurrentCulture);
            ComparisonTest(str1, str2, StringComparison.CurrentCultureIgnoreCase);

            PrintSeparator();

            // ordinal comparison: uppercase < lowercase
            ComparisonTest(str1, str2, StringComparison.Ordinal);
            ComparisonTest(str1, str2, StringComparison.OrdinalIgnoreCase);
        }

        static void StringEqualityRules_LinguisticVsOrdinal()
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;

            // linguistic comparison: in en-US file = FILE (ignore case);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            EqualityTest("file", "FILE", StringComparison.CurrentCultureIgnoreCase);

            // linguistic comparison: in tr-TR file != FILE (ignore case);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            EqualityTest("file", "FILE", StringComparison.CurrentCultureIgnoreCase);

            // linguistic comparison: in de-DE Strasse = Straße (ignore case);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            EqualityTest("Strasse", "Straße", StringComparison.CurrentCultureIgnoreCase);

            // restore original cultulre
            Thread.CurrentThread.CurrentCulture = originalCulture;

            string s1 = "aaa";
            string s2 = "a" + '\u0000' + "aa";
            EqualityTest(s1, s2, StringComparison.OrdinalIgnoreCase);
            EqualityTest(s1, s2, StringComparison.CurrentCultureIgnoreCase);
        }

        

        static void ComparisonTest(string a, string b, StringComparison comparisonType)
        {
            int result = string.Compare(a, b, comparisonType);

            if (result < 0)
            {
                Console.WriteLine($"'{a}' < '{b}' ({comparisonType})");
            }
            else if (result == 0)
            {
                Console.WriteLine($"'{a}' = '{b}' ({comparisonType})");
            }
            else if (result > 0)
            {
                Console.WriteLine($"'{a}' > '{b}' ({comparisonType})");
            }
        }

        static void EqualityTest(string a, string b, StringComparison comparisonType)
        {
            bool isEqual = string.Equals(a, b, comparisonType);

            if (isEqual)
            {
                Console.WriteLine($"'{a}' = '{b}' ({comparisonType})");
            }
            else
            {
                Console.WriteLine($"'{a}' != '{b}' ({comparisonType})");
            }
        }


        static void PrintSeparator()
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 70));
            Console.WriteLine();
        }
    }
}
