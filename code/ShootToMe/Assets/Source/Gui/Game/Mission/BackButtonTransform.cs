using UnityEngine;
using System.Collections;

public class BackButtonTransform : MonoBehaviour {
	
	/// <summary>
	/// The relative position.
	/// 相对于屏幕的位置
	/// </summary>
	public Vector2 relativePosition = Vector2.one;
	
	/// <summary>
	/// The size of the relative.
	/// 相对于屏幕的大小
	/// </summary>
	public Vector2 relativeSize = Vector2.one;
	
	void Awake() {
		setSize();
		setPosition();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// Sets the size.
	/// 设置按钮大小
	/// </summary>
	private void setSize() {
		float x = Screen.width * relativeSize.x;
		float y = Screen.height * relativeSize.y;
		if(x >= y) {
			x = y;
		}
		else {
			y = x;
		}
		BoxCollider tmp = this.gameObject.GetComponent<BoxCollider>();
		tmp.size = new Vector3(x, y, tmp.size.z);
		foreach(Transform child in transform) {
			child.gameObject.transform.localScale = 
				new Vector3(x, y, child.gameObject.transform.localScale.z);
		}
	}
	
	/// <summary>
	/// Sets the position.
	/// 设置按钮位置
	/// </summary>
	private void setPosition() {
		this.transform.localPosition = 
			new Vector3(
							Screen.width / 2 * relativePosition.x, 
							Screen.height / 2 * relativePosition.y, 
							this.transform.localPosition.z
						);
	}
}
