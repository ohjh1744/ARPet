using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public enum EPlayer { Idle, MakeMove, Tickling, Petting}
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;

    [SerializeField] private EPlayer _isPlayerDo;

    public EPlayer IsPlayerDo { get { return _isPlayerDo; }  set { _isPlayerDo = value; } }

    public Vector3 TouchPos { get; private set; }

    private Dictionary<string, EPlayer> actDic = new Dictionary<string, EPlayer>
    {
        { "PetWaist", EPlayer.Tickling },
        { "PetHead", EPlayer.Petting }
    };

    // Update is called once per frame
    void Update()
    {
        Touch();
        Dotest();
    }

    private void Touch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("Touch!!!!");
            if (touch.phase == TouchPhase.Began)
            {
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

                        if (raycastManager.Raycast(ray, hits))
                        {
                            TouchPos = hits[0].pose.position;
                            IsPlayerDo = EPlayer.MakeMove;
                            Debug.Log($"��ġ: {TouchPos}");
                        }
                    }
                }
                
            }

        }

    }

    private void Dotest()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _isPlayerDo = EPlayer.Tickling;
        }
    }


}
