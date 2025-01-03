class Program
{
    static void Main(string[] args)
    {
        Blockchain blockchain = new Blockchain();

        // 添加交易
        Console.WriteLine("Adding transactions...");
        blockchain.AddTransaction(new Transaction("Alice", "Bob", 10));
        blockchain.AddTransaction(new Transaction("Bob", "Charlie", 20));

        // 矿工挖矿
        Console.WriteLine("\nMining transactions...");
        blockchain.MinePendingTransactions("Miner1");

        // 添加更多交易
        blockchain.AddTransaction(new Transaction("Charlie", "Alice", 5));

        // 矿工继续挖矿
        Console.WriteLine("\nMining more transactions...");
        blockchain.MinePendingTransactions("Miner2");

        // 打印区块链数据
        Console.WriteLine("\nBlockchain:");
        foreach (var block in blockchain.Chain)
        {
            Console.WriteLine($"Block {block.Index} [{block.Timestamp}]");
            Console.WriteLine($"PreviousHash: {block.PreviousHash}");
            Console.WriteLine($"Hash: {block.Hash}");
            foreach (var transaction in block.Transactions)
            {
                Console.WriteLine($"  {transaction}");
            }
        }
    }
}
