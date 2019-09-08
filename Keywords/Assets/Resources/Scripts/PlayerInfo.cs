using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    public int playerNum;
    public int keys;//how many keys does the player have?
    public Text keyUI;//UI which displays how many keys the player has

    public void IncKeys() {
        keys++;
        keyUI.text = keys.ToString();
    }

    public KeyCode GetKeyCode(string controlName) {
        if (Game.IsOnOSX) {
            return GetKeyCodeOSX(controlName);
        }
        KeyCode[] controlSet = new KeyCode[4];
        if (controlName == "A") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button0,
                KeyCode.Joystick2Button0,
                KeyCode.Joystick3Button0,
                KeyCode.Joystick4Button0
            };
        } else if (controlName == "B") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button1,
                KeyCode.Joystick2Button1,
                KeyCode.Joystick3Button1,
                KeyCode.Joystick4Button1
            };
        } else if (controlName == "LeftBumper") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button4,
                KeyCode.Joystick2Button4,
                KeyCode.Joystick3Button4,
                KeyCode.Joystick4Button4
            };
        } else if (controlName == "RightBumper") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button5,
                KeyCode.Joystick2Button5,
                KeyCode.Joystick3Button5,
                KeyCode.Joystick4Button5
            };
        } else {
            print("control name not recognized");
        }
        return controlSet[playerNum - 1];
    }

    public KeyCode GetKeyCodeOSX(string controlName) {
        KeyCode[] controlSet = new KeyCode[4];
        if (controlName == "A") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button16,
                KeyCode.Joystick2Button16,
                KeyCode.Joystick3Button16,
                KeyCode.Joystick4Button16
            };
        } else if (controlName == "B") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button17,
                KeyCode.Joystick2Button17,
                KeyCode.Joystick3Button17,
                KeyCode.Joystick4Button17
            };
        } else if (controlName == "LeftBumper") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button13,
                KeyCode.Joystick2Button13,
                KeyCode.Joystick3Button13,
                KeyCode.Joystick4Button13
            };
        } else if (controlName == "RightBumper") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button14,
                KeyCode.Joystick2Button14,
                KeyCode.Joystick3Button14,
                KeyCode.Joystick4Button14
            };
        } else {
            print("control name not recognized");
        }
        return controlSet[playerNum - 1];
    }

    public float GetAxis(string axisName) {
        if (axisName == "Horizontal") {
            if (playerNum > 0 && playerNum < 5) {
                return Input.GetAxis("P" + playerNum + "_Horizontal");
            } else {
                print("playerNum not a valid number fix it");
                return 0f;
            }
        } else if (axisName == "Vertical") {
            if (playerNum > 0 && playerNum < 5) {
                return Input.GetAxis("P" + playerNum + "_Vertical");
            } else {
                print("playerNum not a valid number fix it");
                return 0f;
            }
        } else {
            print("axis name not recognized");
            return 0f;
        }
    }
}
