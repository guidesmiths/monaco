﻿using FluentAssertions;
using System;
using System.Diagnostics.CodeAnalysis;
using Monaco.Template.Common.Domain.Model;
using Monaco.Template.Common.Tests.Factories;
using Xunit;

namespace Monaco.Template.Common.Domain.Tests;

[ExcludeFromCodeCoverage]
public class DomainEventTests
{
    [Trait("Common Domain Entities", "Domain Event Entity")]
    [Theory(DisplayName = "New domain event succeeds")]
    [AnonymousData]
    public void NewEntityWithoutParametersSucceeds(DomainEvent sut)
    {
        sut.DateOccurred.Should().BeCloseTo(DateTime.UtcNow, new TimeSpan(0,0,5));
    }
}