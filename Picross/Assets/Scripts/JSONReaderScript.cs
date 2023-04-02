using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReaderScript : MonoBehaviour
{
    public TextAsset jsonlevels;
    private int[,] finishedboard = { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };

    [System.Serializable]
    public class Level
    {
        public string row1;
        public string row2;
        public string row3;
        public string row4;
        public string row5;

    }

    [System.Serializable]
    public class LevelList
    {
        public Level[] levels;
    }

    public LevelList levelList = new LevelList();
    private void Start()
    {
        /*levelList = JsonUtility.FromJson<LevelList>(jsonlevels.text);
        foreach(Level lvl in levelList.levels)
        {
            //Debug.Log("Found Level: " + lvl.row1 + " " + lvl.row2 + " " + lvl.row3 + " " + lvl.row4 + " " + lvl.row5);
        }*/
    }

    public int[ , ] GetLevelBoard(int level)
    {
        levelList = JsonUtility.FromJson<LevelList>(jsonlevels.text);
        Level lvl = levelList.levels[level -1];
        int[,] board = { {  (int)char.GetNumericValue(lvl.row1[0]) , (int)char.GetNumericValue(lvl.row1[1]),(int)char.GetNumericValue(lvl.row1[2]),(int)char.GetNumericValue(lvl.row1[3]) , (int)char.GetNumericValue(lvl.row1[4]) },
                          { (int)char.GetNumericValue(lvl.row2[0]) , (int)char.GetNumericValue(lvl.row2[1]),(int)char.GetNumericValue(lvl.row2[2]),(int)char.GetNumericValue(lvl.row2[3]) , (int)char.GetNumericValue(lvl.row2[4]) },
                          { (int)char.GetNumericValue(lvl.row3[0]) , (int)char.GetNumericValue(lvl.row3[1]),(int)char.GetNumericValue(lvl.row3[2]),(int)char.GetNumericValue(lvl.row3[3]) , (int)char.GetNumericValue(lvl.row3[4]) },
                          { (int)char.GetNumericValue(lvl.row4[0]) , (int)char.GetNumericValue(lvl.row4[1]),(int)char.GetNumericValue(lvl.row4[2]),(int)char.GetNumericValue(lvl.row4[3]) , (int)char.GetNumericValue(lvl.row4[4]) },
                          { (int)char.GetNumericValue(lvl.row5[0]) , (int)char.GetNumericValue(lvl.row5[1]),(int)char.GetNumericValue(lvl.row5[2]),(int)char.GetNumericValue(lvl.row5[3]) , (int)char.GetNumericValue(lvl.row5[4]) }};
        return board;
    }
}
