using UnityEngine;

[CreateAssetMenu(menuName="Database Info", fileName="New Database Info")]
public class DatabaseInfoSO : ScriptableObject
{
    public string url;
    public string registerFile;
    public string logInFile;
    public string saveDataFile;
}
