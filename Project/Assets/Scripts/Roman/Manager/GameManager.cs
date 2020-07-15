using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float _respawnStartTime;
    private bool _respawn;

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        _respawnStartTime = Time.time;

        _respawn = true;
    }


    private void CheckRespawn()
    {
        if( (Time.time >= _respawnStartTime + respawnTime ) && _respawn)
        {
            player.transform.position = respawnPoint.transform.position;

            _respawn = false;
        }
    }
}
