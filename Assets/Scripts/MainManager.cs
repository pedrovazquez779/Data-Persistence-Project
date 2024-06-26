using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
# if UNITY_EDITOR
using UnityEditor;
# endif

public class MainManager : MonoBehaviour
{
    private const string fileName = "/highscore.json"; 
    
    public static MainManager Instance;
    public string playerName;
    
    public int bestScorePoints;
    public string bestScoreName;

    private void Awake()
    {
        InstantiateMainManagerSingleton();
        LoadHighScore();
    }

    private void InstantiateMainManagerSingleton()
    {
        // This is to have a singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        // TODO persist high score
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = bestScorePoints;
        data.playerName = bestScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + fileName, json);
    }
    
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + fileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScorePoints = data.highScore;
            bestScoreName = data.playerName;
        }
    }
}

[System.Serializable]
class SaveData
{
    public int highScore;
    public string playerName;
}