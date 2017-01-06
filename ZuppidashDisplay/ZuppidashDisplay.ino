/*
 Name:		ZuppidashDisplay.ino
 Created:	4/20/2016 10:11:25 PM
 Author:	Zuppi
*/

#include <TM1638.h>

TM1638 module(8, 9, 10, true, 2);

//String incString = "";
String clearCode = "CLEAR|0|0";

//int incByte = 0;
// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(9600);
}

// the loop function runs over and over again until power down or reset
void loop() {
	manageButtons();
	manageLeds();
	manageDisplay();
	checkMessages();

	//if (Serial.available() > 0) {
	//	//incByte = Serial.parseInt();
	//	incString = Serial.readStringUntil('\n');
	//	//dataString = Serial.readStringUntil('\n');
	//	//incByte = Serial.read();
	//	//module.setDisplayToDecNumber(incByte, 0, false);
	//	/*if (incByte == 'T') {
	//	module.setLED(TM1638_COLOR_RED, 1);

	//	}*/
	//	if (incString.equals(clearCode)) {
	//		clear();
	//	}
	//	else {
	//		module.setDisplayToString(incString);
	//	}

	//}

	
	//Serial.write("test");
	/*module.setDisplayToString("TC 5");
	module.setLEDs(0xE007);*/
	//finish();
}


void clear() {
	clearDisplay();
	clearLeds();
}

