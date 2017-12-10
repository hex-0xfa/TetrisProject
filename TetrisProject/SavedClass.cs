using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    /// <summary>
    /// Date saved into the saved file
    /// </summary>
    [Serializable]                               //serialize this object
    class SavedClass
    {
        public int[,] BoardArray;                //the board array representation

        public int ElaspedTime { get; set; }     //the elasped time

        public int CurrentLevel { get; set; }    //the current level
        
        public int LinesCleared { get; set; }    //the lines cleared

        public int Scores { get; set; }          //the score

        public SavedClass(int[,]newBoardArray, int newElaspedTime, int newCurrentLevel, int newLinesCleared, int newScores)  //constructor
        {
            BoardArray = newBoardArray;
            ElaspedTime = newElaspedTime;
            CurrentLevel = newCurrentLevel;
            LinesCleared = newLinesCleared;
            Scores = newScores;
        }
    }
}
