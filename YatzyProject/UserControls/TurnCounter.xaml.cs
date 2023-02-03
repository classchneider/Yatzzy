using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ViewModels;

namespace Yatzy.UserControls
{
    /// <summary>
    /// Interaction logic for TurnCounter.xaml
    /// </summary>
    public partial class UTurnCounter : UserControl
    {
        public UTurnCounter()
        {
            InitializeComponent();

            // Clear current sort descriptions
            gbTurnCounter.Items.SortDescriptions.Clear();
        }

        public void SortPlayers()
        {
            // Clear current sort descriptions
            gbTurnCounter.Items.SortDescriptions.Clear();

            // Add the new sort description
            gbTurnCounter.Items.SortDescriptions.Add(
                new System.ComponentModel.SortDescription(
                    gbTurnCounter.Columns[gbTurnCounter.Columns.Count - 1].SortMemberPath,
                    ListSortDirection.Descending)
                );
        }
    }
}
