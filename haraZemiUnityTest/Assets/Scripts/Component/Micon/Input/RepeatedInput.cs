using System;
using UnityEngine;
using UnityEngine.InputSystem;

//ボタンの連打判定

public class RepeatedInput : MonoBehaviour
{
    [Tooltip("連打と判定する閾値\nこの秒数以内にもう一度押したら連打判定")] [SerializeField]
    float _threshold;

    [SerializeField]
    ButtonInput _buttonInput;

    public event Action OnRepeatedInputDown;//連打時に呼ぶ
    public event Action<int> OnRepeatedInputDown_Count;//連打時に呼ぶ(押下回数も伝える)

    public event Action OnStopRepeatedInput;//連打が止まってしまった瞬間に呼ぶ

    const int _judgeRepeatedCount=2;//連打と判定する回数

    float _current;
    int _count;//連打回数

    public void OnInputDown(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnInputDown();
    }

    private void Awake()
    {
        _buttonInput.OnInputDown += OnInputDown;
    }

    void OnInputDown()
    {
        _count++;

        if(!IsRepeatedTimeOut())//連打判定
        {
            OnRepeatedInputDown?.Invoke();
            OnRepeatedInputDown_Count?.Invoke(_count);
        }

        _current = _threshold;
    }

    private void Start()
    {
        _current=0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRepeatedTimeOut()) return;

        _current -= Time.deltaTime;

        if (!IsRepeatedTimeOut()) return;

        if (_count >= _judgeRepeatedCount)
        {
            OnStopRepeatedInput?.Invoke();
        }

        _count = 0;
    }

    bool IsRepeatedTimeOut()
    {
        return _current <= 0;
    }
}
