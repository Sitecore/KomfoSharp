// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateUsersInCustomAudienceBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update;
  using NUnit.Framework;

  [TestFixture]
  public class UpdateUsersInCustomAudienceBuilderTests
  {
    [Test]
    public void ShouldCreateConfigurationWithEmailsAndWithoutHashing()
    {
      // arrange
      var expectedConfiguration = new UpdateUsersInCustomAudienceConfiguration
      {
        Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
        DataType = UpdateUsersInCustomAudienceDataType.Emails
      };

      // act
      var updateUsersBuilder = new UpdateUsersInCustomAudienceBuilder();

      var updateUsersConfiguration = updateUsersBuilder
        .Emails(expectedConfiguration.Data)
        .Create();

      // assert
      updateUsersConfiguration.Should().NotBeNull();
      updateUsersConfiguration.ShouldBeEquivalentTo(expectedConfiguration);
    }

    [Test]
    public void ShouldCreateConfigurationWithEmailsAndHashing()
    {
      // arrange
      var expectedConfiguration = new UpdateUsersInCustomAudienceConfiguration
      {
        Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
        DataType = UpdateUsersInCustomAudienceDataType.Emails,
        WithHashing = true
      };

      // act
      var updateUsersBuilder = new UpdateUsersInCustomAudienceBuilder();

      var updateUsersConfiguration = updateUsersBuilder
        .Emails(expectedConfiguration.Data)
        .WithHashing()
        .Create();

      // assert
      updateUsersConfiguration.Should().NotBeNull();
      updateUsersConfiguration.ShouldBeEquivalentTo(expectedConfiguration);
    }

    [Test]
    public void ShouldCreateConfigurationWithPhoneNumbersAndWithoutHashing()
    {
      // arrange
      var expectedConfiguration = new UpdateUsersInCustomAudienceConfiguration
      {
        Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
        DataType = UpdateUsersInCustomAudienceDataType.PhoneNumbers
      };

      // act
      var updateUsersBuilder = new UpdateUsersInCustomAudienceBuilder();

      var updateUsersConfiguration = updateUsersBuilder
        .PhoneNumbers(expectedConfiguration.Data)
        .Create();

      // assert
      updateUsersConfiguration.Should().NotBeNull();
      updateUsersConfiguration.ShouldBeEquivalentTo(expectedConfiguration);
    }

    [Test]
    public void ShouldCreateConfigurationWithPhoneNumbersAndHashing()
    {
      // arrange
      var expectedConfiguration = new UpdateUsersInCustomAudienceConfiguration
      {
        Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
        DataType = UpdateUsersInCustomAudienceDataType.PhoneNumbers,
        WithHashing = true
      };

      // act
      var updateUsersBuilder = new UpdateUsersInCustomAudienceBuilder();

      var updateUsersConfiguration = updateUsersBuilder
        .PhoneNumbers(expectedConfiguration.Data)
        .WithHashing()
        .Create();

      // assert
      updateUsersConfiguration.Should().NotBeNull();
      updateUsersConfiguration.ShouldBeEquivalentTo(expectedConfiguration);
    }

    [Test]
    public void ShouldCreateConfigurationWithFacebookIdsAndFacebookApplicationIds()
    {
      // arrange
      var expectedConfiguration = new UpdateUsersInCustomAudienceConfiguration
      {
        Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
        FacebookApplicationsIds = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
        DataType = UpdateUsersInCustomAudienceDataType.FacebookIds
      };

      // act
      var updateUsersBuilder = new UpdateUsersInCustomAudienceBuilder();

      var updateUsersConfiguration = updateUsersBuilder
        .FacebookIds(expectedConfiguration.Data)
        .FacebookApplicationsIds(expectedConfiguration.FacebookApplicationsIds)
        .Create();

      // assert
      updateUsersConfiguration.Should().NotBeNull();
      updateUsersConfiguration.ShouldBeEquivalentTo(expectedConfiguration);
    }
  }
}