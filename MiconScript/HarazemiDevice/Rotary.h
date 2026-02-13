#ifndef ROTARY_H_
#define ROTARY_H_
#include <RotaryEncoder.h>

//使用する際の注意点
//一つまでしかロータリーエンコーダーを使用できません。

class Rotary
{
static Rotary* _instance;//割り込み処理を使う用

int _delta=0;

const int _pinA;
const int _pinB;
const int _rotaryMin = -255;// 最小値
const int _rotaryCenter = 0;
const int _rotaryMax = 255;// 最大値
RotaryEncoder _encoder;

static void IRAM_ATTR readEncoderISR();
public:
Rotary(int pinA,int pinB);
Rotary(int pinA,int pinB,int rotaryMin,int rotaryMax);//最小値と最大値を変更(デフォルトはそれぞれ-255、255)
int Delta() const {return _delta;}
void Setup();
void Tick();
};

#endif