using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _cameraHolder;
    [SerializeField]
    private Vector3 _cameraOffset;
    [SerializeField]
    private float _movementSpeed = 10;
    [SerializeField]
    private float _rotationSpeed = 10;
    private Vector3 _movement;
    private bool _isMoveEnable = true;

    public bool IsMoveEnable => _isMoveEnable;

    private void Update()
    {
        if (_isMoveEnable == true)
        {
            _movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (_movement.Equals(Vector3.zero) == false)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_movement), Time.deltaTime * _rotationSpeed);
                _animator.SetFloat("MoveSpeed", 15f);
            }
            else
            {
                _animator.SetFloat("MoveSpeed", 0);
            }
        }
    }

    public void SetMoveStateTo(bool moveState)
    {
        _isMoveEnable = moveState;
    }

    private void FixedUpdate()
    {
        _movement = _movement.normalized * _movementSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + _movement);
    }

    private void LateUpdate()
    {
        _cameraHolder.transform.position = transform.position + _cameraOffset;
    }
}