#include <Arduino.h>
#include "LEDIndicatorState.h"
#include "LEDIndicatorStateMachine.h"
#include "Mathf.h"

//LEDの通常状態クラス
LEDStateTypeNormal::LEDStateTypeNormal(LEDIndicator &led,ControllerMessageTransmitter &transmitter) : _led(&led),_transmitter(&transmitter)
{

}

LEDStateTypeNormal::LEDStateTypeNormal(int cycle,LEDIndicator &led,ControllerMessageTransmitter &transmitter) : _cycle(cycle),_led(&led),_transmitter(&transmitter)
{

}

void LEDStateTypeNormal::OnEnter(LEDStateMachine &stateMachine)
{
  _previous=millis();
}

void LEDStateTypeNormal::OnUpdate(LEDStateMachine &stateMachine)
{
  //ステートの遷移チェック
  if(_transmitter->Cmd()=='s')
  {
    stateMachine.ChangeState(E_LEDState::Sparking);
    return;
  }

  //LEDを光らせる
  unsigned long now = millis();
  int elapsed= now - _previous;

  float brightness=Mathf::TriangleWave01((float)elapsed,(float)0,(float)_cycle);

  _led->AnalogWrite(brightness);

  if(elapsed>=_cycle)
  {
    _previous=now;
  }
}

void LEDStateTypeNormal::OnExit(LEDStateMachine &stateMachine)
{
  _led->DigitalWrite(LOW);
}


//LEDの電気発生状態クラス
LEDStateTypeSparking::LEDStateTypeSparking(LEDIndicator &led,ControllerMessageTransmitter &transmitter) : _led(&led),_transmitter(&transmitter)
{
  
}

LEDStateTypeSparking::LEDStateTypeSparking(int cycleDif,int cycleCon,LEDIndicator &led,ControllerMessageTransmitter &transmitter) : _cycleDif(cycleDif),_cycleCon(cycleCon),_led(&led),_transmitter(&transmitter)
{
  
}

void LEDStateTypeSparking::OnEnter(LEDStateMachine &stateMachine)
{
  _previous=millis();
  _led->AnalogWrite(0);
  _cycle=(int)Mathf::Lerp((float)_cycleDif, (float)_cycleCon, _transmitter->Rate());
}

void LEDStateTypeSparking::OnUpdate(LEDStateMachine &stateMachine)
{
  //ステートの遷移チェック
  if(_transmitter->Cmd()=='n')
  {
    stateMachine.ChangeState(E_LEDState::Normal);
    return;
  }

  //LEDを光らせる
  unsigned long now = millis();
  int elapsed= now - _previous;

  //周期の再計算
  if(_transmitter->RateOutputFlag())
  {
    _cycle=(int)Mathf::Lerp((float)_cycleDif, (float)_cycleCon, _transmitter->Rate());
  }

  if(elapsed>=_cycle)
  {
    float brightness = _led->IsLit() ? 0 : 1;
    _led->AnalogWrite(brightness);
    _previous=now;
  }
}

void LEDStateTypeSparking::OnExit(LEDStateMachine &stateMachine)
{
  _led->DigitalWrite(LOW);
}
