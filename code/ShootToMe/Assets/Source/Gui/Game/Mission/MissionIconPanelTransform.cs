using UnityEngine;
using System.Collections;

public class MissionIconPanelTransform : MonoBehaviour {
	
	private float _width;
	private float _height;
	
	public float width {
		get {
			return _width;
		}
	}
	
	public float height {
		get {
			return _height;
		}
	}
	
	void Awake() {
		_width = this.transform.localScale.x * Screen.width;
		_height = this.transform.localScale.y * Screen.height;
		this.transform.localPosition += 
			new Vector3(0.0f, (1 - this.transform.localScale.y) / 2 * Screen.height, 0.0f);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
