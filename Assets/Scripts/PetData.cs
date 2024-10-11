using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetData : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } private set { } }
}
