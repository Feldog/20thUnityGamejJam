using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public event Action onEndGame;

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

        // 초기화
        remainEntity = entityMax;
        remainFloor = floorMax;
        
        // 처음 스타트시 실행되는 변경 함수는 실행
        GameStateChange(EGameStae.Play);
        player.Move(startPosition);
    }

    public void RestartCurrentScenes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextFloor()
    {
        // 남은 층의 수를 갱신
        remainFloor--;
        if(remainFloor >= 0)
        {
            // 플레이어 위치 초기화
            UIManager.Instance.SetFloorText(remainFloor);
            UIManager.Instance.WaitFadeIn(1f, 1f);
            player.Move(startPosition);
        }
        else
        {
            // 게임오버
            GameStateChange(EGameStae.GameOver);
        }
    }

    public bool CheckNextFloor(int findEntity)
    {
        // 남은 엔티티의 수 갱신
        remainEntity -= findEntity;
        UIManager.Instance.SetEntityText(remainEntity);

        if(remainEntity > 0)
        {
            return true;
        }
        return false;
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

    public void GameClear()
    {
        GameStateChange(EGameStae.GameClear);
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
            case EGameStae.Intro:
                break;
        }
    }

    private void EnterState(EGameStae gameState)
    {
        switch (gameState)
        {
            case EGameStae.Play:
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                UIManager.Instance.SetPlayUI();
                break;
            case EGameStae.Pause:
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                onPauseGame?.Invoke();
                UIManager.Instance.SetPauseUI();
                break;
            case EGameStae.GameOver:
                Time.timeScale = 0f;
                UIManager.Instance.SetGameOverUI();
                break;
            case EGameStae.GameClear:
                Time.timeScale = 0f;
                UIManager.Instance.SetGameClearUI();
                break;
            case EGameStae.Intro:
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
}
