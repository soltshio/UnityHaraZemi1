#include "ESPNowEz.h"
#include <ctype.h>
//このコントローラのMACアドレス
//10:51:db:18:1f:bc

CESPNowEZ espnow(0);

uint8_t deviceMacAddr[] = { 0x60, 0x55, 0xf9, 0x96, 0x33, 0x8c }; // ID1

ESPNOW_Con2DevData controllerData;

ESPNOW_Dev2ConData deviceData;

int outputFlag;

char inputChar;
const int _rateDigit=3;

char outputText[16];

void setup() {
  outputFlag = 0;
  espnow.Initialize(OnDataReceived, nullptr);
  espnow.SetDeviceMacAddr(deviceMacAddr);
  Serial.begin(115200);
}

void loop() {
  if(outputFlag)
  {
    outputFlag = 0;
    //Unity側に出力
    //S(符号ジャイロX5桁)(符号ジャイロZ5桁)(スイッチ1桁)(符号ロータリーエンコーダー変化量3桁)E
    sprintf(outputText,"S%+06d%+06d%d%+04dE",deviceData.gyroX,deviceData.gyroZ,deviceData.sw,deviceData.delta);
    Serial.println(outputText);
  }

  if(Serial.available() > 0)
  {
    inputChar = Serial.read();

    //Serial.println(inputChar);

    if(isdigit(inputChar))//数字だったら0～9の10段階で来ているはず
    {
      int value = inputChar - '0';

      controllerData.rate=value;
      //Serial.println(controllerData.rate);
    }

    else if(inputChar != '\0')//文字だったらコマンド(1つのみ)
    {
      controllerData.cmd = inputChar;
      //Serial.println(controllerData.cmd);
    }

    
    
    espnow.Send(1, &controllerData, sizeof(controllerData)); // id:1に送る
  }

  // Serial.println(espnow.GetMacAddrChar());
  // delay(1000);
}

void OnDataReceived(const esp_now_recv_info* info, const uint8_t* data, int data_len)
{
  // 受信時の処理
  memcpy(&deviceData, data, data_len);
  outputFlag = 1;
}
