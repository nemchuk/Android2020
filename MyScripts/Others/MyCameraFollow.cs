using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraFollow : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector2 offset;

    private Transform _playerTransform;

    void Start()
    {
        _playerTransform = player.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 temp = transform.position;

        temp.x = _playerTransform.position.x;

        temp.y = _playerTransform.position.y;

        temp.x += offset.x;

        temp.y += offset.y;

        transform.position = temp;
    }
}
