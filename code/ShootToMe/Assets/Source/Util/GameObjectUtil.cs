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
	/// Finds the name of the childs by.
	/// 根据名字查找所有同名的子元素
	/// </summary>
	/// <returns>
	/// The childs by name.
	/// </returns>
	/// <param name='parent'>
	/// Parent.
	/// </param>
	/// <param name='name'>
	/// Name.
	/// </param>
	public static IList findChildsByName(GameObject parent, string name) {
		ArrayList list = new ArrayList();
		foreach(Transform child in parent.transform) {
			if(child.name == name) {
				list.Add(child.gameObject);
			}
		}
		return list;
	}
	
	/// <summary>
	/// Finds the childs by tag.
	/// 根据tag名查找同名的子元素
	/// </summary>
	/// <returns>
	/// The childs by tag.
	/// </returns>
	/// <param name='parent'>
	/// Parent.
	/// </param>
	/// <param name='tag'>
	/// Tag.
	/// </param>
	public static IList findChildsByTag(GameObject parent, string tag) {
		ArrayList list = new ArrayList();
		foreach(Transform child in parent.transform) {
			if(child.tag == tag) {
				list.Add(child.gameObject);
			}
		}
		return list;
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
