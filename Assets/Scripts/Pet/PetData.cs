using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetData : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } private set { } }

    [SerializeField] private float _maxHungryGage;
    public float MaxHunGryGage { get { return _maxHungryGage; } private set { } }

    [SerializeField] private float _decreaseHungryGage;
    public float DecreaseHungryGage { get { return _decreaseHungryGage; } private set { } }

    [SerializeField] private float _decreaseHungryTime;
    public float DecreaseHungryTime { get { return _decreaseHungryTime; } private set { } }
}
