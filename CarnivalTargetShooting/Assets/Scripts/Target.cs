
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Target : MonoBehaviour
{
    public static event Action<int> OnTargetHit;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _leftBoundary = -7f;
    [SerializeField] private float _rightBoundary = 7f;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private float _direction = 1f;
    private int _points;
    private int _type;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Configure(Sprite sprite, float speed,
        float size, int points)
    {
        _spriteRenderer.sprite = sprite;
        _moveSpeed = speed;
        transform.localScale = Vector3.one * size;
        _points = points;
    }

    // Store the duck type so the correct sprite can be restored.
    public void SetType(int type)
    {
        _type = type;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = _rb.position +
            Vector2.right *
            (_direction * _moveSpeed * Time.fixedDeltaTime);

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

    public DuckSaveData GetSaveData()
    {
        // Save the duck's Transform and configuration.
        DuckSaveData data = new DuckSaveData();

        data.transform = new TransformSaveData
        {
            position = transform.position,
            rotation = transform.rotation,
            scale = transform.localScale
        };

        data.type = _type;
        data.speed = _moveSpeed;
        data.points = _points;
        data.leftBoundary = _leftBoundary;
        data.rightBoundary = _rightBoundary;
        data.direction = _direction;

        return data;
    }

    public void LoadFromData(DuckSaveData data)
    {
        // Restore position, rotation, scale and movement.
        _rb.position = data.transform.position;
        transform.rotation = data.transform.rotation;
        transform.localScale = data.transform.scale;

        _moveSpeed = data.speed;
        _points = data.points;
        _leftBoundary = data.leftBoundary;
        _rightBoundary = data.rightBoundary;
        _direction = data.direction;
        _type = data.type;
    }

    public void Hit()
    {
        if (!GameManager.IsRoundActive)
        {
            return;
        }

        OnTargetHit?.Invoke(_points);
        Destroy(gameObject);
    }
}

