﻿using System;
using System.Collections;

namespace Huffman_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine();

            HuffmanTree huffmanTree = new HuffmanTree();
            huffmanTree.Build(input);  // Построение дерева Хаффмана
            BitArray encoded = huffmanTree.Encode(input); // Кодирование введённой строки

            Console.WriteLine();
            Console.Write("Кодирование: ");

            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine();

            string decoded = huffmanTree.Decode(encoded);
            Console.WriteLine("Декодирование: " + "'" + decoded + "'" + "\nСимволов в строке:  " + decoded.Length);
        }
    }
}
