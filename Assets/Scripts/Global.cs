using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Global objects for the Game or Scene
/// </summary>
public class Global : MonoBehaviour {

    public static Global instance;

    public InputHandler inputHandler;
    public Camera mainCamera;
    public Vector3 camOffset = Vector3.zero; //default camera offset from target


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

        mainCamera = Camera.main;
        if (mainCamera == null) {
            Debug.LogError("Camera missing");
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
