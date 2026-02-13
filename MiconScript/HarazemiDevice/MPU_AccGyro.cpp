#include "MPU_AccGyro.h"

MPU_AccGyro::MPU_AccGyro()
:_maGyroX(_maN),
 _maGyroZ(_maN)
{

}

void MPU_AccGyro::setup()
{
  Wire.begin();

  Wire.beginTransmission(MPU6050_ADDR);
  Wire.write(0x6B);
  Wire.write(0);
  Wire.endTransmission();
}

void MPU_AccGyro::tick()
{
  //  send start address
  Wire.beginTransmission(MPU6050_ADDR);
  Wire.write(MPU6050_AX);
  Wire.endTransmission();  

  //  request 14bytes (int16 x 7)
  Wire.requestFrom(MPU6050_ADDR, 14);

  //  get 14bytes
  _accX = Wire.read() << 8;  _accX |= Wire.read();
  _accY = Wire.read() << 8;  _accY |= Wire.read();
  _accZ = Wire.read() << 8;  _accZ |= Wire.read();
  _temp = Wire.read() << 8;  _temp |= Wire.read();  //  (Temp-12421)/340.0 [degC]
  _gyroX = Wire.read() << 8; _gyroX |= Wire.read();
  _gyroY = Wire.read() << 8; _gyroY |= Wire.read();
  _gyroZ = Wire.read() << 8; _gyroZ |= Wire.read();

  _gyroX_ma = _maGyroX.Add(_gyroX);
  _gyroZ_ma = _maGyroZ.Add(_gyroZ);
}