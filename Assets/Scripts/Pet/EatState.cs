using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : IState
{
    private PetController _pet;

    private Animator _anim;

    private float _finishTime;

    private float _currentTime;

    private int _animHash = Animator.StringToHash("Eat");

    public EatState(PetController pet)
    {
        _pet = pet;
        _anim = pet.GetComponent<Animator>();
        _finishTime = pet.StateFinishTime[(int)EPetState.Eat];
    }
    public void Enter()
    {
        Debug.Log("Eat진입");
        _pet.transform.LookAt(Camera.main.transform.position);
        _anim.Play(_animHash, -1, 0);
        _currentTime = 0;
    }

    // Update is called once per frame
    public void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _finishTime)
        {
            _pet.ChangeState(_pet.States[(int)EPetState.Idle]);
        }
    }

    public void Exit()
    {
        Debug.Log("Eat나감");
        _pet.Player.IsPlayerDo = EPlayer.Idle;
    }
}
