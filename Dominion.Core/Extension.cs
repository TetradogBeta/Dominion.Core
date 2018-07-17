using System;
using System.Collections.Generic;
using System.Text;

namespace Dominion.Core
{
   public static class Extension
    {
        public static T Pop<T>(this IList<T> lst)
        {
            T primer = Peek(lst);
            if (lst.Count > 0)
            {

                lst.RemoveAt(0);
            }

            return primer;
        }
        public static T Peek<T>(this IList<T> lst)
        {
            T primer;
            if (lst.Count > 0)
            {
                primer = lst[0];
            }
            else primer = default(T);
            return primer;
        }
        public static void Push<T>(this IList<T> lst,T valor)
        {
            lst.Insert(0, valor);
        }
    }
}
