namespace TodoApplication
{
    public class Counter
    {
        public int Value { get; private set; }

        public Counter()
        {
            Value = 0;
        }

        public void Increase()
        {
            Value++;
        }
    }
    class FinalizableClass
    {
        public Counter Counter;

        public FinalizableClass(Counter counter)
        {
            Counter = counter;
        }

        ~FinalizableClass()
        {
            lock (Counter)
            {
                Counter.Increase();
            }
        }
    }
}
