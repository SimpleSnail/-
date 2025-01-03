using System;
using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Index { get; set; }//索引
    public DateTime Timestamp { get; set; }//时间戳
    //public string Data { get; set; }//数据

    public List<Transaction> Transactions { get; set; } = new List<Transaction>(); // 交易列表
    public string PreviousHash { get; set; }//上一个区块的哈希
    public string Hash { get; private set; }

    public int Nonce { get; private set; } // 添加 Nonce 属性


    public Block(int index, DateTime timestamp, string previousHash)
    {
        Index = index;
        Timestamp = timestamp;
        PreviousHash = previousHash;
        Nonce = 0; // 初始化为 0
        Hash = CalculateHash();//哈希不能直接赋值，只能通过方法计算出来
    }
    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction); // 将交易添加到区块中
    }
    public string CalculateHash()
    {
        using (SHA256 sha256 = SHA256.Create())//using用作资源回收
        {
            string rawData = $"{Index}{Timestamp}{string.Join("", Transactions)}{PreviousHash}{Nonce}";
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            //将字符串 rawData 转换为 UTF-8 字节数组哈希算法只能处理二进制数据，所以需要将字符串编码为字节数组
                        StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));//将每个字节转换为十六进制字符串格式，添加到StringBuilder中
            }
            return builder.ToString();
        }
    }
    public void MineBlock(int difficulty)
    {
        string target = new string('0', difficulty); // 生成目标字符串，如 "0000"
        while (!Hash.StartsWith(target))
        {
            Nonce++;
            Hash = CalculateHash();
        }
        Console.WriteLine($"Block mined: {Hash}");
    }

}
