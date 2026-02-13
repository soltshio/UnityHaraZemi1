#ifndef _MPU_ACCGYRO_H_
#define _MPU_ACCGYRO_H_

#include "MovingAverage.h"
#include <Wire.h>

#define MPU6050_ADDR 0x68
#define MPU6050_AX  0x3B
#define MPU6050_AY  0x3D
#define MPU6050_AZ  0x3F
#define MPU6050_TP  0x41    //  data not used
#define MPU6050_GX  0x43
#define MPU6050_GY  0x45
#define MPU6050_GZ  0x47

//加速度センサー
//ジャイロのXとZのみ使うので、その値を取得

class MPU_AccGyro
{
int _gyroX_ma=0;
int _gyroZ_ma=0;

const int _maN=35;

MovingAverage _maGyroX;
MovingAverage _maGyroZ;

short int _accX, _accY, _accZ;
short int _temp;
short int _gyroX, _gyroY, _gyroZ;

public:
MPU_AccGyro();

int gyroX() const{ return _gyroX_ma; };
int gyroZ() const{ return _gyroZ_ma; };

void setup();
void tick();
};

#endif