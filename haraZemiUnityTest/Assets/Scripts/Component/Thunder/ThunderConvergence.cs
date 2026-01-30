using System;
using System.Collections;
using UnityEngine;

//電気の収束率

public class ThunderConvergence : MonoBehaviour
{
    float _convergenceRate;
    public event Action<float> OnChangedValue;//0～1の間で収束率を返す

    public float ConvergenceRate
    {
        get { return _convergenceRate; }
        set
        {
            _convergenceRate = Mathf.Clamp01(value);
            OnChangedValue?.Invoke(_convergenceRate);
        }
    }

    IEnumerator Start()
    {
        yield return null;//他のコンポーネントがこのコンポーネントにアクセスし終わるまで、1フレーム待つ

        ConvergenceRate = 0;
    }
}
