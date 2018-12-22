using System;

namespace WarframeModder.Exceptions
{
    public class NoMoreModCapacityException : Exception
    {
        private readonly int _leftCapacity;
        private readonly Mod _mod;

        public NoMoreModCapacityException(int leftCapacity, Mod mod)
        {
            _leftCapacity = leftCapacity;
            _mod = mod;
        }
    }
}
