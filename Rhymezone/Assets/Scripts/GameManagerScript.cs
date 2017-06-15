using UnityEngine;
using UnityEngine.UI;
public class GameManagerScript : MonoBehaviour
{

    public GameObject[] m_lowerPrefabs;
    public GameObject[] m_middlePrefabs;
    public GameObject[] m_hardPrefabs;
    public Text[] m_ScreenTexts;
    public Image[] m_ScreenImages;
    public Text m_ScoreText;
    public Text m_HitPercentText;
    public Text m_MultiplerText;
    public Text m_HighScoreTXT;

    public Image m_SongCompletionBar;
    public ParticleSystem m_BackgroundParticles;

    private int m_ScoreMultiplier = 1;
    public int m_PlayerScore = 0;
    public int m_VisualScore;
    public GameObject m_PerfecthitPrefab;
    public GameObject m_NewHighscore;
    public GameObject m_EndScreen;

    private bool m_GameComplete = false;
    private float m_ObjectDelay = 1.0f;
    private double m_hits = 1;
    private double m_OverallHits = 1;
    private AudioSource m_MyAudi;

    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
    public void MissHit()
    {
        m_OverallHits += 1;
        resetMultipler();
    }
    public void GoodHit()
    {
        m_hits += 1;
        m_OverallHits += 1;
        m_ScoreMultiplier += 1;
    }

    public void PerfectHit()
    {
        Instantiate(m_PerfecthitPrefab, transform.position, transform.rotation);
    }

    void Awake()
    {
        m_VisualScore = m_PlayerScore;
        m_MyAudi = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_MyAudi.PlayDelayed(0.1f);
    }

    private void Update()
    {
       
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                RaycastHit2D l_hitObject = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), -Vector2.zero);
                if (l_hitObject.collider != null)
                {
                    if (l_hitObject.collider.gameObject.GetComponent<GameObjectScript>() == true)
                    {
                        l_hitObject.collider.gameObject.GetComponent<GameObjectScript>().ObjectHit();
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            RaycastHit2D l_hitObject = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero);
            if (l_hitObject.collider != null)
            {
                if (l_hitObject.collider.gameObject.GetComponent<GameObjectScript>() == true)
                {
                    l_hitObject.collider.gameObject.GetComponent<GameObjectScript>().ObjectHit();
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_MyAudi.isPlaying == true)
        {
            if ((m_MyAudi.clip.length - m_MyAudi.time) <= 0.5f)
            {
                m_GameComplete = true;
            }
        }

        if (m_PlayerScore >= 1000000)
        {
            m_PlayerScore = 999999;
            m_VisualScore = m_PlayerScore;
        }

        if (m_GameComplete == false)
        {
            m_ObjectDelay -= Time.deltaTime;
            if (m_ObjectDelay <= 0.0f)
            {
                NextPrefab();
                Visualizer();
            }           
            UpdateUI();
        }
        else
        {
            m_EndScreen.SetActive(true);
            if(m_PlayerScore > PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", m_PlayerScore);
                m_NewHighscore.SetActive(true);
            }
            m_HighScoreTXT.text = "" + PlayerPrefs.GetInt("Highscore");
        }
        
    }

    private void UpdateUI()
    {
        //mutiplier display 
        m_MultiplerText.text = "" + m_ScoreMultiplier;

        //score updating
        m_ScoreText.text = "" + m_VisualScore;
        if (m_VisualScore < m_PlayerScore && m_VisualScore != m_PlayerScore)
        {
            if ((m_VisualScore + 1000) < m_PlayerScore || (m_VisualScore + 1000) == m_PlayerScore)
            {
                m_VisualScore += 100;
                m_ScoreText.text = "" + m_VisualScore;
            }
            if ((m_VisualScore + 100) < m_PlayerScore || (m_VisualScore + 100) == m_PlayerScore)
            {
                m_VisualScore += 15;
                m_ScoreText.text = "" + m_VisualScore;
            }

            if ((m_VisualScore + 1) < m_PlayerScore || (m_VisualScore + 1) == m_PlayerScore)
            {
                m_VisualScore += 1;
                m_ScoreText.text = "" + m_VisualScore;
            }
            
        }
        //completion bar
        if (m_MyAudi.isPlaying == true)
        {          
            m_SongCompletionBar.fillAmount = m_MyAudi.time / m_MyAudi.clip.length;
            //Debug.Log(m_MyAudi.time / m_MyAudi.clip.length * 100);
        }

        //hitpercent

        int l_hitpercent = Mathf.RoundToInt((float)(m_hits / m_OverallHits) * 100);
        m_HitPercentText.text = l_hitpercent + "%";
 
    }

    private void NextPrefab()
    {
        //select a prefab based on volume level
       
        //0.3 hard
        //0.2 med
        //0.1 easy
        float l_audiolevel = GetComponent<MusicManagerScript>().ReturnClipLoudness();
        GameObject l_NewPrefab;
        if (l_audiolevel > 0.22f)
        {
            l_NewPrefab = m_hardPrefabs[Random.Range(0, m_hardPrefabs.Length)];
        }
        else if(l_audiolevel > 0.12f)
        {
            l_NewPrefab = m_middlePrefabs[Random.Range(0, m_hardPrefabs.Length)];
        }
        else
        {
            l_NewPrefab = m_lowerPrefabs[Random.Range(0, m_hardPrefabs.Length)];
        }

        GameObject l_SpawnedPrefab = Instantiate(l_NewPrefab, transform);

        m_ObjectDelay = l_SpawnedPrefab.GetComponent<GameObjectScript>().m_PrefabDelayTime;
    }

    //take the current volume level choose a colour to adjust to
    private void Visualizer()
    {
        Color l_newCol = new Color(Random.value, Random.value, Random.value);
        if(l_newCol.r < 0.2f)
        {
            l_newCol.r += 0.4f;
        }
        if (l_newCol.g < 0.4f)
        {
            l_newCol.g += 0.4f;
        }
        if (l_newCol.b < 0.2f)
        {
            l_newCol.b += 0.4f;
        }
        foreach (Text l_txt in m_ScreenTexts)
        {
            l_txt.color = l_newCol;
        }
        foreach (Image l_img in m_ScreenImages)
        {
            l_img.color = l_newCol;
        }

        l_newCol.a = 0.4f;
        m_BackgroundParticles.startColor = l_newCol;
       
        
    }

    public void addScore(int _addedScore)
    {
        m_PlayerScore += (_addedScore * m_ScoreMultiplier);
    }

    public void resetMultipler()
    {
        m_ScoreMultiplier = 1;
    }
}
