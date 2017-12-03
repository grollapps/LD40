using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    public Sprite titleImage;
    public Sprite levelFailImage;
    public Sprite levelPassedImage;

    public Image imageSlot;

    private bool isEnabled = false;
    private CanvasGroup cg;

    void Awake() {
        cg = GetComponent<CanvasGroup>();
        if (cg == null) {
            Debug.LogError("No canvas group on splash screen");
        }
        setTitleImage();
    }

    public void setTitleImage() {
        imageSlot.sprite = titleImage;
        setEnabled(true);
    }

    public void setFailImage() {
        imageSlot.sprite = levelFailImage;
        setEnabled(true);
    }

    public void setPassedImage() {
        imageSlot.sprite = levelPassedImage;
        setEnabled(true);
    }

    public void clearImage() {
        setEnabled(false);
    }

    public void setEnabled(bool enable) {
        isEnabled = enable;
        updateState();
    }

    public void updateState() {
        if (isEnabled) {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        } else {
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }

	// Use this for initialization
	void Start () {
        updateState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
