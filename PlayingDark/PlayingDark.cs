using Exiled.API.Features;

namespace PlayingDark
{
	public class PlayingDark : Plugin<Config>
	{
		private EventHandlers ev;

		public override void OnEnabled()
		{
			ev = new EventHandlers();

			Exiled.Events.Handlers.Server.RoundStarted += ev.OnRoundStart;
			Exiled.Events.Handlers.Server.WaitingForPlayers += ev.OnWaitingForPlayers;

			Exiled.Events.Handlers.Player.ChangingRole += ev.OnSetRole;
		}

		public override void OnDisabled()
		{
			Exiled.Events.Handlers.Server.RoundStarted -= ev.OnRoundStart;
			Exiled.Events.Handlers.Server.WaitingForPlayers -= ev.OnWaitingForPlayers;

			Exiled.Events.Handlers.Player.ChangingRole -= ev.OnSetRole;

			ev = null;
		}

		public override string Author => "Cyanox";
	}
}
