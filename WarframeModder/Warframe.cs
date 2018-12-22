using System;
using System.Collections.Generic;
using System.Linq;
using WarframeModder.Exceptions;

namespace WarframeModder
{
    public class Warframe
    {
        // MESA
        // Base values
        private readonly decimal _baseHealth = 125;
        private readonly decimal _baseShield = 75;
        private readonly decimal _baseArmour = 65;
        private readonly decimal _baseEnergy = 100;

        private readonly decimal _baseAbilityStrength = 1;
        private readonly decimal _baseAbilityEfficiency = 1;
        private readonly decimal _baseAbilityRange = 1;
        private readonly decimal _baseAbilityDuration = 1;

        private readonly int _baseModCapacity = 60;
        private readonly int _baseModSlots = 8;

        // Base modifiers
        private readonly decimal _baseHealthModifier = 2M;
        private readonly decimal _baseShieldModifier = 2M;
        private readonly decimal _baseArmourModifier = 0M;
        private readonly decimal _baseEnergyModifier = 0.5M;

        private readonly decimal _baseAbilityStrengthModifier = 0M;
        private readonly decimal _baseAbilityEfficiencyModifier = 0M;
        private readonly decimal _baseAbilityRangeModifier = 0M;
        private readonly decimal _baseAbilityDurationModifier = 0M;

        public decimal HealthModifier { get; set; } = 0M;
        public decimal ShieldModifier { get; set; } = 0M;
        public decimal ArmourModifier { get; set; } = 0M;
        public decimal EnergyModifier { get; set; } = 0M;

        public decimal AbilityStrengthModifier { get; set; } = 0M;
        public decimal AbilityEfficiencyModifier { get; set; } = 0M;
        public decimal AbilityRangeModifier { get; set; } = 0M;
        public decimal AbilityDurationModifier { get; set; } = 0M;

        public int AuraModCapacityGain { get; set; } = 14;
        public int ModCapacityDrain { get; set; } = 0;
        public int ModsInstalled { get; set; } = 0;

        public List<Mod> InstalledMods { get; private set; } = new List<Mod>();

        public decimal Health
        {
            get
            {
                return _baseHealth * (1 + _baseHealthModifier + HealthModifier);
            }
        }
        public decimal Shield
        {
            get
            {
                return _baseShield * (1 + _baseShieldModifier + ShieldModifier);
            }
        }
        public decimal Armour
        {
            get
            {
                return _baseArmour * (1 + _baseArmourModifier + ArmourModifier);
            }
        }
        public decimal Energy
        {
            get
            {
                return _baseEnergy * (1 + _baseEnergyModifier + EnergyModifier);
            }
        }

        public decimal AbilityStrength
        {
            get
            {
                return _baseAbilityStrength * (1 + _baseAbilityStrengthModifier + AbilityStrengthModifier);
            }
        }
        public decimal AbilityEfficiency
        {
            get
            {
                return Math.Min(_baseAbilityEfficiency * (1 + _baseAbilityEfficiencyModifier + AbilityEfficiencyModifier), 1.75M);
            }
        }
        public decimal AbilityRange
        {
            get
            {
                return _baseAbilityRange * (1 + _baseAbilityRangeModifier + AbilityRangeModifier);
            }
        }
        public decimal AbilityDuration
        {
            get
            {
                return _baseAbilityDuration * (1 + _baseAbilityDurationModifier + AbilityDurationModifier);
            }
        }

        public int ModCapacity
        {
            get
            {
                return _baseModCapacity + AuraModCapacityGain - ModCapacityDrain;
            }
        }
        public int FreeModSlots
        {
            get
            {
                return _baseModSlots - ModsInstalled;
            }
        }

        private void VerifyInstalledMods(Mod mod)
        {
            var installedMod = InstalledMods.FirstOrDefault(m => m.BaseName == mod.BaseName);
            if (installedMod != null)
            {
                throw new DuplicateModException(installedMod, mod);
            }

            if (mod.ModDrain > ModCapacity)
            {
                throw new NoMoreModCapacityException(ModCapacity, mod);
            }

            if (FreeModSlots == 0)
            {
                throw new NoMoreModSlotsException(InstalledMods, mod);
            }
        }

        public void AddMod(Mod newMod)
        {
            VerifyInstalledMods(newMod);

            InstalledMods.Add(newMod);
            ModCapacityDrain += newMod.ModDrain;
            ModsInstalled++;
        }

        public void RemoveMod(Mod mod)
        {
            InstalledMods.Remove(mod);
            ModCapacityDrain -= mod.ModDrain;
            ModsInstalled--;
        }

        public void ClearInstalledMods()
        {
            ModCapacityDrain += InstalledMods.Sum(m => m.ModDrain);
            ModsInstalled += InstalledMods.Count;

            InstalledMods.Clear();
        }

        public override string ToString()
        {
            return $"{ModCapacityDrain}/{_baseModCapacity + AuraModCapacityGain} {ModsInstalled}/{_baseModSlots}";
        }
    }
}
