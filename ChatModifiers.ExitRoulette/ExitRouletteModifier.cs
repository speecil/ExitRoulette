using System;
using System.Collections.Generic;

namespace ExitRoulette
{
    internal static class ExitRouletteModifier
    {
        internal static ChatModifiers.API.CustomModifier customModifier = new ChatModifiers.API.CustomModifier("ExitRoulette", "On note hit, there is a chance to exit the map :MonkaS:", "Speecil", "ExitRoulette.Menu.ExitRoulette.png", "ExitRoulette", null, new ChatModifiers.API.ArgumentInfo[] {}, ChatModifiers.API.Areas.Game, 5, new Dictionary<string, object> { { "SecondsActive", 5 }, { "Chance", 64 } });
    }
}
