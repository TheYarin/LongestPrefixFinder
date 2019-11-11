using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestPrefixFinder
{
    public class LongestPrefixFinder<TValue>
    {
        class Node
        {
            public char? Key { get; set; }
            public TValue Value { get; set; }
            public Dictionary<char?, Node> Children { get; } = new Dictionary<char?, Node>();
        }

        private Node Root { get; } = new Node();

        public void AddPrefix(string prefix, TValue value)
        {
            Node longestPrefixNode = this.FindNodeRepresentingLongestPrefix(prefix, out int foundPrefixLength);

            string unknownPart = prefix.Remove(0, foundPrefixLength);

            if (foundPrefixLength == prefix.Length)
                // Prefix already exists, override value
                longestPrefixNode.Value = value;
            else
            {
                Node tmpNode = longestPrefixNode;

                for (int i = 0; i < unknownPart.Length - 1 /*Except the last character*/; i++)
                {
                    var childNode = new Node() { Key = unknownPart[i] };
                    tmpNode.Children.Add(childNode.Key, childNode);
                    tmpNode = childNode;
                }

                var lastNode = new Node() { Key = prefix.Last(), Value = value };
                tmpNode.Children[lastNode.Key] = lastNode;
            }
        }

        public TValue Find(string str)
        {
            return this.FindNodeRepresentingLongestPrefix(str, out _).Value;
        }

        private Node FindNodeRepresentingLongestPrefix(string str, out int prefixLength)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            Node currNode = this.Root;

            prefixLength = 0;

            for (int currCharIndex = 0; currCharIndex < str.Length; currCharIndex++)
            {
                char c = str[currCharIndex];

                if (!currNode.Children.TryGetValue(c, out Node currNodeChild))
                    break;

                currNode = currNodeChild;
                prefixLength++;
            }

            return currNode;
        }
    }
}
