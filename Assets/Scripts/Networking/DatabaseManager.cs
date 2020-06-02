using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private DatabaseInfoSO info = null;
    private static DatabaseInfoSO Info = null;

    public static string url { get { return Info.url; } }
    public static string registerUrl { get { return Info.url + "/" + Info.registerFile; } }
    public static string logInUrl { get { return Info.url + "/" + Info.logInFile; } }
    private static string saveDataUrl { get { return Info.url + "/" + Info.saveDataFile; } }

    public static UserInfoSO currentUser;
    public static bool LoggedIn { get { return currentUser != null; } }

    public static DatabaseManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy (gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Info = info;
        currentUser = null;
    }

    public static void LogIn(string username, int score) {
        currentUser = ScriptableObject.CreateInstance<UserInfoSO>();
        currentUser.username = username;
        currentUser.score = score;
    }

    public static void LogOut() {
        currentUser = null;
    }

    public void SaveData() {
        if (!LoggedIn) {
            Debug.LogError("DatabaseManager::SaveData() --- User is not logged in!");
            return;
        }

        StartCoroutine(SaveDataCoroutine());
    }

    static IEnumerator SaveDataCoroutine() {
        var wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("name", currentUser.username));
        wwwForm.Add(new MultipartFormDataSection("points", currentUser.score.ToString()));

        var www = UnityWebRequest.Post(saveDataUrl, wwwForm);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "0") {
            Debug.Log("Saved data successfully");
        }
        else {
            Debug.LogError("DatabaseManager::SaveDataCoroutine() --- Save failed. Error #" + www.downloadHandler.text);
        }

    }
}
