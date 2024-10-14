using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using System.Text;
using TMPro;
public enum EPetState {Idle, Eat, Move, Ticklish, Happy, Size}
public class PetController : MonoBehaviour
{
    private IState _currentState;

    private IState[] _states = new IState[(int)EPetState.Size];
    public IState[] States { get { return _states; } private set { } }

    private Coroutine _routine;
    public IEnumerator IEnumerator { get; set; }

    [SerializeField] private PlayerController _player;
    public PlayerController Player { get { return _player; } private set { } }

    [SerializeField] private PetData _petData;
    public PetData PetData { get { return _petData; } private set { } }

    [SerializeField] private PreyDetector _preyDetector;
    public PreyDetector PreyDector {  get { return _preyDetector; } private set { } }


    [SerializeField] private Transform _spine;
    public Transform Spine { get { return _spine; } private set { } }

    [SerializeField] private Transform[] _legs;

    public Transform[] Legs { get { return _legs; } private set { } }

    [SerializeField] private float _moveAnimTime;
    public float MoveAnimTime { get { return _moveAnimTime; } private set { } }

    [SerializeField] private float[] _stateFinishTime;
    public float[] StateFinishTime { get { return _stateFinishTime; } private set { } }

    [SerializeField] private Slider _hungrySlider;

    [SerializeField] private TextMeshProUGUI _hungryText;

    private StringBuilder _sb = new StringBuilder();

    private SaveData _saveData;
    public  SaveData SaveData { get { return _saveData; } private set { } }

    private float _currentTime;

    void Awake()
    {
        _saveData = DataManager.Instance.SaveData;
        DataManager.Instance.ResetHungryGage = PetData.MaxHunGryGage;
        DataManager.Instance.Load();
    }

    private void OnEnable()
    {
        _saveData.GameData.OnHungryGageChange += UpdateHungryGage;
    }
    void Start()
    {
        UpdateHungryGage();
        LoadHungryGage();
        States[(int)EPetState.Idle] = new IdleState(this);
        States[(int)EPetState.Eat] = new EatState(this);
        States[(int)EPetState.Move] = new MoveState(this);
        States[(int)EPetState.Ticklish] = new TicklishState(this);
        States[(int)EPetState.Happy] = new HappyState(this);
        ChangeState(States[(int)EPetState.Idle]);
    }
    private void OnDisable()
    {
        _saveData.GameData.OnHungryGageChange -= UpdateHungryGage;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _petData.DecreaseHungryTime)
        {
            if(_saveData.GameData.HungryGage > 0f)
            {
                _saveData.GameData.HungryGage -= PetData.DecreaseHungryGage;
            }
            _currentTime = 0f;
        }
        _currentState.Update();
    }
    public void ChangeState(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter();
    }

    public void StartRoutine()
    {
        _routine = StartCoroutine(IEnumerator);
    }

    public void StopRoutine()
    {
        if(_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
        }
    }

    public void LoadHungryGage()
    {
        DateTime quitLastTIme = DateTime.Parse(_saveData.GameData.ExitTime);
        TimeSpan timeDiff = DateTime.Now - quitLastTIme;
        double minutes = timeDiff.TotalSeconds / _petData.DecreaseHungryTime;
        int intMinutes = (int)minutes;
        _saveData.GameData.HungryGage -= intMinutes * _petData.DecreaseHungryGage;
        if(_saveData.GameData.HungryGage < 0f)
        {
            _saveData.GameData.HungryGage = 0f;
        }

    }
    public void UpdateHungryGage()
    {
        _hungrySlider.value = _saveData.GameData.HungryGage / PetData.MaxHunGryGage;
        _sb.Clear();
        _sb.Append(_saveData.GameData.HungryGage);
        _hungryText.SetText(_sb);
    }


}
