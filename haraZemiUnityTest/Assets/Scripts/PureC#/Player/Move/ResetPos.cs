using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//位置をリセットする

[System.Serializable]
public class ResetPos
{
    [Tooltip("リセットにかける時間")] [Min(0)] [SerializeField]
    float _resetDuration=0.1f;

    float _current;
    Vector2 _prePos;

    public bool IsReseting { get { return _current > 0; } }

    public void OnPushedResetButton(Vector2 currentPos)//位置リセットのボタンが押された瞬間の処理
    {
        _current = _resetDuration;
        _prePos = currentPos;
    }

    public Vector2 DestinationOnReset()//リセット時の移動先、リセット中は呼ばないようにする
    {
        _current -= Time.deltaTime;
        float t = _current / _resetDuration;

        Vector2 destination;

        destination.x = Mathf.Lerp(0, _prePos.x, t);
        destination.y = Mathf.Lerp(0, _prePos.y, t);

        return destination;
    }

}
