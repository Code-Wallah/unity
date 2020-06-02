using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    [SerializeField] private InputField nameField = null;
    [SerializeField] private InputField passwordField = null;

    [SerializeField] private Button submitButton = null;
    [SerializeField] private string sceneName = null;

    // METHODS

    public void LoginButton() {
        StartCoroutine(Login());
    }

    IEnumerator Login() {
        var wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("name", nameField.text));
        wwwForm.Add(new MultipartFormDataSection("password", passwordField.text));

        var www = UnityWebRequest.Post(DatabaseManager.logInUrl, wwwForm);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text[0] == '0') {
            var username = nameField.text;
            var score = int.Parse(www.downloadHandler.text.Split('\t')[1]);

            DatabaseManager.LogIn(username, score);
            Debug.Log("Logged in");

            SceneManager.LoadScene(sceneName);
        }
        else {
            Debug.LogError("User Login Failed. Error #" + www.downloadHandler.text);
        }
    }
}
