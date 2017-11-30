using UnityEngine;

namespace AbstractClasses
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class Explosive : Bullet
    {
        public GameObject explosionPrefab;

        public int explosionDamage = 10;
        public float explosionRadius = 25;

        private Rigidbody2D rigid;

        void Awake ()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public override void Fire (Vector3 direction, float? speed = null)
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

        public void OnCollisionEnter (Collision other)
        {
            GameObject expl = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;

            Destroy(gameObject);

            Destroy(expl, 3);
        }

        /* public void Explode ()
        {
            GameObject clone = Instantiate(explosionPrefab);
            clone.transform.position = transform.position;

            // Clone the particle explosion then play on contact
            ParticleSystem explosion = clone.GetComponent<ParticleSystem>();

            explosion.Play();

            Destroy(explosionPrefab, 3f);
        } */
    }
}
