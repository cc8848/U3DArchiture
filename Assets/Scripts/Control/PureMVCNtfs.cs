using UnityEngine;
using System.Collections;


/// <summary>
/// 规范：
///		发送给Command的NTF以CMD开头
///		发送给Mediator的NTF以NTF开头
/// </summary
public static class PureMVCNtfs
{
	public const string CMD_OnGameStart = "CMD_OnGameStart";

	public const string CMD_OpnUI_Info = "CMD_OpnUI_Info";
}
