using UnityEngine;

public class Settings : MonoBehaviour
{
    private bool isFullScreen = true;

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
}
