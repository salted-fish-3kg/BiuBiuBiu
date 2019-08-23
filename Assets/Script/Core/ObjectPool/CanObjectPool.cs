using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.Core;
using System;

namespace Knight.Core
{
    /// <summary>
    /// 请给reset赋值；
    /// </summary>
    public abstract class CanObjectPool : MonoBehaviour
    {
        public void Reset()
        {
            Debug.Log(2333);
        }
    }
}