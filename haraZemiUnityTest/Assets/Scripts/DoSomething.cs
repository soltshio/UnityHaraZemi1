// Unityでシリアル通信で送られてくるデータをデコードする雛形
// 2025_8月Ver.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DoSomething : MonoBehaviour
{
    // 制御対象のオブジェクト用に宣言しておいて、Start関数内で名前で検索
    GameObject harazemichanObject;
    StarterAssetsInputs harazemichanSAIScript;

    BlockDetector blockDetectorScript;

    //GameObject targetObject;

    // 制御対象にアタッチされたスクリプト
    Player targetScript; // この記述ではアタッチするGameObjectにMainスクリプトがアタッチされている必要がある

    // シリアル通信のクラス、クラス名は正しく書くこと
    public SerialHandler serialHandler;

  void Start()
    {
        // 制御対象のオブジェクトを取得、このオブジェクトにMain.csが関連付けられている
        harazemichanObject = GameObject.FindWithTag("Player"); // この記述ではUnityのヒエラルキーにGameMasterオブジェクトがいる必要がある。
        harazemichanSAIScript = harazemichanObject.GetComponent<StarterAssetsInputs>();
        blockDetectorScript = harazemichanObject.GetComponent<BlockDetector>();

        // 制御対象にアタッチされたスクリプトを取得。
        // 大文字、小文字を区別するので、player.csを作ったのなら「p」layer。
        targetScript = GameObject.Find("PlayerObject").GetComponent<Player>();// Mainスクリプトがアタッチされている必要がある

        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update()
    {
        // UnityからArduinoに送る場合はココに記述
        //string command = "hogehoge";
        //serialHandler.Write(command);

        if (targetScript.jklPress[0])
        {
            targetScript.jklPress[0] = false;
            if (targetScript.jklToggle[0])
            {
                // LED ON
                serialHandler.Write("a");
                Debug.Log(0+"ON");
            }
            else
            {
                // LED OFF
                serialHandler.Write("b");
                Debug.Log(0 + "OFF");
            }
        }

        if (targetScript.jklPress[1])
        {
            targetScript.jklPress[1] = false;
            if (targetScript.jklToggle[1])
            {
                // LED ON
                serialHandler.Write("c");
                Debug.Log(1 + "ON");
            }
            else
            {
                // LED OFF
                serialHandler.Write("d");
                Debug.Log(1 + "OFF");
            }
        }

        if (targetScript.jklPress[2])
        {
            targetScript.jklPress[2] = false;
            if (targetScript.jklToggle[2])
            {
                // LED ON
                serialHandler.Write("e");
                Debug.Log(2 + "ON");
            }
            else
            {
                // LED OFF
                serialHandler.Write("f");
                Debug.Log(2 + "OFF");
            }
        }


        if (blockDetectorScript.led1on)
        {
            blockDetectorScript.led1on = false;
            serialHandler.Write("a");
        }

        if (blockDetectorScript.led1off)
        {
            blockDetectorScript.led1off = false;
            serialHandler.Write("b");
        }

        if (blockDetectorScript.led2on)
        {
            blockDetectorScript.led2on = false;
            serialHandler.Write("c");
        }

        if (blockDetectorScript.led2off)
        {
            blockDetectorScript.led2off = false;
            serialHandler.Write("d");
        }

        if (blockDetectorScript.led3on)
        {
            blockDetectorScript.led3on = false;
            serialHandler.Write("e");
        }

        if (blockDetectorScript.led3off)
        {
            blockDetectorScript.led3off = false;
            serialHandler.Write("f");
        }

        if (blockDetectorScript.soudnPlay)
        {
            blockDetectorScript.soudnPlay = false;
            serialHandler.Write("g");
        }
    }

    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        if (message == null)
            return;

        // ここでデコード処理等を記述

        string receivedData;
        int t;

        for (int i = 0; i<5 ;i++)
        {
            receivedData = message.Substring(i + 1, 1);
            int.TryParse(receivedData, out t);
            targetScript.sw[i] = t;
        }

        float x = 0f, y = 0f;
        bool isMoveChange = false;

        //x
        if (targetScript.sw[0] == 0 && targetScript.swPre[0] == 1)
        {
            x = 1f;
            isMoveChange = true;
        }
        else if (targetScript.sw[0] == 1 && targetScript.swPre[0] == 0)
        {
            x = 0f;
            isMoveChange = true;
        }

        if (targetScript.sw[1] == 0 && targetScript.swPre[1] == 1)
        {
            x = -1f;
            isMoveChange = true;
        }
        else if (targetScript.sw[1] == 1 && targetScript.swPre[1] == 0)
        {
            x = 0f;
            isMoveChange = true;
        }

        //y
        if (targetScript.sw[2] == 0 && targetScript.swPre[2] == 1)
        {
            y = 1f;
            isMoveChange = true;
        }
        else if (targetScript.sw[2] == 1 && targetScript.swPre[2] == 0)
        {
            y = 0f;
            isMoveChange = true;
        }

        if (targetScript.sw[3] == 0 && targetScript.swPre[3] == 1)
        {
            y = -1f;
            isMoveChange = true;
        }
        else if (targetScript.sw[3] == 1 && targetScript.swPre[3] == 0)
        {
            y = 0f;
            isMoveChange = true;
        }

        if (isMoveChange)
        {
            harazemichanSAIScript.MoveInput(new Vector2(x, y));
        }

        if (targetScript.sw[4] == 0 && targetScript.swPre[4] == 1)
            harazemichanSAIScript.JumpInput(true);
    }
}
