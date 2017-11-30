using UnityEngine;

public class RotateToMainCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update ()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 up = Camera.main.transform.up;

        transform.rotation = Quaternion.LookRotation(forward, up);
    }
}
