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
                return JsonSerializer.Deserialize<List<Checklist>>(json) ?? new List<Checklist>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Checklist>();
            }
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