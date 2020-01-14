using System;
using System.Text.RegularExpressions;

namespace GildtAPI
{
    public class GlobalFunctions
    {
        // Check if the Id of the object exists.
        public static bool CheckValidId(params string[] ids)
        {
            try
            {
                foreach(string id in ids)
                {
                    // Checks if the id is not empty
                    if (id != null)
                    {
                        int Id = Convert.ToInt32(id);

                        // Checks if the id is a positive number
                        if (Id < 0)
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        //Check if all the inputs are filled in.
        public static bool CheckInputs(params string[] values)
        {
            foreach (string value in values)
            {
                if(String.IsNullOrWhiteSpace(value))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public static class Whitelister
    {
        // Regex to catch all characters except alpha/spaces
        static readonly Regex ALPHA = new Regex("[^a-zA-Z ]+");

        // Regex to catch all characters except alpha/spaces and punctuation
        static readonly Regex TEXT = new Regex("[^a-zA-Z .,!?]+");

        /// <summary>
        /// Gets string filtered through regex only allowing alphabet characters and spaces.
        /// </summary>
        public static string GetAlphaFiltered(string input) =>
            ALPHA.Replace(input, "");
        public static string GetTextFiltered(string input) =>
            TEXT.Replace(input, "");

    }
}
