using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Text ScoreText;
    public InputField InputName;
    public int score;
    public string name;
    public GameObject Canvas;
    // Start is called before the first frame update
    private void Awake()
    {
        LoadScore();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }
    public void StartNew()
    {
        
        if (InputName.text.Equals("Hola")){
            Debug.Log("No funciona");
        }
        else
        {
            name = InputName.text;
            SaveScore();
            Canvas.SetActive(false);
            SceneManager.LoadScene(1);
        }
        
       
        
    }
    public void Exit()
    {
        MenuManager.Instance.SaveScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.name = name;
        data.score = score;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            score = data.score;
            name = data.name;
            ScoreText.text = $"Score {name} {score}";

            
            




        }
    }
}
