﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    public Sprite titleImage;
    public Sprite levelFailImage;
    public Sprite levelPassedImage;

    public Image imageSlot;

    public GameObject scorePanel;

    //time to run goes here
    public Text elapsedTimeValue;
    public Text totalHpValue;
    public Text totalBeesValue;
    public Text totalScoreTimeValue;
    public Text totalScoreValue;

    private bool isEnabled = false;
    private CanvasGroup cg;
    private CanvasGroup scoreCg;

    public float transitionTime = 1.3f;

    private bool transitionInProgress = false;
    private float transitionEndTime = 0;
    private float transitionTargetAlpha = 0;
    private float transitionSourceAlpha = 0;

    void Awake() {
        cg = GetComponent<CanvasGroup>();
        if (cg == null) {
            Debug.LogError("No canvas group on splash screen");
        }
        scoreCg = scorePanel.GetComponent<CanvasGroup>();
        if (scoreCg == null) {
            Debug.LogError("No score panel set");
        }
        setTitleImage();
    }

    public void setScore(float elapsedTime, float[] scoreParts) {
        elapsedTimeValue.text = elapsedTime.ToString("F1");
        totalBeesValue.text = scoreParts[0].ToString("F0");
        totalHpValue.text = scoreParts[1].ToString("F0");
        totalScoreTimeValue.text = scoreParts[2].ToString("F1");
        totalScoreValue.text = scoreParts[3].ToString("F0");
        scoreCg.alpha = 1;
        scoreCg.interactable = true;
        scoreCg.blocksRaycasts = true;

    }

    public void clearScore() {
        elapsedTimeValue.text = "0";
        totalHpValue.text = "0";
        totalBeesValue.text = "0";
        totalScoreValue.text = "0";
        scoreCg.alpha = 0;
        scoreCg.interactable = false;
        scoreCg.blocksRaycasts = false;
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
        updateState(true);
    }

    public void setEnabled(bool enable, bool useTransition) {
        isEnabled = enable;
        updateState(useTransition);
    }

    public bool isSplashScreenEnabled() {
        return isEnabled;
    }

    public void updateState(bool useTransition) {
        if (isEnabled) {
            startTransition(1, useTransition);
            cg.interactable = true;
            cg.blocksRaycasts = true;
        } else {
            startTransition(0, useTransition);
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }

    private void startTransition(float targetAlpha, bool useTransition) {
        if (useTransition) {
            transitionTargetAlpha = targetAlpha;
            transitionSourceAlpha = 1 - targetAlpha;
            transitionEndTime = Time.time + transitionTime;
            transitionInProgress = true;
        } else {
            transitionInProgress = false;
            cg.alpha = targetAlpha;
        }
    }

	// Use this for initialization
	void Start () {
        updateState(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (transitionInProgress) {
            float newAlpha = transitionTargetAlpha;
            if (Time.time >= transitionEndTime) {
                transitionInProgress = false;
            } else {
                newAlpha = Mathf.Lerp(transitionSourceAlpha, transitionTargetAlpha, 1 - ((transitionEndTime - Time.time)/transitionTime));
            }
            cg.alpha = newAlpha;
        }
		
	}

}
