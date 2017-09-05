namespace BankingSystem
{
    public class RefillService : IRefillService
    {
        public long Id { get; set; }
        public bool Refill(Account account, int sum)
        {
            // Тут может быть своя политики банка.
            account.MoneyOnAccount += sum;
            return true;
        }
    }
}