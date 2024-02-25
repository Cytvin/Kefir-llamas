using System.Collections;
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
    private GameObject _waypointHolder;
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
                _animator.SetFloat("MoveSpeed", _movementSpeed);
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

    public void CelebrateVictory()
    {
        _movement = Vector3.zero;
        SetMoveStateTo(false);
        _waypointHolder.SetActive(false);
        StartCoroutine(nameof(Rotate));
        _animator.SetFloat("MoveSpeed", 0);
        _animator.SetTrigger("Win");
    }

    private IEnumerator Rotate()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        do
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        while (transform.localRotation != rotation);
    }

    private void FixedUpdate()
    {
        _movement = _movement.normalized * _movementSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + _movement);
    }

    private void LateUpdate()
    {
        _cameraHolder.transform.position = transform.position;
    }
}