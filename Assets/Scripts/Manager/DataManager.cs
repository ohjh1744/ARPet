using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("instance가 null이라 만들어줌");
                SetupInstance();
            }

            return _instance;
        }
    }

    [SerializeField] private SaveData _saveData;
    public SaveData SaveData { get { return _saveData; } private set { } }

    //Reset 
    private float _resetHungryGage;
    public float ResetHungryGage { get { return _resetHungryGage; }  set { _resetHungryGage = value; } }

    private void Awake()
    {
        if (_instance == null)
        {
            SetupInstance();
        }
        else
        {
            Debug.Log("Destory");
            if(SceneManager.GetActiveScene().buildIndex != 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private static void SetupInstance()
    {
        _instance = FindAnyObjectByType<DataManager>();
        if(_instance != null)
        {
            DontDestroyOnLoad(_instance.gameObject);
        }
        else
        {
            Debug.LogWarning("씬에 싱글톤 오브젝트 x");
        }
    }

    private void OnApplicationQuit()
    {
        DateTime now = DateTime.Now;

        // DateTime을 문자열로 변환
        string dateTimeString = now.ToString("o");
        _saveData.GameData.ExitTime = dateTimeString;

        Save();
    }

    private void Save()
    {
        StringBuilder path = new StringBuilder();
        path.Append(Application.persistentDataPath).Append("/Save");
        if (Directory.Exists(path.ToString()) == false)
        {
            Directory.CreateDirectory(path.ToString());
        }

        string json = JsonUtility.ToJson(SaveData.GameData);
        File.WriteAllText($"{path}/SaveFile.txt", json);
    }

    public void Load()
    {
        StringBuilder path = new StringBuilder();
        path.Append(Application.persistentDataPath).Append($"/Save/SaveFile.txt");
        if (File.Exists(path.ToString()) == false)
        {
            Reset();
            Debug.Log("Cant Find Save File");
            return;
        }
        string json = File.ReadAllText(path.ToString());
        SaveData.GameData = JsonUtility.FromJson<GameData>(json);
        Debug.Log("Comoplete");
        Debug.Log($"{Application.persistentDataPath}");

    }

    private void Reset()
    {
        _saveData.GameData.HungryGage = _resetHungryGage;
        _saveData.GameData.ExitTime = DateTime.Now.ToString("o");
    }
}
