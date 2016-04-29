using PureMVC.Interfaces;
using PureMVC.Patterns;

public class CmdStartGame : SimpleCommand
{
	public override void Execute(INotification notification)
	{
		Facade.RegisterCommand(PureMVCNtfs.CMD_OpnUI_Info, typeof(OpnUI_Info));

		SendNotification(PureMVCNtfs.CMD_OpnUI_Info);
	}
}
