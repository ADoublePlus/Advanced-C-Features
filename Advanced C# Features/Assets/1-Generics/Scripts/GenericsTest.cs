using UnityEngine;

namespace Generics
{
    public class GenericsTest : MonoBehaviour
    {
        public class Orc
        {
            int health = 100;

            public void TakeDamage ()
            {
                health -= 0;
            }

            public void TakeDamage (int damage)
            {
                health -= damage;
            }
        }

        public Orc orc = new Orc();
        public CustomList<GameObject> gameObjects;

        // Use this for initialization
        void Start ()
        {
            orc.TakeDamage();

            gameObjects = new CustomList<GameObject>();
            gameObjects.Add(new GameObject());
            gameObjects.Add(new GameObject());
            gameObjects.Add(new GameObject());
            gameObjects.Add(new GameObject());
            gameObjects.Add(new GameObject());

            GameObject firstObject = gameObjects[0];
        }
    }
}
