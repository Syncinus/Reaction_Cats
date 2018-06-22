using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMatchScreen : MonoBehaviour {

	public GameObject loadingBar;
	public Slider slider;
	public Text progressText;
	public float Progress;

	public void LoadLevel(int Level) {
		StartCoroutine (LoadAsynchronously (Level));
	}

	IEnumerator LoadAsynchronously (int SceneIndex) {
		AsyncOperation operation = SceneManager.LoadSceneAsync (SceneIndex);

		loadingBar.SetActive (true);

		while (!operation.isDone) {
			float ThatProgress = Mathf.Clamp01 (operation.progress / 0.9f);

			slider.value = ThatProgress;
			progressText.text = ThatProgress * 100 + "%";
			Progress = ThatProgress;

			yield return null;
		}
		progressText.text = "COMPLETED LOADING";
	}
}
