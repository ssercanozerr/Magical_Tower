using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class SpellsController : MonoBehaviour
    {
        private Vector3 _targetPosition;
        private float _speed;
        private float _damage;

        public void Initialize(Vector3 target, float speed, float damage)
        {
            _targetPosition = target;
            _speed = speed;
            _damage = damage;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Beaver") || collision.gameObject.CompareTag("Ghost") || collision.gameObject.CompareTag("Golem"))
            {
                PoolSignals.Instance.onObjReturnToPool(gameObject.tag, gameObject);
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(_damage);
            }
        }
    }
}