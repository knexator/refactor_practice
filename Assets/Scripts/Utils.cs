using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Taken from https://stackoverflow.com/q/62907548
public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;

    protected virtual void Awake()
    {
        if (instance != null && instance != this) {
            string typename = typeof(T).Name;
            Debug.LogWarning($"More that one instance of {typename} found.");
            Destroy(this);
        } else {
            instance = this as T;
        }
    }
}

public abstract class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    public static T instance;

    protected virtual void Awake()
    {
        if (instance != null && instance != this) {
            string typename = typeof(T).Name;
            Debug.LogWarning($"More that one instance of {typename} found.");
            Destroy(this);
        } else {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
