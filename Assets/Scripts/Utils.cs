using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Taken from https://stackoverflow.com/q/62907548
public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;
    public static bool dontDestroyOnLoad;

    protected virtual void Awake()
    {
        if (instance != null && instance != this) {
            string typename = typeof(T).Name;
            Debug.LogWarning($"More that one instance of {typename} found.");
            Destroy(this);
        } else {
            instance = this as T;

            if (dontDestroyOnLoad) {
                DontDestroyOnLoad(this.gameObject);
            } 
        }
    }
}
