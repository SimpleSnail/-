public class Transaction
{
    public string Sender { get; set; }      // 发送方地址
    public string Receiver { get; set; }   // 接收方地址
    public decimal Amount { get; set; }    // 转账金额

    public Transaction(string sender, string receiver, decimal amount)
    {
        Sender = sender;
        Receiver = receiver;
        Amount = amount;
    }

    public override string ToString()
    {
        return $"{Sender} -> {Receiver}: {Amount}";
    }
}
