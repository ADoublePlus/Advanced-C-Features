using UnityEngine;

namespace Delegates
{
    public class Health : MonoBehaviour
    {
        public int health = 100;

        // Update is called once per frame
        void Update()
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }
    }
}
