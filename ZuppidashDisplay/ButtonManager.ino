
int lastPress = 0;

void manageButtons() {
	byte buttons = module.getButtons();

	if ((millis() - lastPress) > 200) {
		if (buttons != 0) {
			lastPress = millis();
			switch (buttons) {
			case 0x01: // Button 1
				break;
			case 0x02: // Button 2
				break;
			case 0x04: // Button 3
				break;
			case 0x08: // Button 4
				break;
			case 0x10: // Button 5
				break;
			case 0x20: // Button 6
				break;
			case 0x40: // Button 7
				break;
			case 0b10000000: // Button 8
				break;
			default:
				break;
			}
		}	
	}
}
