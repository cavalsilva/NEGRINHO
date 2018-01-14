using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Gerenciamento do estado do jogo
    enum GameState { running, paused, gameOver }
    GameState gameState = GameState.running;
    bool restartingGame = false;

    //Elementos do jogo
    [HideInInspector]
    public Vector3 lastCheckpoint;
    PlayerMovement player;

    Candle candle;
    public int coins;

    [HideInInspector]
    public List<GameObject> colectedCoins = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> monstersAlive = new List<GameObject>();

    public AnimationCurve coinsValue;

    public SfxAudio negrinhoMorre;

    //Menu
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject ui;
    public GameObject blackScreen;
    Text coinsText;
    Image coinsImage;

    //Debug
    [Header("Debug Tools")]
    public bool forceGameOver = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        candle = FindObjectOfType<Candle>();

        coinsText = ui.GetComponentInChildren<Text>();
        coinsImage = ui.GetComponentInChildren<Image>();

        blackScreen.gameObject.SetActive(false);

        lastCheckpoint = transform.position;
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.running:
                if (Input.GetButtonDown("Pause"))
                    PauseGame();

                if (candle.timer <= 0)
                    GameOver();
                break;
            case GameState.paused:
                if (Input.GetButtonDown("Pause"))
                    UnpauseGame();

                break;
            case GameState.gameOver:
                if (!restartingGame)
                    StartCoroutine(ResetGame());
                break;
        }

        if (forceGameOver)
            GameOver();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameState = GameState.paused;

        pauseMenu.SetActive(true);
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gameState = GameState.running;

        pauseMenu.SetActive(false);
    }
    public void GameOver()
    {
        gameState = GameState.gameOver;
        forceGameOver = false;
        Debug.Log("Game Over");
    }

    public void AddCoin(int value)
    {
        coins += value;
        StartCoroutine(IncreaseCoinCount(coins));
    }
    IEnumerator IncreaseCoinCount(int totalValue)
    {
        int uiCoinCount = Convert.ToInt32(coinsText.text);

        while (uiCoinCount < totalValue)
        {
            uiCoinCount++;
            coinsText.text = uiCoinCount.ToString();
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    public IEnumerator DecreaseCoinCount()
    {
        int uiCoinCount = Convert.ToInt32(coinsText.text);

        while (uiCoinCount > 0)
        {
            uiCoinCount--;
            coinsText.text = uiCoinCount.ToString();
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    public void ApplyCoinsBonus()
    {
        float timerBonus = 0;
        if(coins > 0)
            timerBonus = coinsValue.Evaluate(coins);

        StartCoroutine(candle.CoinsBoostOverTime(timerBonus));

        EraseColectedCoins();
    }

    WaitForSeconds wait = new WaitForSeconds(2);
    IEnumerator ResetGame()
    {
        restartingGame = true;

        //TODO fade screen to black, or an overlay cover it
        blackScreen.SetActive(true);
        negrinhoMorre.PlayAudio();

        yield return wait;

        //when screen is black, teleport player back to last checkpoint
        Vector3 restartLocation = new Vector3(lastCheckpoint.x, lastCheckpoint.y, lastCheckpoint.z - 2);

        if (player != null)
            player.transform.position = restartLocation;
        else
            Debug.LogError("Player não está na cena");

        candle.ResetCandle();

        //reenable colected coins
        ReenableColectedCoins();

        //eliminate all monsters alive
        EraseMonsters();

        yield return wait;
        //TODO fadeout the black screen
        blackScreen.SetActive(false);

        //TODO feedback restart

        restartingGame = false;
        gameState = GameState.running;
        yield return null;
    }

    void EraseMonsters()
    {
        foreach (GameObject monster in monstersAlive)
        {
            Destroy(monster);
        }

        monstersAlive.Clear();
    }

    void ReenableColectedCoins()
    {
        foreach (GameObject coin in colectedCoins)
        {
            coin.SetActive(true);
        }

        coins = 0;
        coinsText.text = coins.ToString();
    }
    void EraseColectedCoins()
    {
        foreach (GameObject coin in colectedCoins)
        {
            Destroy(coin);
        }
        colectedCoins.Clear();
        coins = 0;
    }
}
