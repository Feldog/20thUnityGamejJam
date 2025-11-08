using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _collider;
    [SerializeField] private Transform _camera;

    public float moveSpeed;
    public float lookSensitivity;
    public float verticalAngleLimit = 80f;

    private Vector2 _movementInput;
    private Vector2 _lookInput;
    private float cameraVerticalRotation = 0f;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _playerInput = GetComponent<PlayerInput>();

        // 처음 시작 시 플레이어 인풋을 비활성화
        _playerInput.enabled = false;

        var gameManager = GameManager.Instance;

        // 게임매니저에 이벤트 등록
        gameManager.onStartGame += Init;
        gameManager.onPauseGame += () => _playerInput.enabled = false;
        gameManager.onResumeGame += () => _playerInput.enabled = true;
    }

    private void Init()
    {
        _playerInput.enabled = true;
    }

    private void Update()
    {
        // Horaizontal Look
        var newRotate = _rb.rotation * Quaternion.Euler(0f, _lookInput.x * lookSensitivity, 0f);
        _rb.MoveRotation(newRotate);
    }

    private void LateUpdate()
    {
        Look();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Look()
    {
        // Vertical Look
        cameraVerticalRotation -= _lookInput.y * lookSensitivity;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -verticalAngleLimit, verticalAngleLimit);
        _camera.localEulerAngles = new Vector3(cameraVerticalRotation, 0, 0);
    }

    private void Move()
    {
        //var moveDir = transform.forward * _movementInput.y + transform.right * _movementInput.x;
        //_rb.MovePosition(transform.position + moveDir * moveSpeed *  Time.fixedDeltaTime);
        
        // Movement
        Vector3 targetVelocity = (transform.forward * _movementInput.y + transform.right * _movementInput.x).normalized * moveSpeed;
        _rb.linearVelocity = new Vector3(targetVelocity.x, _rb.linearVelocity.y, targetVelocity.z);
    }

    public void Move(Transform target)
    {
        _rb.Move(target.position, target.rotation);
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if(input != null)
        {
            _movementInput = input;
        }
    }

    public void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input != null)
        {
            _lookInput = input;
        }
    }
}
