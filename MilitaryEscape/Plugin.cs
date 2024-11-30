using Exiled.API.Features;
using System;

namespace MilitaryEscape
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "MilitaryEscape";
        public override string Author => "Narin & Ruslan0308c";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);

        public static Plugin Instance { get; private set; }

        public EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers();
            Exiled.Events.Handlers.Player.Escaping += eventHandlers.OnPlayerEscape;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Escaping -= eventHandlers.OnPlayerEscape;
            eventHandlers = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}
