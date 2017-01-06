void checkMessages() {
	if (Serial.available() > 0) {
		parseMessage(Serial.readStringUntil('\n'));
	}
}

void parseMessage(String message) {
	
	if (message.equals(clearCode)) {
		clear();
		return;
	}
	
	int splitPos = message.indexOf('|');

	showShiftLights(message.substring(splitPos + 1, splitPos + 2).toInt());
	showMessage(message.substring(0, splitPos), message.substring(splitPos + 3).toInt());
	//Serial.print(message+"\n");
	//Serial.print(message.substring(0, splitPos) + "\n");
	//Serial.print(message.substring(splitPos + 1, splitPos + 2) + "\n");
	//Serial.print(message.substring(splitPos + 3) + "\n");

	//if (splitPos == -1) {
	//	showMessage(message);
	//	return;
	//}
	//else {
	//	showShiftLights(message.substring(splitPos + 1, splitPos+2).toInt());
	//	showMessage(message.substring(0, splitPos), message.substring(splitPos+3).toInt());
	//}
	
}



