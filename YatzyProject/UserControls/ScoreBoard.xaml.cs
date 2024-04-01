using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels;
using YatzyRepository;

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
            if (e.AddedCells.Count == 0 || viewModel.IsGameOver)
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

        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        private DataGridCell GetCell(int rowIndex, int columnIndex)
        {
            DataGridRow row = dg_ScoreBoard.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                dg_ScoreBoard.UpdateLayout();
                dg_ScoreBoard.ScrollIntoView(dg_ScoreBoard.Items[rowIndex]);
                row = dg_ScoreBoard.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }
            DataGridCellsPresenter p = GetVisualChild<DataGridCellsPresenter>(row);
            DataGridCell cell = p.ItemContainerGenerator.ContainerFromIndex(columnIndex) as DataGridCell;
            return cell;
        }
        private DataGridCell GetCell(int rowIndex, DataGridColumn column)
        {
            return GetCell(rowIndex, column.DisplayIndex);
        }

        public async void MarkScoreChoice((string property, int value) suggestion, VMPlayer player)
        {
            await Task.Delay(100);
            int rowIndex = viewModel.GetPlayerIndex(player);
            if (rowIndex >= 0)
            {
                foreach (var column in dg_ScoreBoard.Columns)
                {
                    if (IsColumnBindedToPath(column, $"{nameof(VMScoreboard)}.{suggestion.property}"))
                    {

                        DataGridCell cell = GetCell(rowIndex, column);
                        Brush b = cell.Background;
                        cell.Background = Brushes.Blue;
                        //cell.Content = suggestion.value;
                        await Task.Delay(3000);
                        cell.Background = b;
                        //cell.Content = suggestion.value;
                        break;
                    }
                }
            }
        }

        public void MarkScoreSuggestions(List<(string property, int value)> suggestions)
        {
            ResetSuggestions();
            foreach (var suggestion in suggestions)
            {
                // Mark the field binded to this property as suggestion

                // Find the field binded to this property
                //dg_ScoreBoard.SelectedCells[0].Column.Header.ToString();

                int rowIndex = viewModel.GetCurrentPlayerIndex();
                if (rowIndex >= 0)
                {
                    foreach (var column in dg_ScoreBoard.Columns)
                    {
                        if (IsColumnBindedToPath(column, $"{nameof(VMScoreboard)}.{suggestion.property}"))
                        {

                            DataGridCell cell = GetCell(rowIndex, column);
                            cell.Background = Brushes.Green;
                            cell.Content = suggestion.value;
                        }
                    }
                }
            }
        }

        public void ResetSuggestions()
        {
            dg_ScoreBoard.UnselectAllCells();
            dg_ScoreBoard.Items.Refresh();
        }
    }
}
