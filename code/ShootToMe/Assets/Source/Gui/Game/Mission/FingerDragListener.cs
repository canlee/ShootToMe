using UnityEngine;
using System.Collections;

/// <summary>
/// Drag listener.
/// 手势左右滑动的监听
/// </summary>
public class FingerDragListener : MonoBehaviour {
	
	/// <summary>
	/// 需要面板移动的最小滑动距离
	/// </summary>
	private const int MIN_DRAG_DELTA_X = 20;
	
	private const int STATE_NOTHING = 0;			//没事儿
	private const int STATE_SCROLL_TO_PRE = 1;		//滑到上一页
	private const int STATE_SCROLL_TO_POST = 2;		//滑到下一页
	private const int STATE_SCROLL_TO_CURRENT = 3;	//滑回到原位 
	
	private const float SCROLL_SPEED = 400.0f;		//滑动速度
	
	private static float SCROLL_ENABLE;
	
	private bool isEnable = false;
	private float startX = 0.0f;				//滑动的开始位置
	private bool isDrag = false;
	
	private IList childPanels;
	
	private int state = STATE_NOTHING;
	
	/// <summary>
	/// Raises the enable event.
	/// 注册事件
	/// </summary>
	void Start() {
		childPanels = GameObjectUtil.findChildsByTag(this.gameObject, "MissionIconPanel");
		if(childPanels.Count > 1) {
			//添加手势监听
			FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
			FingerGestures.OnFingerDragEnd += OnFingerDragEnd;
			FingerGestures.OnFingerDragMove += OnFingerDragMove;
			SCROLL_ENABLE = Screen.width / 3;
		}
	}
	
	/// <summary>
	/// Raises the disable event.
	/// 注销事件
	/// </summary>
	void OnDisable() {
		if(childPanels.Count > 1) {
			FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
			FingerGestures.OnFingerDragEnd -= OnFingerDragEnd;
			FingerGestures.OnFingerDragMove -= OnFingerDragMove;
		}
	}
	
	void OnGUI() {
		switch(state) {
			
		case STATE_SCROLL_TO_PRE:
			scrollToPre();
			break;
			
		case STATE_SCROLL_TO_POST:
			scrollToPost();
			break;
			
		case STATE_SCROLL_TO_CURRENT:
			scrollToCurrent();
			break;
		}
	}
	
