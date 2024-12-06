using Assets.Scripts.Controllers;
using Assets.Scripts.Signals;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpellController : MonoBehaviour
{
    [SerializeField] private float fireballCooldown;
    [SerializeField] private float fireballSpeed;
    [SerializeField] private float fireballDamage;

    [SerializeField] private float barrageCooldown;
    [SerializeField] private float barrageSpeed;
    [SerializeField] private float barrageDamage;

    private float _fireballTimer = 0f;
    private float _barrageTimer = 0f;
    private List<GameObject> _enemies;

    private void Update()
    {
        _enemies = GameSignals.Instance.onReturnEnemies?.Invoke();
        _fireballTimer -= Time.deltaTime;
        _barrageTimer -= Time.deltaTime;
    }

    public void FireballSpell()
    {
        if (_fireballTimer <= 0f && _enemies.Count > 0)
        {
            CastFireball();
            _fireballTimer = fireballCooldown;
        }
    }

    public void BarrageSpell()
    {
        if (_barrageTimer <= 0f && _enemies.Count > 0)
        {
            CastBarrage();
            _barrageTimer = barrageCooldown;
        }
    }

    private void CastFireball()
    {
        GameObject enemy = _enemies[Random.Range(0, _enemies.Count)];
        if (enemy != null)
        {
            GameObject fireball = PoolSignals.Instance.onGetObjFromPool?.Invoke("Fireball");
            fireball.transform.position = transform.position + new Vector3(0, 10, 0);
            fireball.GetComponent<SpellsController>().Initialize(enemy.transform.position, fireballSpeed, fireballDamage);
        }
    }

    private void CastBarrage()
    {
        foreach (GameObject enemy in _enemies)
        {
            if (enemy != null)
            {
                GameObject bullet = PoolSignals.Instance.onGetObjFromPool?.Invoke("Icicle");
                bullet.transform.position = transform.position + new Vector3(0, 10, 0);
                bullet.transform.LookAt(enemy.transform);
                bullet.GetComponent<SpellsController>().Initialize(enemy.transform.position, barrageSpeed, barrageDamage);
            }
        }
    }
}
