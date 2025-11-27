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
            Checklist NewChecklist = new Checklist(NewName.Text, null, 100, true, NewDescription.Text);
            Checklists.Items.Add(NewChecklist);
        }

        private void Checklists_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Checklists.SelectedItem is Checklist selectedChecklist)
            {
            }
        }
    }
}
