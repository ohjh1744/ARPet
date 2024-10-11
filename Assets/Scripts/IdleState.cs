using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private PetController _pet;

    private PlayerController _player;

    private Animator _anim;

    private int _animHash = Animator.StringToHash("Idle");
    public IdleState(PetController pet)
    {
        _pet = pet;
        _player = pet.Player;
        _anim = pet.GetComponent<Animator>();
    }
    public  void Enter()
    {
        _anim.Play(_animHash);
    }

    // Update is called once per frame
    public void Update()
    {
        if (_player.IsPlayerDo == EPlayer.Petting)
        {
            _pet.ChangeState(_pet.States[(int)EPetState.Happy]);
        }
        else if (_player.IsPlayerDo == EPlayer.Tickling)
        {
            _pet.ChangeState(_pet.States[(int)EPetState.Ticklish]);
        }
        else if (_player.IsPlayerDo == EPlayer.MakeMove)
        {
            _pet.ChangeState(_pet.States[(int)EPetState.Move]);
        }
    }

    public void LateUpdate()
    {

    }

    public void Exit()
    {

    }

}
