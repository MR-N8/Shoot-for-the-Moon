using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour {

	private string label = "";
	private float averageDeltaTime = 0.0166666f;

	IEnumerator Start() {
		GUI.depth = 2;
		while (true) {
			averageDeltaTime += (Time.unscaledDeltaTime - averageDeltaTime) * 0.01f;
			float msec = averageDeltaTime * 1000.0f;
			float fps = 1.0f / averageDeltaTime;
			label = string.Format(" {0:0.0} ms | {1:0.} FPS", msec, fps);
			yield return null;
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(2, 0, 120, 20), label);
	}
}