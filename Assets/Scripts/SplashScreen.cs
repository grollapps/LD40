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

    public float transitionTime = 1.3f;

    private bool transitionInProgress = false;
    private float transitionEndTime = 0;
    private float transitionTargetAlpha = 0;

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

    public bool isSplashScreenEnabled() {
        return isEnabled;
    }

    public void updateState() {
        if (isEnabled) {
            startTransition(1);
            cg.interactable = true;
            cg.blocksRaycasts = true;
        } else {
            startTransition(0);
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }

    private void startTransition(float targetAlpha) {
        transitionTargetAlpha = targetAlpha;
        transitionEndTime = Time.time + transitionTime;
        transitionInProgress = true;
    }

	// Use this for initialization
	void Start () {
        updateState();
	}
	
	// Update is called once per frame
	void Update () {
        if (transitionInProgress) {
            float newAlpha = transitionTargetAlpha;
            if (transitionEndTime >= Time.time) {
                transitionInProgress = false;
            } else {
                newAlpha = Mathf.Lerp(cg.alpha, transitionTargetAlpha, 1 - (transitionEndTime - Time.time) / transitionTime);
            }
            cg.alpha = newAlpha;
        }
		
	}

}
