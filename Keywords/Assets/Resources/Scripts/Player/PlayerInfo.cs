using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    public int playerNum;
    public int keys;//how many keys does the player have?
    public GameObject UI;//this player's UI;
    private TMPro.TextMeshProUGUI keyUI;//UI which displays how many keys the player has

    private void Start() {
        keyUI = UI.transform.Find("Keys").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

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
        } else if (controlName == "Y") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button3,
                KeyCode.Joystick2Button3,
                KeyCode.Joystick3Button3,
                KeyCode.Joystick4Button3
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
        } else if (controlName == "Start") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button7,
                KeyCode.Joystick2Button7,
                KeyCode.Joystick3Button7,
                KeyCode.Joystick4Button7
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
        } else if (controlName == "Y") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button19,
                KeyCode.Joystick2Button19,
                KeyCode.Joystick3Button19,
                KeyCode.Joystick4Button19
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
        } else if (controlName == "Start") {
            controlSet = new KeyCode[4] {
                KeyCode.Joystick1Button9,
                KeyCode.Joystick2Button9,
                KeyCode.Joystick3Button9,
                KeyCode.Joystick4Button9
            };
        } else {
            print("control name not recognized");
        }
        return controlSet[playerNum - 1];
    }

    public float GetAxisWindows(string axisName) {
        if (playerNum < 1 || playerNum > 4) {
            print("playerNum not a valid number fix it");
            return 0f;
        }
        if (axisName == "Horizontal") {
            return Input.GetAxis("P" + playerNum + "_Horizontal");
        } else if (axisName == "Vertical") {
            return Input.GetAxis("P" + playerNum + "_Vertical");
        } else if (axisName == "Horizontal_R") {
            return Input.GetAxis("P" + playerNum + "_Horizontal_R");
        } else if (axisName == "Vertical_R") {
            return Input.GetAxis("P" + playerNum + "_Vertical_R");
        } else if (axisName == "RTrigger") {
            return Input.GetAxis("P" + playerNum + "_RTrigger_Windows");
        } else if (axisName == "LTrigger") {
            return Input.GetAxis("P" + playerNum + "_LTrigger_Windows");
        } else {
            print("axis name not recognized");
            return 0f;
        }
    }

    public float GetAxisOSX(string axisName) {
        if (playerNum < 1 || playerNum > 4) {
            print("playerNum not a valid number fix it");
            return 0f;
        }
        if (axisName == "Horizontal") {
            return Input.GetAxis("P" + playerNum + "_Horizontal");
        } else if (axisName == "Vertical") {
            return Input.GetAxis("P" + playerNum + "_Vertical");
        } else if (axisName == "Horizontal_R") {
            return Input.GetAxis("P" + playerNum + "_Horizontal_R_OSX");
        } else if (axisName == "Vertical_R") {
            return Input.GetAxis("P" + playerNum + "_Vertical_R_OSX");
        } else if (axisName == "RTrigger") {
            return Input.GetAxis("P" + playerNum + "_RTrigger");
        } else if (axisName == "LTrigger") {
            return Input.GetAxis("P" + playerNum + "_LTrigger_OSX");
        } else {
            print("axis name not recognized");
            return 0f;
        }
    }

    public float GetAxisLinux(string axisName) {
        if (playerNum < 1 || playerNum > 4) {
            print("playerNum not a valid number fix it");
            return 0f;
        }
        if (axisName == "Horizontal") {
            return Input.GetAxis("P" + playerNum + "_Horizontal");
        } else if (axisName == "Vertical") {
            return Input.GetAxis("P" + playerNum + "_Vertical");
        } else if (axisName == "Horizontal_R") {
            return Input.GetAxis("P" + playerNum + "_Horizontal_R");
        } else if (axisName == "Vertical_R") {
            return Input.GetAxis("P" + playerNum + "_Vertical_R");
        } else if (axisName == "RTrigger") {
            return Input.GetAxis("P" + playerNum + "_RTrigger");
        } else if (axisName == "LTrigger") {
            return Input.GetAxis("P" + playerNum + "_LTrigger_Linux");
        } else {
            print("axis name not recognized");
            return 0f;
        }
    }
}
