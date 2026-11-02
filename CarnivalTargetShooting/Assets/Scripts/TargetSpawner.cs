using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Target _targetPrefab;

    [SerializeField] private Sprite _brownDuck;
    [SerializeField] private Sprite _whiteDuck;
    [SerializeField] private Sprite _yellowDuck;

    [SerializeField] private float _spawnInterval = 2f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;
            SpawnTarget();
        }
    }

    private void SpawnTarget()
    {
        int type = Random.Range(0, 3);

        TargetBuilder builder = new TargetBuilder(_targetPrefab);

        switch (type)
        {
            case 0:
                builder.SetSprite(_brownDuck)
                       .SetSpeed(1.5f)
                       .SetSize(3f)
                       .SetPoints(10);
                break;

            case 1:
                builder.SetSprite(_whiteDuck)
                       .SetSpeed(2.5f)
                       .SetSize(2.5f)
                       .SetPoints(25);
                break;

            case 2:
                builder.SetSprite(_yellowDuck)
                       .SetSpeed(4f)
                       .SetSize(2f)
                       .SetPoints(50);
                break;
        }

        Vector3 spawnPosition = new Vector3(
            Random.Range(-4f, 4f),
            Random.Range(0f, 3f),
            0f
        );

        Target target = builder.Build(spawnPosition);

        // Remember which variation was created.
        target.SetType(type);
    }

    public void RestoreTarget(DuckSaveData data)
    {
        // Recreate the duck using the original Builder pattern.
        TargetBuilder builder = new TargetBuilder(_targetPrefab);

        Sprite sprite = _brownDuck;

        switch (data.type)
        {
            case 0:
                sprite = _brownDuck;
                break;

            case 1:
                sprite = _whiteDuck;
                break;

            case 2:
                sprite = _yellowDuck;
                break;
        }

        builder.SetSprite(sprite)
               .SetSpeed(data.speed)
               .SetSize(data.transform.scale.x)
               .SetPoints(data.points);

        Target target = builder.Build(data.transform.position);

        // Restore the exact saved configuration.
        target.LoadFromData(data);
    }
}


