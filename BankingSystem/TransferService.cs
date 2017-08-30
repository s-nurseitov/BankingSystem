namespace BankingSystem
{
    public class TransferService : ITransferService
    {
        public long Id { get; set; }
        public void Transfer(Account account, Account transferaccount, double Money)
        {
            if(account.MoneyOnAccount
        }
    }
}