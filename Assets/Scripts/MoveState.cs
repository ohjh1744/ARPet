using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveState : IState
{
    private PetController _pet;

    private Animator _anim;

    private float _speed;

    private int _animHash = Animator.StringToHash("Walk");

    private WaitForSeconds _seconds;
    public MoveState(PetController pet)
    {
        _pet = pet;
        _speed = _pet.PetData.MoveSpeed;
        _anim = pet.GetComponent<Animator>();
        _seconds = new WaitForSeconds(_pet.MoveAnimTime);
        _pet.IEnumerator = ActiveAnim();
    }

    public  void Enter()
    {
        Debug.Log("Move진입");
        _pet.StartRoutine();
    }

    // Update is called once per frame
    public void Update()
    {
        if(_pet.transform.position == _pet.Player.TouchPos)
        {
            _pet.ChangeState(_pet.States[(int)EPetState.Idle]);
        }
        Move();
    }

    public void LateUpdate()
    {

    }

    public void Exit()
    {
        Debug.Log("Move나감");
        _pet.StopRoutine();
        _pet.Player.IsPlayerDo = EPlayer.Idle;
    }

    private void Move()
    {
        _pet.transform.LookAt(_pet.Player.TouchPos);
        _pet.transform.position = Vector3.MoveTowards(_pet.transform.position, _pet.Player.TouchPos, Time.deltaTime * _speed); 
    }

    IEnumerator ActiveAnim()
    {
        while (true)
        {
            Debug.Log("movemove");
            _anim.Play(_animHash, -1, 0);
            yield return _seconds;
        }
    }
}
