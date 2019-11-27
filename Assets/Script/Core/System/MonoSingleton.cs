using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = GameObject.FindObjectOfType(typeof(T)) as T;
            if (_instance != null) return _instance;
            GameObject obj = new GameObject();
            _instance = obj.AddComponent<T>();
            string[] str = typeof(T).ToString().Split('.');
            obj.name = str[str.Length - 1];
            if (_instance.transform.root != null)
                DontDestroyOnLoad(_instance.transform.root);
            else
                DontDestroyOnLoad(_instance);
            return _instance;
        }
    }
    private void Awake()
    {
        Initialization();
    }
    protected virtual void Initialization()
    {
    }
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {

    }
}
