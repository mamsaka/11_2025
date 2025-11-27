using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CheckListCreator
{
    public class Checklist : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private int _completness;
        private List<Task> _tasks;
        private bool _isUpdating;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public List<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public int Completness
        {
            get => _completness;
            private set
            {
                if (_completness != value)
                {
                    _completness = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CompleteFlag => Completness == 100;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public Guid Id { get; private set; }

        public Checklist(string name, List<Task> tasks, string description = "")
        {
            Id = Guid.NewGuid();
            Name = name;
            Tasks = tasks ?? new List<Task>();
            Description = description;
            _isUpdating = false;
            UpdateProgress();
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
            UpdateProgress();
        }

        public void RemoveTask(Task task)
        {
            Tasks.Remove(task);
            UpdateProgress();
        }

        public void UpdateProgress()
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                if (Tasks == null || Tasks.Count == 0)
                {
                    Completness = 0;
                    return;
                }

                // Считаем средний прогресс по всем основным задачам
                int totalProgress = 0;
                int taskCount = Tasks.Count;

                foreach (var task in Tasks)
                {
                    totalProgress += GetTaskProgress(task);
                }

                Completness = taskCount > 0 ? totalProgress / taskCount : 0;
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private int GetTaskProgress(Task task)
        {
            // Всегда используем текущий прогресс задачи
            // Для задач с подзадачами прогресс будет обновляться автоматически
            return task.Completness;
        }

        // Метод для подсчета количества выполненных задач (для отображения)
        public int GetCompletedTasksCount()
        {
            return Tasks.Count(task => task.CompleteFlag);
        }

        public override string ToString()
        {
            string tasks = Tasks?.Count > 0 ? $" [Задачи: {Tasks.Count}]" : "";
            string completed = Completness == 100 ? "✅" : Completness == 0 ? "❎" : $"{Completness}%";

            return $"{Name} ({completed}) - {Description}{tasks}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}