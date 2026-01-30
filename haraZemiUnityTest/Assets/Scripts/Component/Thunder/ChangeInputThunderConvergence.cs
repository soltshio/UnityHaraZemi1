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

    [Tooltip("マウス入力の変化量")] [SerializeField]
    float _inputMouseDelta = 0.5f;

    [Tooltip("ロータリーエンコーダー入力の変化量")] [SerializeField]
    float _inputRotaryEncoderDelta = 0.5f;

    public void SetValue(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        _thunderConvergence.ConvergenceRate += input.y * _inputMouseDelta;
    }

    private void Awake()
    {
        _rotaryEncoderInput.OnChangeValue += OnChangeRotaryEncoderValue;
    }

    void OnChangeRotaryEncoderValue(int input)
    {
        _thunderConvergence.ConvergenceRate += _inputRotaryEncoderDelta * (float)input;
    }
}
