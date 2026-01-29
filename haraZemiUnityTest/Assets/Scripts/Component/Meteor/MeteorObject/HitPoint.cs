using System;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    [Tooltip("HPの最大値")] [Min(0)] [SerializeField]
    float _hpMax;

    [Tooltip("HPが0になった時にロックを外さない限り、体力が変動しないようにするか")] [SerializeField]
    bool _shouldLockHPOnDead=true;

    public event Action OnDead;

    float _hp;//現在の体力
    bool _isLockHP;//HPをロック中か

    public float HP
    {
        get { return _hp; }
        set 
        {
            if (_isLockHP) return;

            _hp = Mathf.Max(0,value);

            if(_hp<=0)//体力が尽きた時
            {
                if (_shouldLockHPOnDead) _isLockHP = true;

                OnDead?.Invoke();
            }
        }
    }

    public float HPMax
    {
        get { return _hpMax; }
    }

    public bool IsLockHP
    {
        get { return _isLockHP; }
        set { _isLockHP = value; }
    }

    private void OnEnable()
    {
        _isLockHP = false;
        _hp = _hpMax;
    }
}
