using System;
using NUnit.Framework;

[TestFixture]
public class CalculateDiagonalDifferenceTest()
{
    [Test(ExpectedResult = 0)]
    public int DiagonalDifference()
    {
        var matrix = new List<List<int>>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 },
            new List<int> { 7, 8, 9 }
        };
        return CalculateDiagonalDifference(matrix);
    }
    [Test]
    public void CalculateDiagonalDifference_ForInvalidMatrix()
    {
        var matrix = new List<List<int>>
        {
            new List<int> { 1, 2 },
            new List<int> { 4, 5, 6 }
        };
        Assert.Throws<ArgumentException>(() => CalculateDiagonalDifference(matrix));
    }
}