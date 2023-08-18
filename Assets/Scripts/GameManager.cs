using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> foodPrefabs;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject titleScreen;
    [SerializeField] Button gameOverButton;
    private AudioSource music;

    // Food score
    [SerializeField] int broccoliScore = 2;
    [SerializeField] int carrotScore = 3;
    [SerializeField] int fishScore = 5;
    [SerializeField] int mushroomScore = 7;
    [SerializeField] int steakScore = 10;
    [SerializeField] int pepperScore = 4;

    // Game settings
    public int lives = 3;
    [SerializeField] float spawnInterval = 2f;

    private int _score;
    private bool _paused;
    public bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ChangedPaused();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnInterval);
            int index = Random.Range(0, foodPrefabs.Count);
            Instantiate(foodPrefabs[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        scoreText.text = $"Score: {_score}";
    }

    public void UpdateLives()
    {
        if (lives >= 0)
        {
            lives--;
            livesText.text = $"Lives: {lives}";
        }
    }

    public void ChangedPaused()
    {
        if(!_paused)
        {
            _paused = true;
            Time.timeScale = 0;
            music.Pause();
        } else
        {
            _paused = false;
            Time.timeScale = 1;
            music.Play();
        }
    }

    public void AddFoodScore(string foodTag)
    {
        int foodScoreCount;

        switch (foodTag)
        {
            case "Broccoli":
                foodScoreCount = broccoliScore;
                break;
            case "Carrot":
                foodScoreCount = carrotScore;
                break;
            case "Fish":
                foodScoreCount = fishScore;
                break;
            case "Mushroom":
                foodScoreCount = mushroomScore;
                break;
            case "Steak":
                foodScoreCount = steakScore;
                break;
            case "Pepper":
                foodScoreCount = pepperScore;
                break;
            default:
                foodScoreCount = 0;
                break;
        }
        UpdateScore(foodScoreCount);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        _score = 0;
        spawnInterval /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
