using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CheckListCreator
{
    public class Task : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private int _completness;
        private bool _completeFlag;
        private List<Task> _subTasks;
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

        public List<Task> SubTasks
        {
            get => _subTasks;
            set
            {
                _subTasks = value;
                OnPropertyChanged();
            }
        }

        public int Completness
        {
            get => _completness;
            set
            {
                if (_completness != value)
                {
                    _completness = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CompleteFlag
        {
            get => _completeFlag;
            set
            {
                if (_completeFlag != value && !_isUpdating)
                {
                    _isUpdating = true;
                    _completeFlag = value;
                    OnPropertyChanged();

                    if (value)
                    {
                        // Если включаем галочку - включаем все подзадачи
                        SetAllSubTasksComplete(true);
                        // Для задач с подзадачами устанавливаем прогресс в 100%
                        if (HasSubTasks)
                        {
                            Completness = 100;
                        }
                    }
                    else
                    {
                        // Если выключаем галочку - выключаем все подзадачи
                        SetAllSubTasksComplete(false);
                        // Для задач с подзадачами устанавливаем прогресс в 0%
                        if (HasSubTasks)
                        {
                            Completness = 0;
                        }
                    }

                    // Для задач без подзадач обновляем прогресс
                    if (!HasSubTasks)
                    {
                        Completness = value ? 100 : 0;
                    }

                    _isUpdating = false;
                }
            }
        }

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
        public bool HasSubTasks => SubTasks?.Count > 0;

        public Task(string name, string description = "")
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            SubTasks = new List<Task>();
            CompleteFlag = false;
            Completness = 0;
            _isUpdating = false;
        }

        public void AddSubTask(Task subTask)
        {
            SubTasks.Add(subTask);

            // Синхронизируем состояние, если основная задача уже выполнена
            if (CompleteFlag)
            {
                subTask.CompleteFlag = true;
            }

            UpdateProgressFromSubTasks();
        }

        public void RemoveSubTask(Task subTask)
        {
            SubTasks.Remove(subTask);
            UpdateProgressFromSubTasks();
        }

        private void SetAllSubTasksComplete(bool complete)
        {
            foreach (var subTask in SubTasks)
            {
                subTask.CompleteFlag = complete;

                // Рекурсивно применяем ко всем вложенным подзадачам
                subTask.SetAllSubTasksComplete(complete);
            }
        }

        // Обновляем прогресс на основе состояния подзадач
        public void UpdateProgressFromSubTasks()
        {
            if (_isUpdating) return;

            _isUpdating = true;

            try
            {
                if (SubTasks == null || SubTasks.Count == 0)
                {
                    // Без подзадач прогресс равен 100% если задача выполнена
                    Completness = CompleteFlag ? 100 : 0;
                }
                else
                {
                    // С подзадачами считаем прогресс по выполненным подзадачам
                    var completedSubTasks = SubTasks.Count(t => t.CompleteFlag);
                    Completness = SubTasks.Count > 0 ?
                        (int)Math.Round((double)completedSubTasks / SubTasks.Count * 100) : 0;

                    // Синхронизируем CompleteFlag с прогрессом
                    if (Completness == 100 && !CompleteFlag)
                    {
                        _completeFlag = true;
                        OnPropertyChanged(nameof(CompleteFlag));
                    }
                    else if (Completness < 100 && CompleteFlag)
                    {
                        _completeFlag = false;
                        OnPropertyChanged(nameof(CompleteFlag));
                    }
                }
            }
            finally
            {
                _isUpdating = false;
            }
        }

        public override string ToString()
        {
            string subTasksInfo = HasSubTasks ? $" [Подзадачи: {SubTasks.Count}]" : "";
            return $"{Name} ({Completness}%) - {Description}{subTasksInfo}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}