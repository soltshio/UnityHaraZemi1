#include "LEDIndicator.h"
#include <Arduino.h>
#include "Mathf.h"

LEDIndicator::LEDIndicator(int LEDPin):_LEDPin(LEDPin),_isLit(false)
{

}

void LEDIndicator::Setup()
{
  pinMode(_LEDPin,OUTPUT);
}

void LEDIndicator::AnalogWrite(float brightness)
{
  brightness = Mathf::Clamp(brightness,(float)0,(float)1);

  int b = brightness*MAX_BRIGHTNESS;

  analogWrite(_LEDPin,b);
  _isLit = (b==0) ? false : true;
}

void LEDIndicator::DigitalWrite(int state)
{
  if(state != HIGH && state != LOW) return;

  digitalWrite(_LEDPin,state);
  _isLit = (state==LOW) ? false : true;
}