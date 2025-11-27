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
        public bool CompleteFlag { get; set; }
        public string Description { get; set; }

        public Task(string name, List<Task> SubTasks, int completness, bool complete_flag, string description)
        {
            Name = name;
            SubTasks = SubTasks ?? new List<Task>();
            Completness = completness; // Show this, if no sub tasks
            CompleteFlag = complete_flag; // Use it as toogle button in redactor if no sub tasks
            Description = description;
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
