using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (_instance == null) return default(T);
            return _instance;
        }
    }
    private static T _instance;

    void Awake()
    {
        if (null == _instance)
        {
            _instance = gameObject.GetComponent<T>();
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
