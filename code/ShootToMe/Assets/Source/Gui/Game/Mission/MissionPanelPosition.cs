using UnityEngine;
using System.Collections;

public class MissionPanelPosition : MonoBehaviour {

	void Awake() {
		this.transform.localPosition = new Vector3(Screen.width, 0.0f, 0.0f);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
