using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerAim : MonoBehaviour
{
    private VideoPlayerHelper selected;
    [Header("Object References")]
    [SerializeField] private Image crosshair = null;
    [SerializeField] private Text popUpText = null;

    [Header("Preferences")]
    [SerializeField] private float aimRange = 3f;

    [Space(10)]
    [Tooltip("If false, the crosshair will change to the specified color")]
    [SerializeField] private bool showImage = false;
    [SerializeField] private Sprite defaultImage = null;
    [SerializeField] private Sprite targetingImage = null;

    [Space(10)]
    [SerializeField] private bool changeColor = false;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color newColor = Color.white;

    private void Start() {
        if (changeColor) crosshair.color = defaultColor;
    }

    private void Update() {
        if (selected != null) {

            if (changeColor) crosshair.color = defaultColor;
            if (showImage) crosshair.sprite = defaultImage;

            popUpText.gameObject.SetActive(false);
            selected = null;
        }

        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, aimRange)) {

            var selection = hit.transform;
            var video = selection.GetComponent<VideoPlayerHelper>();
            if (video == null) return;

            if (changeColor) crosshair.color = newColor;
            if (showImage)  crosshair.sprite = targetingImage;
            
            popUpText.gameObject.SetActive(true);
            selected = video;

        }

        if (selected != null) {
            if (Input.GetMouseButtonDown(0)) {
                selected.Play();
                DatabaseManager.currentUser.score += 10;
                DatabaseManager.instance.SaveData();
            }
        }
    }
}