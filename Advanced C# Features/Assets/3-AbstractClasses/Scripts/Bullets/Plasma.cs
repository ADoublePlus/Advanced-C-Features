using UnityEngine;

namespace AbstractClasses
{
    public class Plasma : Bullet
    {
        private Rigidbody2D rigid;

        void Awake ()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public override void Fire(Vector3 direction, float? speed = null)
        {
            // SET currentSpeed to the member speed 
            float currentSpeed = this.speed;

            // IF the optional argument has been set
            if (speed != null)
            {
                // Replace currentSpeed with the argument
                currentSpeed = speed.Value;
            }

            // AddForce in the direction and currentSpeed
            rigid.AddForce(direction * currentSpeed, ForceMode2D.Impulse);
        }
    }
}