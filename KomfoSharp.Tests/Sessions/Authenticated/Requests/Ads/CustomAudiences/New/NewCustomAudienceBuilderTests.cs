// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCustomAudienceBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New;
  using NUnit.Framework;

  [TestFixture]
  public class NewCustomAudienceBuilderTests
  {
    [Test]
    public void ShouldCreateExpectedConfiguration()
    {
      // arrange
      var expectedCustomAudience = new CustomAudience
      {
        Name = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString()
      };

      // act
      var newCustomAudienceBuilder = new NewCustomAudienceBuilder();

      var configuration = newCustomAudienceBuilder
        .Name(expectedCustomAudience.Name)
        .Description(expectedCustomAudience.Description)
        .Create();

      // assert
      configuration.Should().NotBeNull();
      configuration.ShouldBeEquivalentTo(expectedCustomAudience);
    }
  }
}