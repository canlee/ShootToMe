using UnityEngine;
using System.Collections;

public class MissionInfo : MonoBehaviour {
	
	/// <summary>
	/// Gets or sets the number.
	/// 当前关卡号
	/// </summary>
	/// <value>
	/// The number.
	/// </value>
	public int number {
		get {
			GameObject child = GameObjectUtil.findChildByName(this.gameObject, "MissionLabel");
			UILabel uilabel = child.GetComponent<UILabel>() as UILabel;
			return int.Parse(uilabel.text);
		}
		set {
			GameObject child = GameObjectUtil.findChildByName(this.gameObject, "MissionLabel");
			UILabel uilabel = child.GetComponent<UILabel>() as UILabel;
			uilabel.text = value.ToString();
		}
	}
	
	/// <summary>
	/// Gets or sets the level.
	/// 当前关卡的分数等级
	/// </summary>
	/// <value>
	/// The level.
	/// </value>
	public MissionLevel level {
		get {
			GameObject child = GameObjectUtil.findChildByName(this.gameObject, "LevelLabel");
			UILabel uilabel = child.GetComponent<UILabel>() as UILabel;
			return MissionData.stringToLevel(uilabel.text);
		}
		set {
			GameObject child = GameObjectUtil.findChildByName(this.gameObject, "LevelLabel");
			UILabel uilabel = child.GetComponent<UILabel>() as UILabel;
			uilabel.text = MissionData.levelToString(value);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
