using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;

    // Singleton pattern to ensure we have one instance of CoroutineRunner in the scene
    private static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("CoroutineRunner");
                _instance = obj.AddComponent<CoroutineRunner>();
                Object.DontDestroyOnLoad(obj); // Persist across scene loads
            }
            return _instance;
        }
    }

    // This method starts a coroutine from the static class
    public static void StartRoutine(IEnumerator routine)
    {
        Instance.StartCoroutine(routine);
    }
}
