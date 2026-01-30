using System;
using UnityEngine;

//ゲームオーバー条件を満たしているかを判定する(ゲームオーバー・クリアの判定はGameSetManagerから)
public class GameOverConditionChecker : MonoBehaviour
{
    [SerializeField]
    LayerMask _meteorLayer;

    public event Action OnSetGameOver;

    private void OnTriggerEnter(Collider other)
    {
        if (!_meteorLayer.Contains(other.gameObject)) return;

        OnSetGameOver?.Invoke();
    }
}
