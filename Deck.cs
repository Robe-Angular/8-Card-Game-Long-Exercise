using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_Card_Game_Long_Exercise
{
    class Deck
    {
        private List<Card> cards;
        private Random random = new Random();
        
        public Deck()
        {
            cards = new List<Card>();
            for (int suit = 0; suit <= 3; suit++)
                for (int value = 1; value <= 13; value++)
                    cards.Add(new Card(suit, value));
        }

        public Deck(Card[] initialCards)
        {
            cards = new List<Card>(initialCards);
        }

        public int Count { get { return cards.Count; } }

        public void Add(Card cardToAdd)
        {
            cards.Add(cardToAdd);
        }

        public Card Deal(int index)
        {
            Card CardToDeal = cards[index];
            cards.RemoveAt(index);
            return CardToDeal;
        }

        public void Shuffle()
        {
            List<Card> NewCards = new List<Card>();
            while(cards.Count > 0)
            {
                int IndexCardToMove = random.Next(cards.Count);
                NewCards.Add(cards[IndexCardToMove]);
                cards.RemoveAt(IndexCardToMove);
            }
            cards = NewCards;
        }

        public string[] GetCardNames()
        {
            string[] cardNames = new string[cards.Count];
            int counter = 0;
            foreach(Card card in cards)
            {
                cardNames[counter]=card.Name;
                counter++;
            }


            return cardNames;
        }

        public void Sort()
        {
            cards.Sort(new CardComparerByValue());
        }

        public Card Peek(int cardNumber)
        {
            Console.WriteLine(cards[cardNumber].Name);
            return cards[cardNumber];
        }

        public Card Deal()
        {
            return Deal(0);
        }

        public bool ContainsValue(Card.Values value)
        {
            foreach (Card card in cards)
                if (card.Value == value)
                    return true;
            return false;
        }

        public Deck PullOutValues(Card.Values value)
        {
            Deck deckToReturn = new Deck(new Card[] { });
            for(int i = cards.Count -1; i >= 0; i--)
            {
                if (cards[i].Value == value)
                    deckToReturn.Add(Deal(i));                
            }
            return deckToReturn;
        }

        public bool HasBook(Card.Values value)
        {
            int NumberOfCards = 0;
            foreach (Card card in cards)
                if (card.Value == value)
                    NumberOfCards++; ;
            if (NumberOfCards == 4)
                return true;
            else
                return false;
        }

        public void SortByValue()
        {
            cards.Sort(new CardComparerByValue());
        }
    }
}
