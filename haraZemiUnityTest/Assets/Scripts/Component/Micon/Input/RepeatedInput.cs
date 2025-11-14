using UnityEngine;

//ボタンの連打判定(1回だけ押すのと連続で押すのを分ける)

public class RepeatedInput : MonoBehaviour
{
    [Tooltip("連打と判定する閾値")] [SerializeField]
    float repeatedInputThreshold;

    [SerializeField]
    ButtonInput _buttonInput;

    bool _repeatedInputting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
