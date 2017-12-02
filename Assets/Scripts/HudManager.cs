using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages what is displayed on the HUD
/// </summary>
public class HudManager : MonoBehaviour {

    //One trigger per mover button that can be pressed
    private bool[] triggerStateOn = new bool[6];

    public Text[] triggerOnText;
    public Text[] triggerOffText;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < triggerStateOn.Length; i++) {
            triggerStateOn[i] = false;
        }

        triggerStateOn[0] = true; //first always starts on	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void toggleTrigger(int index) {
        bool newState = !triggerStateOn[index];
        triggerStateOn[index] = newState;
        if (triggerOnText.Length > index && triggerOnText[index] != null) {
            triggerOnText[index].GetComponent<CanvasRenderer>().SetAlpha(newState ? 1.0f : 0.0f);
        }
        if (triggerOffText.Length > index && triggerOffText[index] != null) {
            triggerOffText[index].GetComponent<CanvasRenderer>().SetAlpha(newState ? 0.0f : 1.0f);
        }
    }
}
