using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace CheckListCreator
{
    public class DataService
    {
        private static readonly string DataFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "CheckListCreator", "checklists.json");

        public List<Checklist> LoadChecklists()
        {
            try
            {
                if (!File.Exists(DataFilePath))
                    return new List<Checklist>();

                var json = File.ReadAllText(DataFilePath);
                var checklists = JsonSerializer.Deserialize<List<Checklist>>(json) ?? new List<Checklist>();

                checklists.ForEach(c => UpdateChecklistProgress(c));

                return checklists;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Checklist>();
            }
        }

        private void UpdateChecklistProgress(Checklist checklist)
        {
            if (checklist.Tasks == null) return;

            foreach (var task in checklist.Tasks)
            {
                UpdateTaskProgressRecursively(task);
            }
            checklist.UpdateProgress(); // Обновить прогресс самого чек-листа
        }

        private void UpdateTaskProgressRecursively(Task task)
        {
            if (task.SubTasks == null || task.SubTasks.Count == 0)
            {
                // Для задач без подзадач, прогресс должен быть синхронизирован с CompleteFlag.
                // Это уже делается в CompleteFlag, но можно убедиться:
                task.Completness = task.CompleteFlag ? 100 : 0;
                return;
            }

            // Сначала рекурсивно обновляем все подзадачи
            foreach (var subTask in task.SubTasks)
            {
                UpdateTaskProgressRecursively(subTask);
            }

            // Обновляем прогресс родительской задачи на основе подзадач
            task.UpdateProgressFromSubTasks();
        }

        public void SaveChecklists(List<Checklist> checklists)
        {
            try
            {
                var directory = Path.GetDirectoryName(DataFilePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(checklists, options);
                File.WriteAllText(DataFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}