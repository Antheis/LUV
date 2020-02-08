using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization; 

public class GameStateManager : MonoBehaviour
{
    private GameData m_data = null;

    public void Save(GameManager game)
    {
        if (m_data == null)
            m_data = GetComponent<GameData>();

        PlayerPrefs.SetString("Feels", game.m_feels.ToString());
        PlayerPrefs.SetInt("FpC", game.m_feelPerClick);

        for (int i = 0; i < m_data.movies.Count; ++i)
            PlayerPrefs.SetInt("Movie"+i.ToString(), m_data.movies[i].nbPurchased);
        for (int i = 0; i < m_data.upgrades.Count; ++i)
            PlayerPrefs.SetInt("Upgrade"+i.ToString(), m_data.upgrades[i].isPurchased ? 1 : 0);

        PlayerPrefs.SetString("LastDate", DateTime.Now.ToString());
    }

    public void Load(GameManager game)
    {
        if (m_data == null)
            m_data = GetComponent<GameData>();

        game.m_feels = Convert.ToDouble(PlayerPrefs.GetString("Feels", "0"));
        game.m_feelPerClick = PlayerPrefs.GetInt("FpC", 1);

        for (int i = 0; i < m_data.movies.Count; ++i)
        {
            var elem = m_data.movies[i];
            elem.nbPurchased = PlayerPrefs.GetInt("Movie"+i.ToString(), 0);
            elem.newPrice = (long)(elem.priceFirst * Mathf.Pow(1.15f, (float)elem.nbPurchased));
        }
        for (int i = 0; i < m_data.upgrades.Count; ++i) {
            if (m_data.upgrades[i].isPurchased = PlayerPrefs.GetInt("Upgrade"+i.ToString(), 0) == 1)
                game.applyEffect(m_data.upgrades[i].Effect);
        }
    }

    public double CalculateIdle(double fps)
    {
        string date = PlayerPrefs.GetString("LastDate", "");
        if (date == "")
            return 0.0;
        TimeSpan time = DateTime.Now.Subtract(DateTime.Parse(date));
        if (time.TotalHours > 1)
            time = new TimeSpan(0, 1, 0, 0, 0);
        return fps * (time.TotalHours * 60 * 60);
    }
}
