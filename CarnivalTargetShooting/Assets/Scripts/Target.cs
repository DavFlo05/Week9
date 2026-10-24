using UnityEngine;
using UnityEngine.Rendering;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Target : MonoBehaviour
{
    public static event Action<int> OnTargetHit;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _leftBoundary = -7f;
    [SerializeField] private float _rightBoundary = 7f;

    private Rigidbody2D _rb;
    private float _direction = 1f;

    private SpriteRenderer _spriteRenderer;
    private int _points;

    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Configure(Sprite sprite, float speed, float size, int points)
    {
        _spriteRenderer.sprite = sprite;
        _moveSpeed = speed;
        transform.localScale = Vector3.one * size;
        _points = points;
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
        // Announce that this target was hit.
        OnTargetHit?.Invoke(_points);

        // Remove the target from the scene.
        Destroy(gameObject);
    }
}
