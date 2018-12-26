using System;
using System.Text;

namespace KmaOoad18.Assignments.Week2
{
    public partial class Rubik
    {
        private class Part
        {
            public Color[][] Tiles { get; } = new Color[3][];

            private Part()
            {
                for (var i = 0; i < 3; ++i)
                    Tiles[i] = new Color[3];
            }

            public Part(Color clr) : this()
            {
                for (var i = 0; i < 3; ++i)
                    for (var j = 0; j < 3; ++j)
                        Tiles[i][j] = clr;
            }

            public Part(Part prt) : this()
            {
                for (var i = 0; i < 3; ++i)
                    for (var j = 0; j < 3; ++j)
                        Tiles[i][j] = prt.Tiles[i][j];
            }

            public bool IsSameColor()
            {
                for (var i = 0; i < 3; ++i)
                    for (var j = 0; j < 3; ++j)
                        if (Tiles[i][j] != GetColor())
                            return false;

                return true;
            }

            public Part RotateClockwise()
            {
                var clone = new Part(this).Tiles;

                for (var i = 0; i < 3; ++i)
                    for (var j = 0; j < 3; ++j)
                        Tiles[i][j] = clone[2 - j][i];

                return this;
            }

            public Part RotateCounterClockwise()
            {
                var clone = new Part(this).Tiles;

                for (var i = 0; i < 3; ++i)
                    for (var j = 0; j < 3; ++j)
                        Tiles[2 - j][i] = clone[i][j];

                return this;
            }

            public Color[] GetLine(int index)
            {
                if (index > 3 || index < 0)
                    throw new ArgumentOutOfRangeException("Between 0 and 3!");

                if (index < 2)
                    return Tiles[index * 2];
                else
                {
                    var line = new Color[3];

                    for (int i = 0; i < 3; i++)
                        line[i] = Tiles[i][index % 3];

                    return line;
                }
            }

            public Part SetLine(int index, Color[] line)
            {
                if (index > 3 || index < 0)
                    throw new ArgumentOutOfRangeException("Between 0 and 3!!!");

                if (index < 2)
                    Tiles[index * 2] = line;
                else
                {
                    for (int i = 0; i < 3; i++)
                        Tiles[i][index % 3] = line[i];
                }

                return this;
            }

            public Color GetColor() => Tiles[1][1];

            public override String ToString()
            {
                var sb = new StringBuilder(" _____\n");

                for (var i = 0; i < 3; ++i)
                {
                    for (var j = 0; j < 3; ++j)
                        sb.AppendFormat("|{0}", Tiles[i][j].ToString()[0]);

                    sb.Append("|\n");
                }

                sb.Append(" -----");

                return sb.ToString();
            }
        }
    }
}
