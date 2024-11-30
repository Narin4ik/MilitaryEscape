using Exiled.API.Features;
using PlayerRoles;
using Respawning;

namespace MilitaryEscape
{
    public class EventHandlers
    {
        public void OnPlayerEscape(Exiled.Events.EventArgs.Player.EscapingEventArgs ev)
        {
            if (!ev.Player.IsCuffed)
                return;

            switch (ev.Player.Role.Type)
            {
                case RoleTypeId.NtfPrivate:
                case RoleTypeId.NtfSergeant:
                case RoleTypeId.NtfCaptain:
                case RoleTypeId.FacilityGuard:
                    ProcessEscape(ev.Player, RoleTypeId.ChaosConscript, SpawnableTeamType.ChaosInsurgency);
                    break;

                case RoleTypeId.ChaosConscript:
                case RoleTypeId.ChaosRifleman:
                case RoleTypeId.ChaosRepressor:
                case RoleTypeId.ChaosMarauder:
                    ProcessEscape(ev.Player, RoleTypeId.NtfPrivate, SpawnableTeamType.NineTailedFox);
                    break;
            }
        }

        private void ProcessEscape(Player player, RoleTypeId newRole, SpawnableTeamType team)
        {
            player.Role.Set(newRole);

            switch (team)
            {
                case SpawnableTeamType.ChaosInsurgency:
                case SpawnableTeamType.NineTailedFox:
                    Respawn.GrantTickets(PlayerRolesUtils.GetFaction(player.Role), 5);
                    break;
            }

            Log.Debug($"Player {player.Nickname} escaped and was converted to {newRole}. Tickets for {team} adjusted.");
        }
    }
}