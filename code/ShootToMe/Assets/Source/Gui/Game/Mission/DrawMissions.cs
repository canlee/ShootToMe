using UnityEngine;
using System.Collections;

/// <summary>
/// Draw missions.
/// 把所有的关卡都画出来
/// </summary>
public class DrawMissions : MonoBehaviour {
	
	private GameObject missionIconPanel;
	private GameObject missionButton;
	
	void Awake() {
		initGameObject();
		drawMissionIconPanel();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void initGameObject() {
		missionIconPanel = Resources.Load("Mission/MissionIconPanel") as GameObject;
		missionButton = Resources.Load("Mission/MissionButton") as GameObject;
	}
	
	/// <summary>
	/// Draws the mission icon panel.
	/// 画出装载关卡图标的面板
	/// </summary>
	private void drawMissionIconPanel() {
		GameObject parent = GameObject.Find("MissionPanel");
		int missionEachPage = MissionData.MISSION_COL * MissionData.MISSION_ROW;
		int pageCount = (MissionData.MISSION_COUNT - 1) / missionEachPage;
		pageCount++;
		float x = 0.0f;
		float detalX = Screen.width;
		for(int i = 0; i < pageCount; i++, x += detalX) {
			GameObject tmpPanel = GameObject.Instantiate(missionIconPanel) as GameObject;
			Vector3 originPosition = tmpPanel.transform.localPosition;
			Vector3 originScale = tmpPanel.transform.localScale;
			tmpPanel.transform.parent = parent.transform;
			//每个面板所在的位置
			tmpPanel.transform.localPosition = new Vector3(x, originPosition.y, originPosition.z);
			tmpPanel.transform.localScale = originScale;
			drawMiisionButton(tmpPanel, i * missionEachPage + 1);
		}
	}
	
	/// <summary>
	/// Draws the miision button.
	/// 在关卡面板上画出关卡
	/// </summary>
	/// <param name='panel'>
	/// Panel.
	/// </param>
	/// <param name='index'>
	/// Index.
	/// 第几个关卡，从1开始
	/// </param>
	private void drawMiisionButton(GameObject panel, int index) {
		int missionEachPage = MissionData.MISSION_COL * MissionData.MISSION_ROW;
		float x = 0.0f;
		float y = 0.0f;
		float size;
		for(int i = 0; i < missionEachPage && index <= MissionData.MISSION_COUNT; i++, index++) {
			GameObject tmpButton = GameObject.Instantiate(missionButton) as GameObject;
			Vector3 originScale = tmpButton.transform.localScale;
			tmpButton.transform.parent = panel.transform;
			tmpButton.transform.localScale = originScale;
			((MissionButtonTransform) tmpButton.GetComponent<MissionButtonTransform>()).setSize();
			size = ((BoxCollider) tmpButton.GetComponent<BoxCollider>()).size.x;
			if(i % MissionData.MISSION_COL == 0) {
				//x的开始
				x = -(3 * MissionData.MISSION_COL + 1) * size / 4 + size / 2;
				int row = i / MissionData.MISSION_COL;
				if(i == 0) {
					y = (3 * MissionData.MISSION_ROW + 1) * size / 4 - size;
				}
				else {
					y -= size * 3 / 2;
				}
			}
			x += size / 2;
			tmpButton.transform.localPosition = new Vector3(x, y, 0.0f);
			((MissionInfo) tmpButton.GetComponent<MissionInfo>()).number = index;
			x += size;
		}
	}
}
