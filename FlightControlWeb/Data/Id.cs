using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControlWeb.Data
{
    public class Id
    {
        private static readonly Random random = new Random();

        public static string GetRandomId()
        {
            // const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            // return new string(Enumerable.Repeat(chars, 8)
            //   .Select(s => s[random.Next(s.Length)]).ToArray());

            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var numbers = "0123456789";
            var stringChars = new char[8];

            for (int i = 0; i < stringChars.Length; i++)
            {
                if (i > 3)
                {
                    stringChars[i] = numbers[random.Next(numbers.Length)];
                }
                else
                {
                    stringChars[i] = letters[random.Next(letters.Length)];
                }
            }
            return new String(stringChars);
        }
    }
}