namespace BrewCoffee.Services.Repository
{
    public interface ICoffeeCounter
    {
        int Count { get; set; }
        void Brew();
        void Reset();
    }

    public class CoffeeCounter : ICoffeeCounter
    {
        private int _count = 0;
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }

        public void Brew()
        {
            _count++;
        }

        public void Reset()
        {
            _count = 0;
        }
    }
}
