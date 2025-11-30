using System.Collections.Generic;
using UnityEngine;

public class SendManager : MonoBehaviour
{
    // シリアル通信のクラス、クラス名は正しく書くこと
    [SerializeField]
    SerialHandler _serialHandler;

    Queue<string> _messageQueue=new Queue<string>();

    public void Send(string message)
    {
        _messageQueue.Enqueue(message);
    }
    
    void Update()
    {
        while(_messageQueue.Count != 0)
        {
            string message = _messageQueue.Dequeue();

            _serialHandler.Write(message);
        }
    }
}
