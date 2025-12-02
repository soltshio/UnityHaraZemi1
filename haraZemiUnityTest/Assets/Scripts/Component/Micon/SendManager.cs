using System.Collections.Generic;
using UnityEngine;

public class SendManager : MonoBehaviour
{
    // シリアル通信のクラス、クラス名は正しく書くこと
    [SerializeField]
    SerialHandler _serialHandler;

    [SerializeField]
    DebugOnHit _onHit;

    [SerializeField]
    DebugSendConvergence _sendConvergence;
    
    void Update()
    {
        string message;

        if(_onHit.HasMessage(out message))
        {
            _serialHandler.Write(message);
        }
        if(_sendConvergence.HasMessage(out message))
        {
            _serialHandler.Write(message);
        }
    }
}
