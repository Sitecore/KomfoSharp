// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomAudiencesInCampaignBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update;
  using NUnit.Framework;

  [TestFixture]
  public class UpdateCustomAudiencesInCampaignBuilderTests
  {
    [Test]
    public void ShouldCreateConfigurationWithPassedData()
    {
      // arrange
      var customAudienceId = Guid.NewGuid().ToString();

      // act
      var updateCustomAudiencesBuilder = new UpdateCustomAudiencesInCampaignBuilder();

      var updateCustomAudiencesConfiguration = updateCustomAudiencesBuilder
        .CustomAudienceId(customAudienceId)
        .Create();

      // assert
      updateCustomAudiencesConfiguration.Should().NotBeNull();
      updateCustomAudiencesConfiguration.CustomAudienceId.Should().Be(customAudienceId);
    }
  }
}