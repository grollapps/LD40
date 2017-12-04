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

    private float startTime = 0;
    private float lastElapsedTime = 0;

    private bool isFrozen = false;

    void Awake() {
        Reset();
    }

    // Use this for initialization
    void Start() {
        FreezeAll(); //no input until StartAll is called
    }

    public void Reset() {
        startTime = 0;
        lastElapsedTime = 0;
        numMovers = 0;
        FreezeAll();

        Debug.Log("Cleaning up bees");
        Bee[] bees = FindObjectsOfType<Bee>();
        foreach (Bee b in bees) {
            Destroy(b.gameObject);
        }

        for (int i = 0; i < moverInputActive.Length; i++) {
            movers[i] = null;
            moverInputActive[i] = false;
        }
    }

    /// <summary>
    /// Add the queen to the front of the mover list
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public int addFirstMover(Mover m) {
        Debug.Log("Add first mover");
        numMovers = 0;
        return addMover(m);
    }

    public int addMover(Mover m) {
        Debug.Log("Add mover " + numMovers);
        movers[numMovers++] = m;
        if (isFrozen) {
            m.Freeze();
        }
        return numMovers - 1;
    }

    public void setMoverInputActive(int index, bool isActive) {
        moverInputActive[index] = isActive;
    }

    public bool isMoverInputActive(int index) {
        return moverInputActive[index];
    }

    public void StartTimer() {
        startTime = Time.time;
    }

    public float StopTimer() {
        lastElapsedTime = Time.time - startTime;
        return lastElapsedTime;
    }

    public float getElapsedTime() {
        return lastElapsedTime;
    }

    /// <summary>
    /// returns score as {numBees, numTotHp, scoreFactor, totalScore}
    /// </summary>
    /// <returns></returns>
    public float[] calcScore() {
        float[] sc = new float[4];
        for (int i = 0; i < numMovers; i++) {
            if (movers[i] != null) {
                Bee bee = movers[i].gameObject.GetComponent<Bee>();
                if (bee != null) {
                    sc[0]++;
                    sc[1] += bee.getHp();
                }
            }
        }
        sc[2] = 600.0f / lastElapsedTime;

        float totalScore = sc[0] * 4.5f + sc[1] + sc[2];
        sc[3] = totalScore;

        return sc;
    }

    public void StartAll() {
        Debug.Log("Start all");
        isFrozen = false;
        for (int i = 0; i < numMovers; i++) {
            if (movers[i] != null) {
                movers[i].Unfreeze();
            }
        }
        StartTimer();
    }

    /// <summary>
    /// Stop all movers and all keyboard input other than a few keys
    /// </summary>
    public void FreezeAll() {
        Debug.Log("Freeze all");
        isFrozen = true;
        StopTimer();
        for (int i = 0; i < numMovers; i++) {
            if (movers[i] != null) {
                movers[i].Freeze();
            }
        }
    }

    public void DecreaseAllSpeed(float amt) {
        for (int i = 0; i < numMovers; i++) {
            if (movers[i] != null) {
                movers[i].DecreaseSpeed(amt);
            }
        }
    }

    /// <summary>
    /// Determines where the camera should be to be able to see all movers 
    /// </summary>
    /// <returns></returns>
    public Vector3 getCamTarget() {
        if (numMovers < 1) {
            return Vector3.zero;
        }

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
    void Update() {

        if (!isFrozen) {
            float leftRight = Input.GetAxis("Horizontal");
            fireLeftRight(leftRight);

            float downUp = Input.GetAxis("Vertical");
            fireDownUp(downUp);

            //Test num keys
            if (Input.GetButtonDown("1")) {
                if (movers[0] != null) {
                    Global.instance.hudManager.toggleTrigger(0);
                }
            }
            if (Input.GetButtonDown("2")) {
                if (movers[1] != null) {
                    Global.instance.hudManager.toggleTrigger(1);
                }
            }
            if (Input.GetButtonDown("3")) {
                if (movers[2] != null) {
                    Global.instance.hudManager.toggleTrigger(2);
                }
            }
            if (Input.GetButtonDown("Q")) {
                if (movers[3] != null) {
                    Global.instance.hudManager.toggleTrigger(3);
                }
            }
            if (Input.GetButtonDown("W")) {
                if (movers[4] != null) {
                    Global.instance.hudManager.toggleTrigger(4);
                }
            }
            if (Input.GetButtonDown("E")) {
                if (movers[5] != null) {
                    Global.instance.hudManager.toggleTrigger(5);
                }
            }
        }

        //these keys are available outside of normal level play
        if (Input.GetButtonDown("enter")) {
            handleAckPressed();
        }
        if (Input.GetButtonDown("space")) {
            handleAckPressed();
        }
        if (Input.GetButtonDown("G")) {
            handleResetPressed();
        }
        if (Input.GetButtonDown("cancel")) {
            Application.Quit();
        }
    }

    private void handleAckPressed() {
        if (isFrozen) {
            Global.instance.Advance();
        }
    }

    private void handleResetPressed() {
        Debug.Log("Reset");
        Global.instance.FailLevel();
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
