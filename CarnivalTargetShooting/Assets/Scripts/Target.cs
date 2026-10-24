using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Target : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _leftBoundary = -7f;
    [SerializeField] private float _rightBoundary = 7f;

    private Rigidbody2D _rb;
    private float _direction = 1f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = _rb.position + Vector2.right * (_direction * _moveSpeed * Time.fixedDeltaTime);
        if (newPosition.x >= _rightBoundary)
        {
            newPosition.x = _rightBoundary;
            _direction = -1f;
        }
        else if (newPosition.x <= _leftBoundary)
        {
            newPosition.x = _leftBoundary;
            _direction = 1f;
        }
        _rb.MovePosition(newPosition);
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
