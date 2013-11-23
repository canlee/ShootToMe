using UnityEngine;
using System.Collections;

public class MissionButtonTransform : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// 设置关卡按钮的大小
	/// </summary>
	public void setSize() {
		GameObject parent = GameObjectUtil.getParent(this.gameObject);
		MissionIconPanelTransform tmp = parent.GetComponent<MissionIconPanelTransform>() as MissionIconPanelTransform;
		float width = tmp.width / (3 * MissionData.MISSION_COL + 1) * 2;
		float height = tmp.height / (3 * MissionData.MISSION_ROW + 1) * 2;
		if(width >= height) {
			width = height;
		}
		else {
			height = width;
		}
		BoxCollider boxCollider = this.GetComponent<BoxCollider>() as BoxCollider;
		boxCollider.size = new Vector3(width, height, boxCollider.size.z);
		
		setChildSize(width);
	}
	
	/// <summary>
	/// Sets the size of the child.
	/// 设置子元素的大小
	/// </summary>
	private void  setChildSize(float pSize) {
		foreach(Transform child in this.transform) {
			switch(child.name) {
				
			case "Background":
			case "MissionLabel":
				child.transform.localScale = new Vector3(pSize, pSize, child.transform.localScale.z);
				break;
				
			case "LevelLabel":
				float cSize = pSize / 2;
				child.localScale = new Vector3(cSize, cSize, child.transform.localScale.z);
				child.localPosition = new Vector3(0.0f, -cSize, 0.0f);
				//设置字体的大小
				((UILabel) child.GetComponent<UILabel>()).font.dynamicFontSize = (int) cSize;
				break;
				
			default:
				break;
			}
		}
	}
}
