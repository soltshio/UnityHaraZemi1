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

    [SerializeField]
    RotaryEncoderInput _rotaryEncoderInput;//ロータリーエンコーダーの情報を渡す用のクラス

    const int _errorNum = -1; 

    void Start()
    {
        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        _serialHandler.OnDataReceived += OnDataReceived;
    }

    //受信した信号(message)に対する処理
    //messageのプロトコル
    //加速度センサー系は符号+5桁数字=6桁
    //S(6桁GyroX)(6桁GyroZ)(スイッチ1桁)(4桁ロータリーエンコーダー)E
    void OnDataReceived(string message)
    {
        if (message == null)
            return;

        // ここでデコード処理等を記述
        //Debug.Log(message);

        int start = 1;

        //加速度センサーからジャイロの抽出

        const int accelerationSensorLength = 6;

        int gyroX = GetValueFromMessage(message, start, accelerationSensorLength);
        if (gyroX == _errorNum) return;
        start += accelerationSensorLength;

        int gyroZ = GetValueFromMessage(message, start, accelerationSensorLength);
        if (gyroZ == _errorNum) return;
        start += accelerationSensorLength;

        _accelerationSensorInput.UpdateSensorInfo(gyroX, gyroZ);

        //ボタンの入力情報の抽出

        const int switchLength = 1;

        int sw = GetValueFromMessage(message, start, switchLength);
        start++;

        _buttonInput.UpdateInputInfo(sw);

        //ロータリーエンコーダーの入力情報の抽出

        const int rotaryEncoderLength = 4;

        int delta = GetValueFromMessage(message, start, rotaryEncoderLength);
        start += rotaryEncoderLength;

        _rotaryEncoderInput.UpdateInputInfo(delta);
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
