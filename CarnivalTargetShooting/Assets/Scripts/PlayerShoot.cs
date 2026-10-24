using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private InputActionReference _shootAction;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _crosshair;
    [SerializeField] private BulletPool _bulletPool;

    private void OnEnable()
    {
        _shootAction.action.Enable();
    }

    private void OnDisable()
    {
        _shootAction.action.Disable();
    }

    private void Update()
    {
        if (_shootAction.action.WasPressedThisFrame())
        {
            Vector2 direction = _crosshair.position - _firePoint.position;

            _bulletPool.Fire(_firePoint.position, direction);
        }
    }
}
