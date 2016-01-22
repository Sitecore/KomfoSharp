// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticatedSessionTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Net;
  using System.Threading.Tasks;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Endpoints;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Provider;
  using KomfoSharp.Sessions.Authenticated;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class AuthenticatedSessionTests
  {
    #region twitter/followers/metrics

    [Test]
    public async void ShouldExecuteMetricsRequestWithExpectedParameters()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var metricsConfiguration = new MetricsRequestConfiguration
      {
        TwitterHandles = new List<string>
        {
          "twh1", "twh2", "twh3"
        },
        Fields = MetricFields.Channel | MetricFields.Engagement | MetricFields.RequestHandle
      };

      var expectedMetrics = new List<Metric> { new Metric(), new Metric() };

      komfoProvider
        .RetrieveMetricsAsync(sessionConfiguration.Token.AccessToken, metricsConfiguration.TwitterHandles, metricsConfiguration.Fields)
        .Returns(Task.FromResult(expectedMetrics.AsEnumerable()));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationFactory, sessionConfiguration, komfoProvider))
      {
        var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
          .TwitterHandles(metricsConfiguration.TwitterHandles)
          .Fields(metricsConfiguration.Fields)
          .Create();

        await komfoSession.ExecuteAsync(metricsRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RetrieveMetricsAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(metricsConfiguration.TwitterHandles),
            Arg.Is(metricsConfiguration.Fields));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.ShouldBeEquivalentTo(expectedMetrics);
        });
      }
    }

    #endregion

    #region twitter/followers/stream

    [Test]
    public async void ShouldExecuteStreamRequestWithExpectedParameters()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var config = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15)
        },
        EndpointsConfiguration = new EndpointsConfiguration(
          new List<EndpointBase>
          {
            new StreamEndpoint
            {
              MaxResultsPerCall = 5
            }
          },
          new Uri("https://connect.komfo.com"))
      };

      configurationFactory.GetConfiguration().Returns(config);
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var streamConfiguration = new StreamRequestConfiguration
      {
        TwitterHandles = new List<string>
        {
          "twh1", "twh2", "twh3"
        },
        Fields = TweetFields.Channel | TweetFields.Text | TweetFields.RequestHandle,
        Since = DateTime.UtcNow.AddMonths(-1),
        Until = DateTime.UtcNow.AddHours(-1)
      };

      var expectedTweets = new List<Tweet> { new Tweet(), new Tweet() };

      komfoProvider
        .RetrieveTweetsAsync(sessionConfiguration.Token.AccessToken, streamConfiguration.TwitterHandles, streamConfiguration.Since, streamConfiguration.Until, streamConfiguration.Fields)
        .Returns(Task.FromResult(expectedTweets.AsEnumerable()));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationFactory, sessionConfiguration, komfoProvider))
      {
        var streamRequest = komfoSession.Requests.Twitter.Followers.Stream
          .TwitterHandles(streamConfiguration.TwitterHandles)
          .Fields(streamConfiguration.Fields)
          .Since(streamConfiguration.Since.Value)
          .Until(streamConfiguration.Until.Value)
          .Create();

        await komfoSession.ExecuteAsync(streamRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RetrieveTweetsAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(streamConfiguration.TwitterHandles),
            Arg.Is(streamConfiguration.Since),
            Arg.Is(streamConfiguration.Until),
            Arg.Is(streamConfiguration.Fields));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.ShouldBeEquivalentTo(expectedTweets);
        });
      }
    }

    [Test]
    public async void ShouldCreateNextStreamRequest()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var config = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15)
        },
        EndpointsConfiguration = new EndpointsConfiguration(
          new List<EndpointBase>
          {
            new StreamEndpoint
            {
              MaxResultsPerCall = 5
            }
          },
          new Uri("https://connect.komfo.com"))
      };

      configurationFactory.GetConfiguration().Returns(config);
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromMilliseconds(1)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var utcNow = DateTime.UtcNow;
      var since = utcNow.AddMinutes(-20);
      var until = utcNow.AddMinutes(-1);


      komfoProvider
        .RetrieveTweetsAsync(sessionConfiguration.Token.AccessToken, Arg.Any<IEnumerable<string>>(), since, until)
        .Returns(Task.FromResult(new List<Tweet>
        {
          new Tweet { GatheredTime = utcNow.AddMinutes(-2) },
          new Tweet { GatheredTime = utcNow.AddMinutes(-3) },
          new Tweet { GatheredTime = utcNow.AddMinutes(-4) },
          new Tweet { GatheredTime = utcNow.AddMinutes(-5) },
          new Tweet { GatheredTime = utcNow.AddMinutes(-6) }
        }.AsEnumerable()));

      komfoProvider
        .RetrieveTweetsAsync(sessionConfiguration.Token.AccessToken, Arg.Any<IEnumerable<string>>(), since, utcNow.AddMinutes(-6).AddSeconds(-1))
        .Returns(Task.FromResult(new List<Tweet>
        {
          new Tweet { GatheredTime = utcNow.AddMinutes(-7) },
          new Tweet { GatheredTime = utcNow.AddMinutes(-8) }
        }.AsEnumerable()));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationFactory, sessionConfiguration, komfoProvider))
      {
        var streamRequest = komfoSession.Requests.Twitter.Followers.Stream
          .TwitterHandles(new[] { "twh1", "twh2", "twh3" })
          .Since(since)
          .Until(until)
          .Create();

        var streamResponse = await komfoSession.ExecuteAsync(streamRequest);

        // assert
        streamResponse.Next.Should().NotBeNull();
        streamResponse.Next.Configuration.TwitterHandles.SequenceEqual(streamRequest.Configuration.TwitterHandles).Should().BeTrue();
        streamResponse.Next.Configuration.Fields.ShouldBeEquivalentTo(streamRequest.Configuration.Fields);
        streamResponse.Next.Configuration.Polling.ShouldBeEquivalentTo(streamRequest.Configuration.Polling);
        streamResponse.Next.Configuration.Since.Should().Be(since);
        streamResponse.Next.Configuration.Until.Should().Be(utcNow.AddMinutes(-6).AddSeconds(-1));

        var nextResponse = await komfoSession.ExecuteAsync(streamResponse.Next);
        nextResponse.Data.Should().NotBeNull();
        nextResponse.Next.Should().BeNull();
      }
    }

    #endregion

    #region ads/customaudiences

    [Test]
    public async void ShouldExecuteCustomAudiencesRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var expectedCustomAudiences = new List<CustomAudience> { new CustomAudience(), new CustomAudience() };

      komfoProvider
        .RetrieveCustomAudiencesAsync(sessionConfiguration.Token.AccessToken)
        .Returns(Task.FromResult(expectedCustomAudiences.AsEnumerable()));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var customAudiencesRequest = komfoSession.Requests.Ads.CustomAudiences.Create();

        await komfoSession.ExecuteAsync(customAudiencesRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RetrieveCustomAudiencesAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.ShouldBeEquivalentTo(expectedCustomAudiences);
        });
      }
    }

    [Test]
    public async void ShouldExecuteNewCustomAudienceRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var newCustomAudienceRequestConfiguration = new NewCustomAudienceRequestConfiguration
      {
        CustomAudience = new CustomAudience
        {
          Name = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString()
        }
      };

      var expectedCustomAudienceId = Guid.NewGuid().ToString();

      komfoProvider
        .CreateCustomAudienceAsync(sessionConfiguration.Token.AccessToken, newCustomAudienceRequestConfiguration.CustomAudience.Name, newCustomAudienceRequestConfiguration.CustomAudience.Description)
        .Returns(Task.FromResult(expectedCustomAudienceId));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var newCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .New(customAudience => customAudience
            .Name(newCustomAudienceRequestConfiguration.CustomAudience.Name)
            .Description(newCustomAudienceRequestConfiguration.CustomAudience.Description))
          .Create();

        await komfoSession.ExecuteAsync(newCustomAudienceRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).CreateCustomAudienceAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(newCustomAudienceRequestConfiguration.CustomAudience.Name),
            Arg.Is(newCustomAudienceRequestConfiguration.CustomAudience.Description));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.CustomAudienceId.Should().Be(expectedCustomAudienceId);
        });
      }
    }

    [Test]
    public async void ShouldExecuteCustomAudienceRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var customAudienceRequestConfiguration = new CustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString()
      };

      var expectedCustomAudience = new CustomAudience
      {
        Id = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString()
      };

      komfoProvider
        .RetrieveCustomAudienceAsync(sessionConfiguration.Token.AccessToken, customAudienceRequestConfiguration.CustomAudienceId)
        .Returns(Task.FromResult(expectedCustomAudience));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var customAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId(customAudienceRequestConfiguration.CustomAudienceId)
          .Create();

        await komfoSession.ExecuteAsync(customAudienceRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RetrieveCustomAudienceAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(customAudienceRequestConfiguration.CustomAudienceId));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.ShouldBeEquivalentTo(expectedCustomAudience);
        });
      }
    }

    [Test]
    public async void ShouldExecuteCustomAudienceStatusRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var customAudienceStatusRequestConfiguration = new CustomAudienceStatusRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString()
      };

      var expectedCustomAudienceStatus = new CustomAudienceStatus
      {
        Id = Guid.NewGuid().ToString(),
        ApproximateSize = 20,
        InvalidEntries = 0,
        UsersDeleted = 2,
        UsersInserted = 3
      };

      komfoProvider
        .RetrieveCustomAudienceStatusAsync(sessionConfiguration.Token.AccessToken, customAudienceStatusRequestConfiguration.CustomAudienceId)
        .Returns(Task.FromResult(expectedCustomAudienceStatus));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var customAudienceStatusRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId(customAudienceStatusRequestConfiguration.CustomAudienceId)
          .Status
          .Create();

        await komfoSession.ExecuteAsync(customAudienceStatusRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RetrieveCustomAudienceStatusAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(customAudienceStatusRequestConfiguration.CustomAudienceId));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.ShouldBeEquivalentTo(expectedCustomAudienceStatus);
        });
      }
    }

    [Test]
    public async void ShouldExecuteAddUsersEmailsRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var updateUsersRequestConfiguration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString(),
        Users =
        {
          Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
          DataType = UpdateUsersInCustomAudienceDataType.Emails,
          WithHashing = true
        }
      };

      var expectedEntriesReceived = updateUsersRequestConfiguration.Users.Data.Count();

      komfoProvider
        .AddEmailsToCustomAudienceAsync(sessionConfiguration.Token.AccessToken, updateUsersRequestConfiguration.CustomAudienceId, updateUsersRequestConfiguration.Users.Data, updateUsersRequestConfiguration.Users.WithHashing)
        .Returns(Task.FromResult(expectedEntriesReceived));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var addUsersRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId(updateUsersRequestConfiguration.CustomAudienceId)
          .Users
          .Add(users => users
            .Emails(updateUsersRequestConfiguration.Users.Data)
            .WithHashing())
          .Create();

        await komfoSession.ExecuteAsync(addUsersRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).AddEmailsToCustomAudienceAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(updateUsersRequestConfiguration.CustomAudienceId),
            Arg.Is(updateUsersRequestConfiguration.Users.Data),
            Arg.Is(updateUsersRequestConfiguration.Users.WithHashing));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.EntriesReceived.Should().Be(expectedEntriesReceived);
        });
      }
    }

    [Test]
    public async void ShouldExecuteAddUsersPhoneNumbersRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var updateUsersRequestConfiguration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString(),
        Users =
        {
          Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
          DataType = UpdateUsersInCustomAudienceDataType.PhoneNumbers,
          WithHashing = true
        }
      };

      var expectedEntriesReceived = updateUsersRequestConfiguration.Users.Data.Count();

      komfoProvider
        .AddPhoneNumbersToCustomAudienceAsync(sessionConfiguration.Token.AccessToken, updateUsersRequestConfiguration.CustomAudienceId, updateUsersRequestConfiguration.Users.Data, updateUsersRequestConfiguration.Users.WithHashing)
        .Returns(Task.FromResult(expectedEntriesReceived));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var addUsersRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId(updateUsersRequestConfiguration.CustomAudienceId)
          .Users
          .Add(users => users
            .PhoneNumbers(updateUsersRequestConfiguration.Users.Data)
            .WithHashing())
          .Create();

        await komfoSession.ExecuteAsync(addUsersRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).AddPhoneNumbersToCustomAudienceAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(updateUsersRequestConfiguration.CustomAudienceId),
            Arg.Is(updateUsersRequestConfiguration.Users.Data),
            Arg.Is(updateUsersRequestConfiguration.Users.WithHashing));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.EntriesReceived.Should().Be(expectedEntriesReceived);
        });
      }
    }

    [Test]
    public async void ShouldExecuteAddUsersFacebookIdsRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var updateUsersRequestConfiguration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString(),
        Users =
        {
          Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
          FacebookApplicationsIds = new[] { Guid.NewGuid().ToString() },
          DataType = UpdateUsersInCustomAudienceDataType.FacebookIds
        }
      };

      var expectedEntriesReceived = updateUsersRequestConfiguration.Users.Data.Count();

      komfoProvider
        .AddFacebookIdsToCustomAudienceAsync(sessionConfiguration.Token.AccessToken, updateUsersRequestConfiguration.CustomAudienceId, updateUsersRequestConfiguration.Users.Data, updateUsersRequestConfiguration.Users.FacebookApplicationsIds)
        .Returns(Task.FromResult(expectedEntriesReceived));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var addUsersRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId(updateUsersRequestConfiguration.CustomAudienceId)
          .Users
          .Add(users => users
            .FacebookIds(updateUsersRequestConfiguration.Users.Data)
            .FacebookApplicationsIds(updateUsersRequestConfiguration.Users.FacebookApplicationsIds))
          .Create();

        await komfoSession.ExecuteAsync(addUsersRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).AddFacebookIdsToCustomAudienceAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(updateUsersRequestConfiguration.CustomAudienceId),
            Arg.Is(updateUsersRequestConfiguration.Users.Data),
            Arg.Is(updateUsersRequestConfiguration.Users.FacebookApplicationsIds));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.EntriesReceived.Should().Be(expectedEntriesReceived);
        });
      }
    }

    [Test]
    public void ShouldThrowExceptionWhenDataTypeIsInvalidInAddUsersRequest()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var addUsersRequest = new AddUsersToCustomAudienceRequest(Guid.NewGuid().ToString(), new UpdateUsersInCustomAudienceConfiguration
        {
          DataType = UpdateUsersInCustomAudienceDataType.Unknown
        });

        komfoSession
          .Awaiting(async session => await session.ExecuteAsync(addUsersRequest))
          .ShouldThrow<ArgumentOutOfRangeException>()
          .And
          .Message.Should().Be(string.Format(CultureInfo.InvariantCulture, "Unsupported data type: {0}\r\nParameter name: addUsersRequest", addUsersRequest.Configuration.Users.DataType));
      }
    }

    [Test]
    public async void ShouldExecuteRemoveUsersEmailsRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var updateUsersRequestConfiguration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString(),
        Users =
        {
          Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
          DataType = UpdateUsersInCustomAudienceDataType.Emails,
          WithHashing = true
        }
      };

      var expectedEntriesReceived = updateUsersRequestConfiguration.Users.Data.Count();

      komfoProvider
        .RemoveEmailsFromCustomAudienceAsync(sessionConfiguration.Token.AccessToken, updateUsersRequestConfiguration.CustomAudienceId, updateUsersRequestConfiguration.Users.Data, updateUsersRequestConfiguration.Users.WithHashing)
        .Returns(Task.FromResult(expectedEntriesReceived));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var removeUsersRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId(updateUsersRequestConfiguration.CustomAudienceId)
          .Users
          .Remove(users => users
            .Emails(updateUsersRequestConfiguration.Users.Data)
            .WithHashing())
          .Create();

        await komfoSession.ExecuteAsync(removeUsersRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RemoveEmailsFromCustomAudienceAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(updateUsersRequestConfiguration.CustomAudienceId),
            Arg.Is(updateUsersRequestConfiguration.Users.Data),
            Arg.Is(updateUsersRequestConfiguration.Users.WithHashing));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.EntriesReceived.Should().Be(expectedEntriesReceived);
        });
      }
    }

    [Test]
    public async void ShouldExecuteRemoveUsersPhoneNumbersRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var updateUsersRequestConfiguration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString(),
        Users =
        {
          Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
          DataType = UpdateUsersInCustomAudienceDataType.PhoneNumbers,
          WithHashing = true
        }
      };

      var expectedEntriesReceived = updateUsersRequestConfiguration.Users.Data.Count();

      komfoProvider
        .RemovePhoneNumbersFromCustomAudienceAsync(sessionConfiguration.Token.AccessToken, updateUsersRequestConfiguration.CustomAudienceId, updateUsersRequestConfiguration.Users.Data, updateUsersRequestConfiguration.Users.WithHashing)
        .Returns(Task.FromResult(expectedEntriesReceived));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var removeUsersRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId(updateUsersRequestConfiguration.CustomAudienceId)
          .Users
          .Remove(users => users
            .PhoneNumbers(updateUsersRequestConfiguration.Users.Data)
            .WithHashing())
          .Create();

        await komfoSession.ExecuteAsync(removeUsersRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RemovePhoneNumbersFromCustomAudienceAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(updateUsersRequestConfiguration.CustomAudienceId),
            Arg.Is(updateUsersRequestConfiguration.Users.Data),
            Arg.Is(updateUsersRequestConfiguration.Users.WithHashing));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.EntriesReceived.Should().Be(expectedEntriesReceived);
        });
      }
    }

    [Test]
    public async void ShouldExecuteRemoveUsersFacebookIdsRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var updateUsersRequestConfiguration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString(),
        Users =
        {
          Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
          FacebookApplicationsIds = new[] { Guid.NewGuid().ToString() },
          DataType = UpdateUsersInCustomAudienceDataType.FacebookIds
        }
      };

      var expectedEntriesReceived = updateUsersRequestConfiguration.Users.Data.Count();

      komfoProvider
        .RemoveFacebookIdsFromCustomAudienceAsync(sessionConfiguration.Token.AccessToken, updateUsersRequestConfiguration.CustomAudienceId, updateUsersRequestConfiguration.Users.Data, updateUsersRequestConfiguration.Users.FacebookApplicationsIds)
        .Returns(Task.FromResult(expectedEntriesReceived));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var removeUsersRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId(updateUsersRequestConfiguration.CustomAudienceId)
          .Users
          .Remove(users => users
            .FacebookIds(updateUsersRequestConfiguration.Users.Data)
            .FacebookApplicationsIds(updateUsersRequestConfiguration.Users.FacebookApplicationsIds))
          .Create();

        await komfoSession.ExecuteAsync(removeUsersRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RemoveFacebookIdsFromCustomAudienceAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(updateUsersRequestConfiguration.CustomAudienceId),
            Arg.Is(updateUsersRequestConfiguration.Users.Data),
            Arg.Is(updateUsersRequestConfiguration.Users.FacebookApplicationsIds));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.EntriesReceived.Should().Be(expectedEntriesReceived);
        });
      }
    }

    [Test]
    public void ShouldThrowExceptionWhenDataTypeIsInvalidInRemoveUsersRequest()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var removeUsersRequest = new RemoveUsersFromCustomAudienceRequest(Guid.NewGuid().ToString(), new UpdateUsersInCustomAudienceConfiguration
        {
          DataType = UpdateUsersInCustomAudienceDataType.Unknown
        });

        komfoSession
          .Awaiting(async session => await session.ExecuteAsync(removeUsersRequest))
          .ShouldThrow<ArgumentOutOfRangeException>()
          .And
          .Message.Should().Be(string.Format(CultureInfo.InvariantCulture, "Unsupported data type: {0}\r\nParameter name: removeUsersRequest", removeUsersRequest.Configuration.Users.DataType));
      }
    }

    #endregion

    #region ads/campaigns

    [Test]
    public async void ShouldExecuteCampaignsRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var expectedCampaigns = new List<Campaign> { new Campaign(), new Campaign() };

      komfoProvider
        .RetrieveCampaignsAsync(sessionConfiguration.Token.AccessToken)
        .Returns(Task.FromResult(expectedCampaigns.AsEnumerable()));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var campaignsRequest = komfoSession.Requests.Ads.Campaigns.Create();

        await komfoSession.ExecuteAsync(campaignsRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RetrieveCampaignsAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.ShouldBeEquivalentTo(expectedCampaigns);
        });
      }
    }

    [Test]
    public async void ShouldExecuteNewCampaignRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var newCampaignRequestConfiguration = new NewCampaignRequestConfiguration
      {
        Campaign =
        {
          ExtCampaignId = Guid.NewGuid().ToString(),
          ExtCampaignKey = Guid.NewGuid().ToString(),
          Name = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString()
        }
      };

      var expectedCampaignId = Guid.NewGuid().ToString();

      komfoProvider
        .CreateCampaignAsync(
          sessionConfiguration.Token.AccessToken, 
          newCampaignRequestConfiguration.Campaign.ExtCampaignId, 
          newCampaignRequestConfiguration.Campaign.ExtCampaignKey, 
          newCampaignRequestConfiguration.Campaign.Name, 
          newCampaignRequestConfiguration.Campaign.Description)
        .Returns(Task.FromResult(expectedCampaignId));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var newCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .New(campaign => campaign
            .ExternalCampaignKey(newCampaignRequestConfiguration.Campaign.ExtCampaignKey)
            .ExternalCampaignId(newCampaignRequestConfiguration.Campaign.ExtCampaignId)
            .Name(newCampaignRequestConfiguration.Campaign.Name)
            .Description(newCampaignRequestConfiguration.Campaign.Description))
          .Create();

        await komfoSession.ExecuteAsync(newCampaignRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).CreateCampaignAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(newCampaignRequestConfiguration.Campaign.ExtCampaignId),
            Arg.Is(newCampaignRequestConfiguration.Campaign.ExtCampaignKey),
            Arg.Is(newCampaignRequestConfiguration.Campaign.Name),
            Arg.Is(newCampaignRequestConfiguration.Campaign.Description));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.CampaignId.Should().Be(expectedCampaignId);
        });
      }
    }

    [Test]
    public async void ShouldExecuteCampaignCustomAudiencesRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var customAudiencesRequestConfiguration = new CampaignCustomAudiencesRequestConfiguration
      {
        CampaignId = Guid.NewGuid().ToString()
      };

      var expectedCustomAudiences = new[] { new CustomAudience(), new CustomAudience() };

      komfoProvider
        .RetrieveCustomAudiencesAsync(
          sessionConfiguration.Token.AccessToken,
          customAudiencesRequestConfiguration.CampaignId)
        .Returns(Task.FromResult(expectedCustomAudiences.AsEnumerable()));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var newCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .CampaignId(customAudiencesRequestConfiguration.CampaignId)
          .CustomAudiences
          .Create();

        await komfoSession.ExecuteAsync(newCampaignRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RetrieveCustomAudiencesAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(customAudiencesRequestConfiguration.CampaignId));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.ShouldBeEquivalentTo(expectedCustomAudiences);
        });
      }
    }

    [Test]
    public async void ShouldExecuteAddCustomAudiencesRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var updateCustomAudiencesRequestConfiguration = new UpdateCustomAudiencesInCampaignRequestConfiguration
      {
        CampaignId = Guid.NewGuid().ToString(),
        CustomAudiences =
        {
          CustomAudienceId = Guid.NewGuid().ToString()
        }
      };

      var expectedCustomAudienceId = updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId;

      komfoProvider
        .AddCustomAudienceToCampaignAsync(
          sessionConfiguration.Token.AccessToken,
          updateCustomAudiencesRequestConfiguration.CampaignId,
          updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId)
        .Returns(Task.FromResult(expectedCustomAudienceId));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var newCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .CampaignId(updateCustomAudiencesRequestConfiguration.CampaignId)
          .CustomAudiences
          .Add(customAudiences => customAudiences.CustomAudienceId(updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId))
          .Create();

        await komfoSession.ExecuteAsync(newCampaignRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).AddCustomAudienceToCampaignAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(updateCustomAudiencesRequestConfiguration.CampaignId),
            Arg.Is(updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.CustomAudienceId.Should().Be(expectedCustomAudienceId);
        });
      }
    }

    [Test]
    public async void ShouldExecuteRemoveCustomAudiencesRequestWithExpectedParameters()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      var updateCustomAudiencesRequestConfiguration = new UpdateCustomAudiencesInCampaignRequestConfiguration
      {
        CampaignId = Guid.NewGuid().ToString(),
        CustomAudiences =
        {
          CustomAudienceId = Guid.NewGuid().ToString()
        }
      };

      var expectedCustomAudienceId = updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId;

      komfoProvider
        .RemoveCustomAudienceFromCampaignAsync(
          sessionConfiguration.Token.AccessToken,
          updateCustomAudiencesRequestConfiguration.CampaignId,
          updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId)
        .Returns(Task.FromResult(expectedCustomAudienceId));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationProvider, sessionConfiguration, komfoProvider))
      {
        var newCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .CampaignId(updateCustomAudiencesRequestConfiguration.CampaignId)
          .CustomAudiences
          .Remove(customAudiences => customAudiences.CustomAudienceId(updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId))
          .Create();

        await komfoSession.ExecuteAsync(newCampaignRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RemoveCustomAudienceFromCampaignAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(updateCustomAudiencesRequestConfiguration.CampaignId),
            Arg.Is(updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId));

          task.Result.Should().NotBeNull();
          task.Result.Data.Should().NotBeNull();
          task.Result.Data.CustomAudienceId.Should().Be(expectedCustomAudienceId);
        });
      }
    }

    #endregion

    #region token renewal

    [Test]
    public async void ShouldRenewTokenInMetricsRequest()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromMilliseconds(1)
        },
        TokenRenewal =
        {
          Enabled = true,
          ClientId = Guid.NewGuid().ToString(),
          ClientSecret = Guid.NewGuid().ToString(),
          Scopes = new[] { Guid.NewGuid().ToString() }
        }
      };

      var newToken = new Token
      {
        AccessToken = Guid.NewGuid().ToString(),
        ExpiresIn = TimeSpan.FromDays(60)
      };

      komfoProvider
        .When(provider => provider.RetrieveMetricsAsync(sessionConfiguration.Token.AccessToken, Arg.Any<IEnumerable<string>>(), Arg.Any<MetricFields>()))
        .Throw(new KomfoProviderException(Guid.NewGuid().ToString(), KomfoStatusCode.InvalidAccessToken, HttpStatusCode.Unauthorized));

      komfoProvider.RetrieveAccessTokenAsync(sessionConfiguration.TokenRenewal.ClientId, sessionConfiguration.TokenRenewal.ClientSecret, sessionConfiguration.TokenRenewal.Scopes).Returns(Task.FromResult(newToken));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationFactory, sessionConfiguration, komfoProvider))
      {
        var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
          .TwitterHandles(new[] { "twh1", "twh2", "twh3" })
          .Create();

        await komfoSession.ExecuteAsync(metricsRequest).ContinueWith(task =>
        {
          task.Result.Should().NotBeNull();

          // assert
          komfoProvider.Received(1).RetrieveMetricsAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            metricsRequest.Configuration.TwitterHandles,
            metricsRequest.Configuration.Fields);

          komfoProvider.Received(1).RetrieveAccessTokenAsync(
            Arg.Is(sessionConfiguration.TokenRenewal.ClientId),
            Arg.Is(sessionConfiguration.TokenRenewal.ClientSecret),
            Arg.Is(sessionConfiguration.TokenRenewal.Scopes));

          komfoProvider.Received(1).RetrieveMetricsAsync(
            Arg.Is(newToken.AccessToken),
            metricsRequest.Configuration.TwitterHandles,
            metricsRequest.Configuration.Fields);

          sessionConfiguration.Token.ShouldBeEquivalentTo(newToken);
        });
      }
    }

    [Test]
    public void ShouldThrowExceptionWhenTokenIsInvalidAndTokenRenewalDisabledInMetricsRequest()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromMilliseconds(1)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      komfoProvider
        .When(provider => provider.RetrieveMetricsAsync(sessionConfiguration.Token.AccessToken, Arg.Any<IEnumerable<string>>(), Arg.Any<MetricFields>()))
        .Throw(new KomfoProviderException(Guid.NewGuid().ToString(), KomfoStatusCode.InvalidAccessToken, HttpStatusCode.Unauthorized));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationFactory, sessionConfiguration, komfoProvider))
      {
        var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
          .TwitterHandles(new[] { "twh1", "twh2", "twh3" })
          .Create();

        komfoSession
          .Awaiting(async session => await session.ExecuteAsync(metricsRequest))
          .ShouldThrow<KomfoProviderException>()
          .And
          .KomfoStatusCode.Should().Be(KomfoStatusCode.InvalidAccessToken);
      }
    }

    #endregion

    #region polling

    [Test]
    public async void ShouldReturnResultWhenPollingSuccessfulInMetricsRequest()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var config = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15)
        }
      };

      configurationFactory.GetConfiguration().Returns(config);
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromMilliseconds(1)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      komfoProvider
        .RetrieveMetricsAsync(sessionConfiguration.Token.AccessToken, Arg.Any<IEnumerable<string>>(), Arg.Any<MetricFields>())
        .Returns(
          info => { throw new KomfoProviderException(Guid.NewGuid().ToString(), KomfoStatusCode.RateLimit, (HttpStatusCode)429); },
          info => { throw new KomfoProviderException(Guid.NewGuid().ToString(), KomfoStatusCode.RateLimit, (HttpStatusCode)429); },
          info => Task.FromResult(new List<Metric> { new Metric() }.AsEnumerable()));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationFactory, sessionConfiguration, komfoProvider))
      {
        var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
          .TwitterHandles(new[] { "twh1", "twh2", "twh3" })
          .WithPolling(polling => polling.Interval(TimeSpan.FromMilliseconds(10)).Attempts(3))
          .Create();

        await komfoSession.ExecuteAsync(metricsRequest).ContinueWith(task =>
        {
          // assert
          task.Result.Should().NotBeNull();

          komfoProvider.Received(3).RetrieveMetricsAsync(
            Arg.Is(sessionConfiguration.Token.AccessToken),
            Arg.Is(metricsRequest.Configuration.TwitterHandles),
            Arg.Is(metricsRequest.Configuration.Fields));
        });
      }
    }

    [Test]
    public void ShouldThrowExceptionWhenPollingNotSuccessfulInMetricsRequest()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var config = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15)
        }
      };

      configurationFactory.GetConfiguration().Returns(config);
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromMilliseconds(1)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      komfoProvider
        .RetrieveMetricsAsync(sessionConfiguration.Token.AccessToken, Arg.Any<IEnumerable<string>>(), Arg.Any<MetricFields>())
        .Returns(
          info => { throw new KomfoProviderException(Guid.NewGuid().ToString(), KomfoStatusCode.RateLimit, (HttpStatusCode)429); },
          info => { throw new KomfoProviderException(Guid.NewGuid().ToString(), KomfoStatusCode.RateLimit, (HttpStatusCode)429); },
          info => { throw new KomfoProviderException(Guid.NewGuid().ToString(), KomfoStatusCode.RateLimit, (HttpStatusCode)429); });

      // act
      using (var komfoSession = new AuthenticatedSession(configurationFactory, sessionConfiguration, komfoProvider))
      {
        var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
          .TwitterHandles(new[] { "twh1", "twh2", "twh3" })
          .WithPolling(polling => polling.Interval(TimeSpan.FromMilliseconds(10)).Attempts(3))
          .Create();

        // assert
        komfoSession.Awaiting(async session => await session.ExecuteAsync(metricsRequest))
          .ShouldThrow<KomfoProviderException>()
          .And
          .KomfoStatusCode.Should().Be(KomfoStatusCode.RateLimit);
      }
    }

    #endregion

    #region exception handling

    [Test]
    public void ShouldThrowExceptionWhenMetricsRequestIsInvalid()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var sessionConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromMilliseconds(1)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      komfoProvider
        .When(provider => provider.RetrieveMetricsAsync(sessionConfiguration.Token.AccessToken, Arg.Any<IEnumerable<string>>(), Arg.Any<MetricFields>()))
        .Throw(new KomfoProviderException(Guid.NewGuid().ToString(), KomfoStatusCode.MissingOrInvalidRequiredParameter, HttpStatusCode.BadRequest));

      // act
      using (var komfoSession = new AuthenticatedSession(configurationFactory, sessionConfiguration, komfoProvider))
      {
        var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
          .TwitterHandles(new List<string> { })
          .Create();

        komfoSession
          .Awaiting(async session => await session.ExecuteAsync(metricsRequest))
          .ShouldThrow<KomfoProviderException>()
          .And
          .KomfoStatusCode.Should().Be(KomfoStatusCode.MissingOrInvalidRequiredParameter);
      }
    }

    #endregion
  }
}