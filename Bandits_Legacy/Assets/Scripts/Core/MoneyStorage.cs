namespace Core
{
    public class MoneyStorage
    {
        private int _money;

        public void AddMoney(int amount)
        {
            _money += amount;
        }
    }
}