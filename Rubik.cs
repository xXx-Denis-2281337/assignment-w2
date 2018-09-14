using System.Collections.Generic;
using System.Linq;

namespace KmaOoad18.Assignments.Week2
{
    public enum Color { Red, White, Blue, Orange, Yellow, Green }

    // To help, here is the "adjacency" lists for each Rubik's face
    // Color.Red = [ Color.White, Color.Blue, Color.Yellow, Color.Green ]
    // Color.Orange = [ Color.Green, Color.Yellow, Color.Blue, Color.White ]

    // Color.White = [ Color.Blue, Color.Red, Color.Orange, Color.Green ]
    // Color.Yellow = [ Color.Green, Color.Orange, Color.Red, Color.Blue ]

    // Color.Blue = [ Color.Red, Color.White, Color.Orange, Color.Yellow ]
    // Color.Green = [ Color.Yellow, Color.Orange, Color.White, Color.Red ]



    public class Rubik
    {
        private Rubik() { }

        public static Rubik Init()
        {
            var r = new Rubik();

            // Init Rubik here            

            return r;
        }

        public Rubik Scramble()
        {
            // Randomize Rubik here

            return this;
        }

        public bool Solved()
        {
            // Put your check for solution here

            return false;
        }

        public string Display(Color face)
        {
            // Return string of colors for given face

            return string.Empty;
        }

        public string Display()
        {
            // Print string of colors of all faces

            return string.Empty;
        }

        public Rubik RotateClockwise(Color face)
        {
            // Implement clockwise rotation for given face

            return this;
        }

        public Rubik RotateCounterClockwise(Color face)
        {
            // Implement counter-clockwise rotation for given face

            return this;
        }
    }

}
