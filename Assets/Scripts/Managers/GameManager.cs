﻿using Assets.Scripts.Controllers;
using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameController gameController;

        private void OnEnable()
        {
            GameSignals.Instance.onReturnEnemies += gameController.OnReturnEnemies;
        }

        private void OnDisable()
        {
            GameSignals.Instance.onReturnEnemies -= gameController.OnReturnEnemies;
        }
    }
}