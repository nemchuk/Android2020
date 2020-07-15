using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private PlayerController _player;

    [SerializeField]
    private GameObject playerGO;
    [SerializeField]
    private Transform start;
    [SerializeField]
    private GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        _player = playerGO.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player.transform.position = start.position;
        }
    }
}
