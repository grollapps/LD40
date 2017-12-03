using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Global objects for the Game or Scene
/// </summary>
public class Global : MonoBehaviour {

    public static Global instance;

    public LevelManager curLevel;
    public HudManager hudManager;
    public InputHandler inputHandler;
    public Camera mainCamera;
    public Vector3 camOffset = Vector3.zero; //default camera offset from target
    public float moverOffsetFactor = 0.5f; //multiplier for camera distance per mover added

    public int levelNum = 1;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        inputHandler = GameObject.Find("GameManager").GetComponent<InputHandler>();
        if (inputHandler == null) {
            Debug.LogError("Could not find InputHandler");
        }

        hudManager = GameObject.Find("HUD").GetComponent<HudManager>();
        if (hudManager == null) {
            Debug.LogError("Could not find HUD Manager");
        }

        mainCamera = Camera.main;
        if (mainCamera == null) {
            Debug.LogError("Camera missing");
        }

        curLevel = GameObject.Find("LevelManager_" + levelNum).GetComponent<LevelManager>();
        if (curLevel == null) {
            Debug.LogError("Current level manager is not set");
        }
    }

    void LateUpdate() {
        Camera cam = Global.instance.mainCamera;
        Vector3 target = inputHandler.getCamTarget();
        Vector3 curCamPos = cam.transform.position;
        Vector3 offset = Global.instance.camOffset;
        offset = new Vector3(offset.x, offset.y * moverOffsetFactor * inputHandler.getNumMovers(), offset.z); //adjust height (zoom out for more movers)
        Vector3 nextCamPos = target - offset;
        float smoothSpeed = Vector3.Magnitude(nextCamPos - curCamPos) * 0.5f;
        cam.transform.position = Vector3.Lerp(curCamPos, nextCamPos, smoothSpeed * Time.deltaTime);
    }

    public void FailLevel() {
        inputHandler.FreezeAll();
        Debug.Log("Level failed");
    }

    public void EndRound() {
        inputHandler.FreezeAll();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
