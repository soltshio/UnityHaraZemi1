using System;
using UnityEngine;
using System.Collections;

public class ActivateThunder : MonoBehaviour
{
    [SerializeField]
    HoldDownInput _holdDownInput;

    bool _isActive;
    public event Action<bool> OnChangedValue;

    public bool IsActive
    {
        get { return _isActive; }
        private set
        {
            _isActive = value;
            OnChangedValue?.Invoke(value);
        }
    }


    private void Awake()
    {
        _holdDownInput.OnStartHold += Activate;
        _holdDownInput.OnEndHold += DeActivate;
    }

    void Activate()
    {
        IsActive = true;
    }

    void DeActivate()
    {
        IsActive = false;
    }

    IEnumerator Start()
    {
        yield return null;//他のコンポーネントがこのコンポーネントにアクセスし終わるまで、1フレーム待つ

        IsActive = false;
    }
}
