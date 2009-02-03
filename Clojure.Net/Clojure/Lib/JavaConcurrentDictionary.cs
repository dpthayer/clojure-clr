﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clojure.lang
{
    /// <summary>
    /// Faking a few of the methods from the Java ConcurrentHashTable class.
    /// </summary>
    public class JavaConcurrentDictionary<TKey, TValue>
    {
        Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

        public TValue Get(TKey key)
        {
            lock (_dict)
            {
                TValue val;
                return _dict.TryGetValue(key, out val) ? val : default(TValue);
            }
        }

        public TValue PutIfAbsent(TKey key, TValue val)
        {
            lock (_dict)
            {
                TValue existingVal;
                if (_dict.TryGetValue(key, out existingVal))
                    return existingVal;
                else
                {
                    _dict[key] = val;
                    return default(TValue);
                }
            }
        }

        public TValue Remove(TKey key)
        {
            lock ( _dict )
            {
                TValue existingVal;
                if ( _dict.TryGetValue(key,out existingVal) )
                {
                    _dict.Remove(key);
                    return existingVal;
                }
                else
                    return default(TValue);
            }
        }

        public TValue[] Values
        {
            get
            {
                lock (_dict)
                {
                    Dictionary<TKey, TValue>.ValueCollection coll = _dict.Values;
                    TValue[] values = new TValue[coll.Count];
                    coll.CopyTo(values, 0);
                    return values;
                }
            }
        }

    }
}