using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyDetector : MonoBehaviour
{
    [SerializeField] private float _setfalseTime;

    private bool _isDetectPrey;
    public bool IsDetectPrey { get { return _isDetectPrey; } set { _isDetectPrey = value; } }


    private Coroutine _coroutine;

    private WaitForSeconds _waitTime;

    private void Awake()
    {
        _waitTime = new WaitForSeconds(_setfalseTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Prey")
        {
            _isDetectPrey = true;
            collision.gameObject.SetActive(false);
            _coroutine = StartCoroutine(SetFalseIsDetect());
        }
    }

    IEnumerator SetFalseIsDetect()
    {
        yield return _waitTime;
        _isDetectPrey = false;
        _coroutine = null;

    }
}
