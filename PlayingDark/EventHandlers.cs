using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayingDark
{
	class EventHandlers
	{
		private List<RoleType> SCPs = new List<RoleType>()
		{
			RoleType.Scp049,
			RoleType.Scp0492,
			RoleType.Scp096,
			RoleType.Scp106,
			RoleType.Scp173,
			RoleType.Scp93953,
			RoleType.Scp93989
		};

		private void RunPlayingDark()
		{
			foreach (Room room in Map.Rooms)
			{
				if (room.Zone == Exiled.API.Enums.ZoneType.LightContainment || room.Zone == Exiled.API.Enums.ZoneType.HeavyContainment)
				{
					room.LightIntensity = 0f;
					room.Color = Color.black;
				}
				else if (room.Zone == Exiled.API.Enums.ZoneType.Entrance)
				{
					room.Color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
				}
			}
		}

		internal void OnRoundStart()
		{
			RunPlayingDark();
			Timing.CallDelayed(0.5f, () =>
			{
				SCPs.ShuffleList();
				List<Player> scps = Player.List.Where(x => x.Team == Team.SCP).ToList();
				foreach (Player scp in scps)
				{
					if (scp.Role == RoleType.Scp079)
					{
						scp.Role = SCPs.First(x => !scps.Select(y => y.Role).Contains(x));
					}
				}
			});
		}

		internal void OnSetRole(ChangingRoleEventArgs ev)
		{
			Timing.CallDelayed(0.3f, () =>
			{
				if (!ev.Player.HasItem(ItemType.Flashlight))
				{
					ev.Player.AddItem(ItemType.Flashlight);
				}
			});
		}
	}
}
