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
                        Debug.Log("Pet탐지함!");
                        IsPlayerDo = action;
                    }
                    else
                    {
                        //디버깅 결과 Plane이 탐지되고 나서, 그 영역을 눌렀을때 로그 확인.
                        Debug.Log("Pet탐지못함!");
                        //AR Ray
                        List<ARRaycastHit> hits = new List<ARRaycastHit>();

                        if (raycastManager.Raycast(ray, hits))
                        {
                            TouchPos = hits[0].pose.position;
                            IsPlayerDo = EPlayer.MakeMove;
                            Debug.Log($"위치: {TouchPos}");
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
