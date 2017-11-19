using UnityEngine;

namespace Delegates
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform target;
        public GameObject orcPrefab, trollPrefab;
        public float minAmount = 0, maxAmount = 20;
        public float spawnRate = 1f;

        // Use this for initialization
        void Start ()
        {
            // Spawn the trollPrefab every second
            InvokeRepeating("SpawnTroll", 2f, spawnRate);
        }

        void SpawnOrc ()
        {
            Instantiate(orcPrefab, transform.position, Quaternion.identity);
        }

        void SpawnTroll ()
        {
            GameObject clone = Instantiate(trollPrefab, transform.position, Quaternion.identity);

            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.SetTarget(target);
        }

        public delegate void MyDelegate();

        // Delegate function that stores SpawnOrc & SpawnTroll
        public delegate void SpawnEnenmy(float spawnRate);
        SpawnEnenmy[] delegates = new SpawnEnenmy[5];
    }
}