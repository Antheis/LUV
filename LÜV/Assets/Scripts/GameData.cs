﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public List<string> feelings = new List<string>
    {
        "Sadness",
        "Rage",
        "Anxiety",
        "Confusion",
        "Calmness",
        "Happiness",
        "Love"
    };

    public class Movie
    {
        public Movie(string name, float val, long price)
        {
            Name = name;
            initValue = val;
            priceFirst = price;
            nbPurchased = 0;
            newPrice = priceFirst;
            multiplier = 1;
        }

        public void Reset()
        {
            nbPurchased = 0;
            newPrice = priceFirst;
            multiplier = 1;
        }

        public string Name { get; }
        public float initValue { get; }
        public long priceFirst { get; }
        public int nbPurchased { get; set; }
        public long newPrice { get; set; }
        public int multiplier { get; set; }
    };

    public List<Movie> movies = new List<Movie>
    {
        new Movie("Tragedy", 0.1f, 10),
        new Movie("Action", 2.0f, 60),
        new Movie("Thriller", 12.0f, 600),
        new Movie("Experimental", 52.0f, 4000),
        new Movie("Documentary", 260.0f, 99999),
        new Movie("Comedy", 1111.0f, 1111111),
        new Movie("Love story", 6969.0f, 69696969)
    };

    public class Upgrade
    {
        public Upgrade(string name, string effect, string desc, long val)
        {
            Name = name;
            Effect = effect;
            Description = desc;
            Price = val;
            isPurchased = false;
        }

        public void Reset()
        {
            isPurchased = false;
        }

        public string Name { get; }
        public string Effect { get; }
        public string Description { get; }
        public long Price { get; }
        public bool isPurchased { get; set; }
    }

    public List<Upgrade> upgrades = new List<Upgrade>
    {
        //Tragedy
        new Upgrade("Drowning", "x1", "Doubles the amount of FEELS generated by tragedy movies", 100),
        new Upgrade("Catharsis", "x1", "Doubles the amount of FEELS generated by tragedy movies", 1000),
        new Upgrade("The Great Calamity", "x1", "Doubles the amount of FEELS generated by tragedy movies", 10000),
        new Upgrade("Tragedie", "x1", "Doubles the amount of FEELS generated by tragedy movies", 100000),
        new Upgrade("The Fall", "x1", "Doubles the amount of FEELS generated by tragedy movies", 1000000),
        new Upgrade("Wipe tears", ":1", "Discover sadness", 10000000),

        //Action movies
        new Upgrade("Street Fight", "x2", "Doubles the amount of FEELS generated by action movies", 500),
        new Upgrade("King Fury", "x2", "Doubles the amount of FEELS generated by action movies", 5000),
        new Upgrade("Aliens!!!", "x2", "Doubles the amount of FEELS generated by action movies", 50000),
        new Upgrade("BFG", "x2", "Doubles the amount of FEELS generated by action movies", 500000),
        new Upgrade("Last Man Standing", "x2", "Doubles the amount of FEELS generated by action movies", 5000000),
        new Upgrade("Calm", ":2", "Experience rage", 50000000),

        //Thriller
        new Upgrade("Shivers", "x3", "Doubles the amount of FEELS generated by thriller movies", 3000),
        new Upgrade("Zodiac", "x3", "Doubles the amount of FEELS generated by thriller movies", 30000),
        new Upgrade("Pool of Blood", "x3", "Doubles the amount of FEELS generated by thriller movies", 300000),
        new Upgrade("The Thing", "x3", "Doubles the amount of FEELS generated by thriller movies", 3000000),
        new Upgrade("Elementary", "x3", "Doubles the amount of FEELS generated by thriller movies", 15000000),
        new Upgrade("Reassure", ":3", "Experience fear", 300000000),

        //Experimental
        new Upgrade("??????", "x4", "Doubles the amount of FEELS generated by experimental movies", 10000),
        new Upgrade("Eraserhead", "x4", "Doubles the amount of FEELS generated by experimental movies", 100000),
        new Upgrade("Andalusia", "x4", "Doubles the amount of FEELS generated by experimental movies", 1000000),
        new Upgrade("Abstract Process", "x4", "Doubles the amount of FEELS generated by experimental movies", 10000000),
        new Upgrade("Illumination", "x4", "Doubles the amount of FEELS generated by experimental movies", 500000000),
        new Upgrade("Clear thoughts", ":4", "Discover confusion", 10000000000),

        //Documentary
        new Upgrade("Wild", "x5", "Doubles the amount of FEELS generated by documentaries", 40000),
        new Upgrade("Minuscule", "x5", "Doubles the amount of FEELS generated by documentaries", 400000),
        new Upgrade("Gigantic", "x5", "Doubles the amount of FEELS generated by documentaries", 4000000),
        new Upgrade("Precarity POV", "x5", "Doubles the amount of FEELS generated by documentaries", 40000000),
        new Upgrade("Life & Death", "x5", "Doubles the amount of FEELS generated by documentaries", 200000000),
        new Upgrade("Serenity", ":5", "Clear your electronic mind", 40000000000),

        //Comedy
        new Upgrade("Good vibes", "x6", "Doubles the amount of FEELS generated by comedy movies", 200000),
        new Upgrade("Brain & Dense", "x6", "Doubles the amount of FEELS generated by comedy movies", 2000000),
        new Upgrade("Dumb & more dumb", "x6", "Doubles the amount of FEELS generated by comedy movies", 20000000),
        new Upgrade("LOL", "x6", "Doubles the amount of FEELS generated by comedy movies", 200000000),
        new Upgrade("Stand-up Master", "x6", "Doubles the amount of FEELS generated by comedy movies", 1000000000),
        new Upgrade("Laugh", ":6", "Learn how to laugh", 200000000000),

        //Romance
        new Upgrade("Full Moon", "x7", "Doubles the amount of FEELS generated by romance movies", 696969),
        new Upgrade("Nice.", "x7", "Doubles the amount of FEELS generated by romance movies", 69696969),
        new Upgrade("Incoherent Chase", "x7", "Doubles the amount of FEELS generated by romance movies", 6969696969),
        new Upgrade("Romance isn't dead", "x7", "Doubles the amount of FEELS generated by romance movies", 696969696969),
        new Upgrade("Love Story", "x7", "Doubles the amount of FEELS generated by romance movies", 69696969696969),
        new Upgrade("Love", ":(", "Learn how to love.", 6969696969696969),

        //FpC
        new Upgrade("Remote Controller", "+10", "Adds 10 FEELS per click", 250),
        new Upgrade("Universal Controller", "+100", "Adds 100 FEELS per click", 800),
        new Upgrade("Ouimotte", "+1000", "Adds 1000 FEELS per click", 20000),
        new Upgrade("Alpha circuitry", "+10000", "Adds 10000 FEELS per click", 500000),
        new Upgrade("Electrokinesis", "+100000", "Adds 100000 FEELS per click", 50000000),
    };

    void Awake()
    {
        upgrades.Sort( (up1, up2) => up1.Price.CompareTo(up2.Price) );
    }
}