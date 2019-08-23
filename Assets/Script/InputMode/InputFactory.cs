using Knight.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Knight.InputMode
{
    public class InputFactory : Singleton<InputFactory>
    {
        public Func<Vector3> GetDirectionInputFunc(RuntimePlatform platform)
        {
            return null;
        }
        public Func<bool> GetFireInputFunc(RuntimePlatform platform)
        {
            return null;
        }
    }
}