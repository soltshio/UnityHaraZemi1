#include "ControllerMessageTransmitter.h"

void ControllerMessageTransmitter::SetMessage(char cmd,int rate)
{
  //コマンド系
  if(_cmd!=cmd)
  {
    _cmdOutputFlag=true;
    _cmd=cmd;
  }

  //収束率系
  if(_rate!=rate)
  {
    _rateOutputFlag=true;
    _rate=rate;
  }
}

void ControllerMessageTransmitter::ResetOutputFlag()
{
  _cmdOutputFlag=false;
  _rateOutputFlag=false;
}