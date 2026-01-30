using UnityEngine;
using System;
using UnityEngine.InputSystem;

//ボタンの長押し判定(何秒押し続けたら長押し判定になるか)

public class HoldDownInput : MonoBehaviour
{
    [Tooltip("長押しと判定する閾値\nこの秒数以上押し続けたら長押し判定")] [SerializeField]
    float _threshold;

    [SerializeField]
    ButtonInput _buttonInput;

    public event Action OnStartHold;//長押し開始
    public event Action OnEndHold;//長押し終了

    float _current;
    bool _isHolding = false;//ボタンを押しているか

    public void OnInputDown(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnInputDown();
    }

    public void OnInputUp(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;

        OnInputUp();
    }

    private void Awake()
    {
        _buttonInput.OnInputDown += OnInputDown;
        _buttonInput.OnInputUp += OnInputUp;
    }

    void OnInputDown()
    {
        _current = 0;
        _isHolding=true;
    }

    void OnInputUp()
    {
        _isHolding = false;

        if(IsOverThreshold())
        {
            OnEndHold?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isHolding) return;

        if (IsOverThreshold()) return;

        _current += Time.deltaTime;

        if (!IsOverThreshold()) return;
        
        OnStartHold?.Invoke();
    }

    bool IsOverThreshold()
    {
        return _current > _threshold;
    }
}
