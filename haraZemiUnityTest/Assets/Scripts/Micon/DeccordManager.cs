// Unityでシリアル通信で送られてくるデータをデコードする雛形
// 2025_8月Ver.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeccordManager : MonoBehaviour
{
    // シリアル通信のクラス、クラス名は正しく書くこと
    [SerializeField]
    SerialHandler serialHandler;

    [SerializeField]
    SensorInput _sensorInput;//センサーの情報を渡す用のクラス

  void Start()
    {
        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        serialHandler.OnDataReceived += OnDataReceived;
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
    //S(6桁AccX)(6桁AccY)(6桁AccZ)(6桁GyroX)(6桁GyroY)(6桁GyroZ)E
    void OnDataReceived(string message)
    {
        if (message == null)
            return;

        // ここでデコード処理等を記述
        //Debug.Log(message);

        const int accelerationSensorLength = 6;

        //加速度の抽出
        const int accLength=3;
        int[] acc = new int[accLength];

        int start = 1;

        for(int i=0; i<accLength ;i++)
        {
            acc[i] = GetValueFromMessage(message,start,accelerationSensorLength);
            start += accelerationSensorLength;
        }

        //ジャイロの抽出
        const int gyroLength = 3;
        int[] gyro = new int[gyroLength];

        for(int i=0; i<gyroLength ;i++)
        {
            gyro[i] = GetValueFromMessage(message, start, accelerationSensorLength);
            start += accelerationSensorLength;
        }

        _sensorInput.UpdateSensorInfo(acc, gyro);
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
