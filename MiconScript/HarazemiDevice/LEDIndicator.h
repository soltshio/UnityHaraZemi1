#ifndef LEDINDICATOR_H_
#define LEDINDICATOR_H_

#define MAX_BRIGHTNESS 255

class LEDIndicator
{
 const int _LEDPin;
 bool _isLit;
public:
 LEDIndicator(int LEDPin);
 bool IsLit() const{return _isLit;}
 void Setup();
 void AnalogWrite(float brightness);//brightnessは0~1で指定
 void DigitalWrite(int state);//stateはHIGHかLOWで指定
};

#endif