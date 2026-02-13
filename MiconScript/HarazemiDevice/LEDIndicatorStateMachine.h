#ifndef LEDINDICATORSTATEMACHINE_H_
#define LEDINDICATORSTATEMACHINE_H_
#include "LEDIndicatorState.h"

//LEDの状態
#define LEDSTATE_LENGTH 2

enum class E_LEDState
{
  Normal,//通常(電源が点いている時)
  Sparking//電気を出している時
};

//LEDの状態のステートマシン
class LEDStateMachine
{
LEDStateTypeBase* _currentState;
LEDStateTypeNormal _normal;
LEDStateTypeSparking _sparking;
LEDStateTypeBase* _states[LEDSTATE_LENGTH];
public:
LEDStateMachine(LEDIndicator &led,ControllerMessageTransmitter &transmitter);
void Tick();
void ChangeState(E_LEDState newState);
};

#endif
