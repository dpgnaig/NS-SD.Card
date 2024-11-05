using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NS.SD.Card.Extensions
{
    public static class DictionaryExtensions
    {
        public static ObservableCollection<KeyValuePair<TKey, TValue>> ToObservableCollection<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
            => new ObservableCollection<KeyValuePair<TKey, TValue>>(dictionary.ToList());
    }
}
