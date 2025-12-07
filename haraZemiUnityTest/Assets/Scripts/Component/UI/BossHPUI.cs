using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    [SerializeField]
    Image _hpBar;

    [SerializeField]
    HitPoint _hp;

    void Update()
    {
        _hpBar.fillAmount = _hp.HP / _hp.HPMax;
    }
}
