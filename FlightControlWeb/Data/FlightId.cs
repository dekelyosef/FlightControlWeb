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

        public FlightId() { }

        public static string GetRandomId()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var numbers = "0123456789";
            var stringChars = new char[8];
            string newId;

            do
            {
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
                newId = new String(stringChars);
            } while (!IsUnique(newId));

            idList.Add(newId);
            return newId;
        }

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