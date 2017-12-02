using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles keyboard input and directs it to movable game objects.  Assign to a level manager.
/// </summary>
public class InputHandler : MonoBehaviour {

    private Mover[] movers = new Mover[6];  //one per input toggle key
    private int numMovers = 0; //number of actual movers in play

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
        numMovers = 1;
    }

    public void addMover(Mover m) {
        Debug.Log("Add mover " + numMovers);
        movers[numMovers++] = m;
    }

    /// <summary>
    /// Determines where the camera should be to be able to see all movers 
    /// </summary>
    /// <returns></returns>
    public Vector3 getCamTarget() {
        if (numMovers == 1) {
            return movers[0].transform.position;
        }
        //Somewhere between first and last
        return Vector3.Lerp(movers[0].transform.position, movers[numMovers - 1].transform.position, 0.9f);
    }

    public int getNumMovers() {
        return numMovers;
    }

	
	// Update is called once per frame
	void Update () {

        float leftRight = Input.GetAxis("Horizontal");
        fireLeftRight(leftRight);

        float downUp = Input.GetAxis("Vertical");
        fireDownUp(downUp);

        //Test num keys
        if (Input.GetButtonDown("1")) {
            Global.instance.hudManager.toggleTrigger(0);
        }
        if (Input.GetButtonDown("2")) {
            Global.instance.hudManager.toggleTrigger(1);
        }
        if (Input.GetButtonDown("3")) {
            Global.instance.hudManager.toggleTrigger(2);
        }
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
