using UnityEngine;
using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyInfos enemyInfos;
        
        private float _attackDistance;
        private float _speed;
        private Transform _tower;
        private Animator _animator;

        private void Start()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            
            _tower = tower.transform;
            _speed = enemyInfos.speed;
            _attackDistance = enemyInfos.attackDistance;
            
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            float distanceToTower = Vector3.Distance(transform.position, _tower.position);

            if (distanceToTower <= _attackDistance)
            {
                TriggerAttack();
            }
            else
            {
                MoveTowardsTower();
            }
        }

        private void MoveTowardsTower()
        {
            Vector3 direction = (_tower.position - transform.position).normalized;
            transform.position += _speed * Time.deltaTime * direction;
        }

        private void TriggerAttack()
        {
            _animator.SetBool("Attack", true);            
        }

        private void HitDamage()
        {
            Debug.Log("hit damage " + enemyInfos.damage);
        }
    }
}
