/* ###################################################################
**     This component module is generated by Processor Expert. Do not modify it.
**     Filename    : gpio_pins.h
**     Project     : frdmk64f120m-pin
**     Processor   : MK64FN1M0VMD12
**     Component   : fsl_gpio
**     Version     : Component 01.106, Driver 01.00, CPU db: 3.00.000
**     Compiler    : GNU C Compiler
**     Date/Time   : 2014-06-09, 10:08, # CodeGen: 1
**     Contents    :
**         GPIO_DRV_Init            - void GPIO_DRV_Init(const gpio_input_pin_user_config_t * inputPins,const...
**         GPIO_DRV_InputPinInit    - void GPIO_DRV_InputPinInit(const gpio_input_pin_user_config_t * inputPin);
**         GPIO_DRV_OutputPinInit   - void GPIO_DRV_OutputPinInit(const gpio_output_pin_user_config_t * outputPin);
**         GPIO_DRV_GetPinDir       - gpio_pin_direction_t GPIO_DRV_GetPinDir(uint32_t pinName);
**         GPIO_DRV_SetPinDir       - void GPIO_DRV_SetPinDir(uint32_t pinName,gpio_pin_direction_t direction);
**         GPIO_DRV_WritePinOutput  - void GPIO_DRV_WritePinOutput(uint32_t pinName,uint32_t output);
**         GPIO_DRV_SetPinOutput    - void GPIO_DRV_SetPinOutput(uint32_t pinName);
**         GPIO_DRV_ClearPinOutput  - void GPIO_DRV_ClearPinOutput(uint32_t pinName);
**         GPIO_DRV_TogglePinOutput - void GPIO_DRV_TogglePinOutput(uint32_t pinName);
**         GPIO_DRV_ReadPinInput    - uint32_t GPIO_DRV_ReadPinInput(uint32_t pinName);
**         GPIO_DRV_ClearPinIntFlag - void GPIO_DRV_ClearPinIntFlag(uint32_t pinName);
**
**     Copyright : 1997 - 2014 Freescale Semiconductor, Inc. 
**     All Rights Reserved.
**     
**     Redistribution and use in source and binary forms, with or without modification,
**     are permitted provided that the following conditions are met:
**     
**     o Redistributions of source code must retain the above copyright notice, this list
**       of conditions and the following disclaimer.
**     
**     o Redistributions in binary form must reproduce the above copyright notice, this
**       list of conditions and the following disclaimer in the documentation and/or
**       other materials provided with the distribution.
**     
**     o Neither the name of Freescale Semiconductor, Inc. nor the names of its
**       contributors may be used to endorse or promote products derived from this
**       software without specific prior written permission.
**     
**     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
**     ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
**     WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
**     DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
**     ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
**     (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
**     LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
**     ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
**     (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
**     SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**     
**     http: www.freescale.com
**     mail: support@freescale.com
** ###################################################################*/
        

#ifndef __gpio_pins_H
#define __gpio_pins_H

/* MODULE gpio_pins. */

/* Include inherited beans */
#include "fsl_gpio.h"

/* SPI Master Data Available Pin */
#define gSpiMasterDap_Port_d  gpioPort_C_c
#define gSpiMasterDap_Pin_d   2
/* SPI Slave Data Available Pin */
#define gSpiSlaveDap_Port_d   gpioPort_C_c
#define gSpiSlaveDap_Pin_d    2
/* I2C Master Data Available Pin */
#define gI2cMasterDap_Port_d  gpioPort_C_c
#define gI2cMasterDap_Pin_d   2
/* I2C Slave Data Available Pin */
#define gI2cSlaveDap_Port_d   gpioPort_C_c
#define gI2cSlaveDap_Pin_d    2

/* switch pins to be initialized by the low power component if the keyboard isn't enabled*/
/* 1<<n for sitchPins[n] */   
#define gLowPower_switchPinsToInitBitmap_d  ( 1<<1 | 1<<0 )

/*! @brief Configuration structure 0 for input pins */
extern gpioInputPinConfig_t switchPins[];

/*! @brief Configuration structure 0 for output pins */
extern gpioOutputPinConfig_t ledPins[];

/*! @brief Configuration structure for XCVR pins */
extern const gpioOutputPinConfig_t mXcvrSpiCsCfg;
extern const gpioOutputPinConfig_t mXcvrResetPinCfg;
extern const gpioInputPinConfig_t mXcvrIrqPinCfg;
  
#endif
