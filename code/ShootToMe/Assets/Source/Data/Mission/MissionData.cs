using UnityEngine;
using System.Collections;

/// <summary>
/// Mission data.
/// 关于关卡的数据
/// </summary>
public class MissionData {
	
	/// <summary>
	/// The MISSIO n_ COUN.
	/// 关卡的数量
	/// </summary>
	public static int MISSION_COUNT =20;
	
	/// <summary>
	/// The MISSIO n_ RO.
	/// 显示关卡的面板能显示多少行
	/// </summary>
	public static int MISSION_ROW = 2;
	
	/// <summary>
	/// The MISSIO n_ CO.
	/// 显示关卡的面板1行能够显示多少个关卡图标
	/// </summary>
	public static int MISSION_COL = 4;
	
	public static string levelToString(MissionLevel level) {
		return System.Enum.GetName(typeof(MissionLevel), level);
	}
	
	public static MissionLevel stringToLevel(string level) {
		return (MissionLevel) System.Enum.Parse(typeof(MissionLevel), level);
	}
	
}
