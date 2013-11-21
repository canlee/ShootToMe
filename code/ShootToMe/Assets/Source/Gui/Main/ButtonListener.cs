using UnityEngine;
using System.Collections;

public class ButtonListener : MonoBehaviour {
	
	private const int STATE_NOTHING = 0;				//没事
	private const int STATE_JUMP_TO_MISSION = 1;		//跳去选择关卡
	
	private int gameState = 0;
	
	private GameObject buttonPanel;						//放主按钮的面板
	private GameObject missionPanel;					//放关卡的面板
	private float scrollSpeed = 600.0f;					//滑动的速度
	
	void Awake() {
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("MainButton");
		foreach(GameObject button in buttons) {
			UIEventListener.Get(button).onClick = OnClick;
		}
		buttonPanel = GameObject.Find("ButtonPanel");
		missionPanel = GameObject.Find("MissionPanel");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		switch(gameState) {
			
		case STATE_JUMP_TO_MISSION:
			//跳去选择关卡
			scrollToMission();
			break;
			
		default:
			break;
		}
	}
	
	void OnClick(GameObject button) {
		switch(button.name) {
			
		case "StartButton":
			//点击了开始按钮
			gameState = STATE_JUMP_TO_MISSION;
			break;
			
		default:
			break;
		}
	}
	
	private void scrollToMission() {
		if(buttonPanel.transform.localPosition.x > -Screen.width) {
			float deltaX = scrollSpeed * Time.deltaTime;
			buttonPanel.transform.localPosition -= new Vector3(deltaX, 0.0f, 0.0f);
			missionPanel.transform.localPosition -= new Vector3(deltaX, 0.0f, 0.0f);
		}
		else {
			buttonPanel.transform.localPosition = 
				new Vector3(
								-Screen.width, 
								buttonPanel.transform.localPosition.y, 
								buttonPanel.transform.localPosition.z
							);
			missionPanel.transform.localPosition = 
				new Vector3(
								0.0f,
								missionPanel.transform.localPosition.y, 
								missionPanel.transform.localPosition.z
							);
			gameState = STATE_NOTHING;
		}
	}
	
}
