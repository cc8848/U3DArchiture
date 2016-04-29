using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class InfoMediator : Mediator {

	static readonly string NAME = "InfoMediator";

	public InfoMediator() : base(NAME)
	{

	}

	public override void OnRegister()
	{
		LoadView();
	}

	/// <summary>
	/// 
	/// </summary>
	void LoadView()
	{
		GameObject go = Resources.Load<GameObject>("Canvas");

		if (go == null)
		{
			Debug.LogError("error");
			return;
		}

		go.Spawn();
	}
}
