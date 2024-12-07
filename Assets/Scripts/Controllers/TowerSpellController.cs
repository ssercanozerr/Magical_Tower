using Assets.Scripts.Controllers;
using Assets.Scripts.Signals;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpellController : MonoBehaviour
{
    [SerializeField] private Image fireballCoolDownImg;
    [SerializeField] private float fireballCooldown;
    [SerializeField] private float fireballSpeed;
    [SerializeField] private float fireballDamage;

    [SerializeField] private Image barrageCoolDownImg;
    [SerializeField] private float barrageCooldown;
    [SerializeField] private float barrageSpeed;
    [SerializeField] private float barrageDamage;

    private float _fireballTimer = 0f;
    private float _barrageTimer = 0f;
    private List<GameObject> _enemies;

    private void Awake()
    {
        SetCooldown();
    }

    private void Update()
    {
        _enemies = GameSignals.Instance.onReturnEnemies?.Invoke();

        _fireballTimer -= Time.deltaTime;
        _barrageTimer -= Time.deltaTime;

        _fireballTimer = Mathf.Clamp(_fireballTimer, 0f, fireballCooldown);
        _barrageTimer = Mathf.Clamp(_barrageTimer, 0f, barrageCooldown);

        SetCooldown();
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
                GameObject icicle = PoolSignals.Instance.onGetObjFromPool?.Invoke("Icicle");
                icicle.transform.position = transform.position + new Vector3(0, 10, 0);
                icicle.transform.LookAt(enemy.transform);
                icicle.GetComponent<SpellsController>().Initialize(enemy.transform.position, barrageSpeed, barrageDamage);
            }
        }
    }

    private void SetCooldown()
    {
        fireballCoolDownImg.fillAmount = _fireballTimer / fireballCooldown;
        barrageCoolDownImg.fillAmount = _barrageTimer / barrageCooldown;
    }
}
