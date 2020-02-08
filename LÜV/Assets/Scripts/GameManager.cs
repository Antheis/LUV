using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameData))]
public class GameManager : MonoBehaviour
{
    public Text disp_feels;
    public Text disp_fps;
    private string m_init_fps;

    public GameObject MoviesPanel;
    private List<GameObject> disp_movies = new List<GameObject>();
    public GameObject UpgradesPanel;
    private List<GameObject> disp_upgrades = new List<GameObject>();

    private HandleSmiles m_smile;
    private GameStateManager m_state;
    private GameData m_data;

    public AudioSource click;
    public AudioSource learn;

    public double m_feels { get; set; }
    public int m_feelPerClick { get; set; }
    public double m_fps { get; set; }
    private int m_idxFeeling = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_init_fps = disp_fps.text;

        for (int i = 0; i < MoviesPanel.transform.childCount; ++i)
            disp_movies.Add(MoviesPanel.transform.GetChild(i).gameObject);
        for (int i = 0; i < UpgradesPanel.transform.childCount; ++i)
            disp_upgrades.Add(UpgradesPanel.transform.GetChild(i).gameObject);

        m_smile = GetComponent<HandleSmiles>();
        m_state = GetComponent<GameStateManager>();
        m_data = GetComponent<GameData>();

        m_state.Load(this);

        for (int i = 0; i < disp_movies.Count; ++i)
        {
            disp_movies[i].transform.GetChild(1).GetComponent<Text>().text = m_data.movies[i].Name.ToUpper();
            disp_movies[i].transform.GetChild(2).GetComponent<Text>().text = m_data.movies[i].newPrice.ToString() + " F";
            disp_movies[i].transform.GetChild(3).GetComponent<Text>().text = m_data.movies[i].nbPurchased.ToString();

            Button button = disp_movies[i].GetComponent<Button>();
            int becauseCSisShit = i;
            button.onClick.AddListener(delegate{
                click.Play();
                setNbMovie(becauseCSisShit);
            });
        }
        displayUpgrades();
        for (int i = 0; i < disp_upgrades.Count; ++i)
        {
            Button button = disp_upgrades[i].GetComponent<Button>();
            int becauseCSisShit = i;
            button.onClick.AddListener(() => {
                click.Play();
                buyUpgrade(becauseCSisShit);
            });
        }

        UpdateFpS();

        m_feels += m_state.CalculateIdle(m_fps);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        m_feels += m_fps * Time.fixedDeltaTime;
        disp_feels.text = ((int)m_feels).ToString();
    }

    void setNbMovie(int idx)
    {     
        if (m_feels < m_data.movies[idx].newPrice)
            return;
        m_feels -= m_data.movies[idx].newPrice;
        m_data.movies[idx].nbPurchased += 1;
        m_data.movies[idx].newPrice = (long)(m_data.movies[idx].priceFirst * Mathf.Pow(1.15f, (float)m_data.movies[idx].nbPurchased));

        disp_movies[idx].transform.GetChild(2).GetComponent<Text>().text = m_data.movies[idx].newPrice.ToString() + " F";
        disp_movies[idx].transform.GetChild(3).GetComponent<Text>().text = m_data.movies[idx].nbPurchased.ToString();

        UpdateFpS();
    }

    bool buyUpgrade(int nb)
    {
        int nbUpgrade = -1;
        int tmp = 0;
        for (int i = 0; i < m_data.upgrades.Count; ++i)
        {
            if (!m_data.upgrades[i].isPurchased)
            {
                if (tmp == nb)
                {
                    nbUpgrade = i;
                    break;
                }
                ++tmp;
            }
        }
        if (nbUpgrade == -1 || m_data.upgrades[nbUpgrade].isPurchased)
            return false;

        if (m_feels < m_data.upgrades[nbUpgrade].Price)
            return false;
        m_feels -= m_data.upgrades[nbUpgrade].Price;
        m_data.upgrades[nbUpgrade].isPurchased = true;
        applyEffect(m_data.upgrades[nbUpgrade].Effect);

        displayUpgrades(nbUpgrade+1, nb);

        UpdateFpS();

        return true;
    }

    public void displayUpgrades(int nb1 = 0, int nb2 = 0)
    {
        int idx = nb1;
        for (int i = nb2; i < disp_upgrades.Count; ++i)
        {
            if (idx >= m_data.upgrades.Count)
            {
                Destroy(disp_upgrades[i].gameObject);
                continue;
            }
            if (m_data.upgrades[idx].isPurchased)
            {
                ++idx;
                --i;
                continue;
            }

            disp_upgrades[i].transform.GetChild(1).GetComponent<Text>().text = m_data.upgrades[idx].Name.ToUpper();
            disp_upgrades[i].transform.GetChild(2).GetComponent<Text>().text = m_data.upgrades[idx].Description.ToUpper();
            disp_upgrades[i].transform.GetChild(3).GetComponent<Text>().text = m_data.upgrades[idx].Price.ToString();

            ++idx;
        }
    }

    public void applyEffect(string effect)
    {
        switch (effect[0])
        {
            case(':'):
                if (effect[1] != '(') newFeeling( (int)Char.GetNumericValue(effect[1]) );
                else StartCoroutine(EndGame());
                break;
            case('x'):
                int idx = (int)Char.GetNumericValue(effect[1]) - 1;
                m_data.movies[idx].multiplier = (int)(m_data.movies[idx].multiplier * 2);
                break;
            case('+'):
                m_feelPerClick += int.Parse(effect);
                break;
            default:
                break;
        }
    }

    public void UpdateFpS()
    {
        m_fps = 0;
        m_data.movies.ForEach(movie => {
            m_fps += movie.initValue * movie.nbPurchased * movie.multiplier;
        });
        disp_fps.text = m_init_fps + Math.Round(m_fps, 1).ToString();
    }

    public void newFeeling(int feeling)
    {
        m_idxFeeling = feeling;
        m_smile.SetSmile(m_idxFeeling);
        learn.Play();
    }
    public IEnumerator EndGame()
    {
        m_smile.SetSmile(m_data.feelings.Count);
        yield return new WaitForSeconds(2.0f);
        ResetGame();
        SceneManager.LoadScene("Ending");
    }

    public void OnScreenClick()
    {
        click.Play();
        m_smile.SetSmileOnClick(m_idxFeeling);
        m_feels += m_feelPerClick;
    }

    public void ResetGame()
    {
        m_feels = 0;
        m_feelPerClick = 1;

        for (int i = 0; i < m_data.movies.Count; ++i)
            m_data.movies[i].Reset();
        for (int i = 0; i < m_data.upgrades.Count; ++i)
            m_data.upgrades[i].Reset();

        m_state.Save(this);
    }
    public void ResetScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnApplicationQuit()
    {
        m_state.Save(this);
    }
}
