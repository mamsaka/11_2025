using System;
using System.Collections.Generic;
using System.Text;

namespace CheckListCreator
{
    class Checklist
    {
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
        public int Completness { get; set; }
        public string Description { get; set; }

        public Checklist(string name, List<Task> Tasks, int completness, string description)
        {
            Name = name;
            Tasks = Tasks ?? new List<Task>();
            Completness = completness;
            Description = description;
        }

        public Checklist(string name, int completness, string description)
            : this(name, new List<Task>(), completness, description)
        {
        }

        public void AddSubTask(Task subTask)
        {
            if (Tasks == null)
            {
                Tasks = new List<Task>();
            }
            Tasks.Add(subTask);
        }

        public override string ToString()
        {
            int tasks;

            if (Tasks == null)
            {
                tasks = 0;
            }
            else
            {
                tasks = Tasks.Count;
            }
            return $"{Name} ({Completness}%) - {Description} [Задачи: {tasks}]";
        }
    }
}
