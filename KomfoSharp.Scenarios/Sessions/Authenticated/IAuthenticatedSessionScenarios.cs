// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticatedSessionScenarios.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Scenarios.Sessions.Authenticated
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream;
  using KomfoSharp.Sessions.Fluent;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class IAuthenticatedSessionScenarios : BaseScenario
  {
    #region twitter/followers

    [Test]
    public async void ExecuteAsyncStreamRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeResponse = Substitute.For<IStreamResponse>();
      var fakeRequest = Substitute.For<IStreamRequest>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Twitter.Followers.Stream
        .TwitterHandles(Arg.Any<IEnumerable<string>>())
        .Create()
        .Returns(fakeRequest);

      fakeKomfoSession.ExecuteAsync(fakeRequest).Returns(Task.FromResult(fakeResponse));

      fakeResponse.Data.Returns(this.CreateFakeTweets(new[] { "<twh1>", "<twh2>", "<twh3>" }, "GeekFlavour"));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var streamRequest = komfoSession.Requests.Twitter.Followers.Stream
          .TwitterHandles(new[] { "<twh1>", "<twh2>", "<twh3>" })
          .Create();

        var streamResponse = await komfoSession.ExecuteAsync(streamRequest);

        foreach (var tweet in streamResponse.Data)
        {
          this.ShowTweet(tweet);
        }
      }
    }

    [Test]
    public async void ExecuteAsyncStreamRequestExntendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeResponse = Substitute.For<IStreamResponse>();
      var fakeRequest = Substitute.For<IStreamRequest>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Twitter.Followers.Stream
        .TwitterHandles(Arg.Any<IEnumerable<string>>())
        .Fields(Arg.Any<TweetFields>())
        .Since(Arg.Any<DateTime>())
        .Until(Arg.Any<DateTime>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeRequest);

      fakeKomfoSession.ExecuteAsync(fakeRequest).Returns(Task.FromResult(fakeResponse));

      fakeResponse.Data.Returns(this.CreateFakeTweets(new[] { "<twh1>", "<twh2>", "<twh3>" }, "GeekFlavour"));
      fakeResponse.Next.Returns(fakeRequest, fakeRequest, null);

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var streamRequest = komfoSession.Requests.Twitter.Followers.Stream
          .TwitterHandles(new[] { "<twh1>", "<twh2>", "<twh3>" })
          .Fields(TweetFields.Channel | TweetFields.RequestHandle | TweetFields.Text)
          .Since(DateTime.UtcNow.AddMonths(-1))
          .Until(DateTime.UtcNow.AddHours(-1))
          .WithPolling(polling => polling.Interval(TimeSpan.FromSeconds(15)).Attempts(4))
          .Create();

        var tweets = new List<Tweet>();

        do
        {
          var streamResponse = await komfoSession.ExecuteAsync(streamRequest);
          tweets.AddRange(streamResponse.Data);
          streamRequest = streamResponse.Next;
        }
        while (streamRequest != null);

        foreach (var tweet in tweets)
        {
          this.ShowTweet(tweet);
        }
      }
    }

    [Test]
    public async void ExecuteAsyncMetricsRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeMetricsRequest = Substitute.For<IMetricsRequest>();
      var fakeMetricsResponse = Substitute.For<IMetricsResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Twitter.Followers.Metrics
        .TwitterHandles(Arg.Any<IEnumerable<string>>())
        .Create()
        .Returns(fakeMetricsRequest);

      fakeKomfoSession.ExecuteAsync(fakeMetricsRequest).Returns(Task.FromResult(fakeMetricsResponse));

      fakeMetricsResponse.Data.Returns(this.CreateFakeMetrics(new[] { "<twh1>", "<twh2>", "<twh3>" }, "GeekFlavour"));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
          .TwitterHandles(new[] { "<twh1>", "<twh2>", "<twh3>" })
          .Create();

        var metricsResponse = await komfoSession.ExecuteAsync(metricsRequest);

        foreach (var metric in metricsResponse.Data)
        {
          this.ShowMetric(metric);
        }
      }
    }

    [Test]
    public async void ExecuteAsyncMetricsRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeMetricsRequest = Substitute.For<IMetricsRequest>();
      var fakeMetricsResponse = Substitute.For<IMetricsResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Twitter.Followers.Metrics
        .TwitterHandles(Arg.Any<IEnumerable<string>>())
        .Fields(Arg.Any<MetricFields>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeMetricsRequest);

      fakeKomfoSession.ExecuteAsync(fakeMetricsRequest).Returns(Task.FromResult(fakeMetricsResponse));

      fakeMetricsResponse.Data.Returns(this.CreateFakeMetrics(new[] { "<twh1>", "<twh2>", "<twh3>" }, "GeekFlavour"));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
          .TwitterHandles(new[] { "<twh1>", "<twh2>", "<twh3>" })
          .Fields(MetricFields.Channel | MetricFields.RequestHandle | MetricFields.Engagement)
          .WithPolling(polling => polling.Interval(TimeSpan.FromSeconds(15)).Attempts(4))
          .Create();

        var metricsResponse = await komfoSession.ExecuteAsync(metricsRequest);

        foreach (var metric in metricsResponse.Data)
        {
          this.ShowMetric(metric);
        }
      }
    }

    #endregion

    #region ads/customaudiences

    [Test]
    public async void ExecuteAsyncCustomAudiencesRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCustomAudiencesRequest = Substitute.For<ICustomAudiencesRequest>();
      var fakeCustomAudiencesResponse = Substitute.For<ICustomAudiencesResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .Create()
        .Returns(fakeCustomAudiencesRequest);

      fakeKomfoSession.ExecuteAsync(fakeCustomAudiencesRequest).Returns(Task.FromResult(fakeCustomAudiencesResponse));

      fakeCustomAudiencesResponse.Data.Returns(new[] { new CustomAudience { Id = "Id1", Name = "Name1", Description = "Description1", CampaignIds = new[]{"1", "2"} },
                                                       new CustomAudience { Id = "Id2", Name = "Name2", Description = "Description2", CampaignIds = new[]{"3", "4"} }});

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudiencesRequest = komfoSession.Requests.Ads.CustomAudiences.Create();

        var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);

        foreach (var customAudience in customAudiencesResponse.Data)
        {
          this.ShowCustomAudience(customAudience);
        }
      }
    }

    [Test]
    public async void ExecuteAsyncCustomAudiencesRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCustomAudiencesRequest = Substitute.For<ICustomAudiencesRequest>();
      var fakeCustomAudiencesResponse = Substitute.For<ICustomAudiencesResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeCustomAudiencesRequest);

      fakeKomfoSession.ExecuteAsync(fakeCustomAudiencesRequest).Returns(Task.FromResult(fakeCustomAudiencesResponse));

      fakeCustomAudiencesResponse.Data.Returns(new[] { new CustomAudience { Id = "Id1", Name = "Name1", Description = "Description1", CampaignIds = new[] { "1", "2" } } });

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudiencesRequest = komfoSession.Requests.Ads.CustomAudiences
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);

        foreach (var customAudience in customAudiencesResponse.Data)
        {
          this.ShowCustomAudience(customAudience);
        }
      }
    }

    [Test]
    public async void ExecuteAsyncNewCustomAudienceRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCustomAudienceRequest = Substitute.For<INewCustomAudienceRequest>();
      var fakeNewCustomAudienceResponse = Substitute.For<INewCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .New(Arg.Any<CustomAudience>())
        .Create()
        .Returns(fakeNewCustomAudienceRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCustomAudienceRequest).Returns(Task.FromResult(fakeNewCustomAudienceResponse));

      fakeNewCustomAudienceResponse.Data.Returns(new NewCustomAudienceResponseData("321"));

      var customAudience = new CustomAudience();

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var newCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .New(customAudience)
          .Create();

        var newCustomAudienceResponse = await komfoSession.ExecuteAsync(newCustomAudienceRequest);

        this.Show("New custom audience ID: {0}", newCustomAudienceResponse.Data.CustomAudienceId);
      }
    }

    [Test]
    public async void ExecuteAsyncNewCustomAudienceRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCustomAudienceRequest = Substitute.For<INewCustomAudienceRequest>();
      var fakeNewCustomAudienceResponse = Substitute.For<INewCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .New(Arg.Any<Func<INewCustomAudienceBuilder, ICreateCalling<CustomAudience>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeNewCustomAudienceRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCustomAudienceRequest).Returns(Task.FromResult(fakeNewCustomAudienceResponse));

      fakeNewCustomAudienceResponse.Data.Returns(new NewCustomAudienceResponseData("321"));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var newCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .New(customAudience => customAudience
            .Name("Sportsmen")
            .Description("The people who like sport"))
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var newCustomAudienceResponse = await komfoSession.ExecuteAsync(newCustomAudienceRequest);

        this.Show("New custom audience ID: {0}", newCustomAudienceResponse.Data.CustomAudienceId);
      }
    }

    [Test]
    public async void ExecuteAsyncCustomAudienceRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCustomAudienceRequest = Substitute.For<ICustomAudienceRequest>();
      var fakeCustomAudienceResponse = Substitute.For<ICustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Create()
        .Returns(fakeCustomAudienceRequest);

      fakeKomfoSession.ExecuteAsync(fakeCustomAudienceRequest).Returns(Task.FromResult(fakeCustomAudienceResponse));

      fakeCustomAudienceResponse.Data.Returns(new CustomAudience
      {
        Name = "Sportsmen",
        Description = "The people who like sport"
      });

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("11122")
          .Create();

        var customAudienceResponse = await komfoSession.ExecuteAsync(customAudienceRequest);

        this.ShowCustomAudience(customAudienceResponse.Data);
      }
    }

    [Test]
    public async void ExecuteAsyncCustomAudienceRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCustomAudienceRequest = Substitute.For<ICustomAudienceRequest>();
      var fakeCustomAudienceResponse = Substitute.For<ICustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeCustomAudienceRequest);

      fakeKomfoSession.ExecuteAsync(fakeCustomAudienceRequest).Returns(Task.FromResult(fakeCustomAudienceResponse));

      fakeCustomAudienceResponse.Data.Returns(new CustomAudience
      {
        Name = "Sportsmen",
        Description = "The people who like sport"
      });

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("11122")
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(10.0))
            .Attempts(6))
          .Create();

        var customAudienceResponse = await komfoSession.ExecuteAsync(customAudienceRequest);

        this.ShowCustomAudience(customAudienceResponse.Data);
      }
    }

    [Test]
    public async void ExecuteAsyncCustomAudienceDeleteRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCustomAudienceDeleteRequest = Substitute.For<ICustomAudienceDeleteRequest>();
      var fakeCustomAudienceDeleteResponse = Substitute.For<ICustomAudienceDeleteResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Delete()
        .Create()
        .Returns(fakeCustomAudienceDeleteRequest);

      fakeKomfoSession.ExecuteAsync(fakeCustomAudienceDeleteRequest).Returns(Task.FromResult(fakeCustomAudienceDeleteResponse));

      fakeCustomAudienceDeleteResponse.Data.Returns(new CustomAudience
      {
        Name = "Sportsmen",
        Description = "The people who like sport"
      });

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudienceDeleteRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("11122")
          .Delete()
          .Create();

        var customAudienceDeleteResponse = await komfoSession.ExecuteAsync(customAudienceDeleteRequest);

        this.ShowCustomAudience(customAudienceDeleteResponse.Data);
      }
    }

    [Test]
    public async void ExecuteAsyncCustomAudienceDeleteRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCustomAudienceDeleteRequest = Substitute.For<ICustomAudienceDeleteRequest>();
      var fakeCustomAudienceDeleteResponse = Substitute.For<ICustomAudienceDeleteResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Delete()
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeCustomAudienceDeleteRequest);

      fakeKomfoSession.ExecuteAsync(fakeCustomAudienceDeleteRequest).Returns(Task.FromResult(fakeCustomAudienceDeleteResponse));

      fakeCustomAudienceDeleteResponse.Data.Returns(new CustomAudience
      {
        Name = "Sportsmen",
        Description = "The people who like sport"
      });

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudienceDeleteRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("11122")
          .Delete()
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(10.0))
            .Attempts(6))
          .Create();

        var customAudienceDeleteResponse = await komfoSession.ExecuteAsync(customAudienceDeleteRequest);

        this.ShowCustomAudience(customAudienceDeleteResponse.Data);
      }
    }

    [Test]
    public async void ExecuteAsyncNewCustomAudienceUserEmailsRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCustomAudienceUsersRequest = Substitute.For<IAddUsersToCustomAudienceRequest>();
      var fakeNewCustomAudienceUsersResponse = Substitute.For<IAddUsersToCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Add(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .Create()
        .Returns(fakeNewCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCustomAudienceUsersRequest).Returns(Task.FromResult(fakeNewCustomAudienceUsersResponse));

      fakeNewCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Add(users => users.Emails(new[] { "user1@domain.com", "user2@domain.com" }))
          .Create();

        var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);

        this.Show("Entries received: {0}", addUsersToCustomAudienceResponse.Data.EntriesReceived);
      }
    }

    [Test]
    public async void ExecuteAsyncNewCustomAudienceUserEmailsRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCustomAudienceUsersRequest = Substitute.For<IAddUsersToCustomAudienceRequest>();
      var fakeNewCustomAudienceUsersResponse = Substitute.For<IAddUsersToCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Add(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeNewCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCustomAudienceUsersRequest).Returns(Task.FromResult(fakeNewCustomAudienceUsersResponse));

      fakeNewCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Add(users => users
            .Emails(new[] { "user1@domain.com", "user2@domain.com" })
            .WithHashing())
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);

        this.Show("amount of added users: {0}", addUsersToCustomAudienceResponse.Data);
      }
    }

    [Test]
    public async void ExecuteAsyncNewCustomAudienceUserPhoneNumbersRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCustomAudienceUsersRequest = Substitute.For<IAddUsersToCustomAudienceRequest>();
      var fakeNewCustomAudienceUsersResponse = Substitute.For<IAddUsersToCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Add(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .Create()
        .Returns(fakeNewCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCustomAudienceUsersRequest).Returns(Task.FromResult(fakeNewCustomAudienceUsersResponse));

      fakeNewCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Add(users => users.PhoneNumbers(new[] { "+38 (050) 111-11-11", "+38 (050) 222-22-22" }))
          .Create();

        var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);

        this.Show("Entries received: {0}", addUsersToCustomAudienceResponse.Data.EntriesReceived);
      }
    }

    [Test]
    public async void ExecuteAsyncNewCustomAudienceUserPhoneNumbersRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCustomAudienceUsersRequest = Substitute.For<IAddUsersToCustomAudienceRequest>();
      var fakeNewCustomAudienceUsersResponse = Substitute.For<IAddUsersToCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Add(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeNewCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCustomAudienceUsersRequest).Returns(Task.FromResult(fakeNewCustomAudienceUsersResponse));

      fakeNewCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Add(users => users
            .PhoneNumbers(new[] { "380501111111", "380502222222" })
            .WithHashing())
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);

        this.Show("amount of added users: {0}", addUsersToCustomAudienceResponse.Data);
      }
    }

    [Test]
    public async void ExecuteAsyncNewCustomAudienceUserFacebookIdsRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCustomAudienceUsersRequest = Substitute.For<IAddUsersToCustomAudienceRequest>();
      var fakeNewCustomAudienceUsersResponse = Substitute.For<IAddUsersToCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Add(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .Create()
        .Returns(fakeNewCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCustomAudienceUsersRequest).Returns(Task.FromResult(fakeNewCustomAudienceUsersResponse));

      fakeNewCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Add(users => users
            .FacebookIds(new[] { "11111111", "2222222" })
            .FacebookApplicationsIds(new[] { "12121212" }))
          .Create();

        var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);

        this.Show("Entries received: {0}", addUsersToCustomAudienceResponse.Data.EntriesReceived);
      }
    }

    [Test]
    public async void ExecuteAsyncNewCustomAudienceUserFacebookIdsRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCustomAudienceUsersRequest = Substitute.For<IAddUsersToCustomAudienceRequest>();
      var fakeNewCustomAudienceUsersResponse = Substitute.For<IAddUsersToCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Add(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeNewCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCustomAudienceUsersRequest).Returns(Task.FromResult(fakeNewCustomAudienceUsersResponse));

      fakeNewCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Add(users => users
            .FacebookIds(new[] { "11111111", "2222222" })
            .FacebookApplicationsIds(new[] { "12121212" }))
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);

        this.Show("amount of added users: {0}", addUsersToCustomAudienceResponse.Data);
      }
    }

    [Test]
    public async void ExecuteAsyncDeleteCustomAudienceUserEmailsRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeDeleteCustomAudienceUsersRequest = Substitute.For<IRemoveUsersFromCustomAudienceRequest>();
      var fakeDeleteCustomAudienceUsersResponse = Substitute.For<IRemoveUsersFromCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Remove(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .Create()
        .Returns(fakeDeleteCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeDeleteCustomAudienceUsersRequest).Returns(Task.FromResult(fakeDeleteCustomAudienceUsersResponse));

      fakeDeleteCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Remove(users => users.Emails(new[] { "user1@domain.com", "user2@domain.com" }))
          .Create();

        var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);

        this.Show("Entries received: {0}", removeUsersFromCustomAudienceResponse.Data.EntriesReceived);
      }
    }

    [Test]
    public async void ExecuteAsyncDeleteCustomAudienceUserEmailsRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeDeleteCustomAudienceUsersRequest = Substitute.For<IRemoveUsersFromCustomAudienceRequest>();
      var fakeDeleteCustomAudienceUsersResponse = Substitute.For<IRemoveUsersFromCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Remove(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeDeleteCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeDeleteCustomAudienceUsersRequest).Returns(Task.FromResult(fakeDeleteCustomAudienceUsersResponse));

      fakeDeleteCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Remove(users => users
            .Emails(new[] { "user1@domain.com", "user2@domain.com" })
            .WithHashing())
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);

        this.Show("Entries received: {0}", removeUsersFromCustomAudienceResponse.Data.EntriesReceived);
      }
    }

    [Test]
    public async void ExecuteAsyncDeleteCustomAudienceUserPhoneNumbersRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeDeleteCustomAudienceUsersRequest = Substitute.For<IRemoveUsersFromCustomAudienceRequest>();
      var fakeDeleteCustomAudienceUsersResponse = Substitute.For<IRemoveUsersFromCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Remove(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .Create()
        .Returns(fakeDeleteCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeDeleteCustomAudienceUsersRequest).Returns(Task.FromResult(fakeDeleteCustomAudienceUsersResponse));

      fakeDeleteCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Remove(users => users.PhoneNumbers(new[] { "+38 (050) 111-11-11", "+38 (050) 222-22-22" }))
          .Create();

        var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);

        this.Show("Entries received: {0}", removeUsersFromCustomAudienceResponse.Data.EntriesReceived);
      }
    }

    [Test]
    public async void ExecuteAsyncDeleteCustomAudienceUserPhoneNumbersRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeDeleteCustomAudienceUsersRequest = Substitute.For<IRemoveUsersFromCustomAudienceRequest>();
      var fakeDeleteCustomAudienceUsersResponse = Substitute.For<IRemoveUsersFromCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Remove(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeDeleteCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeDeleteCustomAudienceUsersRequest).Returns(Task.FromResult(fakeDeleteCustomAudienceUsersResponse));

      fakeDeleteCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Remove(users => users
            .PhoneNumbers(new[] { "380501111111", "380502222222" })
            .WithHashing())
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);

        this.Show("Entries received: {0}", removeUsersFromCustomAudienceResponse.Data.EntriesReceived);
      }
    }

    [Test]
    public async void ExecuteAsyncDeleteCustomAudienceUserFacebookIdsRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeDeleteCustomAudienceUsersRequest = Substitute.For<IRemoveUsersFromCustomAudienceRequest>();
      var fakeDeleteCustomAudienceUsersResponse = Substitute.For<IRemoveUsersFromCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Remove(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .Create()
        .Returns(fakeDeleteCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeDeleteCustomAudienceUsersRequest).Returns(Task.FromResult(fakeDeleteCustomAudienceUsersResponse));

      fakeDeleteCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Remove(users => users
            .FacebookIds(new[] { "1111111", "2222222" })
            .FacebookApplicationsIds(new[] { "12121212" }))
          .Create();

        var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);

        this.Show("Entries received: {0}", removeUsersFromCustomAudienceResponse.Data.EntriesReceived);
      }
    }

    [Test]
    public async void ExecuteAsyncDeleteCustomAudienceUserFacebookIdsRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeDeleteCustomAudienceUsersRequest = Substitute.For<IRemoveUsersFromCustomAudienceRequest>();
      var fakeDeleteCustomAudienceUsersResponse = Substitute.For<IRemoveUsersFromCustomAudienceResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Users
        .Remove(Arg.Any<Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeDeleteCustomAudienceUsersRequest);

      fakeKomfoSession.ExecuteAsync(fakeDeleteCustomAudienceUsersRequest).Returns(Task.FromResult(fakeDeleteCustomAudienceUsersResponse));

      fakeDeleteCustomAudienceUsersResponse.Data.Returns(new UpdateUsersInCustomAudienceResponseData(2));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Users
          .Remove(users => users
            .FacebookIds(new[] { "1111111", "2222222" })
            .FacebookApplicationsIds(new[] { "12121212" }))
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);

        this.Show("Entries received: {0}", removeUsersFromCustomAudienceResponse.Data.EntriesReceived);
      }
    }

    [Test]
    public async void ExecuteAsyncCustomAudienceStatusRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCustomAudienceStatusRequest = Substitute.For<ICustomAudienceStatusRequest>();
      var fakeCustomAudienceStatusResponse = Substitute.For<ICustomAudienceStatusResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Status
        .Create()
        .Returns(fakeCustomAudienceStatusRequest);

      fakeKomfoSession.ExecuteAsync(fakeCustomAudienceStatusRequest).Returns(Task.FromResult(fakeCustomAudienceStatusResponse));

      fakeCustomAudienceStatusResponse.Data.Returns(new CustomAudienceStatus { Id = "1", ApproximateSize = 2, InvalidEntries = 3, UsersDeleted = 4, UsersInserted = 5 });

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudienceStatusRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Status
          .Create();

        var customAudienceStatusResponse = await komfoSession.ExecuteAsync(customAudienceStatusRequest);

        this.ShowCustomAudienceStatus(customAudienceStatusResponse.Data);
      }
    }

    [Test]
    public async void ExecuteAsyncCustomAudienceStatusRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCustomAudienceStatusRequest = Substitute.For<ICustomAudienceStatusRequest>();
      var fakeCustomAudienceStatusResponse = Substitute.For<ICustomAudienceStatusResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.CustomAudiences
        .CustomAudienceId(Arg.Any<string>())
        .Status
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeCustomAudienceStatusRequest);

      fakeKomfoSession.ExecuteAsync(fakeCustomAudienceStatusRequest).Returns(Task.FromResult(fakeCustomAudienceStatusResponse));

      fakeCustomAudienceStatusResponse.Data.Returns(new CustomAudienceStatus { Id = "1", ApproximateSize = 2, InvalidEntries = 3, UsersDeleted = 4, UsersInserted = 5 });

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudienceStatusRequest = komfoSession.Requests.Ads.CustomAudiences
          .CustomAudienceId("55544")
          .Status
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var customAudienceStatusResponse = await komfoSession.ExecuteAsync(customAudienceStatusRequest);

        this.ShowCustomAudienceStatus(customAudienceStatusResponse.Data);
      }
    }

    #endregion

    #region ads/campaigns

    [Test]
    public async void ExecuteAsyncCampaignsRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCampaignsRequest = Substitute.For<ICampaignsRequest>();
      var fakeCampaignsResponse = Substitute.For<ICampaignsResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns
        .Create()
        .Returns(fakeCampaignsRequest);

      fakeKomfoSession.ExecuteAsync(fakeCampaignsRequest).Returns(Task.FromResult(fakeCampaignsResponse));

      fakeCampaignsResponse.Data.Returns(new Campaign[] { new Campaign { Id = "Id1", Name = "Name1", Description = "Description1", ExtCampaignId = "ExtCampaignId1", ExtCampaignKey = "ExtCampaignKey1" },
                                                         new Campaign { Id = "Id2", Name = "Name2", Description = "Description2", ExtCampaignId = "ExtCampaignId2", ExtCampaignKey = "ExtCampaignKey2" }});

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var campaignsRequest = komfoSession.Requests.Ads.Campaigns.Create();

        var campaignsResponse = await komfoSession.ExecuteAsync(campaignsRequest);

        foreach (var campaign in campaignsResponse.Data)
        {
          this.ShowCampaign(campaign);
        }
      }
    }

    [Test]
    public async void ExecuteAsyncCampaignsRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCampaignsRequest = Substitute.For<ICampaignsRequest>();
      var fakeCampaignsResponse = Substitute.For<ICampaignsResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeCampaignsRequest);

      fakeKomfoSession.ExecuteAsync(fakeCampaignsRequest).Returns(Task.FromResult(fakeCampaignsResponse));

      fakeCampaignsResponse.Data.Returns(new Campaign[] { new Campaign { Id = "Id1", Name = "Name1", Description = "Description1", ExtCampaignId = "ExtCampaignId1", ExtCampaignKey = "ExtCampaignKey1" },
                                                         new Campaign { Id = "Id2", Name = "Name2", Description = "Description2", ExtCampaignId = "ExtCampaignId2", ExtCampaignKey = "ExtCampaignKey2" }});

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var campaignsRequest = komfoSession.Requests.Ads.Campaigns
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var campaignsResponse = await komfoSession.ExecuteAsync(campaignsRequest);

        foreach (var campaign in campaignsResponse.Data)
        {
          this.ShowCampaign(campaign);
        }
      }
    }

    [Test]
    public async void ExecuteAsyncNewCampaignRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCampaignRequest = Substitute.For<INewCampaignRequest>();
      var fakeNewCampaignResponse = Substitute.For<INewCampaignResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns.New(Arg.Any<Campaign>())
        .Create()
        .Returns(fakeNewCampaignRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCampaignRequest).Returns(Task.FromResult(fakeNewCampaignResponse));

      fakeNewCampaignResponse.Data.Returns(new NewCampaignResponseData("123"));

      var campaign = new Campaign();

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var newCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .New(campaign)
          .Create();

        var newCampaignResponse = await komfoSession.ExecuteAsync(newCampaignRequest);

        this.Show("New campaign ID: {0}", newCampaignResponse.Data.CampaignId);
      }
    }

    [Test]
    public async void ExecuteAsyncNewCampaignRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCampaignRequest = Substitute.For<INewCampaignRequest>();
      var fakeNewCampaignResponse = Substitute.For<INewCampaignResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns
        .New(Arg.Any<Func<INewCampaignBuilder, ICreateCalling<Campaign>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeNewCampaignRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCampaignRequest).Returns(Task.FromResult(fakeNewCampaignResponse));

      fakeNewCampaignResponse.Data.Returns(new NewCampaignResponseData("123"));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var newCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .New(campaign => campaign
            .ExternalCampaignKey("sc_camp")
            .ExternalCampaignId("497E00EE842F4AE18AEEACBDA0FCE6C6")
            .Name("Summer 2015")
            .Description("The campaign to be started at summer, 2015"))
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var newCampaignResponse = await komfoSession.ExecuteAsync(newCampaignRequest);

        this.Show("New campaign ID: {0}", newCampaignResponse.Data.CampaignId);
      }
    }

    [Test]
    public async void ExecuteAsyncCampaignsCustomAudiencesRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCampaignsCustomAudiencesRequest = Substitute.For<ICampaignCustomAudiencesRequest>();
      var fakeCampaignsCustomAudiencesResponse = Substitute.For<ICampaignCustomAudiencesResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns
        .CampaignId(Arg.Any<string>())
        .CustomAudiences
        .Create()
        .Returns(fakeCampaignsCustomAudiencesRequest);

      fakeKomfoSession.ExecuteAsync(fakeCampaignsCustomAudiencesRequest).Returns(Task.FromResult(fakeCampaignsCustomAudiencesResponse));

      fakeCampaignsCustomAudiencesResponse.Data.Returns(new[] { new CustomAudience { Id = "Id1", Name = "Name1", Description = "Description1", CampaignIds = new[]{"1", "2"} },
                                                                new CustomAudience { Id = "Id2", Name = "Name2", Description = "Description2", CampaignIds = new[]{"3", "4"} }});

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudiencesRequest = komfoSession.Requests.Ads.Campaigns
          .CampaignId("11233")
          .CustomAudiences
          .Create();

        var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);

        foreach (var customAudience in customAudiencesResponse.Data)
        {
          this.ShowCustomAudience(customAudience);
        }
      }
    }

    [Test]
    public async void ExecuteAsyncCampaignsCustomAudiencesRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeCampaignsCustomAudiencesRequest = Substitute.For<ICampaignCustomAudiencesRequest>();
      var fakeCampaignsCustomAudiencesResponse = Substitute.For<ICampaignCustomAudiencesResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns
        .CampaignId(Arg.Any<string>())
        .CustomAudiences
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeCampaignsCustomAudiencesRequest);

      fakeKomfoSession.ExecuteAsync(fakeCampaignsCustomAudiencesRequest).Returns(Task.FromResult(fakeCampaignsCustomAudiencesResponse));

      fakeCampaignsCustomAudiencesResponse.Data.Returns(new[] { new CustomAudience { Id = "Id1", Name = "Name1", Description = "Description1", CampaignIds = new[]{"1", "2"} },
                                                                new CustomAudience { Id = "Id2", Name = "Name2", Description = "Description2", CampaignIds = new[]{"3", "4"} }});

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var customAudiencesRequest = komfoSession.Requests.Ads.Campaigns
          .CampaignId("11123")
          .CustomAudiences
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);

        foreach (var customAudience in customAudiencesResponse.Data)
        {
          this.ShowCustomAudience(customAudience);
        }
      }
    }

    [Test]
    public async void ExecuteAsyncNewCampaignCustomAudienceRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCampaignCustomAudienceRequest = Substitute.For<IAddCustomAudienceToCampaignRequest>();
      var fakeNewCampaignCustomAudienceResponse = Substitute.For<IAddCustomAudienceToCampaignResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns
        .CampaignId(Arg.Any<string>())
        .CustomAudiences
        .Add(Arg.Any<Func<IUpdateCustomAudiencesInCampaignBuilder, ICreateCalling<UpdateCustomAudiencesInCampaignConfiguration>>>())
        .Create()
        .Returns(fakeNewCampaignCustomAudienceRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCampaignCustomAudienceRequest).Returns(Task.FromResult(fakeNewCampaignCustomAudienceResponse));

      fakeNewCampaignCustomAudienceResponse.Data.Returns(new UpdateCustomAudiencesInCampaignResponseData("123"));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var addCustomAudienceToCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .CampaignId("11123")
          .CustomAudiences
          .Add(customAudience => customAudience.CustomAudienceId("55544"))
          .Create();

        var addCustomAudienceToCampaignResponse = await komfoSession.ExecuteAsync(addCustomAudienceToCampaignRequest);

        this.Show("Added custom audience ID: {0}", addCustomAudienceToCampaignResponse.Data.CustomAudienceId);
      }
    }

    [Test]
    public async void ExecuteAsyncNewCampaignCustomAudienceRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeNewCampaignCustomAudienceRequest = Substitute.For<IAddCustomAudienceToCampaignRequest>();
      var fakeNewCampaignCustomAudienceResponse = Substitute.For<IAddCustomAudienceToCampaignResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns
        .CampaignId(Arg.Any<string>())
        .CustomAudiences
        .Add(Arg.Any<Func<IUpdateCustomAudiencesInCampaignBuilder, ICreateCalling<UpdateCustomAudiencesInCampaignConfiguration>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeNewCampaignCustomAudienceRequest);

      fakeKomfoSession.ExecuteAsync(fakeNewCampaignCustomAudienceRequest).Returns(Task.FromResult(fakeNewCampaignCustomAudienceResponse));

      fakeNewCampaignCustomAudienceResponse.Data.Returns(new UpdateCustomAudiencesInCampaignResponseData("321"));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var addCustomAudienceToCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .CampaignId("11123")
          .CustomAudiences
          .Add(customAudience => customAudience.CustomAudienceId("55544"))
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var addCustomAudienceToCampaignResponse = await komfoSession.ExecuteAsync(addCustomAudienceToCampaignRequest);

        this.Show("Added custom audience ID: {0}", addCustomAudienceToCampaignResponse.Data.CustomAudienceId);
      }
    }

    [Test]
    public async void ExecuteAsyncDeleteCampaignCustomAudienceRequestSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeDeleteCampaignCustomAudienceRequest = Substitute.For<IRemoveCustomAudienceFromCampaignRequest>();
      var fakeDeleteCampaignCustomAudienceResponse = Substitute.For<IRemoveCustomAudienceFromCampaignResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns
        .CampaignId(Arg.Any<string>())
        .CustomAudiences
        .Remove(Arg.Any<Func<IUpdateCustomAudiencesInCampaignBuilder, ICreateCalling<UpdateCustomAudiencesInCampaignConfiguration>>>())
        .Create()
        .Returns(fakeDeleteCampaignCustomAudienceRequest);

      fakeKomfoSession.ExecuteAsync(fakeDeleteCampaignCustomAudienceRequest).Returns(Task.FromResult(fakeDeleteCampaignCustomAudienceResponse));

      fakeDeleteCampaignCustomAudienceResponse.Data.Returns(new UpdateCustomAudiencesInCampaignResponseData("321"));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var removeCustomAudienceFromCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .CampaignId("11123")
          .CustomAudiences
          .Remove(customAudience => customAudience.CustomAudienceId("55544"))
          .Create();

        var removeCustomAudienceFromCampaignResponse = await komfoSession.ExecuteAsync(removeCustomAudienceFromCampaignRequest);

        this.Show("Removed custom audience ID: {0}", removeCustomAudienceFromCampaignResponse.Data.CustomAudienceId);
      }
    }

    [Test]
    public async void ExecuteAsyncDeleteCampaignCustomAudienceRequestExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeKomfoSession = Substitute.For<IAuthenticatedSession>();
      var fakeDeleteCampaignCustomAudienceRequest = Substitute.For<IRemoveCustomAudienceFromCampaignRequest>();
      var fakeDeleteCampaignCustomAudienceResponse = Substitute.For<IRemoveCustomAudienceFromCampaignResponse>();
      var token = new Token();

      komfoSessions
        .Authenticated
        .Token(token)
        .Create()
        .Returns(fakeKomfoSession);

      fakeKomfoSession.Requests.Ads.Campaigns
        .CampaignId(Arg.Any<string>())
        .CustomAudiences
        .Remove(Arg.Any<Func<IUpdateCustomAudiencesInCampaignBuilder, ICreateCalling<UpdateCustomAudiencesInCampaignConfiguration>>>())
        .WithPolling(Arg.Any<Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>>>())
        .Create()
        .Returns(fakeDeleteCampaignCustomAudienceRequest);

      fakeKomfoSession.ExecuteAsync(fakeDeleteCampaignCustomAudienceRequest).Returns(Task.FromResult(fakeDeleteCampaignCustomAudienceResponse));

      fakeDeleteCampaignCustomAudienceResponse.Data.Returns(new UpdateCustomAudiencesInCampaignResponseData("321"));

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        var removeCustomAudienceFromCampaignRequest = komfoSession.Requests.Ads.Campaigns
          .CampaignId("11123")
          .CustomAudiences
          .Remove(customAudience => customAudience.CustomAudienceId("55544"))
          .WithPolling(polling => polling
            .Interval(TimeSpan.FromSeconds(15))
            .Attempts(4))
          .Create();

        var removeCustomAudienceFromCampaignResponse = await komfoSession.ExecuteAsync(removeCustomAudienceFromCampaignRequest);

        this.Show("campaign id: {0}", removeCustomAudienceFromCampaignResponse.Data);
      }
    }

    #endregion
  }
}