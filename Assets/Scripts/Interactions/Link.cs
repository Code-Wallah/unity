using UnityEngine;
using System.Runtime.InteropServices;

public class Link : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void openWindow(string url);

    public void OpenLinkJSPlugin(string url) {
        #if !UNITY_EDITOR
        openWindow(url);
        #endif

        #if UNITY_EDITOR
        Application.OpenURL(url);
        #endif
    }
}
