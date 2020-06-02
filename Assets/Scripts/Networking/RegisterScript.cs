using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class RegisterScript : MonoBehaviour
{
    [SerializeField] private InputField nameField = null;
    [SerializeField] private InputField passwordField = null;

    [SerializeField] private Button submitButton = null;

    // METHODS

    public void RegisterButton() {
        StartCoroutine(Register());
    }

    IEnumerator Register() {
        var wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("name", nameField.text));
        wwwForm.Add(new MultipartFormDataSection("password", passwordField.text));

        var www = UnityWebRequest.Post(DatabaseManager.registerUrl, wwwForm);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.LogError(www.error);
        }

        if (www.downloadHandler.text == "0") {
            Debug.Log("User created successfully.");
        }
        else {
            Debug.LogError("RegisterScript::Register() --- Error #" + www.downloadHandler.text);
        }
    }
}
