using System;
using System.Collections.Generic;
using System.IO;

namespace WarframeModder
{
    public class Mod
    {
        public string Name { get; private set; } = "Unknown";
        public string BaseName { get; private set; } = "Unknown";

        public int ModDrain { get; private set; } = 0;

        public decimal HealthModifier { get; private set; } = 0M;
        public decimal ShieldModifier { get; private set; } = 0M;
        public decimal ArmourModifier { get; private set; } = 0M;
        public decimal EnergyModifier { get; private set; } = 0M;

        public decimal AbilityStrengthModifier { get; private set; } = 0M;
        public decimal AbilityEfficiencyModifier { get; private set; } = 0M;
        public decimal AbilityRangeModifier { get; private set; } = 0M;
        public decimal AbilityDurationModifier { get; private set; } = 0M;

        public static List<Mod> LoadModList(string fullFileName)
        {
            var modList = new List<Mod>();

            using (var reader = new StreamReader(fullFileName))
            {
                while (!reader.EndOfStream)
                {
                    var values = reader.ReadLine().Split(';');
                    modList.Add(new Mod
                    {
                        Name = values[0],
                        BaseName = values[1],
                        ModDrain = Convert.ToInt32(values[2]),
                        HealthModifier = Convert.ToDecimal(values[3]),
                        ShieldModifier = Convert.ToDecimal(values[4]),
                        ArmourModifier = Convert.ToDecimal(values[5]),
                        EnergyModifier = Convert.ToDecimal(values[6]),
                        AbilityStrengthModifier = Convert.ToDecimal(values[7]),
                        AbilityEfficiencyModifier = Convert.ToDecimal(values[8]),
                        AbilityRangeModifier = Convert.ToDecimal(values[9]),
                        AbilityDurationModifier = Convert.ToDecimal(values[10])
                    });
                }
            }

            return modList;
        }
    }
}
