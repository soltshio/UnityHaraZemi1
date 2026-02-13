#ifndef CONTROLLER_MESSAGE_TRANSMITTER_H_
#define CONTROLLER_MESSAGE_TRANSMITTER_H_

class ControllerMessageTransmitter
{
//コマンドと収束率の意味の詳細はESPNOW_SendData.hのesp_now_c2d_dataを参照

//コマンド系
bool _cmdOutputFlag=false;
char _cmd='n';
//収束率系
bool _rateOutputFlag=false;
int _rate=0;

const float _rateConvertToFloat =9;

public:
ControllerMessageTransmitter() {}

//コマンド系
bool CmdOutputFlag() const {return _cmdOutputFlag;}
char Cmd() const {return _cmd;}
//収束率系
bool RateOutputFlag() const {return _rateOutputFlag;}
float Rate() const {return (float)_rate/_rateConvertToFloat;}//0~1に変換して返す(0~9で取得できるのでそれを0~1に変換し直す)

void SetMessage(char cmd,int rate);
void ResetOutputFlag();
};

#endif