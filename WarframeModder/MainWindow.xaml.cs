using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WarframeModder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<Mod> _mods = Mod.LoadModList(@"C:\Projects\Files\Temp\Mods.csv");
        private readonly Warframe _warframe = new Warframe();

        public MainWindow()
        {
            InitializeComponent();

            RefreshValues();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshValues();
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
    }
}
