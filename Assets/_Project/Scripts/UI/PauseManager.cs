using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
    }
}
