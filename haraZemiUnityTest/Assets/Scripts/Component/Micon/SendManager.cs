using System.Collections.Generic;
using UnityEngine;

public class SendManager : MonoBehaviour
{
    [SerializeField]
    ThunderOnHitMeteor _onHit;

    [SerializeField]
    DebugSendConvergence _sendConvergence;
    
    void Update()
    {
        var serialHandlerInstance = SerialHandler.Instance;

        if (serialHandlerInstance == null) return;
        
        string message;

        if (_onHit.HasMessage(out message))
        {
            serialHandlerInstance.Write(message);
        }
        if(_sendConvergence.HasMessage(out message))
        {
            serialHandlerInstance.Write(message);
        }
    }
}
