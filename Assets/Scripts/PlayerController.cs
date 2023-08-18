using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;


    private Rigidbody _playerRb;
    private float _zRange = 15f;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (transform.position.z < -_zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -_zRange);
        }
        else if (transform.position.z > _zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zRange);
        }
        else
        {
            Vector3 movePlayer = new Vector3(transform.position.x, transform.position.y, Input.GetAxis("Horizontal"));
            _playerRb.MovePosition(transform.position + movePlayer * _speed);
        }
    }


}