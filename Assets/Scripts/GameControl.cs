using UnityEngine;
using PureMVC.Patterns;

/// <summary>
/// 游戏入口
/// 控制游戏流程
/// </summary>
public class GameControl : MonoBehaviour
{
	void Awake()
	{
		Facade.Instance.RegisterCommand(PureMVCNtfs.CMD_OnGameStart, typeof(CmdStartGame));
		Facade.Instance.SendNotification(PureMVCNtfs.CMD_OnGameStart);
	}
}