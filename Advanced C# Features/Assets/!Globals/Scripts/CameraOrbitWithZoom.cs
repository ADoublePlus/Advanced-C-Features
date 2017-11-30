using UnityEngine;

public class CameraOrbitWithZoom : MonoBehaviour
{
    public Transform target;

    public float distance = 5f;
    public float distanceMin = .5f;
    public float distanceMax = 15f;
    public float sensitivity = 1f;

    float x = 0f;
    float y = 0f;

    // Use this for initialization
    void Start ()
    {
        Vector3 angles = transform.eulerAngles;

        x = angles.y;
        y = angles.x;
    }

    void LateUpdate ()
    {
        if (Input.GetMouseButton(1))
        {
            HideCursor(true);

            GetInput();
        }
        else
        {
            HideCursor(false);
        }

        Movement();
    }

    void HideCursor (bool isHiding)
    {
        if (isHiding)
        {
            // Hide the cursor
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        else
        {
            // Unhide the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void GetInput ()
    {
        x += Input.GetAxis("Mouse X") * sensitivity;
        y -= Input.GetAxis("Mouse Y") * sensitivity;

        float inputScroll = Input.GetAxis("Mouse ScrollWheel");

        distance = Mathf.Clamp(distance - inputScroll, distanceMin, distanceMax);
    }

    void Movement ()
    {
        if (target)
        {
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 negDistance = new Vector3(0f, 0f, -distance);

            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
