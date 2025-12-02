using UnityEngine;

public class DebugOnHit : MonoBehaviour
{
    [SerializeField]
    SendManager _sendManager;

    [SerializeField]
    ShotThunderRay _shotThunderray;

    [SerializeField]
    float _timeOutDuration = 0.5f;

    private float _timer = 0f;//残り時間

    bool IsTimeOut { get { return _timer <= 0; } }

    bool _outputFlag = false;
    string _message;

    public bool HasMessage(out string message)
    {
        message = null;

        if (_outputFlag)
        {
            message = _message;
            _outputFlag = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Awake()
    {
        _shotThunderray.OnHit += OnHit;
    }

    private void Update()
    {
        if (IsTimeOut) return;

        _timer -= Time.deltaTime;

        if (!IsTimeOut) return;
        //タイムアウト
        _outputFlag = true;
        _message = MiconCommandDictionary.normal;
    }

    private void OnHit(RaycastHit hit)
    {
        if(IsTimeOut)
        {
            //ヒット
            _outputFlag = true;
            _message = MiconCommandDictionary.sparking;
        }

        _timer = _timeOutDuration;
    }
}
