using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseInfoUI : MonoBehaviour
{
    public static DatabaseInfoUI instance;

    // VARIABLES

    [SerializeField] private Text usernameText = null;
    [SerializeField] private Text pointText = null;    

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        usernameText.text = "Username: ";
        pointText.text = "Points: ";
        usernameText.text += DatabaseManager.LoggedIn ? DatabaseManager.currentUser.username : "No User Logged In";
        pointText.text += DatabaseManager.LoggedIn ? DatabaseManager.currentUser.score.ToString() : "No User Logged In";
    }
}
