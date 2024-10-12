using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private Rigidbody _rigid;

    private float _currentTIme;
    void OnEnable()
    {
        _rigid.velocity = Vector3.zero;
        Debug.Log("Prey»ý¼º!");
        _currentTIme = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTIme += Time.deltaTime;
        if(_currentTIme > _lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
}
