using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    [Serializable]
    class SavedClass
    {
        public int[,] BoardArray;

        public int ElaspedTime { get; set; }

        public int CurrentLevel { get; set; }
        
        public int LinesCleared { get; set; }

        public int Scores { get; set; }

        public SavedClass(int[,]newBoardArray, int newElaspedTime, int newCurrentLevel, int newLinesCleared, int newScores)
        {
            BoardArray = newBoardArray;
            ElaspedTime = newElaspedTime;
            CurrentLevel = newCurrentLevel;
            LinesCleared = newLinesCleared;
            Scores = newScores;
        }
    }
}
