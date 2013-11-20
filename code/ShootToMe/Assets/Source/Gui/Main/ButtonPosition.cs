using UnityEngine;
using System.Collections;

public class ButtonPosition : MonoBehaviour {
	
	//按钮的在他老豆下位置
	public Vector2 relativePosition = Vector2.one;
	
	//是否设置按钮弹出动画
	public bool isAnimation = true;
	//按钮弹出的动画延迟
	public float delay = 0.0f;
	
	private Vector3 desPosition;		//目标位置
	
	private float animationSpeed = 200.0f;
	private float currentDelay = -1.0f;
	
	/// <summary>
	/// Starts the animation.
	/// 开始该按钮的弹出动画
	/// </summary>
	public void startAnimation() {
		isAnimation = true;
	}
	
	// Use this for initialization
	void Start () {
		desPosition = new Vector3(Screen.width / 2 * relativePosition.x, 
									Screen.height / 2 * relativePosition.y, 
									0.0f);
		this.transform.localPosition = desPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		doAnimation();
	}
	
	//按钮出现动画
	private void doAnimation() {
		if(isAnimation) {
			if(currentDelay < 0) {
				currentDelay = 0.0f;
				hidePosition();
			}
			if(currentDelay >= delay) {
				if(this.transform.localPosition.x > desPosition.x) {
					this.transform.localPosition -= new Vector3(animationSpeed * Time.deltaTime, 0.0f, 0.0f);
				}
				else {
					resetPosition();
					isAnimation = false;
					currentDelay = -1.0f;
				}
			}
			else {
				currentDelay += Time.deltaTime;
			}
		}
	}
	
	//隐藏按钮
	private void hidePosition() {
		this.transform.localPosition = new Vector3(
			Screen.width / 2 + GetComponent<BoxCollider>().size.x / 2, 
			desPosition.y, 
			desPosition.z);
	}
	
	//按钮还原到原本的位置
	private void resetPosition() {
		this.transform.localPosition = desPosition;
	}
	
}
