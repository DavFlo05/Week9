using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _poolSize = 20;

    private readonly List<Bullet> _bullets = new();

    private void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet bullet = Instantiate(_bulletPrefab, transform);

            bullet.gameObject.SetActive(false);

            _bullets.Add(bullet);
        }
    }

    public void Fire(Vector3 position, Vector2 direction)
    {
        foreach (Bullet bullet in _bullets)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.transform.position = position;
                bullet.Launch(direction);

                return;
            }
        }
    }
}
