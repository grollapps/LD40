using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles keyboard input and directs it to movable game objects.  Assign to a level manager.
/// </summary>
public class InputHandler : MonoBehaviour {

    private Mover[] movers = new Mover[6];  //one per input toggle key

    //Amount side tick move multiplier
    public float sideAmt = 0.05f;

    //Amount fwd tick move multiplier
    public float fwdAmt = 0.01f;


	// Use this for initialization
	void Start () {
		
	}

    public void addFirstMover(Mover m) {
        Debug.Log("Add first mover");
        movers[0] = m;
    }

    public void addMover(Mover m) {
        Debug.Log("Add mover");
        for (int i = 0; i < movers.Length; i++) {
            if (movers[i] == null) {
                Debug.Log("new mover i=" + i);
                movers[i] = m;
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        float leftRight = Input.GetAxis("Horizontal");
        fireLeftRight(leftRight);

        float downUp = Input.GetAxis("Vertical");
        fireDownUp(downUp);

        //TODO Test num keys
	}

    private void fireLeftRight(float val) {
        for (int i = 0; i < movers.Length; i++) {
            Mover m = movers[i];
            if (m != null) {
                m.addLeftRight(val * sideAmt);
            }
        }

    }

    private void fireDownUp(float val) {
        for (int i = 0; i < movers.Length; i++) {
            Mover m = movers[i];
            if (m != null) {
                m.addDownUp(val * fwdAmt);
            }
        }

    }
}
