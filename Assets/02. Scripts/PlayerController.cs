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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
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

        // Horizontal Look
        var newRotate = _rb.rotation * Quaternion.Euler(0f, _lookInput.x * lookSensitivity, 0f);
        _rb.MoveRotation(newRotate);
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
