using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform tower;
        [SerializeField] private float spawnRadius;
        [SerializeField] private int enemyCount;

        private void Start()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < enemyCount; i++)
            {
                float angle = Random.Range(0f, 360f);
                float radians = angle * Mathf.Deg2Rad;

                Vector3 spawnPosition = tower.position + new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * spawnRadius;

                string enemyType = GetRandomEnemyType();
                GameObject enemy = PoolSignals.Instance.onGetObjFromPool?.Invoke(enemyType);

                if (enemy != null)
                {
                    enemy.transform.position = spawnPosition;
                    enemy.transform.LookAt(tower);
                }
            }
        }

        private string GetRandomEnemyType()
        {
            string[] enemyTypes = { "Beaver", "Ghost", "Golem" };
            int randomIndex = Random.Range(0, enemyTypes.Length);
            return enemyTypes[randomIndex];
        }
    }
}