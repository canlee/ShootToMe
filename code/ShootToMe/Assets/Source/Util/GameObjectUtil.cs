using UnityEngine;
using System.Collections;

public class GameObjectUtil {

	/// <summary>
	/// Finds the name of the child by.
	/// 根据名字查找子元素
	/// </summary>
	/// <returns>
	/// The child by name.
	/// 如果找不到返回null
	/// </returns>
	/// <param name='parent'>
	/// Parent.
	/// </param>
	/// <param name='name'>
	/// Name.
	/// </param>
	public static GameObject findChildByName(GameObject parent, string name) {
		foreach(Transform transform in  parent.transform) {
			if(transform.name == name) {
				return transform.gameObject;
			}
		}
		return null;
	}
	
	/// <summary>
	/// Gets the parent.
	/// 获取一个元素的父亲
	/// </summary>
	/// <returns>
	/// The parent.
	/// </returns>
	/// <param name='gameObj'>
	/// Game object.
	/// </param>
	public static GameObject getParent(GameObject gameObj) {
		return gameObj.transform.parent.gameObject;
	}
	
}
