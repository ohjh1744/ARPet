using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
{
    [SerializeField] private GameObject _apple;

    private List<GameObject> _pool;

    public void Awake()
    {
        _pool = new List<GameObject>();
    }
    public GameObject Get()
    {
        GameObject select = null;

        foreach (GameObject item in _pool)
        {
            if (item.activeSelf == false)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(_apple, transform);
            _pool.Add(select);
        }

        return select;
    }
}
