using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VideoPlayerHelper : MonoBehaviour
{
    [System.Serializable]
    public class PressEvent : UnityEvent { }

    public PressEvent OnPress = new PressEvent();

    public void Play() {
        OnPress.Invoke();
    }
}
