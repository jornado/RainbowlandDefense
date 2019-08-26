using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float scrollMinY = 10f;
    public float scrollMaxY = 80f;

    private bool doMovement = true;

    // These are messed up, because the board is turned, oops :(
    private Vector3 realForward = Vector3.left;
    private Vector3 realBack = Vector3.right;
    private Vector3 realLeft = Vector3.back;
    private Vector3 realRight = Vector3.forward;

    /* Move the camera with the mouse/WASD keys */
    void Update()
    {
        // Check if ESC is pressed
        if (DisableMovement())
        {
            return;
        }

        // Move camera with mouse/WASD keys
        MaybeMoveCamera();

        // Zoom camera with scroll wheel/gesture
        MaybeZoomCamera();
    }

    /* Scrolling controls */
    void MaybeZoomCamera()
    {
        // TODO: RTS-Camera scrolling: http://bit.ly/2b3SHDY
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 position = transform.position;

        position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        // constrain scrolling, so not too high or low
        position.y = Mathf.Clamp(position.y, scrollMinY, scrollMaxY);
        transform.position = position;
    }

    /* Camera panning controls */
    void MaybeMoveCamera()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(realForward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(realLeft * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(realBack * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(realRight * panSpeed * Time.deltaTime, Space.World);
        }
    }

    /* Use Escape key to prevent movement during testing */
    bool DisableMovement()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (doMovement)
        {
            return false;
        }

        return true;
    }
}
