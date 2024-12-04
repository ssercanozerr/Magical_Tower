using Assets.Scripts.Signals;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        void Start()
        {
            for (int i = 0; i < 6; i++)
            {
                PoolSignals.Instance.onGetObjFromPool?.Invoke("Beaver");
                PoolSignals.Instance.onGetObjFromPool?.Invoke("Ghost");
                PoolSignals.Instance.onGetObjFromPool?.Invoke("Golem");
            }
        }

        void Update()
        {

        }
    }
}