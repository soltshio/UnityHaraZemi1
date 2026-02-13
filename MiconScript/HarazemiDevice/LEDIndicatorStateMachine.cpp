#include <Arduino.h>
#include "LEDIndicatorStateMachine.h"


//LEDの状態のステートマシン
LEDStateMachine::LEDStateMachine(LEDIndicator &led,ControllerMessageTransmitter &transmitter): _normal(led,transmitter),_sparking(led,transmitter)
{
  _states[0]= &_normal;
  _states[1]= &_sparking;

  ChangeState(E_LEDState::Normal);
}

void LEDStateMachine::Tick()
{
  if(_currentState != nullptr) _currentState->OnUpdate(*this);
}

void LEDStateMachine::ChangeState(E_LEDState newState)
{
  if(_currentState != nullptr) _currentState->OnExit(*this);//前の状態の最後の処理

  _currentState=_states[(int)newState];//状態の変化

  if(_currentState != nullptr) _currentState->OnEnter(*this);//次の状態の最初の処理
}
