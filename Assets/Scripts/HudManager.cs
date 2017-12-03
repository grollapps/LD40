using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages what is displayed on the HUD
/// </summary>
public class HudManager : MonoBehaviour {

    //Reference to the Text that will display remaining distance
    public Text distTextValue;

    //One trigger per mover button that can be pressed
    private bool[] triggerStateOn = new bool[6];

    public Text[] triggerOnText;
    public Text[] triggerOffText;

    public float metersToGoal = 0f;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < triggerStateOn.Length; i++) {
            triggerStateOn[i] = false;
        }

        for (int i = 0; i < triggerStateOn.Length; i++) {
            toggleTrigger(i, false);
        }

        //triggerStateOn[0] = true; //first always starts on	
        toggleTrigger(0);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void setRemainingDist(float value) {
        string distStr = value.ToString("F1");
        distTextValue.text = distStr;
    }

    /// <summary>
    /// Swap the referenced trigger to either on or off, opposite of the current state
    /// </summary>
    /// <param name="index"></param>
    public void toggleTrigger(int index) {
        bool newState = !triggerStateOn[index];
        toggleTrigger(index, newState);
    }

     public void toggleTrigger(int index, bool newState) {
        Global.instance.inputHandler.setMoverInputActive(index, newState);
        triggerStateOn[index] = newState;
        if (triggerOnText.Length > index) {
            setTextOnOff(triggerOnText[index], newState);
        }
        if (triggerOffText.Length > index) {
            setTextOnOff(triggerOffText[index], !newState);
        }
    }

    private void setTextOnOff(Text t, bool isVisible) {
        if (t != null) {
            float a = isVisible ? 1.0f : 0.0f;
            CanvasGroup cg = t.GetComponent<CanvasGroup>();
            if (cg != null) {
                cg.alpha = a;
            } else {
                t.GetComponent<CanvasRenderer>().SetAlpha(a);
            }
        }

    }
}
