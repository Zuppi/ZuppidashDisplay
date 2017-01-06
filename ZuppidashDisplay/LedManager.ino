bool pitlimiterEnabled = false;
int pitLimiterTimer = 0;
int pitLimiterPhase = 0;
int shiftLightAmount = 10;

void manageLeds() {
	if (pitlimiterEnabled) {
		pitLimiter();
	}
}

//void enablePitlimiter() {
//	pitlimiterEnabled = true;
//	pitLimiterTimer = millis();
//	pitLimiterPhase = 1;
//}
//
//void disablePitlimiter() {
//	pitlimiterEnabled = false;
//	pitLimiterTimer = 0;
//	pitLimiterPhase = 0;
//	module.setLEDs(0x0000);
//}

void showShiftLights(int ledAmount) {
	if (pitlimiterEnabled) {
		if (ledAmount == 99) {
			togglePitlimiter();
		}
		return;
	}
	else if (ledAmount == shiftLightAmount) {
		return;
	}

	switch (ledAmount) {
	case 0:
		module.setLEDs(0x0000);
		return;
	case 1:
		module.setLEDs(0x0001);
		return;
	case 2:
		module.setLEDs(0x0003);
		return;
	case 3:
		module.setLEDs(0x0007);
		return;
	case 4:
		module.setLEDs(0x000F);
		return;
	case 5:
		module.setLEDs(0x001F);
		return;
	case 6:
		module.setLEDs(0x003F);
		return;
	case 7:
		module.setLEDs(0x007F);
		return;
	case 8:
		module.setLEDs(0x00FF);
		return;
	case 9:
		module.setLEDs(0xFF00);
		return;
	case 99:
		togglePitlimiter();
		return;
	default:
		module.setLEDs(0x0000);
		return;
	}
}

void togglePitlimiter() {
	if (pitlimiterEnabled) {
		pitlimiterEnabled = false;
		pitLimiterTimer = 0;
		pitLimiterPhase = 0;
		module.setLEDs(0x0000);
	}
	else{
		pitlimiterEnabled = true;
		pitLimiterTimer = millis();
		pitLimiterPhase = 1;
	}
}

void pitLimiter() {
	if ((millis() - pitLimiterTimer) > 500) {
		if (pitLimiterPhase == 1) {
			clearLeds();
			module.setLEDs(0xAA55);
			pitLimiterPhase = 2;
		}
		else if (pitLimiterPhase == 2) {
			clearLeds();
			module.setLEDs(0x55AA);
			pitLimiterPhase = 1;
		}
		pitLimiterTimer = millis();
	}
}

void clearLeds() {
	module.setLEDs(0x0000);
}



