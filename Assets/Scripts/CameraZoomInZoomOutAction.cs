using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoomInZoomOut : MonoBehaviour
{
    private float currentZoom;
    private float targetZoom;
    private const float DefaultZoom = 7.0f;
    private const float MapZoom = 200f; // Half of 300 to cover full map
    private const float ZoomSpeed = 10f; // Speed of zoom transition
    
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        currentZoom = DefaultZoom;
        targetZoom = DefaultZoom;
        
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = currentZoom;
        }
    }

    void Update()
    {
        var keyboard = Keyboard.current;

        // Toggle zoom with Z key
        if (keyboard.zKey.wasPressedThisFrame)
        {
            if (targetZoom == DefaultZoom)
            {
                targetZoom = MapZoom; // Zoom out to see whole map
                mainCamera.transform.position = new Vector3(0, 0, mainCamera.transform.position.z);
            }
            else
            {
                targetZoom = DefaultZoom;
            }
        }

        // Smooth zoom transition
        if (Mathf.Abs(currentZoom - targetZoom) > 0.01f)
        {
            currentZoom = Mathf.Lerp(currentZoom, targetZoom, ZoomSpeed * Time.deltaTime);
            mainCamera.orthographicSize = currentZoom;
        }
    }
}
