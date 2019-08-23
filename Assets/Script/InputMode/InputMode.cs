using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Knight.InputMode
{
    public interface IInputMode
    {
        Vector3 DirextionInput();
        bool FireInput();
    }
}
