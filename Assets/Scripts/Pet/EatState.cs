using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : IState
{
    private PetController _pet;

    private PetData _petData;

    private PreyDetector _preyDetector;

    private SaveData _saveData;

    private Animator _anim;

    private float _finishTime;

    private float _currentTime;

    private int _animHash = Animator.StringToHash("Eat");

    public EatState(PetController pet)
    {
        _pet = pet;
        _anim = pet.GetComponent<Animator>();
        _finishTime = pet.StateFinishTime[(int)EPetState.Eat];
        _preyDetector = pet.PreyDector;
        _saveData = pet.SaveData;
        _petData = pet.PetData;

    }
    public void Enter()
    {
        Debug.Log("Eat진입");
        Eat();
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

    private void Eat()
    {
        _pet.transform.LookAt(Camera.main.transform.position);
        _anim.Play(_animHash, -1, 0);
        _saveData.GameData.HungryGage += _preyDetector.EatAmount;
        if (_saveData.GameData.HungryGage > _petData.MaxHunGryGage)
        {
            _saveData.GameData.HungryGage = _petData.MaxHunGryGage;
        }
        Debug.Log(_saveData.GameData.HungryGage);
    }
}
