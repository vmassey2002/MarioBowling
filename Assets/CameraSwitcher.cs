using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
   [SerializeField] private Camera topViewCamera;
   [SerializeField] private Camera backViewCamera;

    void Start()
    {
        // Disable one of the cameras initially
        topViewCamera.enabled = false;
        backViewCamera.enabled = true;
    }

    void Update()
    {
        // Check for key press to switch between cameras
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        // Toggle the enabled state of both cameras
        topViewCamera.enabled = !topViewCamera.enabled;
        backViewCamera.enabled = !backViewCamera.enabled;
    }
}
