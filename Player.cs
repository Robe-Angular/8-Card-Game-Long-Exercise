﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8_Card_Game_Long_Exercise
{
    class Player
    {
        private string name;
        public string Name { get { return name; } }
        private Random random;
        private Deck cards;
        private TextBox textBoxOnForm;
        public Player(String name, Random random, TextBox textBoxOnForm)
        {
            this.name = name;
            this.random = random;
            this.cards = new Deck(new Card[] { });
            this.textBoxOnForm = textBoxOnForm;
            this.textBoxOnForm.Text += $"{this.name} has joined the game \r\n";

        }
        public List<Card.Values> PullOutBooks()
        {
            List<Card.Values> Books = new List<Card.Values>();
            for(int i = 1; i <= 13; i++)
            {
                Card.Values value = (Card.Values)i;
                int howMany = 0;
                for (int card = 0; card < this.cards.Count; card++)
                    if (this.cards.Peek(card).Value == value)
                        howMany++;
                if(howMany == 4)
                {
                    Books.Add(value);
                    for (int card = cards.Count - 1; card >= 0; card--)
                        cards.Deal(card);
                }
            }
            return Books;
        }

        public Card.Values GetRandomValue()
        {
            int intRdmCard = this.random.Next(cards.Count);
            Card rdmCard = this.cards.Peek(intRdmCard);
            Card.Values valueCard = rdmCard.Value;
            return valueCard;
        }

        public Deck DoYouHaveAny(Card.Values value)
        {
            Deck cardsIHave = cards.PullOutValues(value);
            this.textBoxOnForm.Text += $"{this.Name} has {cardsIHave.Count} {Card.Plural(value)} \r\n";
            return cardsIHave;
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock)
        {
            Card.Values rdmValue = this.GetRandomValue();
            this.AskForACard(players, myIndex, stock, rdmValue);

        }
        public void AskForACard(List<Player> players, int myIndex, Deck stock, Card.Values value)
        {
            textBoxOnForm.Text += $"{this.Name} asks if anybody has a {value} \r\n";
            int totalCardsGiven = 0;
            for(int i = 0; i < players.Count; i++)
            {
                if(i != myIndex)
                {
                    Player player = players[i];
                    Deck CardsGiven = player.DoYouHaveAny(value);
                    totalCardsGiven += CardsGiven.Count;
                    while (CardsGiven.Count > 0)
                        cards.Add(CardsGiven.Deal());
                }
            }
            if(totalCardsGiven == 0)
            {
                textBoxOnForm.Text += $"{this.Name} must draw from the stock \r\n";
                cards.Add(stock.Deal());
            }
        }

        public int CardCount { get { return cards.Count; } }
        public void TakeCard(Card card) { cards.Add(card); }
        public string[] GetCardNames() { return cards.GetCardNames(); }
        public Card Peek(int cardNumber) 
        { 
            Console.WriteLine(cardNumber); 
            return cards.Peek(cardNumber); 
        }
        public void SortHand() { cards.SortByValue(); }

    }
    public partial class Card
    {
        
        public static string Plural(Card.Values value)
        {
            if (value == Values.Six)
                return "Sixes";
            else
                return value.ToString() + "s";
        }   
    }
}

