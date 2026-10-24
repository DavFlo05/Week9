using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _lifetime = 2f;

    private Vector2 _direction;
    private float _remainingTime;

    public void Launch(Vector2 direction)
    {
        _direction = direction.normalized;
        _remainingTime = _lifetime;

        gameObject.SetActive(true);
    }

    public void Update()
    {
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);

        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Target target))
        {
            target.Hit();

            gameObject.SetActive(false);
        }
    }
}
