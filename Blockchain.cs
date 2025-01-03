using System;
using System.Collections.Generic;

public class Blockchain
{
    public List<Block> Chain { get; private set; }

    public int Difficulty { get; set; } = 3;         // �ڿ��Ѷ�
    public List<Transaction> PendingTransactions { get; private set; } = new List<Transaction>(); // ���׳�

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
        PendingTransactions.Add(transaction); // �����׼��뽻�׳�
    }

    public void MinePendingTransactions(string minerAddress)
    {
        Block newBlock = new Block(Chain.Count, DateTime.Now, Chain.Last().Hash);

        // �����׳��еĽ��״������������
        foreach (var transaction in PendingTransactions)
        {
            newBlock.AddTransaction(transaction);
        }

        // ����һ����������
        newBlock.AddTransaction(new Transaction("System", minerAddress, 50));

        // �ڿ�
        newBlock.MineBlock(Difficulty);

        // ����������ӵ�����
        Chain.Add(newBlock);

        // ��ս��׳�
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

            if (currentBlock.PreviousHash != previousBlock.Hash)//��Ҫ��һ�������ͷ��ϣ���ڱ�����ĸ���ϣ
                return false;
        }
        return true;
    }
}
