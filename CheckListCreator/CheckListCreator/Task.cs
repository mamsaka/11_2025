using System;
using System.Collections.Generic;
using System.Text;

namespace CheckListCreator
{
    class Task
    {
        public string Name { get; set; }
        public List<Task> SubTasks { get; set; }
        public int Completness { get; set; }
        public string Description { get; set; }

        public Task(string name, List<Task> subTasks, int completness, string description)
        {
            Name = name;
            SubTasks = subTasks ?? new List<Task>();
            Completness = completness;
            Description = description;
        }

        public Task(string name, int completness, string description)
            : this(name, new List<Task>(), completness, description)
        {
        }
        
        public void AddSubTask(Task subTask)
        {
            if (SubTasks == null)
            {
                SubTasks = new List<Task>();
            }
            SubTasks.Add(subTask);
        }

        public override string ToString()
        {
            return $"{Name} ({Completness}%) - {Description} [Sub-tasks: {SubTasks.Count}]";
        }
    }
}
