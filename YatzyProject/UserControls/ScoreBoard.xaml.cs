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
    /// Interaction logic for ScoreBoard.xaml
    /// </summary>
    public partial class UCScoreBoard : UserControl
    {
        VMYatzyGeneral viewModel;

        public UCScoreBoard(VMYatzyGeneral viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
        }

        public string SelectedHeaderPath
        {
            get
            {
                if (dg_ScoreBoard.SelectedCells.Count > 0)
                {
                    return ColumnBindPath(dg_ScoreBoard.SelectedCells[0].Column as DataGridTextColumn);
                }
                else
                {
                    return "";
                }
            }
        }

        private string ColumnBindPath(DataGridTextColumn column)
        {
            if (column.Binding is Binding)
            {
                return (column.Binding as Binding).Path.Path;
            }
            else
            {
                return "";
            }
        }

        private bool IsColumnBindedToPath(DataGridColumn column, string path)
        {
            if (ColumnBindPath(column as DataGridTextColumn) == path)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetColumnName(string HeaderPath)
        {
            foreach (var col in dg_ScoreBoard.Columns)
            {
                if (IsColumnBindedToPath(col, HeaderPath))
                {
                    return col.ToString();
                }
            }
            return "";
        }

        public string SelectedCellName
        {
            get
            {
                if (dg_ScoreBoard.SelectedCells.Count > 0)
                {
                    return dg_ScoreBoard.SelectedCells[0].Column.Header.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public void ResetScoreboard()
        {
            dg_ScoreBoard.UnselectAllCells();
            dg_ScoreBoard.SelectedItem = viewModel.CurrentPlayerScore;
            //SortScoreBoard();
            dg_ScoreBoard.Items.Refresh();
        }

        private void SortScoreBoard()
        {
            // Clear current sort descriptions
            dg_ScoreBoard.Items.SortDescriptions.Clear();

            // Add the new sort description
            dg_ScoreBoard.Items.SortDescriptions.Add(
                new System.ComponentModel.SortDescription(
                    dg_ScoreBoard.Columns[dg_ScoreBoard.Columns.Count - 1].SortMemberPath,
                    ListSortDirection.Descending)
                );
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the property associated with the selected column
            string propertyName = ((dg_ScoreBoard.Columns[0] as System.Windows.Controls.DataGridTextColumn).Binding as Binding).Path.Path;
        }

        private void dg_ScoreBoard_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count == 0)
            {
                return;
            }

            if (viewModel.CurrentPlayerScore == null || e.AddedCells[0].Item != viewModel.CurrentPlayerScore)
            {
                // Wrong cell selected
                SelectPlayer(viewModel.CurrentPlayerScore);
            }
        }

        public void SelectPlayer(VMPlayerScore? playerScore)
        {
            dg_ScoreBoard.UnselectAllCells();
            dg_ScoreBoard.SelectedItem = playerScore;
        }
    }
}
