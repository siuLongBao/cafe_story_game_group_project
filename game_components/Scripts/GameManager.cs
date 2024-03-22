using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Private Variable
    public static GameManager Instance; // Singleton instance

    private float roundTimeSeconds = 180;
    public bool timeIsUp;
    public int score;
    public int penaltyScore = 10;
    private bool isPaused = false;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    private void Awake(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timeIsUp = false;
        score = 0;
        UpdateScoreUI();
        Debug.Log("Game starts!");
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeIsUp)
        {
            roundTimeSeconds -= Time.deltaTime;
            UpdateTimerUI();
            if (roundTimeSeconds <= 0)
            {
                timeIsUp = true;
                EndRound();
            }
        }
    }

    // If delivery dishes successfully
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
        // Todo: You can also update any UI elements related to the score here, if needed.
    }

    // If the orders are not delivered in time
    public void SubScore()
    {
        score -= penaltyScore;
        UpdateScoreUI();
        // Todo: You can also update any UI elements related to the score here, if needed.
    }

    // Update score UI
    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }

    // When the game is over
    public void EndRound()
    {
        // Todo: Handle what happens when the round is over
        Debug.Log("Game is over.");
    }

    public void UpdateTimerUI()
    {
        int totalSeconds = (int)roundTimeSeconds;
        string timer = totalSeconds.ToString() + "s";
        timerText.text = timer;
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1; 
            isPaused = false;
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
