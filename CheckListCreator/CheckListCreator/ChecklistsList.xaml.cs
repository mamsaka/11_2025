using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace CheckListCreator
{
    /// <summary>
    /// Логика взаимодействия для ChecklistsList.xaml
    /// </summary>
    public partial class ChecklistsList : Page
    {
        public ChecklistsList()
        {
            InitializeComponent();
        }

        private void Add_checklist_Click(object sender, RoutedEventArgs e)
        {
            Checklist NewChecklist = new Checklist(NewName.Text, null, 100, NewDescription.Text);
            Checklists.Items.Add(NewChecklist);
            string json = JsonConvert.SerializeObject(NewChecklist);
        }

        private void Checklists_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Checklists.SelectedItem is Checklist selectedChecklist)
            {
                // Здесь ваш код для обработки двойного клика
                // Например:
                MessageBox.Show($"Двойной клик по: {selectedChecklist.Name}");

                // Или открытие страницы редактирования:
                // NavigationService.Navigate(new EditChecklistPage(selectedChecklist));
            }
        }
    }
}
