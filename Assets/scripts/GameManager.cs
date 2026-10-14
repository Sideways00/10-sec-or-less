using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BasketballGameManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text messageText;

    public Transform player;
    public Transform[] playerSpots;

    public float timeRemaining = 10f;

    private int currentSpot = 0;
    private bool timerRunning = true;
    private bool canScore = true;

    void Start()
    {
        SetupSpot();
    }

    void Update()
    {
        if (!timerRunning)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            RestartGame();
            return;
        }

        timerText.text =
            Mathf.CeilToInt(timeRemaining).ToString();
    }

    void SetupSpot()
    {
        player.position = playerSpots[currentSpot].position;
        player.rotation = playerSpots[currentSpot].rotation;

        timeRemaining = 10f;
        timerRunning = true;
        canScore = true;

        timerText.text = "10";

        if (messageText != null)
        {
            messageText.text =
                "Shot " + (currentSpot + 1) + " of 3";
        }
    }

    public void MadeBasket()
    {
        if (!canScore)
            return;

        canScore = false;

        if (currentSpot == 2)
        {
            timerRunning = false;

            if (messageText != null)
                messageText.text = "YOU WIN!";

            timerText.text = "";
            return;
        }

        currentSpot++;
        SetupSpot();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}