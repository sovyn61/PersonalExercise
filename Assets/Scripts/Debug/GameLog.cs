//#define LOG

#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameLog
{
    [System.Diagnostics.Conditional("LOG_ENABLE")]
    public static void Log(string message)
    {
        UnityEngine.Debug.Log(message);
    }

    [System.Diagnostics.Conditional("LOG_ENABLE")]
    public static void Error(string message)
    {
        UnityEngine.Debug.LogError(message);
    }

    [System.Diagnostics.Conditional("LOG_ENABLE")]
    public static void Alert(string message)
    {
        UnityEngine.Debug.LogWarning(message);
    }
}
