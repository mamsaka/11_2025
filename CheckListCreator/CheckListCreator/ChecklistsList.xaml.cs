using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CheckListCreator
{
    public partial class ChecklistsList : Page
    {
        private List<Checklist> _checklists;
        private DataService _dataService;

        public ChecklistsList()
        {
            InitializeComponent();
            _dataService = new DataService();
            LoadChecklists();
        }

        private void LoadChecklists()
        {
            _checklists = _dataService.LoadChecklists();
            RefreshList();
        }

        private void SaveChecklists()
        {
            _dataService.SaveChecklists(_checklists);
        }

        private void Add_checklist_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewName.Text))
            {
                MessageBox.Show("Введите название чек-листа", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newChecklist = new Checklist(NewName.Text.Trim(), null, NewDescription.Text.Trim());
            _checklists.Add(newChecklist);
            SaveChecklists();
            RefreshList();

            // Очистка полей
            NewName.Text = "Новый чек-лист";
            NewDescription.Text = "Описание";
        }

        private void DeleteChecklist_Click(object sender, RoutedEventArgs e)
        {
            if (Checklists.SelectedItem is Checklist selectedChecklist)
            {
                var result = MessageBox.Show($"Удалить чек-лист '{selectedChecklist.Name}'?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _checklists.Remove(selectedChecklist);
                    SaveChecklists();
                    RefreshList();
                }
            }
            else
            {
                MessageBox.Show("Выберите чек-лист для удаления", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Checklists_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Checklists.SelectedItem is Checklist selectedChecklist)
            {
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.MainFrame.Navigate(new ChecklistEditor(selectedChecklist, _checklists));
            }
        }

        private void RefreshList()
        {
            Checklists.ItemsSource = null;
            Checklists.ItemsSource = _checklists;
        }
    }
}