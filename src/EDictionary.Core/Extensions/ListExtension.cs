using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Extensions
{
    public static class ListExtension
    {
        public static T Pop<T>(this List<T> list)
        {
            T last = list[list.Count - 1];

            list.RemoveAt(list.Count - 1);
            return last;
        }
    }
}
