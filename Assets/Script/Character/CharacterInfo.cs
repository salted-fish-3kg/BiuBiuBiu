using Knight.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CharacterInfo: ScriptableObject
{
    [SerializeField]
    public List<info> characters;
    [Serializable]
    public class info
    {
        public GameObject prefab;
        public CharacterStatus character;
    }
}
