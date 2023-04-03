using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSetScript : MonoBehaviour
{
    private int[,] levelboard;
    private int currentlevel = 0;
    private JSONReaderScript jsonobj;
    private ButtonScript btnobj;
    AudioSource audiosource;
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private List<GameObject> numbertexts;
    [SerializeField] private List<GameObject> cells;
    [SerializeField] private GameObject nxtlvlbutton,finishgamebutton,solutiontext;
    [SerializeField] private List<GameObject> solutions;
    void Start()
    {
        audiosource = GameObject.Find("GameAudioSource").GetComponent<AudioSource>();
        btnobj = GameObject.Find("Canvas").GetComponent<ButtonScript>();
        jsonobj = this.gameObject.GetComponent<JSONReaderScript>();
        currentlevel = 1;
        levelboard = jsonobj.GetLevelBoard(currentlevel);
        Debug.Log("Level:" + currentlevel);
        SetupNumbers(levelboard);
    }

    public int[,] ReturnCurrentLevelBoard()
    {
        return levelboard;
    }

    public void LevelComplete()
    {
        audiosource.PlayOneShot(clips[0]);
        solutions[currentlevel-1].SetActive(true);
        solutiontext.SetActive(true);
        solutiontext.GetComponent<TextMeshProUGUI>().text = solutions[currentlevel - 1].name.ToString();
        StartCoroutine(FadeInSolution(solutions[currentlevel - 1]));
        if(currentlevel < 10)
        {
            currentlevel++;
            nxtlvlbutton.SetActive(true);
        }
        else
        {
            finishgamebutton.SetActive(true);
        }
    }

    public void SetLevel()
    {
        for(int i=0;i<solutions.Count;i++)
        {
            solutions[i].SetActive(false);
        }
        solutiontext.SetActive(false);
        btnobj.ResetBoard();
        nxtlvlbutton.SetActive(false);
        levelboard = jsonobj.GetLevelBoard(currentlevel);
        SetBoardColors();
        SetupNumbers(levelboard);
        Debug.Log("Level:" + currentlevel);
    }

    private void SetBoardColors()
    {
        foreach(GameObject cell in cells)
        {
            cell.GetComponent<Image>().color = Color.white;
        }
    }

    private void SetupNumbers(int[,] levelboard)
    {
        int existingnumbersforrows = 0;
        int[] rownumbers = { 0, 0, 0, 0, 0 };
        int[] columnnumbers = { 0, 0, 0, 0, 0 };
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (levelboard[i, j] == 1)
                {
                    existingnumbersforrows++;
                    rownumbers[i] = existingnumbersforrows;
                    columnnumbers[j]++;
                }
            }
            existingnumbersforrows = 0;
        }
        for (int k=0;k<5;k++)
        {
            numbertexts[k].GetComponent<TextMeshProUGUI>().text = rownumbers[k].ToString();
            numbertexts[k+5].GetComponent<TextMeshProUGUI>().text = columnnumbers[k].ToString();
        }
    }

    private IEnumerator FadeInSolution(GameObject currentobject)
    {
        Image currentimage = currentobject.GetComponent<Image>();
        for(float i=0;i<=1 ; i += Time.deltaTime)
        {
            currentimage.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
