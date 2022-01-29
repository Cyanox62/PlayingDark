using Exiled.API.Extensions;
using Exiled.API.Features;
using HarmonyLib;

namespace PlayingDark
{
	public class PlayingDark : Plugin<Config>
	{
		private EventHandlers ev;

		private Harmony hInstance;

		public override void OnEnabled()
		{
			ev = new EventHandlers();

			hInstance = new Harmony("cyan.playingdark");
			hInstance.PatchAll();

			Exiled.Events.Handlers.Server.RoundStarted += ev.OnRoundStart;
			Exiled.Events.Handlers.Player.ChangingRole += ev.OnSetRole;
		}

		public override void OnDisabled()
		{
			Exiled.Events.Handlers.Server.RoundStarted -= ev.OnRoundStart;
			Exiled.Events.Handlers.Player.ChangingRole -= ev.OnSetRole;

			hInstance.UnpatchAll(hInstance.Id);
			hInstance = null;

			ev = null;
		}

		public override string Author => "Cyanox";
	}

	[HarmonyPatch(typeof(RoleExtensionMethods), nameof(RoleExtensionMethods.Is939))]
	static class VisionPatch
	{
		public static void Postfix(RoleType roleType, ref bool __result)
		{
			if (roleType.GetTeam() == Team.SCP) __result = true;
		}
	}
}
