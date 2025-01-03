using System;
using System.Collections.Generic;

public class Blockchain
{
    public List<Block> Chain { get; private set; }

    public int Difficulty { get; set; } = 3;         // 挖矿难度
    public List<Transaction> PendingTransactions { get; private set; } = new List<Transaction>(); // 交易池

    public Blockchain()
    {
        Chain = new List<Block> { CreateGenesisBlock() };
    }

    private Block CreateGenesisBlock()
    {
        return new Block(0, DateTime.Now, "0");
    }

    public void AddTransaction(Transaction transaction)
    {
        PendingTransactions.Add(transaction); // 将交易加入交易池
    }

    public void MinePendingTransactions(string minerAddress)
    {
        Block newBlock = new Block(Chain.Count, DateTime.Now, Chain.Last().Hash);

        // 将交易池中的交易打包到新区块中
        foreach (var transaction in PendingTransactions)
        {
            newBlock.AddTransaction(transaction);
        }

        // 给矿工一个奖励交易
        newBlock.AddTransaction(new Transaction("System", minerAddress, 50));

        // 挖矿
        newBlock.MineBlock(Difficulty);

        // 将新区块添加到链上
        Chain.Add(newBlock);

        // 清空交易池
        PendingTransactions.Clear();

        Console.WriteLine($"Block {newBlock.Index} has been mined!");
    }

    public bool IsValid()
    {
        for (int i = 1; i < Chain.Count; i++)
        {
            Block currentBlock = Chain[i];
            Block previousBlock = Chain[i - 1];

            if (currentBlock.Hash != currentBlock.CalculateHash())
                return false;

            if (currentBlock.PreviousHash != previousBlock.Hash)//需要上一个区块的头哈希等于本区块的父哈希
                return false;
        }
        return true;
    }
}
