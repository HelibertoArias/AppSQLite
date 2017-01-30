using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppSQLite.Helpers
{
    public static class ObservableCollectionHelper
    {
        public static void Sort<TSource, TKey>(this Collection<TSource> source, 
                                                List<TSource> data, 
                                                Func<TSource, TKey> keySelector)
        {
            List<TSource> sortedList = data.OrderBy(keySelector).ToList();

            if (source == null)
                source = new Collection<TSource>(); 
            else
                source.Clear();

            foreach (var sortedItem in sortedList)
                source.Add(sortedItem);
        }
    }
}