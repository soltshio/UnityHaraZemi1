//デバイス側

#include "ESPNowEz.h"
#include "MPU_AccGyro.h"
#include "ControllerMessageTransmitter.h"
#include "LEDIndicator.h"
#include "LEDIndicatorStateMachine.h"
#include "Rotary.h"

#define LEDPin 0
#define SWPin 7
#define RotaryAPin 4
#define RotaryBPin 5

//このデバイスのMACアドレス
//60:55:f9:96:33:8c

CESPNowEZ espnow(1); // Deviceは1以上で必ずかぶらないこと。

ESPNOW_Dev2ConData deviceData;

uint8_t controllerAddress[] = { 0x10, 0x51, 0xdb, 0x18, 0x1f, 0xbc };

//加速度センサー
MPU_AccGyro _mpu;

//ロータリーエンコーダー
Rotary _rotaryEncoder(RotaryAPin,RotaryBPin);

//LED
LEDIndicator _led(LEDPin);

//unity側からの通信を受け取るための機能
ESPNOW_Con2DevData controllerData;
ControllerMessageTransmitter _transmitter;
int outputFlag;

//LEDの制御
LEDStateMachine _ledStateMachine(_led,_transmitter);



char outputText[16];

void setup() {

  pinMode(SWPin,INPUT_PULLUP);
  _led.Setup();
  _rotaryEncoder.Setup();

  espnow.Initialize(OnDataReceived, nullptr);
  espnow.SetControllerMacAddr(controllerAddress);
  Serial.begin(115200);

  _mpu.setup();//見本だとSerial.beginの後に書いているため、念のためこっちに書いておく
}
 
void loop() {

  _mpu.tick();
  _ledStateMachine.Tick();
  _rotaryEncoder.Tick();

  //〇送信
  //データ書き込み
  deviceData.gyroX=_mpu.gyroX();
  deviceData.gyroZ=_mpu.gyroZ();
  deviceData.sw=digitalRead(SWPin);
  deviceData.delta=_rotaryEncoder.Delta();

  // 送信、loop内で実施。
  // Serial.printlnを以下の行で置き換えるが、デバッグ用にSerial.printlnは残しておいても良い。
  espnow.Send(&deviceData, sizeof(deviceData));

  //sprintf(outputText,"S%+06d%+06d%dE",deviceData.gyroX,deviceData.gyroZ,deviceData.sw);
  //Serial.println(outputText);


  //〇unity側からの通信を受け取る
  _transmitter.ResetOutputFlag();
  if(outputFlag)
  {
    outputFlag = 0;
    _transmitter.SetMessage(controllerData.cmd, controllerData.rate);
    //Serial.print(controllerData.cmd);
  }

  // loop内に適切なdeleyを付加
  delay(10);

  // Serial.println(espnow.GetMacAddrChar());
  // delay(1000);
}

void OnDataReceived(const esp_now_recv_info* info, const uint8_t* data, int data_len)
{
   // 受信時の処理
   memcpy(&controllerData, data, data_len);
   outputFlag = 1;
}