using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    private void Awake()
    {
        IsPaused = false;
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
    }
}
