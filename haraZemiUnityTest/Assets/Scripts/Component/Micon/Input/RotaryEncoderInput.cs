using UnityEngine;
using System;
using Unity.VisualScripting.Dependencies.Sqlite;

//ロータリーエンコーダーの入力

public class RotaryEncoderInput : MonoBehaviour
{
    [Tooltip("閾値(最大)")] [SerializeField]
    int _thresholdMax = 0;

    [Tooltip("閾値(最小)")] [SerializeField]
    int _thresholdMin = 0;

    public event Action<int> OnChangeValue;//変化量が閾値以内の時は呼ばない

    public void UpdateInputInfo(int input)
    {
        if (MathfExtension.IsInRange(input, _thresholdMin, _thresholdMax)) return;//閾値以内であれば無視

        OnChangeValue?.Invoke(input);
    }
}
