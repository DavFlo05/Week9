using UnityEngine;

public class TargetBuilder
{
    private readonly Target _targetPrefab;

    private Sprite _sprite;
    private float _speed = 2f;
    private float _size = 1f;
    private int _points = 10;

    public TargetBuilder(Target targetPrefab)
    {
        _targetPrefab = targetPrefab;
    }

    public TargetBuilder SetSprite(Sprite sprite)
    {
        _sprite = sprite;
        return this;
    }

    public TargetBuilder SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    public TargetBuilder SetSize(float size)
    {
        _size = size;
        return this;
    }

    public TargetBuilder SetPoints(int points)
    {
        _points = points;
        return this;
    }

    public Target Build(Vector3 position)
    {
        Target target = Object.Instantiate(
            _targetPrefab,
            position,
            Quaternion.identity
        );

        target.Configure(_sprite, _speed, _size, _points);

        return target;
    }

}

