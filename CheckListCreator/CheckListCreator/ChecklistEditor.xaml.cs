using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CheckListCreator
{
    public partial class ChecklistEditor : Page
    {
        private Checklist _checklist;
        private List<Checklist> _allChecklists;
        private DataService _dataService;

        public ChecklistEditor(Checklist checklist, List<Checklist> allChecklists)
        {
            InitializeComponent();
            _checklist = checklist;
            _allChecklists = allChecklists;
            _dataService = new DataService();
            LoadChecklistData();
        }

        private void LoadChecklistData()
        {
            ChecklistName.Text = _checklist.Name;
            ChecklistDescription.Text = _checklist.Description;
            RefreshTasksList();
            UpdateProgress();
        }

        private void RefreshTasksList()
        {
            TasksList.ItemsSource = null;
            TasksList.ItemsSource = _checklist.Tasks;
        }

        private void UpdateProgress()
        {
            _checklist.UpdateProgress();

            // Считаем статистику для отображения
            var totalMainTasks = _checklist.Tasks.Count;
            var completedMainTasks = _checklist.GetCompletedTasksCount();
            var averageProgress = _checklist.Completness;

            ProgressText.Text = $"Прогресс: {averageProgress}% ({completedMainTasks}/{totalMainTasks} задач завершено)";

            // Сохраняем изменения при каждом обновлении прогресса
            _dataService.SaveChecklists(_allChecklists);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new ChecklistsList());
        }

        private void ChecklistName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ChecklistName.Text))
            {
                _checklist.Name = ChecklistName.Text.Trim();
                UpdateProgress();
            }
        }

        private void ChecklistDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            _checklist.Description = ChecklistDescription.Text.Trim();
            UpdateProgress();
        }

        private void TaskCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Task task)
            {
                try
                {
                    // Инвертируем значение галочки
                    // Вся логика синхронизации с подзадачами и прогрессом теперь в свойстве CompleteFlag
                    task.CompleteFlag = !task.CompleteFlag;

                    // Обновляем прогресс чек-листа
                    _checklist.UpdateProgress();
                    UpdateProgress();

                    // Обновляем отображение списка
                    RefreshTasksList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при изменении задачи: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SubTaskCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Task subTask)
            {
                try
                {
                    // Инвертируем значение подзадачи
                    subTask.CompleteFlag = !subTask.CompleteFlag;

                    // Находим родительскую задачу и обновляем её прогресс на основе подзадач
                    var parentTask = FindParentTask(subTask);
                    if (parentTask != null)
                    {
                        parentTask.UpdateProgressFromSubTasks();
                    }

                    // Обновляем прогресс чек-листа
                    _checklist.UpdateProgress();
                    UpdateProgress();

                    // Обновляем отображение списка
                    RefreshTasksList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при изменении подзадачи: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private Task FindParentTask(Task subTask)
        {
            foreach (var task in _checklist.Tasks)
            {
                if (task.SubTasks.Contains(subTask))
                {
                    return task;
                }

                // Рекурсивно ищем в подзадачах
                var parent = FindParentTaskInSubTasks(task, subTask);
                if (parent != null)
                {
                    return parent;
                }
            }
            return null;
        }

        private Task FindParentTaskInSubTasks(Task parent, Task subTask)
        {
            foreach (var child in parent.SubTasks)
            {
                if (child.SubTasks.Contains(subTask))
                {
                    return child;
                }

                var result = FindParentTaskInSubTasks(child, subTask);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewTaskName.Text))
            {
                MessageBox.Show("Введите название задачи", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newTask = new Task(NewTaskName.Text.Trim(), NewTaskDescription.Text.Trim());
            _checklist.AddTask(newTask);
            RefreshTasksList();
            UpdateProgress();

            NewTaskName.Text = "Новая задача";
            NewTaskDescription.Text = "Описание задачи";
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is Task task)
            {
                var result = MessageBox.Show($"Удалить задачу '{task.Name}' и все её подзадачи?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _checklist.RemoveTask(task);
                    RefreshTasksList();
                    UpdateProgress();
                }
            }
        }

        private void AddSubTask_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is Task parentTask)
            {
                // Создаем новую подзадачу
                var newSubTask = new Task("Новая подзадача", "Описание подзадачи");

                // Если родительская задача уже выполнена, новая подзадача тоже должна быть выполнена
                if (parentTask.CompleteFlag)
                {
                    newSubTask.CompleteFlag = true;
                }

                parentTask.AddSubTask(newSubTask);
                RefreshTasksList();
                UpdateProgress();
            }
        }

        private void AddSubTaskConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is Task parentTask)
            {
                // Находим TextBox'ы для ввода подзадачи
                var stackPanel = ((Button)sender).Parent as StackPanel;
                if (stackPanel != null)
                {
                    var nameTextBox = stackPanel.Children.OfType<TextBox>()
                        .FirstOrDefault(tb => tb.Name == "NewSubTaskName");
                    var descTextBox = stackPanel.Children.OfType<TextBox>()
                        .FirstOrDefault(tb => tb.Name == "NewSubTaskDescription");

                    if (nameTextBox != null && !string.IsNullOrWhiteSpace(nameTextBox.Text))
                    {
                        var newSubTask = new Task(nameTextBox.Text.Trim(),
                            descTextBox?.Text?.Trim() ?? "");

                        // Если родительская задача уже выполнена, новая подзадача тоже должна быть выполнена
                        if (parentTask.CompleteFlag)
                        {
                            newSubTask.CompleteFlag = true;
                        }

                        parentTask.AddSubTask(newSubTask);
                        RefreshTasksList();
                        UpdateProgress();

                        // Очищаем поля
                        if (nameTextBox != null) nameTextBox.Text = "Новая подзадача";
                        if (descTextBox != null) descTextBox.Text = "Описание";
                    }
                }
            }
        }

        private void DeleteSubTask_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is Task subTask)
            {
                var result = MessageBox.Show($"Удалить подзадачу '{subTask.Name}'?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Находим родительскую задачу
                    var parentTask = FindParentTask(subTask);
                    if (parentTask != null)
                    {
                        parentTask.RemoveSubTask(subTask);
                        RefreshTasksList();
                        UpdateProgress();
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _dataService.SaveChecklists(_allChecklists);
            MessageBox.Show("Изменения сохранены", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteChecklist_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show($"Удалить чек-лист '{_checklist.Name}'?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _allChecklists.Remove(_checklist);
                _dataService.SaveChecklists(_allChecklists);

                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.MainFrame.Navigate(new ChecklistsList());
            }
        }
    }
}