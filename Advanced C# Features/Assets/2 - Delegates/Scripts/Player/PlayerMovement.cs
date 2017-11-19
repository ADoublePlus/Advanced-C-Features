using UnityEngine;

namespace Delegates
{
    public class PlayerMovement : MonoBehaviour
    {
        public float acceleration = 200f;
        public float deceleration = 0.01f;

        private Rigidbody rigid;

        void Awake ()
        {
            rigid = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update ()
        {
            Accelerate();
            Decelerate();
        }

        void Accelerate ()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            // Calculate inputDir
            Vector3 inputDir = new Vector3(inputH, 0, inputV);

            // AddForce in the direction of inputDir
            rigid.AddForce(inputDir * acceleration);
        }

        void Decelerate ()
        {
            // Decelerate (velocity = -velocity * deceleration)
            rigid.velocity = -rigid.velocity * deceleration;
        }
    }
}
