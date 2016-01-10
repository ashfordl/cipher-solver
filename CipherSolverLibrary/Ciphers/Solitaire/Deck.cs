using System.Collections.Generic;
using System.Text;
using CipherSolver.Analysis;

namespace CipherSolver.Ciphers.Solitaire
{
    public class Deck
    {
        public Deck()
        {
            this.Cards = new List<int>();

            for (int i = 0; i < 54; i++)
            {
                this.Cards.Add(i);
            }
        }

        public Deck(string key, bool keySetJokers = true)
        {
            // Initialise the deck
            this.Cards = new Deck().Cards;

            // Order deck according to key
            foreach (char c in key)
            {
                this.MoveCard(JokerA, JokerA + 1);
                this.MoveCard(JokerB, JokerB + 2);
                this.TripleCut(JokerA, JokerB);
                this.CountCut(this.Cards[this.Cards.Count - 1] + 1);
                this.CountCut(Alphabet.IndexOf(c) + 1);
            }

            // Set jokers according to key
            if (keySetJokers && key.Length >= 2)
            {
                this.Cards.Remove(52);
                this.Cards.Remove(53);

                this.Cards.Insert(Alphabet.IndexOf(key[key.Length - 2]), 52);
                this.Cards.Insert(Alphabet.IndexOf(key[key.Length - 1]), 53);
            }
        }

        public List<int> Cards { get; set; }

        public int JokerA
        {
            get
            {
                return Cards.IndexOf(52);
            }
        }

        public int JokerB
        {
            get
            {
                return Cards.IndexOf(53);
            }
        }

        /// <summary>
        /// Moves a card to a new position.
        /// </summary>
        public void MoveCard(int originalIndex, int newIndex)
        {
            int card = this.Cards[originalIndex];
            this.Cards.RemoveAt(originalIndex);

            // Make sure if it has to loop round, the moved card is not top of the deck
            if (newIndex % (this.Cards.Count + 1) != newIndex)
            {
                newIndex = (newIndex % (this.Cards.Count + 1)) + 1;
            }
            this.Cards.Insert(newIndex, card);
        }

        /// <summary>
        /// Performs a triple cut on the deck.
        /// </summary>
        /// <param name="middleIndex1"> The start/end index of the middle section. </param>
        /// <param name="middleIndex2"> The end/start index of the middle section. </param>
        public void TripleCut(int middleIndex1, int middleIndex2)
        {
            // Get the correct begin and end indexes
            int middleBegin = middleIndex1;
            int middleEnd = middleIndex2;
            if (middleIndex1 > middleIndex2)
            {
                middleBegin = middleIndex2;
                middleEnd = middleIndex1;
            }
            
            // Get sections to triple cut
            List<int> section1 = this.Cards.GetRange(0, middleBegin);
            List<int> section2 = this.Cards.GetRange(middleBegin, middleEnd - middleBegin + 1);
            List<int> section3 = this.Cards.GetRange(middleEnd + 1, this.Cards.Count - middleEnd - 1);

            // Cut and reorder the deck accordingly
            this.Cards = new List<int>();
            this.Cards.AddRange(section3);
            this.Cards.AddRange(section2);
            this.Cards.AddRange(section1);
        }

        /// <summary>
        /// Performs a count cut.
        /// </summary>
        public void CountCut(int count)
        {
            // If count is Joker B, make sure it acts like Joker A
            if (count == this.Cards.Count)
            {
                count -= 1;
            }

            // Get the section to cut
            List<int> countCards = this.Cards.GetRange(0, count);
            this.Cards.RemoveRange(0, count);

            // Put cards at second to bottom
            this.Cards.InsertRange(this.Cards.Count - 1, countCards);
        }

        /// <summary>
        /// Generates a keystream to encrypt by. 
        /// </summary>
        public string GenerateKeystream(int keystreamLength)
        {
            StringBuilder sb = new StringBuilder();
            
            while (sb.Length < keystreamLength)
            {
                // Perform steps
                this.MoveCard(JokerA, JokerA + 1);
                this.MoveCard(JokerB, JokerB + 2);
                this.TripleCut(JokerA, JokerB);
                this.CountCut(this.Cards[this.Cards.Count - 1] + 1);

                // Get keystream letter
                int count = this.Cards[0];
                if (count == this.Cards.Count - 1)
                {
                    // Make sure Joker B acts like Joker A
                    count -= 1;
                }

                int keyValue = this.Cards[count + 1];
                if (keyValue < 52)
                {
                    // Only take letter if it is not a joker
                    sb.Append(Alphabet.LetterAt((keyValue + 1) % 26, false));
                }
            }

            return sb.ToString();
        }
    }
}
