// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCampaignBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New;
  using NUnit.Framework;

  [TestFixture]
  public class NewCampaignBuilderTests
  {
    [Test]
    public void ShouldCreateConfigurationWithoutOptionalData()
    {
      // arrange
      var expectedNewCampaign = new Campaign
      {
        ExtCampaignId = Guid.NewGuid().ToString(),
        ExtCampaignKey = Guid.NewGuid().ToString()
      };

      // act
      var newCampaignBuilder = new NewCampaignBuilder();

      var newCampaignConfiguration = newCampaignBuilder
        .ExternalCampaignKey(expectedNewCampaign.ExtCampaignKey)
        .ExternalCampaignId(expectedNewCampaign.ExtCampaignId)
        .Create();

      // assert
      newCampaignConfiguration.Should().NotBeNull();
      newCampaignConfiguration.ShouldBeEquivalentTo(expectedNewCampaign);
    }

    [Test]
    public void ShouldCreateConfigurationWithOptionalData()
    {
      // arrange
      var expectedNewCampaign = new Campaign
      {
        ExtCampaignId = Guid.NewGuid().ToString(),
        ExtCampaignKey = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString()
      };

      // act
      var newCampaignBuilder = new NewCampaignBuilder();

      var newCampaignConfiguration = newCampaignBuilder
        .ExternalCampaignKey(expectedNewCampaign.ExtCampaignKey)
        .ExternalCampaignId(expectedNewCampaign.ExtCampaignId)
        .Name(expectedNewCampaign.Name)
        .Description(expectedNewCampaign.Description)
        .Create();

      // assert
      newCampaignConfiguration.Should().NotBeNull();
      newCampaignConfiguration.ShouldBeEquivalentTo(expectedNewCampaign);
    }
  }
}