// Unityでシリアル通信で送られてくるデータをデコードする雛形
// 2025_8月Ver.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//マイコンから来るメッセージをデコードする

public class DeccordManager : MonoBehaviour
{
    // シリアル通信のクラス、クラス名は正しく書くこと
    [SerializeField]
    SerialHandler _serialHandler;

    [SerializeField]
    AccelerationSensorInput _accelerationSensorInput;//センサーの情報を渡す用のクラス

    [SerializeField]
    ButtonInput _buttonInput;//スイッチの情報を渡す用のクラス

    void Start()
    {
        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        _serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update()
    {
        // UnityからArduinoに送る場合はココに記述
        //string command = "hogehoge";
        //serialHandler.Write(command);
    }

    //受信した信号(message)に対する処理
    //messageのプロトコル
    //加速度センサー系は符号+5桁数字=6桁
    //S(6桁GyroX)(6桁GyroZ)(スイッチ1桁)E
    void OnDataReceived(string message)
    {
        if (message == null)
            return;

        // ここでデコード処理等を記述
        Debug.Log(message);

        int start = 1;

        //加速度センサーからジャイロの抽出

        const int accelerationSensorLength = 6;

        int gyroX = GetValueFromMessage(message, start, accelerationSensorLength);
        start += accelerationSensorLength;

        int gyroZ = GetValueFromMessage(message, start, accelerationSensorLength);
        start += accelerationSensorLength;

        _accelerationSensorInput.UpdateSensorInfo(gyroX, gyroZ);

        //ボタンの入力情報の抽出

        int sw = GetValueFromMessage(message, start, 1);
        start++;

        _buttonInput.UpdateInputInfo(sw);
        
    }

    int GetValueFromMessage(string message,int start,int length)
    {
        string receivedData = message.Substring(start, length);

        if(!int.TryParse(receivedData, out int t))
        {
            Debug.Log("データの変換に失敗");
            return -1;
        }

        return t;
    }
}
