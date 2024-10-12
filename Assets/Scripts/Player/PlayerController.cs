using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum EPlayer { Idle,  TouchSpace, TouchWaist, TouchHead }
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _raycastManager;

    [SerializeField] private EPlayer _isPlayerDo;

    [SerializeField] private PullManager _pullManager;

    public EPlayer IsPlayerDo { get { return _isPlayerDo; } set { _isPlayerDo = value; } }

    public Vector3 TouchPos { get; private set; }

    private Dictionary<string, EPlayer> actDic = new Dictionary<string, EPlayer>
    {
        { "PetWaist", EPlayer.TouchWaist },
        { "PetHead", EPlayer.TouchHead }
    };

    [SerializeField] private Transform _throwPreyPos;

    [SerializeField] private float _throwPower;

    [SerializeField] private float _setThrowPowerdegree;

    private float _throwSpeed;

    private Vector2 _dragLastPosition;

    private Vector2 _dragDistance;

    private void Update()
    {
        Touch();
    }

    private void Touch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch!!!!");
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit hit, 100) == (true || false))
                {
                    if (actDic.TryGetValue(hit.collider.tag, out EPlayer action))
                    {
                        Debug.Log("Pet탐지함!");
                        IsPlayerDo = action;
                    }
                    else
                    {
                        //디버깅 결과 Plane이 탐지되고 나서, 그 영역을 눌렀을때 로그 확인.
                        Debug.Log("Pet탐지못함!");
                        //AR Ray
                        List<ARRaycastHit> hits = new List<ARRaycastHit>();

                        if (_raycastManager.Raycast(ray, hits))
                        {
                            TouchPos = hits[0].pose.position;
                            IsPlayerDo = EPlayer.TouchSpace;
                            Debug.Log($"위치: {TouchPos}");
                        }
                    }
                }

            }

        }

    }

    public void CreatePrey()
    {
        Debug.Log("CreatePrey");
        _dragLastPosition = Input.GetTouch(0).position;
    }

    public void GetThrowPower()
    {
        Debug.Log("GetThrowPower");
        Touch touch = Input.GetTouch(0);
        _dragDistance = touch.position - _dragLastPosition;
        _throwSpeed = _dragDistance.magnitude / _setThrowPowerdegree;
        _dragLastPosition = touch.position;
    }

    public void ThrowPrey()
    {
        Debug.Log("ThrowPrey");
        GameObject prey = _pullManager.Get();
        prey.transform.position = _throwPreyPos.position;
        Rigidbody rigid = prey.GetComponent<Rigidbody>();
        // 구한 거리 벡터를 방향 벡터로 변환.
        _dragDistance.x = _dragDistance.normalized.x;
        _dragDistance.y = _dragDistance.normalized.y;
        // 방향벡터에서 회전한 값만큼 곱하여 새로운 벡터를 계산.
        Vector3 newThrowVec = _throwPreyPos.rotation * (Vector3.forward + (Vector3)_dragDistance);
        rigid.AddForce(newThrowVec * _throwPower * _throwSpeed);
    }

    //public void ThrowPrey()
    //{
    //    Debug.Log("ThrowPrey");
    //    GameObject prey = _pullManager.Get();
    //    prey.transform.position = _throwPreyPos.position;
    //    Rigidbody rigid = prey.GetComponent<Rigidbody>();

    //    _dragDistance.x = _dragDistance.normalized.x;
    //    _dragDistance.y = _dragDistance.normalized.y;

    //    Vector3 newThrowVec = _dragDistance.x * _throwPreyPos.transform.right + _dragDistance.y * _throwPreyPos.transform.up + _throwPreyPos.transform.forward;
    //    rigid.AddForce(newThrowVec * _throwPower * _throwSpeed);
    //}

}





