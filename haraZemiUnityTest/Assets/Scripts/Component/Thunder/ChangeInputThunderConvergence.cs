using UnityEngine;
using UnityEngine.InputSystem;

//入力により、電気の収束率を変更
//念のため、キーボードとマイコン両方からの入力を可能にする

public class ChangeInputThunderConvergence : MonoBehaviour
{
    [SerializeField]
    ThunderConvergence _thunderConvergence;

    [SerializeField]
    RotaryEncoderInput _rotaryEncoderInput;

    [Tooltip("キーボード入力の変化量")] [SerializeField]
    float _inputKeyBoardDelta = 0.5f;

    [Tooltip("ロータリーエンコーダー入力の変化量")] [SerializeField]
    float _inputRotaryEncoderDelta = 0.5f;

    bool inc=false;
    bool dec=false;

    public void Inc(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            inc = true;
        }
        else if(context.canceled)
        {
            inc = false;
        }
    }

    public void Dec(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dec = true;
        }
        else if (context.canceled)
        {
            dec = false;
        }
    }
    private void Awake()
    {
        _rotaryEncoderInput.OnChangeValue += OnChangeRotaryEncoderValue;
    }

    void OnChangeRotaryEncoderValue(int input)
    {
        _thunderConvergence.ConvergenceRate += _inputRotaryEncoderDelta * (float)input;
    }

    private void Update()
    {
        if(inc)
        {
            _thunderConvergence.ConvergenceRate += _inputKeyBoardDelta * Time.deltaTime;
        }

        if(dec)
        {
            _thunderConvergence.ConvergenceRate -= _inputKeyBoardDelta * Time.deltaTime;
        }
    }
}
