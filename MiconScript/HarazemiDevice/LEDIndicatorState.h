#ifndef LEDINDICATORSTATE_H_
#define LEDINDICATORSTATE_H_
#include "LEDIndicator.h"
#include "ControllerMessageTransmitter.h"

class LEDStateMachine;//前方宣言


//LEDの状態クラス(ベース)
class LEDStateTypeBase
{
public:
LEDStateTypeBase() {}
virtual void OnEnter(LEDStateMachine &stateMachine)=0;//ステートの開始処理
virtual void OnUpdate(LEDStateMachine &stateMachine)=0;//ステートの更新処理
virtual void OnExit(LEDStateMachine &stateMachine)=0;//ステートの終了処理
};



//LEDの通常状態クラス
class LEDStateTypeNormal : public LEDStateTypeBase
{
LEDIndicator *_led;
ControllerMessageTransmitter* _transmitter;
unsigned long _previous=0;
const int _cycle=4000;//この周期でゆっくりと明るくなったり、暗くなったりする
public:
LEDStateTypeNormal(LEDIndicator &led,ControllerMessageTransmitter &transmitter);
LEDStateTypeNormal(int cycle,LEDIndicator &led,ControllerMessageTransmitter &transmitter);
void OnEnter(LEDStateMachine &stateMachine) override;
void OnUpdate(LEDStateMachine &stateMachine) override;
void OnExit(LEDStateMachine &stateMachine) override;
};

//LEDの電気発生状態クラス
class LEDStateTypeSparking : public LEDStateTypeBase
{
LEDIndicator *_led;
ControllerMessageTransmitter* _transmitter;
unsigned long _previous=0;
//周期
int _cycle;
const int _cycleDif=200;//電気拡散時の周期
const int _cycleCon=50;//電気収束時の周期
public:
LEDStateTypeSparking(LEDIndicator &led,ControllerMessageTransmitter &transmitter);
LEDStateTypeSparking(int cycleDif,int cycleCon,LEDIndicator &led,ControllerMessageTransmitter &transmitter);
void OnEnter(LEDStateMachine &stateMachine) override;
void OnUpdate(LEDStateMachine &stateMachine) override;
void OnExit(LEDStateMachine &stateMachine) override;
};





#endif