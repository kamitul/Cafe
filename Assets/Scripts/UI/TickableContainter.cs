using System.Collections.Generic;

namespace UI
{
    public class TickableContainter
    {
        private readonly List<ITickable> tickables;

        public TickableContainter(List<ITickable> list)
        {
            tickables = list;
        }

        public ITickable this[int index]
        {
            get => tickables[index];
        }

        public int Count { get => tickables.Count; }

        public T Get<T>()
            where T : class, ITickable
        {
            return tickables.Find(x => x.GetType() == typeof(T)) as T;
        }
    }
}