﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using HouraiTeahouse.HouraiInput;

public class SplashScreen : MonoBehaviour {
    [SerializeField] private AnimationCurve alphaOverTime;

    [SerializeField] private GameObject[] disableWhileLoading;

    [SerializeField] private Graphic[] splashGraphics;

    [SerializeField] private string targetSceneName;

    // Use this for initialization
    private void Start() {
        StartCoroutine(DisplaySplashScreen());
    }

	private bool CheckForSkip(){
		int i = 0;
		while (i < HInput.Devices.Count) {
			if (HInput.Devices[i].MenuWasPressed) {
				return true;
			}
			i++;
		}
		return false;
	}

    private IEnumerator DisplaySplashScreen() {
        foreach (GameObject target in disableWhileLoading)
            target.SetActive(false);
        float logoDisplayDuration = alphaOverTime.keys[alphaOverTime.length - 1].time;
        foreach (Graphic graphic in splashGraphics)
            graphic.enabled = false;
        foreach (Graphic graphic in splashGraphics) {
            if (graphic == null)
                continue;
            graphic.enabled = true;
            float t = 0;
            Color baseColor = graphic.color;
            Color targetColor = baseColor;
            baseColor.a = 0f;
            while (t < logoDisplayDuration) {
                graphic.color = Color.Lerp(baseColor, targetColor, alphaOverTime.Evaluate(t));

                //Wait one frame
                yield return null;
                t += Time.deltaTime;
				if (CheckForSkip ()) {
					if (t < logoDisplayDuration * 0.80f) {
						t = logoDisplayDuration * 0.80f;
					} else if (t < logoDisplayDuration * 0.95f) {
						t = logoDisplayDuration * 0.95f;
					}
				}
            }
            graphic.enabled = false;
            graphic.color = targetColor;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetSceneName);
        if (operation != null && !operation.isDone) {
            foreach (GameObject target in disableWhileLoading)
                target.SetActive(true);
            while (!operation.isDone)
                yield return null;
        }
        Destroy(gameObject);
    }
}