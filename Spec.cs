using Xbehave;
using FluentAssertions;
using Xunit;
using FsCheck;
using FsCheck.Xunit;
using System.Collections.Generic;

namespace KmaOoad18.Assignments.Week2
{
    public class Spec
    {
        [Property]
        public bool Reverse_Rotations(List<Color> rotations)
        {
            var rubik = Rubik.Init();

            foreach (var r in rotations)
                rubik.RotateClockwise(r);

            var reverseRotations = new List<Color>(rotations);
            reverseRotations.Reverse();

            foreach (var rr in reverseRotations)
                rubik.RotateCounterClockwise(rr);

            return rubik.Solved();
        }

        [Scenario]
        public void Init()
        {            
            var rubik = Rubik.Init();
            rubik.Should().Match<Rubik>(r => r.Solved());
        }

        [Property]
        public bool Idempotent_Rotations(Color c)
        {
            var rubik = Rubik.Init();

            rubik.RotateClockwise(c).RotateClockwise(c).RotateClockwise(c).RotateClockwise(c);

            return rubik.Solved();
        }
    }
}