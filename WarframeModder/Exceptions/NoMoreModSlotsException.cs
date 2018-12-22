using System;
using System.Collections.Generic;

namespace WarframeModder.Exceptions
{
    public class NoMoreModSlotsException : Exception
    {
        private readonly List<Mod> _installedMods = new List<Mod>();
        private readonly Mod _mod;

        public NoMoreModSlotsException(List<Mod> installedMods, Mod mod)
        {
            _installedMods.AddRange(installedMods);
            _mod = mod;
        }
    }
}
