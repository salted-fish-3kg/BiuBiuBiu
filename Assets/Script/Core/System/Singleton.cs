using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Knight.Core
{
    public class Singleton<T> where T :new()
    {
        private static T _Instance;
        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new T();
                }
                return _Instance;
            }
        }
        static string name;
    }
}