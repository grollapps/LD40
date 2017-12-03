using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles keyboard input and directs it to movable game objects.  Assign to a level manager.
/// </summary>
public class InputHandler : MonoBehaviour {

    public string[] moverKeys = new string[] { "1", "2", "3", "Q", "W", "E" };

    private Mover[] movers = new Mover[6];  //one per input toggle key
    private bool[] moverInputActive = new bool[6];
    private int numMovers = 0; //number of actual movers in play

    //Amount side tick move multiplier
    public float sideAmt = 0.05f;

    //Amount fwd tick move multiplier
    public float fwdAmt = 0.01f;

    void Awake() {
        for (int i = 0; i < moverInputActive.Length; i++) {
            moverInputActive[i] = false;
        }
    }

	// Use this for initialization
	void Start () {
		
	}

    public int addFirstMover(Mover m) {
        Debug.Log("Add first mover");
        movers[0] = m;
        numMovers = 1;
        return 0;
    }

    public int addMover(Mover m) {
        Debug.Log("Add mover " + numMovers);
        movers[numMovers++] = m;
        return numMovers - 1;
    }

    public void setMoverInputActive(int index, bool isActive) {
        moverInputActive[index] = isActive;
    }

    public bool isMoverInputActive(int index) {
        return moverInputActive[index];
    }

    /// <summary>
    /// Stop all movers
    /// </summary>
    public void FreezeAll() {
        for (int i = 0; i < numMovers; i++) {
            if (movers[i] != null) {
                movers[i].Freeze();
            }
        }
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
        return Vector3.Lerp(movers[0].transform.position, movers[numMovers - 1].transform.position, 0.6f);
    }

    /// <summary>
    /// Get the current player position in world coordinates
    /// </summary>
    /// <returns></returns>
    public Vector3 getQueenCurPos() {
        if (numMovers < 1) {
            return Vector3.zero;
        }
        return movers[0].transform.position;
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
        if (Input.GetButtonDown("Q")) {
            Global.instance.hudManager.toggleTrigger(3);
        }
        if (Input.GetButtonDown("W")) {
            Global.instance.hudManager.toggleTrigger(4);
        }
        if (Input.GetButtonDown("E")) {
            Global.instance.hudManager.toggleTrigger(5);
        }
	}

    private void fireLeftRight(float val) {
        for (int i = 0; i < movers.Length; i++) {
            Mover m = movers[i];
            if (m != null && isMoverInputActive(i)) {
                m.addLeftRight(val * sideAmt);
            }
        }

    }

    private void fireDownUp(float val) {
        for (int i = 0; i < movers.Length; i++) {
            Mover m = movers[i];
            if (m != null && isMoverInputActive(i)) {
                m.addDownUp(val * fwdAmt);
            }
        }

    }
}
