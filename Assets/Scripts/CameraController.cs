using System.Text;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float scrollMinY = 10f;
    public float scrollMaxY = 80f;

    public TextMeshProUGUI xyzUi;
    public int scrollLevel;

    // These are messed up, because the board is turned, oops :(
    private Vector3 realForward = Vector3.left;
    private Vector3 realBack = Vector3.right;
    private Vector3 realLeft = Vector3.back;
    private Vector3 realRight = Vector3.forward;

    private Dictionary<int, float[]> movementValues = new Dictionary<int, float[]>();

    void Awake()
    {
        // Set up maximum movement values in each direction fo each zoom level
        // Top, Bottom, Left, Right
        movementValues.Add(1, new float[] { 5f, 90f, 19f, 90f });
        movementValues.Add(2, new float[] { 15f, 90f, 25f, 70f });
        movementValues.Add(3, new float[] { 10f, 90f, 30f, 60f });
        movementValues.Add(4, new float[] { 25f, 90f, 35f, 60f });
        movementValues.Add(5, new float[] { 35f, 90f, 37f, 50f });
        movementValues.Add(6, new float[] { 49f, 90f, 40f, 45f });
        movementValues.Add(7, new float[] { 60f, 90f, 40f, 45f });
        movementValues.Add(8, new float[] { 70f, 95f, 40f, 45f });
    }

    /* Move the camera with the mouse/WASD keys */
    void Update()
    {
        // Debug stuff: xyzUi.text = transform.position + " : tb.y.lr " + scrollLevel;

        // Disable camera controls when game is over
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
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
		if (Input.GetKey("q"))
		{
			position.y = position.y - 1;
		}
		if (Input.GetKey("e"))
		{
			position.y = position.y + 1;
		}

		position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        scrollLevel = (int)position.y / 10;
        // constrain scrolling, so not too high or low
        position.y = Mathf.Clamp(position.y, scrollMinY, scrollMaxY);
        transform.position = position;
    }

    /* Don't allow movement beyond a certain point in any direction */
    bool ExceedsMaxPosition(string direction)
    {
        if (scrollLevel <= 0)
            return false;
		if (scrollLevel > 8)
			scrollLevel = 8;
        
        float[] maxValues = movementValues[scrollLevel];
        StringBuilder greaterOrLess = new StringBuilder("");

        if (direction == "forward")
        {
            return transform.position.x <= maxValues[0];
        }
        if (direction == "backward")
        {
            return transform.position.x >= maxValues[1];
        }
        if (direction == "left")
        {
            return transform.position.z <= maxValues[2];
        }
        if (direction == "right")
        {
            return transform.position.z >= maxValues[3];
        }
        else
        {
            Debug.Log("Unexpected ExceeedsMaxPosition value! " + direction);
            return true;
        }
    }

    /* Camera panning controls */
    void MaybeMoveCamera()
    {
        // If we move beyond valid bounds, start going in opposite direction
        if (ExceedsMaxPosition("forward"))
        {
            MoveInDirection(realBack);
            return;
        }
        if (ExceedsMaxPosition("left"))
        {
            MoveInDirection(realRight);
            return;
        }
        if (ExceedsMaxPosition("backward"))
        {
            MoveInDirection(realForward);
            return;
        }
        if (ExceedsMaxPosition("right"))
        {
            MoveInDirection(realLeft);
            return;
        }

        // Move in the direction of mouse or WASD
        if (ShouldMoveForward())
        {
            MoveInDirection(realForward);
        }
        if (ShouldMoveLeft())
        {
            MoveInDirection(realLeft);
        }
        if (ShouldMoveBackward())
        {
            MoveInDirection(realBack);
        }
        if (ShouldMoveRight())
        {
            MoveInDirection(realRight);
        }
    }

    void MoveInDirection(Vector3 direction)
    {
        transform.Translate(direction * panSpeed * Time.deltaTime, Space.World);
    }

    bool ShouldMoveForward()
    {
        return Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness;
    }

    bool ShouldMoveBackward()
    {
        return Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness;
    }

    bool ShouldMoveLeft()
    {
        return Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness;
    }

    bool ShouldMoveRight()
    {
        return Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness;
    }
}
