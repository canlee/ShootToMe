using UnityEngine;
using System.Collections;

/// <summary>
/// Draw browse button.
/// 如果关卡太多会显示浏览页的点点
/// </summary>
public class DrawBrowseButton : MonoBehaviour {
	
	/// <summary>
	/// The current page.
	/// 现在所显示的点点的高亮位置
	/// </summary>
	private static int _currentPage = -1;
	private static IList buttonList;
	
	/// <summary>
	/// Gets or sets the current page.
	/// 当先显示的点点高亮的位置，从0开始
	/// </summary>
	/// <value>
	/// The current page.
	/// </value>
	public static int currentPage {
		get {
			return _currentPage;
		}
		set {
			if(buttonList != null && value >=0 && value < buttonList.Count) {
				GameObject gameObj;
				if(_currentPage >= 0 && _currentPage < buttonList.Count) {
					gameObj = buttonList[_currentPage] as GameObject;
					if(gameObj.GetComponent<UISprite>().spriteName == "picture_browse_radiobutton_hl") {
						gameObj.GetComponent<UISprite>().spriteName = "picture_browse_radiobutton";
						gameObj.transform.localScale = new Vector3(
																	gameObj.transform.localScale.x / 3, 
																	gameObj.transform.localScale.y / 3, 
																	gameObj.transform.localScale.z
																);
					}
				}
				gameObj = buttonList[value] as GameObject;
				if(gameObj.GetComponent<UISprite>().spriteName == "picture_browse_radiobutton") {
					gameObj.GetComponent<UISprite>().spriteName = "picture_browse_radiobutton_hl";
					gameObj.transform.localScale = new Vector3(
																gameObj.transform.localScale.x * 3, 
																gameObj.transform.localScale.y * 3, 
																gameObj.transform.localScale.z
															);
				}
				_currentPage = value;
			}
		}
	}
	
	void Awake() {
		int page = (MissionData.MISSION_COUNT - 1) / (MissionData.MISSION_COL * MissionData.MISSION_ROW);
		if(page > 0) {
			//需要显示点点
			page++;
			showPage(page);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void showPage(int page) {
		GameObject button = Resources.Load("Mission/BrowseButton", typeof(GameObject)) as GameObject;
		float width = button.transform.localScale.x;
		float sumWidth = (page * 2 - 1) * width;
		float x = -sumWidth / 2;
		float y = Screen.height / 2 * -0.75f;
		buttonList = new ArrayList();
		for(int i = 0; i < page; i++, x+= width * 2) {
			GameObject tmp = GameObject.Instantiate(button) as GameObject;
			buttonList.Add(tmp);
			tmp.transform.parent = this.transform;
			tmp.transform.localScale = button.transform.localScale;
			tmp.transform.localPosition = new Vector3(x, y, button.transform.localPosition.z);
		}
		currentPage = 0;
	}
}
