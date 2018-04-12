using EDictionary.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Presenters
{
    public static class History
    {
        public static List<string> history { get; set; }
        public static List<string> upHistory { get; set; }


        public static void Add(string wordID)
        {
            history.Add(wordID);
        }

        public static string Pop()
        {
           return history.Pop();
        }

        public static void AddLink(string wordID)
        {
            upHistory.Add(wordID);
        }

        public static string PopLink()
        {
            return upHistory.Pop();
        }
    }
}
