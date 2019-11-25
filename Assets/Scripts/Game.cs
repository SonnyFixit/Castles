using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


    public enum GameMode
    {
        idle,
        playing,
        levelEnd,
    }

public class Game : MonoBehaviour
{
    static private Game G;

    public GameObject Text1;
    public GameObject Text2;

    public Text uitLevel;
    public Text uitShots;
    public Text Button;
    public Vector3 castleP;
    public GameObject[] castles;

    public int levelNumber;
    public int levelMax;
    public int shotsFired;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";


    void Start()
    {
        G = this;

        levelNumber = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject temp in gos)
        {
            Destroy(temp);
        }

        castle = Instantiate<GameObject>(castles[levelNumber]);
        castle.transform.position = castleP;
        shotsFired = 0;

        SwitchView("Show both");

        Target.hitConfirm = false;

        UpdateGUI();

        mode = GameMode.playing;



    }

    void UpdateGUI()
    {
        uitLevel.text = "Poziom: " + (levelNumber + 1) + " z " + levelMax;
        uitShots.text = "Strzały: " + shotsFired;

      
    }

    void Update()
    {
        UpdateGUI();

        if ((mode == GameMode.playing) && Target.hitConfirm)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show both");
            Invoke("NextLevel", 2f);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void NextLevel()
    {
        levelNumber++;
        if (levelNumber == levelMax)
        {
            levelNumber = 0;
        }

        StartLevel();
    }


    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = Button.text;
        }

        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                Follow.C = null;
                Button.text = "Pokaż wszystko";
                break;

            case "Show Castle":
                Follow.C = G.castle;
                Button.text = "Pokaż wszystko";
                break;

            case "Show Both":
                Follow.C = GameObject.Find("BothView");
                Button.text = "Pokaż procę";
                break;
        }
    }

    public static void ShotFired()
    {
        G.shotsFired++;
    }
}
    
    
    



