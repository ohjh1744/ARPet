using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameData 
{
    [SerializeField]private string _exitTIme;
    public string ExitTime { get { return _exitTIme; } set { _exitTIme = value;} }

    [SerializeField]private float _hungryGage;
    public  float HungryGage {  get { return _hungryGage; } set { _hungryGage = value; OnHungryGageChange?.Invoke(); } }

    public event UnityAction OnHungryGageChange;
}

[CreateAssetMenu(menuName = "ScriptableObjects/SaveData")]
public class SaveData : ScriptableObject
{
    [SerializeField] private GameData _gameData;
    public GameData GameData { get { return _gameData; }  set { _gameData = value; } }
}
