using System;
using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Index { get; set; }//����
    public DateTime Timestamp { get; set; }//ʱ���
    //public string Data { get; set; }//����

    public List<Transaction> Transactions { get; set; } = new List<Transaction>(); // �����б�
    public string PreviousHash { get; set; }//��һ������Ĺ�ϣ
    public string Hash { get; private set; }

    public int Nonce { get; private set; } // ��� Nonce ����


    public Block(int index, DateTime timestamp, string previousHash)
    {
        Index = index;
        Timestamp = timestamp;
        PreviousHash = previousHash;
        Nonce = 0; // ��ʼ��Ϊ 0
        Hash = CalculateHash();//��ϣ����ֱ�Ӹ�ֵ��ֻ��ͨ�������������
    }
    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction); // ��������ӵ�������
    }
    public string CalculateHash()
    {
        using (SHA256 sha256 = SHA256.Create())//using������Դ����
        {
            string rawData = $"{Index}{Timestamp}{string.Join("", Transactions)}{PreviousHash}{Nonce}";
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            //���ַ��� rawData ת��Ϊ UTF-8 �ֽ������ϣ�㷨ֻ�ܴ�����������ݣ�������Ҫ���ַ�������Ϊ�ֽ�����
                        StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));//��ÿ���ֽ�ת��Ϊʮ�������ַ�����ʽ����ӵ�StringBuilder��
            }
            return builder.ToString();
        }
    }
    public void MineBlock(int difficulty)
    {
        string target = new string('0', difficulty); // ����Ŀ���ַ������� "0000"
        while (!Hash.StartsWith(target))
        {
            Nonce++;
            Hash = CalculateHash();
        }
        Console.WriteLine($"Block mined: {Hash}");
    }

}
