using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    static class GameConstants
    {
        public static int rowNumber = 18;          //The number of rows for the game

        public static int columnNumber = 10;       //The number of columns for the game

        public static double baseInterval = 500.00;  //The base interval of falling in ms

        public static int basePointsForRow = 100;  //The basis point earned for one row cleared

        //equation for calculating points: currentLevel * ( basePointsForRow * rowCleared + bonusPointsForExtraRow * ( rowCleared - 1 )

        public static int bonusPointsForExtraRow = 50;  //Bonus Points for extra row cleared

        public static int nextLevelLines = 10;   //The Lines needed to be cleared to enter the next level

        public static double speedIncrease = 0.8;  //The increase of falling speed of the block for each level advanced

        //falling speed = baseInterval * speedIncrease ^ (currentLevel - 1)



    }
}