	/// <summary>
	/// Raises the finger drag start event.
	/// 滑动开始
	/// </summary>
	void OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos) {
		if(this.transform.localPosition.x == 0) {
			isEnable = true;
			startX = fingerPos.x;
		}
		else {
			isEnable = false;
		}
	}
	
	/// <summary>
	/// Raises the finger drag stop event.
	/// 滑动结束
	/// </summary>
	void OnFingerDragEnd(int fingerIndex, Vector2 fingerPos) {
		isDrag = false;
		if(fingerPos.x > startX) {
			if(fingerPos.x - startX > SCROLL_ENABLE 
				&& DrawBrowseButton.currentPage - 1 >= 0) {
				//翻去上一页
				state = STATE_SCROLL_TO_PRE;
			}
			else {
				state = STATE_SCROLL_TO_CURRENT;
			}
		}
		else {
			if(startX - fingerPos.x > SCROLL_ENABLE 
				&& DrawBrowseButton.currentPage + 1 < childPanels.Count) {
				//翻去下一页
				state = STATE_SCROLL_TO_POST;
			}
			else {
				state = STATE_SCROLL_TO_CURRENT;
			}
		}
	}
	
	/// <summary>
	/// Raises the finger drag move event.
	/// 滑动中
	/// </summary>
	void OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta) {
		if(isEnable) {
			if(isDrag) {
				dragMissionPanel(delta.x, DrawBrowseButton.currentPage);
			}
			else {
				if(Mathf.Abs(fingerPos.x - startX) > MIN_DRAG_DELTA_X) {
					isDrag = true;
				}
			}
		}
	}
	
	/// <summary>
	/// Drags the mission panel.
	/// 移动关卡面板
	/// </summary>
	/// <param name='deltaX'>
	/// Delta x.
	/// 移动距离
	/// </param>
	/// <param name='currentPage'>
	/// Current page.
	/// 当前显示的面板
	/// </param>
	private void dragMissionPanel(float deltaX, int currentPage) {
		int pre = currentPage - 1;
		int post = currentPage + 1;
		GameObject tmp;
		Vector3 deltaV = new Vector3(deltaX, 0.0f, 0.0f);
		//前一页的移动
		if(pre >= 0) {
			tmp = childPanels[pre] as GameObject;
			tmp.transform.localPosition += deltaV;
		}
		//当前页的移动
		tmp = childPanels[currentPage] as GameObject;
		tmp.transform.localPosition += deltaV;
		//后一页的移动
		if(post < childPanels.Count) {
			tmp = childPanels[post] as GameObject;
			tmp.transform.localPosition += deltaV;
		}
	}
	
	/// <summary>
	/// Scrolls to pre.
	/// 滑动到上一页
	/// </summary>
	private void scrollToPre() {
		int currentPage = DrawBrowseButton.currentPage;
		GameObject tmpCurrent = childPanels[currentPage] as GameObject;
		GameObject tmpPre = childPanels[currentPage - 1] as GameObject;
		Vector3 deltaV = new Vector3(SCROLL_SPEED * Time.deltaTime, 0.0f, 0.0f);
		if(tmpCurrent.transform.localPosition.x < Screen.width) {
			tmpCurrent.transform.localPosition += deltaV;
			tmpPre.transform.localPosition += deltaV;
		}
		else {
			tmpCurrent.transform.localPosition = new Vector3(
																Screen.width, 
																tmpCurrent.transform.localPosition.y, 
																tmpCurrent.transform.localPosition.z
															);
			tmpPre.transform.localPosition = new Vector3(
															0.0f, 
															tmpPre.transform.localPosition.y, 
															tmpPre.transform.localPosition.z
														);
			if(currentPage - 2 >= 0) {
				tmpPre = childPanels[currentPage - 2] as GameObject;
				tmpPre.transform.localPosition = new Vector3(
																-Screen.width, 
																tmpPre.transform.localPosition.y, 
																tmpPre.transform.localPosition.z
															);
			}
			DrawBrowseButton.currentPage = currentPage - 1;
			state = STATE_NOTHING;
		}
	}
	
	/// <summary>
	/// Scrolls to post.
	/// 滑动到下一页
	/// </summary>
	private void scrollToPost() {
		int currentPage = DrawBrowseButton.currentPage;
		GameObject tmpCurrent = childPanels[currentPage] as GameObject;
		GameObject tmpPost = childPanels[currentPage + 1] as GameObject;
		Vector3 deltaV = new Vector3(SCROLL_SPEED * Time.deltaTime, 0.0f, 0.0f);
		if(tmpCurrent.transform.localPosition.x > -Screen.width) {
			tmpCurrent.transform.localPosition -= deltaV;
			tmpPost.transform.localPosition -= deltaV;
		}
		else {
			tmpCurrent.transform.localPosition = new Vector3(
																-Screen.width, 
																tmpCurrent.transform.localPosition.y, 
																tmpCurrent.transform.localPosition.z
															);
			tmpPost.transform.localPosition = new Vector3(
															0.0f, 
															tmpPost.transform.localPosition.y, 
															tmpPost.transform.localPosition.z
														);
			if(currentPage + 2 < childPanels.Count) {
				tmpPost = childPanels[currentPage + 2] as GameObject;
				tmpPost.transform.localPosition = new Vector3(
																Screen.width, 
																tmpPost.transform.localPosition.y, 
																tmpPost.transform.localPosition.z
															);
			}
			DrawBrowseButton.currentPage = currentPage + 1;
			state = STATE_NOTHING;
		}
	}
	
	/// <summary>
	/// Scrolls to current.
	/// 滑回到原位
	/// </summary>
	private void scrollToCurrent() {
		int currentPage = DrawBrowseButton.currentPage;
		GameObject tmp = childPanels[currentPage] as GameObject;
		GameObject tmpPre = null;
		GameObject tmpPost = null;
		if(currentPage - 1 >= 0) {
			tmpPre = childPanels[currentPage - 1] as GameObject;
		}
		if(currentPage + 1 < childPanels.Count) {
			tmpPost = childPanels[currentPage + 1] as GameObject;
		}
		Vector3 deltaV = new Vector3(SCROLL_SPEED * Time.deltaTime, 0.0f, 0.0f);
		if(tmp.transform.localPosition.x > deltaV.x) {
			tmp.transform.localPosition -= deltaV;
			if(tmpPre != null) {
				tmpPre.transform.localPosition -= deltaV;
			}
			if(tmpPost != null) {
				tmpPost.transform.localPosition -= deltaV;
			}
		}
		else if(tmp.transform.localPosition.x < -deltaV.x) {
			tmp.transform.localPosition += deltaV;
			if(tmpPre != null) {
				tmpPre.transform.localPosition += deltaV;
			}
			if(tmpPost != null) {
				tmpPost.transform.localPosition += deltaV;
			}
		}
		else {
			tmp.transform.localPosition = new Vector3(
														0.0f, 
														tmp.transform.localPosition.y, 
														tmp.transform.localPosition.z
													);
			if(tmpPre != null) {
				tmpPre.transform.localPosition = new Vector3(
																-Screen.width, 
																tmpPre.transform.localPosition.y, 
																tmpPre.transform.localPosition.z
															);
			}
			if(tmpPost != null) {
				tmpPost.transform.localPosition = new Vector3(
																Screen.width, 
																tmpPost.transform.localPosition.y, 
																tmpPost.transform.localPosition.z
															);
			}
			state = STATE_NOTHING;
		}
	}
	
}
