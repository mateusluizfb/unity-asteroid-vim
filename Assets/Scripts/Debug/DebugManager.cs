using UnityEngine;
using UnityEngine.InputSystem;

public class DebugManager : MonoBehaviour
{
    private Camera mainCamera;
    private Camera debugCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        debugCamera = GameObject.Find("DebugCamera").GetComponent<Camera>();   
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        if (keyboard.cKey.wasPressedThisFrame)
        {
            bool isMainCameraActive = mainCamera.gameObject.activeSelf;
            mainCamera.gameObject.SetActive(!isMainCameraActive);
            debugCamera.gameObject.SetActive(isMainCameraActive);
        }

    }
}
