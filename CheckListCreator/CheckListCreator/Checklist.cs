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
        public bool CompleteFlag { get; set; }
        public string Description { get; set; }

        public Checklist(string name, List<Task> Tasks, int completness, bool complete_flag, string description)
        {
            Name = name;
            Tasks = Tasks ?? new List<Task>();
            Completness = completness;
            CompleteFlag = complete_flag;
            Description = description;
        }

        public void AddTask(Task Task)
        {
            if (Tasks == null)
            {
                Tasks = new List<Task>();
            }
            Tasks.Add(Task);
        }

        public override string ToString()
        {
            string tasks;

            if (Tasks == null || Tasks.Count == 0)
            {
                tasks = "";
            }
            else
            {
                tasks = $" [Задачи: {Tasks.Count}]";
            }

            string completed;

            if (Completness == 100)
            {
                completed = "✅";
            }
            else if ( Completness == 0 )
            {
                completed = "❎";
            }
            else
            {
                completed = $"{Completness}%";
            }

            return $"{Name} ({completed}) - {Description}{tasks}";
        }
    }
}
