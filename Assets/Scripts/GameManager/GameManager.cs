using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public PlayerData playerData;
    public int score = 0;
    bool gameOver = false;
    [SerializeField] EndGame endGame;
    [SerializeField] Pause pause;
    public PlayerManager player;
    public CameraHandler cam;

    void Awake()
    {
        instance = this;
        Pause.ResumeGame(); // Resume game if scene switched;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void TogglePause()
    {
        Pause.TogglePause();
        Debug.Log(Time.timeScale == 0);
        pause.panel.SetActive(Time.timeScale == 0);
    }

    public void IncreaseScore()
    {
        if (!gameOver) score++;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Pause.PauseGame(); // Pause game
        gameOver = true; // set value for other scripts
    }

    public void End()
    {
        Debug.Log("End");
        Pause.PauseGame(); // Prevent further score
        Debug.Log(score); // Final score

        endGame.gameObject.SetActive(true); // UI for lose/win
        endGame.Init(player.GetSize());
    }
}
