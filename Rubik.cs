using System;
using System.Text;
using System.Collections.Generic;

namespace KmaOoad18.Assignments.Week2
{
    public enum Color { Red, White, Blue, Orange, Yellow, Green }
    //набір можливих значень
    //сусідні кольори
    //         синій
    //червоний жовтий   білий
    //         помаранч
    //         зелений

    // To help, here is the "adjacency" lists for each Rubik's face
    // Color.Red = [ Color.White, Color.Blue, Color.Yellow, Color.Green ]
    // Color.Orange = [ Color.Green, Color.Yellow, Color.Blue, Color.White ]

    // Color.White = [ Color.Blue, Color.Red, Color.Orange, Color.Green ]
    // Color.Yellow = [ Color.Green, Color.Orange, Color.Red, Color.Blue ]

    // Color.Blue = [ Color.Red, Color.White, Color.Orange, Color.Yellow ]
    // Color.Green = [ Color.Yellow, Color.Orange, Color.White, Color.Red ]

    public partial class Rubik
    {
        static public Random rng = new Random();

        private Part[] Parts = new Part[6];

        private Rubik() { }

        public static Rubik Init()
        {
            var r = new Rubik();
            //Init Rubic here

            foreach (Color clr in Enum.GetValues(typeof(Color)))
                r.Parts[(int)clr] = new Part(clr);

            return r;
        }

        public Rubik Scramble()
        {
            //Randomize Rubic here
            var turnsNum = rng.Next(1, 128);

            for (int i = 0; i < turnsNum; i++)
            {
                var clockwise = rng.Next() % 2 == 0;
                var clr = (Color)rng.Next(0, 6);

                if (clockwise)
                    RotateClockwise(clr);
                else
                    RotateCounterClockwise(clr);
            }

            if (Solved())
                Scramble();

            return this;
        }

        public bool Solved()
        {
            // Put your check for solution here
            foreach (var prt in Parts)
                if (!prt.IsSameColor())
                    return false;

            return true;
        }

        public string Display(Color face) => Parts[(int)face].ToString();

        public string Display() => ToString();

        public Rubik RotateClockwise(Color face)
        {
            // Implement clockwise rotation for given face
            var faceVal = (int)face;

            if (!Parts[faceVal].IsSameColor())
                Parts[faceVal].RotateClockwise();

            var adjVals = getAdjacent(face);
            var length = adjVals.Length;

            var lastVal = Parts[adjVals[length - 1]].GetLine(length - 1);

            for (int i = length - 2; i >= 0; --i)
            {
                var line = Parts[adjVals[i]].GetLine(i);

                Parts[adjVals[i + 1]].SetLine(i + 1, line);
            }

            Parts[adjVals[0]].SetLine(0, lastVal);

            return this;
        }

        public Rubik RotateCounterClockwise(Color face)
        {
            // Implement counter-clockwise rotation for given face
            var faceVal = (int)face;

            if (!Parts[faceVal].IsSameColor())
                Parts[faceVal].RotateCounterClockwise();

            var adjVals = getAdjacent(face);
            var length = adjVals.Length;

            var firstVal = Parts[adjVals[0]].GetLine(0);

            for (int i = 0; i < length - 1; ++i)
            {
                var line = Parts[adjVals[i + 1]].GetLine(i + 1);

                Parts[adjVals[i]].SetLine(i, line);
            }

            Parts[adjVals[length - 1]].SetLine(length - 1, firstVal);

            return this;
        }

        private int[] getAdjacent(Color face)
        {
            var faceVal = (int)face;
            var adj = new List<int>();

            for (int i = (faceVal + 1) % 6; i != faceVal; i++)
            {
                if (i % 3 != faceVal % 3)
                    adj.Add(i);
                if (i > 4)
                    i = -1;
            }

            var adjacent = adj.ToArray();

            if (faceVal % 2 != 0)
                Array.Reverse(adjacent);

            return adjacent;
        }

        public override string ToString()
        {
            var strbldr = new StringBuilder();
            var rotatedBlue   = new Part(Parts[(int)Color.Blue]);
            var rotatedRed    = new Part(Parts[(int)Color.Red]);
            var rotatedOrange = new Part(Parts[(int)Color.Orange]);
            var rotatedYellow = new Part(Parts[(int)Color.Yellow]);

            rotatedBlue.RotateCounterClockwise();
            rotatedRed.RotateClockwise().RotateClockwise();
            rotatedOrange.RotateClockwise();
            rotatedYellow.RotateClockwise();

            Part[] rowSides = {
                rotatedBlue,
                rotatedRed,
                Parts[(int)Color.Green],
                rotatedOrange
            };

            strbldr.Append(DisplayWithSpace(rotatedYellow));
            strbldr.Append(DisplayInRow(rowSides));
            strbldr.Append(DisplayWithSpace(Parts[(int)Color.White]));

            return strbldr.ToString();
        }

        private string DisplayWithSpace(Part prt)
        {
            var strbldr = new StringBuilder();

            var space = "              ";

            foreach (var line in prt.Tiles)
            {
                strbldr.Append(space);

                foreach (var tile in line)
                    strbldr.AppendFormat("{0} ", tile.ToString()[0]);
                strbldr.Append("\n");
            }

            return strbldr.ToString();
        }

        private string DisplayInRow(Part[] allprt)
        {
            var strbldr = new StringBuilder();

            //strbldr.Append("_______________\n");

            for (int i = 0; i < 3; i++)
            {
                foreach (var prt in allprt)
                {
                    foreach (var tile in prt.Tiles[i])
                        strbldr.AppendFormat("{0} ", tile.ToString()[0]);
                    strbldr.Append(" ");
                }

                strbldr.Append('\n');
            }

            //strbldr.Append("_______________\n");

            return strbldr.ToString();
        }
    }
}
