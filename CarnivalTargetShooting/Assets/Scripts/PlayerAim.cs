using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private InputActionReference _aimAction;
    [SerializeField] private Transform _crosshair;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _aimAction.action.Enable();
    }
    private void OnDisable()
    {
        _aimAction.action.Disable();
    }

    private void Update()
    {
        // Read the mouse position.
        Vector2 mousePosition = _aimAction.action.ReadValue<Vector2>();

        // Convert screen position to world position.
        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, -_mainCamera.transform.position.z);

        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);

        // Move the crosshair to the mouse position.
        _crosshair.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
    }
}
