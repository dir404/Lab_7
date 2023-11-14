using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericTaskScheduler
{
    public class TaskScheduler<TTask, TPriority> where TPriority : IComparable<TPriority>
    {
        private readonly PriorityQueue<TTask, TPriority> taskQueue = new PriorityQueue<TTask, TPriority>();

        public delegate void TaskExecution(TTask task);

        public void AddTask(TTask task, TPriority priority)
        {
            taskQueue.Enqueue(task, priority);
        }

        public void ExecuteNext(TaskExecution executeTask)
        {
            if (taskQueue.Count > 0)
            {
                var nextTask = taskQueue.Dequeue();
                executeTask(nextTask);
            }
            else
            {
                Console.WriteLine("Немає завдань для виконання.");
            }
        }

        public void PrintTaskQueue()
        {
            Console.WriteLine("Завдання в черзі:");
            foreach (var task in taskQueue)
            {
                Console.WriteLine(task);
            }
        }
    }

    public class PriorityQueue<TItem, TPriority> where TPriority : IComparable<TPriority>
    {
        private readonly List<Tuple<TItem, TPriority>> elements = new List<Tuple<TItem, TPriority>>();

        public int Count => elements.Count;

        public void Enqueue(TItem item, TPriority priority)
        {
            elements.Add(Tuple.Create(item, priority));
            elements.Sort((x, y) => x.Item2.CompareTo(y.Item2));
        }

        public TItem Dequeue()
        {
            if (elements.Count == 0)
                throw new InvalidOperationException("Черга порожня.");

            var item = elements[0].Item1;
            elements.RemoveAt(0);
            return item;
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return elements.Select(tuple => tuple.Item1).GetEnumerator();
        }
    }

    class Program
    {
        static void Main()
        {
            TaskScheduler<string, int> scheduler = new TaskScheduler<string, int>();

            scheduler.AddTask("Завдання 1", 2);
            scheduler.AddTask("Завдання 2", 1);
            scheduler.AddTask("Завдання 3", 3);

            scheduler.ExecuteNext(task => Console.WriteLine($"Виконується завдання: {task}"));

            scheduler.PrintTaskQueue();

            Console.ReadLine();
        }
    }

}
