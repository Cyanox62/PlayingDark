using Exiled.API.Extensions;
using Exiled.API.Features;
using HarmonyLib;

namespace PlayingDark
{
	public class PlayingDark : Plugin<Config>
	{
		private EventHandlers ev;

		public override void OnEnabled()
		{
			ev = new EventHandlers();

			Exiled.Events.Handlers.Server.RoundStarted += ev.OnRoundStart;
			Exiled.Events.Handlers.Player.ChangingRole += ev.OnSetRole;
		}

		public override void OnDisabled()
		{
			Exiled.Events.Handlers.Server.RoundStarted -= ev.OnRoundStart;
			Exiled.Events.Handlers.Player.ChangingRole -= ev.OnSetRole;

			ev = null;
		}

		public override string Author => "Cyanox";
	}
}
