using UnityEngine;

namespace StarFoxClone
{
    public class UserController : MonoBehaviour
    {
        public ArwingController arwingController;

        // Update is called once per frame
        void Update ()
        {
            // Get inputH and inputV
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            // Move controller based on inputH and inputV
            arwingController.Move(inputH, inputV);

            // --EXTRAS--
            // Call arwing shoot if we press a button

            // Call arwing pulse if we press a button
        }
    }
}
