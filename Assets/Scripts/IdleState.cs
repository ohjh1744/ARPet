using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private PetController _pet;

    private PreyDetector _preyDetector;

    private PlayerController _player;

    private Animator _anim;

    private int _animHash = Animator.StringToHash("Idle");
    public IdleState(PetController pet)
    {
        _pet = pet;
        _player = pet.Player;
        _anim = pet.GetComponent<Animator>();
        _preyDetector = _pet.PreyDector;
    }
    public  void Enter()
    {
        _anim.Play(_animHash);
    }

    // Update is called once per frame
    public void Update()
    {
        if(_preyDetector.IsDetectPrey == true)
        {
            _pet.ChangeState(_pet.States[(int)EPetState.Eat]);
        }
        if (_player.IsPlayerDo == EPlayer.TouchHead)
        {
            _pet.ChangeState(_pet.States[(int)EPetState.Happy]);
        }
        else if (_player.IsPlayerDo == EPlayer.TouchWaist)
        {
            _pet.ChangeState(_pet.States[(int)EPetState.Ticklish]);
        }
        else if (_player.IsPlayerDo == EPlayer.TouchSpace)
        {
            _pet.ChangeState(_pet.States[(int)EPetState.Move]);
        }
    }


    public void Exit()
    {

    }

}
