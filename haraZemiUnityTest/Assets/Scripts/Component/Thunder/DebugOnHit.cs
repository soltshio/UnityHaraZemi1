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
        _sendManager.Send(MiconCommandDictionary.normal);
    }

    private void OnHit(RaycastHit hit)
    {
        _timer = _timeOutDuration;

        if (!IsTimeOut) return;
        //ヒット
        _sendManager.Send(MiconCommandDictionary.sparking);
    }
}
