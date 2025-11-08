using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Define;

public class GameManager : Singleton<GameManager>
{
    // 게임의 Play상태 Pause 상태를 관리
    private EGameStae _gameState;
    public EGameStae GameState {  get { return _gameState; } }

    [SerializeField] private Transform startPosition;

    public PlayerController player;

    private int remainFloor = 5;
    private int remainEntity = 3;

    // 시작시 실행될 액션
    public event Action onStartGame;

    public event Action onPauseGame;
    public event Action onResumeGame;


    // 시작시 
    private bool firstStart = true;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameStateChange(EGameStae.Intro);

        firstStart = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_gameState == EGameStae.Pause)
            {
                GameStateChange(EGameStae.Play);
            }
            else
            {
                GameStateChange(EGameStae.Pause);
            }
        }
    }

    public void Init()
    {
        UIManager.Instance.WaitFadeIn(1f, 2f);

        // 외부 함수 실행
        onStartGame?.Invoke();
        
        // 처음 스타트시 실행되는 변경 함수는 실행
        GameStateChange(EGameStae.Play);
        player.Move(startPosition);
    }

    // 현재 게임의 상태를 조절
    public void GameStateChange(EGameStae changeState)
    {
        // 다른 상태일 경우, 처음 실행하는 경우 실행
        if (_gameState != changeState || firstStart)
        {
            // 처음 실행되는 경우 실행하지 않음
            if (!firstStart)
                ExitState(_gameState);

            _gameState = changeState;
            EnterState(_gameState);
        }
    }

    private void ExitState(EGameStae gameState)
    {
        switch (gameState)
        {
            case EGameStae.Play:
                break;
            case EGameStae.Pause:
                Time.timeScale = 1f;
                onResumeGame?.Invoke();
                break;
            case EGameStae.GameOver:
                break;
            case EGameStae.GameClear:
                break;
        }
    }

    private void EnterState(EGameStae gameState)
    {
        switch (gameState)
        {
            case EGameStae.Play:
                Cursor.lockState = CursorLockMode.Locked;
                UIManager.Instance.SetPlayUI();
                break;
            case EGameStae.Pause:
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                onPauseGame?.Invoke();
                UIManager.Instance.SetPauseUI();
                break;
            case EGameStae.GameOver:
                break;
            case EGameStae.GameClear:
                break;
            case EGameStae.Intro:
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
}
