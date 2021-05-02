using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UCPortal.Utils
{
    public class Function
    {
        public static String getClassification(String classification)
        {
            string newClass = string.Empty;

            if (classification.Equals("O"))
                newClass = "OLD STUDENT";
            else if(classification.Equals("H"))
                newClass = "NEW STUDENT";
            else if (classification.Equals("R"))
                newClass = "RETURNEE";
            else if (classification.Equals("C"))
                newClass = "CROSS ENROLEE";
            else if (classification.Equals("T"))
                newClass = "TRANSFEREE";
            else if (classification.Equals("S"))
                newClass = "SHIFTEE";

            return newClass;
        }

        public static String deClassification(String classification)
        {
            string newClass = string.Empty;

            if (classification.Equals("OLD STUDENT"))
                newClass = "O";
            else if (classification.Equals("NEW STUDENT"))
                newClass = "H";
            else if (classification.Equals("RETURNEE"))
                newClass = "R";
            else if (classification.Equals("CROSS ENROLEE"))
                newClass = "S";
            else if (classification.Equals("TRANSFEREE"))
                newClass = "T";
            else if (classification.Equals("SHIFTEES"))
                newClass = "S";

            return newClass;
        }
        public static string Modulo10(string num)
        {
            int sum = 0, n = 1, d;
            for (int i = num.Length - 1; i >= 0; i--)
            {
                d = Convert.ToInt32(num.Substring(i, 1));
                sum += (n++ % 2 == 0) ? d : (d < 5) ? d * 2 : d * 2 - 9;
            }
            return num + (10 - (sum % 10)) % 10;
        }

        public static int LevenshteinDistance(string source, string target)
        {
            // degenerate cases
            if (source == target) return 0;
            if (source.Length == 0) return target.Length;
            if (target.Length == 0) return source.Length;

            // create two work vectors of integer distances
            int[] v0 = new int[target.Length + 1];
            int[] v1 = new int[target.Length + 1];

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
                v0[i] = i;

            for (int i = 0; i < source.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0

                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                // use formula to fill in the rest of the row
                for (int j = 0; j < target.Length; j++)
                {
                    var cost = (source[i] == target[j]) ? 0 : 1;
                    v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                for (int j = 0; j < v0.Length; j++)
                    v0[j] = v1[j];
            }

            return v1[target.Length];
        }

        public static double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = LevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        public static string EncodeBase64(string password)
        {
            string pass = password;
            for (int counter = 0; counter < 5; counter++)
            {
                pass = ReverseStringDirect(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(pass)));
            }
            return pass;
        }

        public static string DecodeBase64(string password)
        {
            string pass = password;
            for (int counter = 0; counter < 5; counter++)
            {
                pass = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(ReverseStringDirect(pass)));
            }
            return pass;
        }

        public static string ReverseStringDirect(string s)
        {
            char[] array = new char[s.Length];
            int forward = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                array[forward++] = s[i];
            }
            return new string(array);
        }

        public static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static string categoryIdBanilad(String courseAbbr)
        {
            string category = string.Empty;

            if (courseAbbr.ToUpper().Equals("BSA"))
            {
                category = "106";
            }
            else if (courseAbbr.ToUpper().Equals("BSAIS"))
            {
                category = "107";
            }
            else if (courseAbbr.ToUpper().Equals("BSACT"))
            {
                category = "176";
            }
            else if (courseAbbr.ToUpper().Equals("BSBA"))
            {
                category = "105";
            }
            else if (courseAbbr.ToUpper().Equals("BSMA"))
            {
                category = "175";
            }
            else if (courseAbbr.ToUpper().Equals("ACT"))
            {
                category = "98";
            }
            else if (courseAbbr.ToUpper().Equals("BSIS"))
            {
                category = "97";
            }
            else if (courseAbbr.ToUpper().Equals("BSIT"))
            {
                category = "95";
            }
            else if (courseAbbr.ToUpper().Equals("BSCRIM"))
            {
                category = "108";
            }
            else if (courseAbbr.ToUpper().Equals("BSCPE"))
            {
                category = "100";
            }
            else if (courseAbbr.ToUpper().Equals("BSECE"))
            {
                category = "99";
            }
            else if (courseAbbr.ToUpper().Equals("BSHM"))
            {
                category = "110";
            }
            else if (courseAbbr.ToUpper().Equals("BSHRM"))
            {
                category = "169";
            }
            else if (courseAbbr.ToUpper().Equals("BSMT"))
            {
                category = "174";
            }
            else if (courseAbbr.ToUpper().Equals("BSN"))
            {
                category = "114";
            }
            else if (courseAbbr.ToUpper().Equals("BSPSY"))
            {
                category = "209";
            }
            else if (courseAbbr.ToUpper().Equals("BEED"))
            {
                category = "103";
            }
            else if (courseAbbr.ToUpper().Equals("BSED"))
            {
                category = "102";
            }
            else if (courseAbbr.ToUpper().Equals("BSTOUR"))
            {
                category = "172";
            }
            else if (courseAbbr.ToUpper().Equals("BSTM"))
            {
                category = "162";
            }
            else if (courseAbbr.ToUpper().Equals("HCS"))
            {
                category = "208";
            }
            else if (courseAbbr.ToUpper().Equals("DM"))
            {
                category = "149";
            }

            return category;
        }

        public static string categoryIdLM(String courseAbbr)
        {
            string category = string.Empty;

            if (courseAbbr.ToUpper().Equals("BSED-CPE"))
            {
                category = "185";
            }
            else if (courseAbbr.ToUpper().Equals("BSHRM"))
            {
                category = "178";
            }
            else if (courseAbbr.ToUpper().Equals("SEACAST"))
            {
                category = "211";
            }
            else if (courseAbbr.ToUpper().Equals("BSHM"))
            {
                category = "179";
            }
            else if (courseAbbr.ToUpper().Equals("BSTM"))
            {
                category = "180";
            }
            else if (courseAbbr.ToUpper().Equals("BSBAMA"))
            {
                category = "159";
            }
            else if (courseAbbr.ToUpper().Equals("BSBAFM"))
            {
                category = "158";
            }
            else if (courseAbbr.ToUpper().Equals("BSBAHR"))
            {
                category = "157";
            }
            else if (courseAbbr.ToUpper().Equals("BSBAMM"))
            {
                category = "156";
            }
            else if (courseAbbr.ToUpper().Equals("BSA"))
            {
                category = "155";
            }
            else if (courseAbbr.ToUpper().Equals("BSIT"))
            {
                category = "24";
            }
            else if (courseAbbr.ToUpper().Equals("BSCS"))
            {
                category = "142";
            }
            else if (courseAbbr.ToUpper().Equals("BSCRIM"))
            {
                category = "161";
            }
            else if (courseAbbr.ToUpper().Equals("MSCJ"))
            {
                category = "163";
            }
            else if (courseAbbr.ToUpper().Equals("BSA"))
            {
                category = "173";
            }
            else if (courseAbbr.ToUpper().Equals("BSED-S"))
            {
                category = "168";
            }
            else if (courseAbbr.ToUpper().Equals("BSED-E"))
            {
                category = "167";
            }
            else if (courseAbbr.ToUpper().Equals("BSED-F"))
            {
                category = "170";
            }
            else if (courseAbbr.ToUpper().Equals("BSED-M"))
            {
                category = "166";
            }
            else if (courseAbbr.ToUpper().Equals("BEED"))
            {
                category = "165";
            }
            else if (courseAbbr.ToUpper().Equals("BSCOE"))
            {
                category = "144";
            }
            else if (courseAbbr.ToUpper().Equals("BSEE"))
            {
                category = "146";
            }
            else if (courseAbbr.ToUpper().Equals("BSECE"))
            {
                category = "145";
            }
            else if (courseAbbr.ToUpper().Equals("BSME"))
            {
                category = "148";
            }
            else if (courseAbbr.ToUpper().Equals("BSIE"))
            {
                category = "147";
            }
            else if (courseAbbr.ToUpper().Equals("BSMARE") || courseAbbr.ToUpper().Equals("(NSA)MARE"))
            {
                category = "192";
            }
            else if (courseAbbr.ToUpper().Equals("BSMT") || courseAbbr.ToUpper().Equals("(NSA)MT"))
            {
                category = "191";
            }
            else if (courseAbbr.ToUpper().Equals("BSN"))
            {
                category = "153";
            }

            return category;
        }

        public static string statDesc(int stat)
        {
            string stats = string.Empty;

            if (stat.ToString() == "1" || stat.ToString() == "3")
            {
                stats = "add";
            }
            else if (stat.ToString() == "2")
            {
                stats = "del";
            }

            return stats;
        }
    }
}
