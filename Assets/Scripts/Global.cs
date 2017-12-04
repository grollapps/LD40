using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Global objects for the Game or Scene
/// </summary>
public class Global : MonoBehaviour {

    public static Global instance;

    public SplashScreen splashScreen;
    public LevelManager curLevel;
    public HudManager hudManager;
    public InputHandler inputHandler;
    public Camera mainCamera;
    public Vector3 camOffset = Vector3.zero; //default camera offset from target
    public float moverOffsetFactor = 0.5f; //multiplier for camera distance per mover added

    public int levelNum = 1;
    public int maxLevel = 1;
    private bool levelFailed = false;
    private bool gameOver = false;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("Scene loaded " + scene.name);
    }

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

        splashScreen = GameObject.Find("SplashScreen").GetComponent<SplashScreen>();
        if (splashScreen == null) {
            Debug.LogError("Spash screen is not set");
        }
        splashScreen.clearScore();
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

    /// <summary>
    /// User is advancing the current screen
    /// </summary>
    public void Advance() {
        if (levelFailed) {
            ResetLevel();
        } else {
            Debug.Log("Advance screen");
            if (gameOver) {
                splashScreen.setTitleImage();
                gameOver = false;
            } else {
                if (splashScreen.isSplashScreenEnabled()) {
                    StartLevel();
                }
            }
        }
    }

    /// <summary>
    /// Restarts the current level
    /// </summary>
    public void ResetLevel() {
        Debug.Log("Reset level");
        inputHandler.Reset();
        curLevel.Reset();
        hudManager.Reset();
        levelFailed = false;
        gameOver = false;
        StartLevel();
    }

    /// <summary>
    /// Load the level and show a splash screen until the level is ready to go
    /// </summary>
    public void LoadLevel() {
    }

    public void StartLevel() {
        splashScreen.clearImage();
        splashScreen.clearScore();
        inputHandler.StartAll();
    }

    /// <summary>
    /// User fails level or relquest to restart it
    /// </summary>
    public void FailLevel() {
        levelFailed = true;
        inputHandler.FreezeAll();
        splashScreen.setFailImage();
        splashScreen.setScore(inputHandler.getElapsedTime(), inputHandler.calcScore());
        Debug.Log("Level failed");
    }

    public void EndRound() {
        inputHandler.FreezeAll();
        splashScreen.setScore(inputHandler.getElapsedTime(), inputHandler.calcScore());
        splashScreen.setPassedImage();
        gameOver = true;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
