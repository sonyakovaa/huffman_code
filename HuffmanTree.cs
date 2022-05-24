using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_Code
{
    class HuffmanTree
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<char, int> fq = new Dictionary<char, int>();

        // Построение дерева Хаффмана
        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!fq.ContainsKey(source[i]))
                {
                    fq.Add(source[i], 0);
                }
                fq[source[i]]++;
            }

            foreach (KeyValuePair<char, int> symbol in fq)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderNodes.Count >= 2)
                {
                    // Берем первые два элемента
                    List<Node> token = orderNodes.Take(2).ToList<Node>();

                    // Создаем родительский узел, передавая частоту
                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Frequency = token[0].Frequency + token[1].Frequency,
                        Left = token[0],
                        Right = token[1]
                    };

                    nodes.Remove(token[0]);
                    nodes.Remove(token[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();
            }
        }

        // Метод кодирования строки. Передаем строку, а возвращаем ее в двоичном коде
        public BitArray Encode(string source)
        {
            List<bool> encodeSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.NodeRun(source[i], new List<bool>());
                encodeSource.AddRange(encodedSymbol);
            }
            BitArray bits = new BitArray(encodeSource.ToArray());
            return bits;
        }

        // Декодирование. Передаем строку в двоичном коде, а возвращаем преобразованную
        public string Decode(BitArray bits)
        {
            Node current = this.Root;
            string decod = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }
                if (IsLeaf(current))
                {
                    decod += current.Symbol;
                    current = this.Root;
                }
            }

            return decod;
        }

        // Проверка на то, являеься ли переданная нода листом дерева
        public bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }
    }
}
