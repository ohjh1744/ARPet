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
                        Debug.Log("PetŽ����!");
                        IsPlayerDo = action;
                    }
                    else
                    {
                        //����� ��� Plane�� Ž���ǰ� ����, �� ������ �������� �α� Ȯ��.
                        Debug.Log("PetŽ������!");
                        //AR Ray
                        List<ARRaycastHit> hits = new List<ARRaycastHit>();

                        if (_raycastManager.Raycast(ray, hits))
                        {
                            TouchPos = hits[0].pose.position;
                            IsPlayerDo = EPlayer.TouchSpace;
                            Debug.Log($"��ġ: {TouchPos}");
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
        // ���� �Ÿ� ���͸� ���� ���ͷ� ��ȯ.
        _dragDistance.x = _dragDistance.normalized.x;
        _dragDistance.y = _dragDistance.normalized.y;
        // ���⺤�Ϳ��� ȸ���� ����ŭ ���Ͽ� ���ο� ���͸� ���.
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





