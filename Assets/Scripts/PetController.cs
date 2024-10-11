using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public enum EPetState {Idle, Move, Ticklish, Happy, Size}
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

    [SerializeField] private Transform _spine;

    public Transform Spine { get { return _spine; } private set { } }

    [SerializeField] private Transform[] _legs;

    public Transform[] Legs { get { return _legs; } private set { } }

    [SerializeField] private float _moveAnimTime;
    public float MoveAnimTime { get { return _moveAnimTime; } private set { } }

    [SerializeField] private float[] _stateFinishTime;
    public float[] StateFinishTime { get { return _stateFinishTime; } private set { } }

    private void Awake()
    {
        States[(int)EPetState.Idle] = new IdleState(this);
        States[(int)EPetState.Move] = new MoveState(this);
        States[(int)EPetState.Ticklish] = new TicklishState(this);
        States[(int)EPetState.Happy] = new HappyState(this);
    }

    void Start()
    {
        ChangeState(States[(int)EPetState.Idle]);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.Update();
    }

    private void LateUpdate()
    {
        _currentState.LateUpdate();
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
}
