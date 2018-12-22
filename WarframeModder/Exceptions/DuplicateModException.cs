using System;

namespace WarframeModder.Exceptions
{
    public class DuplicateModException : Exception
    {
        private readonly Mod _installedMod;
        private readonly Mod _newMod;

        public DuplicateModException(Mod installedMods, Mod newMod)
        {
            _installedMod = installedMods;
            _newMod = newMod;
        }
    }
}
