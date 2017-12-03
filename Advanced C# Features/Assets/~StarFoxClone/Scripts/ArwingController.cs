using UnityEngine;

namespace StarFoxClone
{
    public class ArwingController : MonoBehaviour
    {
        public enum Mode
        {
            CONFIRMED = 0,
            ALL_RANGE = 1
        }
        public Mode arwingMode = Mode.CONFIRMED;

        [Header("Camera")]
        public float cameraYSpeed = 10f;
        public float cameraMoveSpeed = 90f;
        public float cameraDistance = 20f;

        [Header("Arwing")]
        public Transform aimTarget;
        public Transform moveTarget;
        public float aimimgSpeed = 15f;
        public float movementSpeed = 40f;
        public float rotationSpeed = 10f;

        private Camera parentCam;
        private Vector3 up = Vector3.up;

        private float startDistance = 5f;

        // Use this for initialization
        void Start ()
        {
            parentCam = GetComponentInParent<Camera>();

            // Set startDistance to the distance between the parentCam and arwing
            startDistance = Vector3.Distance(parentCam.transform.position, transform.position);
        }

        // Moves the 'MoveTarget' to target gameObject
        void MoveArwingToMoveTarget ()
        {
            // Get the global aim target pos
            Vector3 moveTargetPos = moveTarget.position;
            Vector3 arwingPos = transform.position;

            // Move the arwing towards the aimTarget
            arwingPos = Vector3.MoveTowards(arwingPos, moveTargetPos, movementSpeed * Time.deltaTime);

            // Apply position to the arwing
            transform.position = arwingPos;

            // Reset z locally to startDistance
            Vector3 localArwingPos = transform.localPosition;
            localArwingPos.z = startDistance;
            transform.localPosition = localArwingPos;
        }

        // Moves the 'MoveTarget' gameObject to arwing
        void MoveTargetToArwing ()
        {
            // Get the aimTarget and arwing's localPosition
            Vector3 localAimTargetPos = aimTarget.localPosition;
            Vector3 localArwingPos = transform.localPosition;

            // Move aimTarget towards local arwing
            localAimTargetPos = Vector3.MoveTowards(localAimTargetPos, localArwingPos, movementSpeed * Time.deltaTime);

            // Apply aimTarget's localPosition
            aimTarget.localPosition = localAimTargetPos;
            transform.localPosition = localArwingPos;
        }

        // Rotates the arwing to 'AimTarget' gameObject
        void RotateArwingToAimTarget ()
        {
            // Get the direction to aimTarget from arwing
            Vector3 direction = aimTarget.position - transform.position;

            // Set rotaton to up
            Quaternion rotation = Quaternion.LookRotation(direction.normalized, up);

            // Lerp the rotation for the arwing
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, aimimgSpeed * Time.deltaTime);
        }

        // Moves forward with the camera
        void MoveForward ()
        {
            parentCam.transform.position += parentCam.transform.forward * cameraMoveSpeed * Time.deltaTime;
        }

        // Get a camera to follow the arwing (only in ALL_RANGE Mode)
        void FollowArwing ()
        {
            // Get the camera's position and rotation
            Vector3 position = parentCam.transform.position;
            Quaternion rotation = parentCam.transform.rotation;

            // Get the local position of the target and arwing
            Vector3 localArwingPos = transform.localPosition;
            Vector3 localTargetPos = moveTarget.localPosition;

            // Calculte the direction with localPos
            Vector3 direction = localTargetPos - localArwingPos;

            // Rotate the camera in the direction
            rotation *= Quaternion.AngleAxis(direction.x * rotationSpeed * Time.deltaTime, Vector3.up);

            // Move the camera up at a different speed
            position.y += direction.y * cameraYSpeed * Time.deltaTime;

            // Apply the newly made position to the camera
            parentCam.transform.position = position;
            parentCam.transform.rotation = rotation;
        }

        void MoveTarget (float inputH, float inputV)
        {
            // Get inputDir
            Vector3 inputDir = new Vector3(inputH, inputV, 0);

            // Calculate force by inputDir * movementSpeed
            Vector3 force = inputDir * movementSpeed;

            // Offset aimTarget by force
            moveTarget.localPosition += force * Time.deltaTime;
        }

        public void Move (float inputH, float inputV)
        {
            // Move the target
            MoveTarget(inputH, inputV);

            MoveForward();

            // Move based on the arwing Mode
            switch (arwingMode)
            {
                case Mode.CONFIRMED:
                    break;

                case Mode.ALL_RANGE:
                    break;

                default:
                    break;
            }

            // IF no input is made
            if(inputH == 0 && inputV == 0)
            {
                MoveTargetToArwing();
            }

            MoveArwingToMoveTarget();

            RotateArwingToAimTarget();
        }
    }
}
