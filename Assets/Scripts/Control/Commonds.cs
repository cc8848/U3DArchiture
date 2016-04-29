using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;

public class OpnUI_Info : SimpleCommand
{
	public override void Execute(INotification notification)
	{
		Facade.RegisterMediator(new InfoMediator());
	}
}