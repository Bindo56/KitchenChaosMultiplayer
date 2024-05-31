using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instane {  get; private set; }

    public event EventHandler OnStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;

    float waitingToStartTimer = 1f;
    float countdownToStartTimer = 3f;
    [SerializeField] float gamePlayingTimer;
   [SerializeField]float gamePlayingTimerMax = 10f;
    bool isGamePause;

    private void Start()
    {
        GameInput.instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.instance.OnInteractAction += Instance_OnInteractAction;
    }

    private void Instance_OnInteractAction(object sender, EventArgs e)
    {
      if(state == State.WaitingToStart)
        {
           
            state = State.CountdownToStart;
            OnStateChange?.Invoke(this,EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Awake()
    {
        Instane = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;
        }
       // Debug.Log(state);
    }


    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOverActive()
    {
        return state == State.GameOver;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
    public void TogglePauseGame()
    {
        isGamePause = !isGamePause;
        if(isGamePause)
        {
        Time.timeScale = 0;
            OnGamePaused?.Invoke(this,EventArgs.Empty);

        }
        else
        {
            Time.timeScale = 1;
            OnGameUnPaused?.Invoke(this,EventArgs.Empty);
        }
    }


}
