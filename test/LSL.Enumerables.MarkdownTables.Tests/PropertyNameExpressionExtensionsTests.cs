using System;
using System.Linq.Expressions;
using FluentAssertions;

namespace LSL.Enumerables.MarkdownTables.Tests;

public class PropertyNameExpressionExtensionsTests
{
    [Test]
    public void GivenAValidPropertyExpression_ItSHouldReturnTheName()
    {
        RunIt(c => c.Description).Should().Be("Description");
    }

    [Test]
    public void GivenAMethodCallExpression_ItShouldThrowAnArgumentException()
    {
        new Action(() => RunIt(c => c.GetANumber()))
            .Should()
            .ThrowExactly<ArgumentException>();
    }

    [Test]
    public void GivenAFieldAccessCallExpression_ItShouldThrowAnArgumentException()
    {
        new Action(() => RunIt(c => c.AField))
            .Should()
            .ThrowExactly<ArgumentException>();
    }

    [Test]
    public void GivenANonTopLevelPropertyExpression_ItShouldThrowAnArgumentException()
    {
        new Action(() => RunIt(c => c.Other.Name))
            .Should()
            .ThrowExactly<ArgumentException>();
    }    

    private static string RunIt(Expression<Func<InclusionAndExclusion, object>> expression) => expression.GetPropertyName();
}