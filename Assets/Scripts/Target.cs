using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _targetRb;
    private GameManager _gameManager;
    [SerializeField] float _maxTorque = 2f;
    [SerializeField] int badScoreCount = -10;
    [SerializeField] float _zRange = 15f;
    [SerializeField] float _ySpawnPos = 25f;
    [SerializeField] float _yPosToDestroy;

    // Start is called before the first frame update
    void Awake()
    {
        _targetRb = GetComponent<Rigidbody>();
        _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        transform.position = RandomSpawnPos();
    }

    private void Update()
    {
        DestroyOnFall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PotSensor"))
        {
            if (gameObject.CompareTag("Bad"))
            {

                _gameManager.UpdateScore(badScoreCount);
                _gameManager.UpdateLives();
                Destroy(gameObject);

                if (_gameManager.lives == 0)
                {
                    _gameManager.GameOver();
                }
            }
            else
            {
                if (_gameManager.isGameActive)
                {
                    _gameManager.AddFoodScore(gameObject.tag);
                    Destroy(gameObject);
                }
            }
        }
    }

    float RandomTorque()
    {
        return Random.Range(-_maxTorque, _maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(0, _ySpawnPos, Random.Range(-_zRange, _zRange));
    }

    void DestroyOnFall()
    {
        if (gameObject.transform.position.y < _yPosToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
