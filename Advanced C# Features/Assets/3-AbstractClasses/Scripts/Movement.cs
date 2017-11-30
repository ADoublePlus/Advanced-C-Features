using UnityEngine;

namespace AbstractClasses
{
    public class Movement : MonoBehaviour
    {
        public float acceleration = 50f;
        public float hyperSpeed = 150f;
        public float rotationSpeed = 20f;

        private Rigidbody2D rigid;

        void Awake ()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update ()
        {
            Accelerate();
            Rotate();
        }

        void Accelerate ()
        {
            float inputV = Input.GetAxis("Vertical");

            Vector3 force = transform.right * inputV;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                force *= hyperSpeed;
            }
            else
            {
                force *= acceleration;
            }

            rigid.AddForce(force);
        }

        void Rotate ()
        {
            float inputH = Input.GetAxis("Horizontal");

            transform.rotation *= Quaternion.AngleAxis(rotationSpeed * inputH, Vector3.back);
        }
    }
}
