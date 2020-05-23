using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Data
{
    public class FlightId
    {
        private static readonly Random random = new Random();
        private static readonly List<string> idList = new List<string>();


        /**
         * Constructor
         **/
        public FlightId() { }


        /**
         * Get random 8-Length string 
         **/
        public static string GetRandomId()
        {
            var bigLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var smallLetters = "abcdefghijklmnopqrstuvwxyz";
            var numbers = "0123456789";
            var stringChars = new char[8];
            string newId;

            do
            {
                // the first two chars will be a big letters
                stringChars[0] = bigLetters[random.Next(bigLetters.Length)];
                stringChars[1] = bigLetters[random.Next(bigLetters.Length)];
                // the first two chars will be a small letters
                stringChars[2] = smallLetters[random.Next(smallLetters.Length)];
                stringChars[3] = smallLetters[random.Next(smallLetters.Length)];
                // the next 4 chars will be numbers
                for (int i = 4; i < stringChars.Length; i++)
                {
                    stringChars[i] = numbers[random.Next(numbers.Length)];
                }
                newId = new String(stringChars);
            } while (!IsUnique(newId));

            idList.Add(newId);
            return newId;
        }

        /**
         * Checks if the string is unique
         **/
        private static bool IsUnique(string newId)
        {
            foreach (string value in idList)
            {
                if (value.Equals(newId))
                {
                    return false;
                }
            }
            return true;
        }
    }
}