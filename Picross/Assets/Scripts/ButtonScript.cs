using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    LevelSetScript lvlsetscript;
    private int[,] board = { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
    private int[,] finishedboard;

    private void Start()
    {
        lvlsetscript = GameObject.Find("LevelObject").GetComponent<LevelSetScript>();
        //finishedboard = lvlsetscript.ReturnCurrentLevelBoard();
    }
    public void CheckCell()
    {
        finishedboard = lvlsetscript.ReturnCurrentLevelBoard();
        GameObject cell = EventSystem.current.currentSelectedGameObject;
        string cellnumber = cell.name;
        int row= (int)char.GetNumericValue(cellnumber[7]);
        int col= (int)char.GetNumericValue(cellnumber[9]);
        if (board[row - 1, col - 1] == 0)
        {
            cell.GetComponent<Image>().color = Color.black;
            board[row - 1, col - 1] = 1;
        }
        else if (board[row -1 ,col - 1] == 1)
        {
            cell.GetComponent<Image>().color = Color.white;
            board[row - 1, col - 1] = 0;
        }
        CheckForEquality();
    }

    private void CheckForEquality()
    {
        bool areEqual = true;
        for(int i=0;i<5;i++)
        {
            for(int j=0;j<5;j++)
            {
                
                if (board[i, j] != finishedboard[i, j])
                {
                    areEqual = false;
                    break;
                }
            }
        }
        if (areEqual)
        {
            //Level Complete
            lvlsetscript.LevelComplete();
        }
    }

    public void ResetBoard()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                board[i, j] = 0;
            }
        }
    }

}
