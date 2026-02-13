#include "Rotary.h"
#include <Arduino.h>

// static 変数の実体定義
Rotary* Rotary::_instance = nullptr;

Rotary::Rotary(int pinA,int pinB):_pinA(pinA),_pinB(pinB),_encoder(_pinA, _pinB, RotaryEncoder::LatchMode::TWO03)
{
  _instance = this;
}

Rotary::Rotary(int pinA,int pinB,int rotaryMin,int rotaryMax):_pinA(pinA),_pinB(pinB),_rotaryMin(rotaryMin),_rotaryMax(rotaryMax),_encoder(_pinA, _pinB, RotaryEncoder::LatchMode::TWO03)
{
  _instance = this;
}

void IRAM_ATTR Rotary::readEncoderISR()
{
  if (_instance != nullptr)
  {
    _instance->_encoder.tick();
  }
}

void Rotary::Setup()
{
  pinMode(_pinA, INPUT_PULLUP);
  pinMode(_pinB, INPUT_PULLUP);

  _encoder.setPosition(_rotaryCenter);

  // ===== 割り込み設定 =====
  // CHANGE : 立ち上がり/立ち下がりどちらも検出
  attachInterrupt(digitalPinToInterrupt(_pinA), readEncoderISR, CHANGE);
  attachInterrupt(digitalPinToInterrupt(_pinB), readEncoderISR, CHANGE);
}

void Rotary::Tick()
{
  _delta = _encoder.getPosition();
  _encoder.setPosition(_rotaryCenter);
}