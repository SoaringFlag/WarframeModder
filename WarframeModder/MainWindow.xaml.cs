using System;
using System.Collections.Generic;
using System.Windows;
using WarframeModder.Exceptions;
using WarframeModder.Extensions;

namespace WarframeModder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<Mod> _mods;
        private readonly Warframe _warframe = new Warframe();

        private readonly List<string> _generatedModCombinations = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            _mods = Mod.LoadModList(System.IO.Path.Combine(Environment.CurrentDirectory, "Data\\Mods.csv"));

            RefreshValues();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateModCombinations();
        }

        private void RefreshValues()
        {
            Health.Text = _warframe.Health.ToString("0");
            Shield.Text = _warframe.Shield.ToString("0");
            Armour.Text = _warframe.Armour.ToString("0");
            Energy.Text = _warframe.Energy.ToString("0");

            AbilityStrength.Text = _warframe.AbilityStrength.ToString("0");
            AbilityEfficiency.Text = _warframe.AbilityEfficiency.ToString("0");
            AbilityDuration.Text = _warframe.AbilityDuration.ToString("0");
            AbilityRange.Text = _warframe.AbilityRange.ToString("0");
        }

        private void GenerateModCombinations(int startingIndex = 0, string modCombination = "")
        {
            for (int i = startingIndex; i < _mods.Count; i++)
            {
                var mod = _mods[i];
                var newModCombination = modCombination;
                try
                {
                    _warframe.AddMod(mod);
                    newModCombination += mod.ToShortName();
                }
                catch (DuplicateModException)
                {
                    continue;
                }
                catch (NoMoreModSlotsException)
                {
                    _generatedModCombinations.AddUnique(modCombination);
                    continue;
                }
                catch (NoMoreModCapacityException)
                {
                    _generatedModCombinations.AddUnique(modCombination);
                    continue;
                }

                GenerateModCombinations(i + 1, newModCombination);
                _warframe.RemoveMod(mod);
            }
        }
    }
}
