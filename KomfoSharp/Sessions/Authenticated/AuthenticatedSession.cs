// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticatedSession.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Threading.Tasks;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Provider;
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

  /// <summary>
  /// Defines the methods that are used to manage authenticated requests.
  /// </summary>
  public class AuthenticatedSession : IAuthenticatedSession
  {
    /// <summary>
    /// If <c>true</c> the current instance is already disposed. 
    /// </summary>
    private bool disposed;

    /// <summary>
    /// If <c>true</c> the Komfo provider should be disposed.
    /// </summary>
    private readonly bool disposeKomfoProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticatedSession" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="configuration">The configuration of the current session.</param>
    public AuthenticatedSession(IConfigurationProvider configurationProvider, AuthenticatedSessionConfiguration configuration)
      : this(configurationProvider, configuration, new KomfoProvider(configurationProvider))
    {
      this.disposeKomfoProvider = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticatedSession" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="configuration">The configuration of the current session.</param>
    /// <param name="komfoProvider">The Komfo provider.</param>
    public AuthenticatedSession(IConfigurationProvider configurationProvider, AuthenticatedSessionConfiguration configuration, IKomfoProvider komfoProvider)
    {
      this.disposed = false;
      this.ConfigurationProvider = configurationProvider;
      this.Configuration = configuration;
      this.KomfoProvider = komfoProvider;
    }

    /// <summary>
    /// Gets the Komfo provider.
    /// </summary>
    /// <value>
    /// The Komfo provider.
    /// </value>
    protected IKomfoProvider KomfoProvider { get; private set; }

    /// <summary>
    /// Gets the configuration provider.
    /// </summary>
    /// <value>
    /// The configuration provider.
    /// </value>
    protected IConfigurationProvider ConfigurationProvider { get; private set; }

    /// <summary>
    /// Gets the configuration of the current session.
    /// </summary>
    /// <value>
    /// The configuration of the current session.
    /// </value>
    public AuthenticatedSessionConfiguration Configuration { get; private set; }

    /// <summary>
    /// Gets the authenticated requests builder.
    /// </summary>
    /// <value>
    /// The authenticated requests builder.
    /// </value>
    public IAuthenticatedRequestsBuilder Requests
    {
      get
      {
        return new AuthenticatedRequestsBuilder(this.ConfigurationProvider);
      }
    }

    #region twitter/followers

    /// <summary>
    /// Executes the <see cref="IStreamRequest"/> asynchronously.
    /// </summary>
    /// <param name="streamRequest">The stream request.</param>
    /// <returns>The <see cref="Task{IStreamResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var streamRequest = komfoSession.Requests.Twitter.Followers.Stream
    ///     .TwitterHandles(new[] { "&lt;twh1&gt;", "&lt;twh2&gt;", "&lt;twh3&gt;" })
    ///     .Create();
    ///
    ///   var streamResponse = await komfoSession.ExecuteAsync(streamRequest);
    ///
    ///   foreach (var tweet in streamResponse.Data)
    ///   {
    ///     this.ShowTweet(tweet);
    ///   }
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var streamRequest = komfoSession.Requests.Twitter.Followers.Stream
    ///     .TwitterHandles(new[] { "&lt;twh1&gt;", "&lt;twh2&gt;", "&lt;twh3&gt;" })
    ///     .Fields(TweetFields.Channel | TweetFields.RequestHandle | TweetFields.Text)
    ///     .Since(DateTime.UtcNow.AddMonths(-1))
    ///     .Until(DateTime.UtcNow.AddHours(-1))
    ///     .WithPolling(polling => polling.Interval(TimeSpan.FromSeconds(15)).Attempts(4))
    ///     .Create();
    ///
    ///   var tweets = new List&lt;Tweet&gt;();
    ///
    ///   do
    ///   {
    ///     var streamResponse = await komfoSession.ExecuteAsync(streamRequest);
    ///     tweets.AddRange(streamResponse.Data);
    ///     streamRequest = streamResponse.Next;
    ///   }
    ///   while (streamRequest != null);
    ///
    ///   foreach (var tweet in tweets)
    ///   {
    ///     this.ShowTweet(tweet);
    ///   }
    /// }
    /// </code>
    /// </example>
    public async Task<IStreamResponse> ExecuteAsync(IStreamRequest streamRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(streamRequest, streamRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="IMetricsRequest"/> asynchronously.
    /// </summary>
    /// <param name="metricsRequest">The metrics request.</param>
    /// <returns>The <see cref="IMetricsResponse"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
    ///     .TwitterHandles(new[] { "&lt;twh1&gt;", "&lt;twh2&gt;", "&lt;twh3&gt;" })
    ///     .Create();
    ///
    ///   var metricsResponse = await komfoSession.ExecuteAsync(metricsRequest);
    ///
    ///   foreach (var metric in metricsResponse.Data)
    ///   {
    ///     this.ShowMetric(metric);
    ///   }
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
    ///     .TwitterHandles(new[] { "&lt;twh1&gt;", "&lt;twh2&gt;", "&lt;twh3&gt;" })
    ///     .Fields(MetricFields.Channel | MetricFields.RequestHandle | MetricFields.Engagement)
    ///     .WithPolling(polling => polling.Interval(TimeSpan.FromSeconds(15)).Attempts(4))
    ///     .Create();
    ///
    ///   var metricsResponse = await komfoSession.ExecuteAsync(metricsRequest);
    ///
    ///   foreach (var metric in metricsResponse.Data)
    ///   {
    ///     this.ShowMetric(metric);
    ///   }
    /// }
    /// </code>
    /// </example>
    public async Task<IMetricsResponse> ExecuteAsync(IMetricsRequest metricsRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(metricsRequest, metricsRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    #endregion

    #region ads/customaudiences

    /// <summary>
    /// Executes the <see cref="ICustomAudiencesRequest"/> asynchronously.
    /// </summary>
    /// <param name="customAudiencesRequest">The custom audiences request.</param>
    /// <returns>The <see cref="Task{ICampaignCustomAudiencesResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudiencesRequest = komfoSession.Requests.Ads.CustomAudiences.Create();
    ///
    ///   var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);
    ///
    ///   foreach (var customAudience in customAudiencesResponse.Data)
    ///   {
    ///     this.ShowCustomAudience(customAudience);
    ///   }
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudiencesRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);
    ///
    ///   foreach (var customAudience in customAudiencesResponse.Data)
    ///   {
    ///     this.ShowCustomAudience(customAudience);
    ///   }
    /// }
    /// </code>
    /// </example>
    public async Task<ICustomAudiencesResponse> ExecuteAsync(ICustomAudiencesRequest customAudiencesRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(customAudiencesRequest, customAudiencesRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="INewCustomAudienceRequest"/> asynchronously.
    /// </summary>
    /// <param name="newCustomAudienceRequest">The new custom audience request.</param>
    /// <returns>The <see cref="Task{INewCustomAudienceResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var newCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .New(customAudience)
    ///     .Create();
    ///
    ///   var newCustomAudienceResponse = await komfoSession.ExecuteAsync(newCustomAudienceRequest);
    ///
    ///   this.Show("New custom audience ID: {0}", newCustomAudienceResponse.Data.CustomAudienceId);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var newCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .New(customAudience => customAudience
    ///       .Name("Sportsmen")
    ///       .Description("The people who like sport"))
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var newCustomAudienceResponse = await komfoSession.ExecuteAsync(newCustomAudienceRequest);
    ///
    ///   this.Show("New custom audience ID: {0}", newCustomAudienceResponse.Data.CustomAudienceId);
    /// }
    /// </code>
    /// </example>
    public async Task<INewCustomAudienceResponse> ExecuteAsync(INewCustomAudienceRequest newCustomAudienceRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(newCustomAudienceRequest, newCustomAudienceRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="ICustomAudienceRequest"/> asynchronously.
    /// </summary>
    /// <param name="customAudienceRequest">The custom audience request.</param>
    /// <returns>The <see cref="Task{ICustomAudienceResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("11122")
    ///     .Create();
    ///
    ///   var customAudienceResponse = await komfoSession.ExecuteAsync(customAudienceRequest);
    ///
    ///   this.ShowCustomAudience(customAudienceResponse.Data);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("11122")
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(10.0))
    ///       .Attempts(6))
    ///     .Create();
    ///
    ///   var customAudienceResponse = await komfoSession.ExecuteAsync(customAudienceRequest);
    ///
    ///   this.ShowCustomAudience(customAudienceResponse.Data);
    /// }
    /// </code>
    /// </example>
    public async Task<ICustomAudienceResponse> ExecuteAsync(ICustomAudienceRequest customAudienceRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(customAudienceRequest, customAudienceRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="ICustomAudienceDeleteRequest"/> asynchronously.
    /// </summary>
    /// <param name="customAudienceDeleteRequest">The custom audience delete request.</param>
    /// <returns>The <see cref="ICustomAudienceDeleteResponse"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudienceDeleteRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("11122")
    ///     .Delete()
    ///     .Create();
    ///
    ///   var customAudienceDeleteResponse = await komfoSession.ExecuteAsync(customAudienceDeleteRequest);
    ///
    ///   this.ShowCustomAudience(customAudienceDeleteResponse.Data);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudienceDeleteRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("11122")
    ///     .Delete()
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(10.0))
    ///       .Attempts(6))
    ///     .Create();
    ///
    ///   var customAudienceDeleteResponse = await komfoSession.ExecuteAsync(customAudienceDeleteRequest);
    ///
    ///   this.ShowCustomAudience(customAudienceDeleteResponse.Data);
    /// }
    /// </code>
    /// </example>
    public async Task<ICustomAudienceDeleteResponse> ExecuteAsync(ICustomAudienceDeleteRequest customAudienceDeleteRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(customAudienceDeleteRequest, customAudienceDeleteRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="IAddUsersToCustomAudienceRequest"/> asynchronously.
    /// </summary>
    /// <param name="addUsersToCustomAudienceRequest">The add users request.</param>
    /// <returns>The <see cref="Task{IAddUsersToCustomAudienceResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Add(users => users.Emails(new[] { "user1@domain.com", "user2@domain.com" }))
    ///     .Create();
    ///
    ///   var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);
    ///
    ///   this.Show("Entries received: {0}", addUsersResponse.Data.EntriesReceived);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Add(users => users
    ///       .Emails(new[] { "user1@domain.com", "user2@domain.com" })
    ///       .WithHashing())
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);
    ///
    ///   this.Show("amount of added users: {0}", addUsersResponse.Data);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Add(users => users.PhoneNumbers(new[] { "+38 (050) 111-11-11", "+38 (050) 222-22-22" }))
    ///     .Create();
    ///
    ///   var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);
    ///
    ///   this.Show("Entries received: {0}", addUsersResponse.Data.EntriesReceived);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Add(users => users
    ///       .PhoneNumbers(new[] { "380501111111", "380502222222" })
    ///       .WithHashing())
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);
    ///
    ///   this.Show("amount of added users: {0}", addUsersResponse.Data);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Add(users => users
    ///       .FacebookIds(new[] { "11111111", "2222222" })
    ///       .FacebookApplicationsIds(new[] { "12121212" }))
    ///     .Create();
    ///
    ///   var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);
    ///
    ///   this.Show("Entries received: {0}", addUsersResponse.Data.EntriesReceived);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Add(users => users
    ///       .FacebookIds(new[] { "11111111", "2222222" })
    ///       .FacebookApplicationsIds(new[] { "12121212" }))
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);
    ///
    ///   this.Show("amount of added users: {0}", addUsersResponse.Data);
    /// }
    /// </code>
    /// </example>
    public async Task<IAddUsersToCustomAudienceResponse> ExecuteAsync(IAddUsersToCustomAudienceRequest addUsersToCustomAudienceRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(addUsersToCustomAudienceRequest, addUsersToCustomAudienceRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="IRemoveUsersFromCustomAudienceRequest"/> asynchronously.
    /// </summary>
    /// <param name="removeUsersFromCustomAudienceRequest">The remove users request.</param>
    /// <returns>The <see cref="Task{IRemoveUsersFromCustomAudienceResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Remove(users => users.Emails(new[] { "user1@domain.com", "user2@domain.com" }))
    ///     .Create();
    ///
    ///   var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);
    ///
    ///   this.Show("Entries received: {0}", removeUsersResponse.Data.EntriesReceived);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Remove(users => users
    ///       .Emails(new[] { "user1@domain.com", "user2@domain.com" })
    ///       .WithHashing())
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);
    ///
    ///   this.Show("Entries received: {0}", removeUsersResponse.Data.EntriesReceived);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Remove(users => users.PhoneNumbers(new[] { "+38 (050) 111-11-11", "+38 (050) 222-22-22" }))
    ///     .Create();
    ///
    ///   var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);
    ///
    ///   this.Show("Entries received: {0}", removeUsersResponse.Data.EntriesReceived);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Remove(users => users
    ///       .PhoneNumbers(new[] { "380501111111", "380502222222" })
    ///       .WithHashing())
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);
    ///
    ///   this.Show("Entries received: {0}", removeUsersResponse.Data.EntriesReceived);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Remove(users => users
    ///       .FacebookIds(new[] { "1111111", "2222222" })
    ///       .FacebookApplicationsIds(new[] { "12121212" }))
    ///     .Create();
    ///
    ///   var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);
    ///
    ///   this.Show("Entries received: {0}", removeUsersResponse.Data.EntriesReceived);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var removeUsersFromCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Users
    ///     .Remove(users => users
    ///       .FacebookIds(new[] { "1111111", "2222222" })
    ///       .FacebookApplicationsIds(new[] { "12121212" }))
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var removeUsersFromCustomAudienceResponse = await komfoSession.ExecuteAsync(removeUsersFromCustomAudienceRequest);
    ///
    ///   this.Show("Entries received: {0}", removeUsersResponse.Data.EntriesReceived);
    /// }
    /// </code>
    /// </example>
    public async Task<IRemoveUsersFromCustomAudienceResponse> ExecuteAsync(IRemoveUsersFromCustomAudienceRequest removeUsersFromCustomAudienceRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(removeUsersFromCustomAudienceRequest, removeUsersFromCustomAudienceRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="ICustomAudienceStatusRequest"/> asynchronously.
    /// </summary>
    /// <param name="customAudienceStatusRequest">The custom audience status request.</param>
    /// <returns>The <see cref="Task{ICustomAudienceStatusResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudienceStatusRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Status
    ///     .Create();
    ///
    ///   var customAudienceStatusResponse = await komfoSession.ExecuteAsync(customAudienceStatusRequest);
    ///
    ///   this.ShowCustomAudienceStatus(customAudienceStatusResponse.Data);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudienceStatusRequest = komfoSession.Requests.Ads.CustomAudiences
    ///     .CustomAudienceId("55544")
    ///     .Status
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var customAudienceStatusResponse = await komfoSession.ExecuteAsync(customAudienceStatusRequest);
    ///
    ///   this.ShowCustomAudienceStatus(customAudienceStatusResponse.Data);
    /// }
    /// </code>
    /// </example>
    public async Task<ICustomAudienceStatusResponse> ExecuteAsync(ICustomAudienceStatusRequest customAudienceStatusRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(customAudienceStatusRequest, customAudienceStatusRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    #endregion

    #region ads/campaigns

    /// <summary>
    /// Executes the <see cref="ICampaignsRequest"/> asynchronously.
    /// </summary>
    /// <param name="campaignsRequest">The campaigns request.</param>
    /// <returns>The <see cref="Task{ICampaignsResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var campaignsRequest = komfoSession.Requests.Ads.Campaigns.Create();
    ///
    ///   var campaignsResponse = await komfoSession.ExecuteAsync(campaignsRequest);
    ///
    ///   foreach (var campaign in campaignsResponse.Data)
    ///   {
    ///     this.ShowCampaign(campaign);
    ///   }
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var campaignsRequest = komfoSession.Requests.Ads.Campaigns
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var campaignsResponse = await komfoSession.ExecuteAsync(campaignsRequest);
    ///
    ///   foreach (var campaign in campaignsResponse.Data)
    ///   {
    ///     this.ShowCampaign(campaign);
    ///   }
    /// }
    /// </code>
    /// </example>
    public async Task<ICampaignsResponse> ExecuteAsync(ICampaignsRequest campaignsRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(campaignsRequest, campaignsRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="INewCampaignRequest"/> asynchronously.
    /// </summary>
    /// <param name="newCampaignRequest">The new campaign request.</param>
    /// <returns>The <see cref="Task{INewCampaignResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var newCampaignRequest = komfoSession.Requests.Ads.Campaigns
    ///     .New(campaign)
    ///     .Create();
    ///
    ///   var newCampaignResponse = await komfoSession.ExecuteAsync(newCampaignRequest);
    ///
    ///   this.Show("New campaign ID: {0}", newCampaignResponse.Data.CampaignId);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var newCampaignRequest = komfoSession.Requests.Ads.Campaigns
    ///     .New(campaign => campaign
    ///       .ExternalCampaignKey("sc_camp")
    ///       .ExternalCampaignId("497E00EE842F4AE18AEEACBDA0FCE6C6")
    ///       .Name("Summer 2015")
    ///       .Description("The campaign to be started at summer, 2015"))
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var newCampaignResponse = await komfoSession.ExecuteAsync(newCampaignRequest);
    ///
    ///   this.Show("New campaign ID: {0}", newCampaignResponse.Data.CampaignId);
    /// }
    /// </code>
    /// </example>
    public async Task<INewCampaignResponse> ExecuteAsync(INewCampaignRequest newCampaignRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(newCampaignRequest, newCampaignRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="ICampaignCustomAudiencesRequest"/> asynchronously.
    /// </summary>
    /// <param name="campaignCustomAudiencesRequest">The campaign custom audiences request.</param>
    /// <returns>The <see cref="Task{ICampaignCustomAudiencesResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudiencesRequest = komfoSession.Requests.Ads.Campaigns
    ///     .CampaignId("11233")
    ///     .CustomAudiences
    ///     .Create();
    ///
    ///   var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);
    ///
    ///   foreach (var customAudience in customAudiencesResponse.Data)
    ///   {
    ///     this.ShowCustomAudience(customAudience);
    ///   }
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var customAudiencesRequest = komfoSession.Requests.Ads.Campaigns
    ///     .CampaignId("11123")
    ///     .CustomAudiences
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);
    ///
    ///   foreach (var customAudience in customAudiencesResponse.Data)
    ///   {
    ///     this.ShowCustomAudience(customAudience);
    ///   }
    /// }
    /// </code>
    /// </example>
    public async Task<ICampaignCustomAudiencesResponse> ExecuteAsync(ICampaignCustomAudiencesRequest campaignCustomAudiencesRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(campaignCustomAudiencesRequest, campaignCustomAudiencesRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="IAddCustomAudienceToCampaignRequest"/> asynchronously.
    /// </summary>
    /// <param name="addCustomAudienceToCampaignRequest">The add custom audience request.</param>
    /// <returns>The <see cref="Task{IAddCustomAudienceToCampaignResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var addCustomAudienceToCampaignRequest = komfoSession.Requests.Ads.Campaigns
    ///     .CampaignId("11123")
    ///     .CustomAudiences
    ///     .Add(customAudience => customAudience.CustomAudienceId("55544"))
    ///     .Create();
    ///
    ///   var addCustomAudienceToCampaignResponse = await komfoSession.ExecuteAsync(addCustomAudienceToCampaignRequest);
    ///
    ///   this.Show("Added custom audience ID: {0}", addCustomAudienceResponse.Data.CustomAudienceId);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var addCustomAudienceToCampaignRequest = komfoSession.Requests.Ads.Campaigns
    ///     .CampaignId("11123")
    ///     .CustomAudiences
    ///     .Add(customAudience => customAudience.CustomAudienceId("55544"))
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var addCustomAudienceToCampaignResponse = await komfoSession.ExecuteAsync(addCustomAudienceToCampaignRequest);
    ///
    ///   this.Show("Added custom audience ID: {0}", addCustomAudienceResponse.Data.CustomAudienceId);
    /// }
    /// </code>
    /// </example>
    public async Task<IAddCustomAudienceToCampaignResponse> ExecuteAsync(IAddCustomAudienceToCampaignRequest addCustomAudienceToCampaignRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(addCustomAudienceToCampaignRequest, addCustomAudienceToCampaignRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    /// <summary>
    /// Executes the <see cref="IRemoveCustomAudienceFromCampaignRequest"/> asynchronously.
    /// </summary>
    /// <param name="removeCustomAudienceFromCampaignRequest">The remove custom audience request.</param>
    /// <returns>The <see cref="Task{IRemoveCustomAudienceFromCampaignResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var removeCustomAudienceFromCampaignRequest = komfoSession.Requests.Ads.Campaigns
    ///     .CampaignId("11123")
    ///     .CustomAudiences
    ///     .Remove(customAudience => customAudience.CustomAudienceId("55544"))
    ///     .Create();
    ///
    ///   var removeCustomAudienceFromCampaignResponse = await komfoSession.ExecuteAsync(removeCustomAudienceFromCampaignRequest);
    ///
    ///   this.Show("Removed custom audience ID: {0}", removeCustomAudienceResponse.Data.CustomAudienceId);
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   var removeCustomAudienceFromCampaignRequest = komfoSession.Requests.Ads.Campaigns
    ///     .CampaignId("11123")
    ///     .CustomAudiences
    ///     .Remove(customAudience => customAudience.CustomAudienceId("55544"))
    ///     .WithPolling(polling => polling
    ///       .Interval(TimeSpan.FromSeconds(15))
    ///       .Attempts(4))
    ///     .Create();
    ///
    ///   var removeCustomAudienceFromCampaignResponse = await komfoSession.ExecuteAsync(removeCustomAudienceFromCampaignRequest);
    ///
    ///   this.Show("campaign id: {0}", removeCustomAudienceResponse.Data);
    /// }
    /// </code>
    /// </example>
    public async Task<IRemoveCustomAudienceFromCampaignResponse> ExecuteAsync(IRemoveCustomAudienceFromCampaignRequest removeCustomAudienceFromCampaignRequest)
    {
      return await this.ExecuteHandlingTokenRenewalAsync(removeCustomAudienceFromCampaignRequest, removeCustomAudienceFromCampaignRequest.Configuration.Polling, this.ExecuteRequestAsync);
    }

    #endregion

    #region disposing

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
      {
        return;
      }

      if (disposing)
      {
        if (this.disposeKomfoProvider)
        {
          var provider = this.KomfoProvider as IDisposable;
          if (provider != null)
          {
            provider.Dispose();
          }
        }
      }

      this.disposed = true;
    }

    #endregion

    #region token renewal, polling

    /// <summary>
    /// Executes the request asynchronously handling token renewal configuration.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="request">The request.</param>
    /// <param name="pollingRequestConfiguration">The polling request configuration.</param>
    /// <param name="executeRequestAsync">The method to execute request asynchronously.</param>
    /// <returns>The <see cref="Task{TResponse}"/>.</returns>
    protected async Task<TResponse> ExecuteHandlingTokenRenewalAsync<TRequest, TResponse>(TRequest request, PollingRequestConfiguration pollingRequestConfiguration,
      Func<TRequest, Task<TResponse>> executeRequestAsync)
    {
      try
      {
        return await this.ExecuteHandlingPollingAsync(request, pollingRequestConfiguration, executeRequestAsync);
      }
      catch (KomfoProviderException ex)
      {
        if (ex.KomfoStatusCode != KomfoStatusCode.InvalidAccessToken || !this.Configuration.TokenRenewal.Enabled)
        {
          throw;
        }
      }

      // renew token
      this.Configuration.Token = await this.KomfoProvider.RetrieveAccessTokenAsync(
        this.Configuration.TokenRenewal.ClientId, this.Configuration.TokenRenewal.ClientSecret, this.Configuration.TokenRenewal.Scopes);

      return await this.ExecuteHandlingPollingAsync(request, pollingRequestConfiguration, executeRequestAsync);
    }

    /// <summary>
    /// Executes the request asynchronously handling polling configuration.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="request">The request.</param>
    /// <param name="pollingRequestConfiguration">The polling request configuration.</param>
    /// <param name="executeRequestAsync">The method to execute request asynchronously.</param>
    /// <returns>The <see cref="Task{TResponse}"/>.</returns>
    protected async Task<TResponse> ExecuteHandlingPollingAsync<TRequest, TResponse>(TRequest request, PollingRequestConfiguration pollingRequestConfiguration,
      Func<TRequest, Task<TResponse>> executeRequestAsync)
    {
      if (!pollingRequestConfiguration.Enabled)
      {
        return await executeRequestAsync(request);
      }

      var attemptsCount = 0;
      do
      {
        try
        {
          attemptsCount++;
          return await executeRequestAsync(request);
        }
        catch (KomfoProviderException ex)
        {
          if (ex.KomfoStatusCode != KomfoStatusCode.RateLimit || attemptsCount >= pollingRequestConfiguration.Attempts)
          {
            throw;
          }
        }

        await Task.Delay(pollingRequestConfiguration.Interval);
      }
      while (true);
    }

    #endregion

    #region twitter/followers execution

    /// <summary>
    /// Executes the <see cref="IMetricsRequest"/> asynchronously.
    /// </summary>
    /// <param name="metricsRequest">The metrics request.</param>
    /// <returns>The <see cref="Task{IMetricsResponse}"/>.</returns>
    protected async Task<IMetricsResponse> ExecuteRequestAsync(IMetricsRequest metricsRequest)
    {
      return new MetricsResponse(await this.KomfoProvider.RetrieveMetricsAsync(
            this.Configuration.Token.AccessToken,
            metricsRequest.Configuration.TwitterHandles,
            metricsRequest.Configuration.Fields));
    }

    /// <summary>
    /// Executes the <see cref="IStreamRequest"/> asynchronously.
    /// </summary>
    /// <param name="streamRequest">The stream request.</param>
    /// <returns>The <see cref="Task{IStreamResponse}"/>.</returns>
    protected async Task<IStreamResponse> ExecuteRequestAsync(IStreamRequest streamRequest)
    {
      var tweets = await this.KomfoProvider.RetrieveTweetsAsync(
        this.Configuration.Token.AccessToken,
        streamRequest.Configuration.TwitterHandles,
        streamRequest.Configuration.Since,
        streamRequest.Configuration.Until,
        streamRequest.Configuration.Fields);

      var nextRequest = (IStreamRequest)null;
      var data = tweets as IList<Tweet> ?? tweets.ToList();
      if (streamRequest.Configuration.Fields.HasFlag(TweetFields.GatheredTime) &&
        data.Count >= this.ConfigurationProvider.GetConfiguration().EndpointsConfiguration.Stream.MaxResultsPerCall)
      {
        var nextUntil = data.Min(tweet => tweet.GatheredTime).AddSeconds(-1);
        nextRequest = new StreamRequest();
        nextRequest.Configuration.TwitterHandles = new List<string>(streamRequest.Configuration.TwitterHandles);
        nextRequest.Configuration.Fields = streamRequest.Configuration.Fields;
        nextRequest.Configuration.Polling.Enabled = streamRequest.Configuration.Polling.Enabled;
        nextRequest.Configuration.Polling.Attempts = streamRequest.Configuration.Polling.Attempts;
        nextRequest.Configuration.Polling.Interval = streamRequest.Configuration.Polling.Interval;
        nextRequest.Configuration.Since = streamRequest.Configuration.Since;
        nextRequest.Configuration.Until = nextUntil;
      }

      return new StreamResponse(data, nextRequest);
    }

    #endregion

    #region ads/customaudiences execution

    /// <summary>
    /// Executes the <see cref="ICustomAudiencesRequest"/> asynchronously.
    /// </summary>
    /// <param name="customAudiencesRequest">The custom audiences request.</param>
    /// <returns>The <see cref="Task{ICampaignCustomAudiencesResponse}"/>.</returns>
    protected async Task<ICustomAudiencesResponse> ExecuteRequestAsync(ICustomAudiencesRequest customAudiencesRequest)
    {
      return new CustomAudiencesResponse(await this.KomfoProvider.RetrieveCustomAudiencesAsync(this.Configuration.Token.AccessToken));
    }

    /// <summary>
    /// Executes the <see cref="INewCustomAudienceRequest"/> asynchronously.
    /// </summary>
    /// <param name="newCustomAudienceRequest">The new custom audience request.</param>
    /// <returns>The <see cref="Task{INewCustomAudienceResponse}"/>.</returns>
    protected async Task<INewCustomAudienceResponse> ExecuteRequestAsync(INewCustomAudienceRequest newCustomAudienceRequest)
    {
      return new NewCustomAudienceResponse(new NewCustomAudienceResponseData(await this.KomfoProvider.CreateCustomAudienceAsync(
        this.Configuration.Token.AccessToken,
        newCustomAudienceRequest.Configuration.CustomAudience.Name,
        newCustomAudienceRequest.Configuration.CustomAudience.Description)));
    }

    /// <summary>
    /// Executes the <see cref="ICustomAudienceRequest"/> asynchronously.
    /// </summary>
    /// <param name="customAudienceRequest">The custom audience request.</param>
    /// <returns>The <see cref="Task{ICustomAudienceResponse}"/>.</returns>
    protected async Task<ICustomAudienceResponse> ExecuteRequestAsync(ICustomAudienceRequest customAudienceRequest)
    {
      return new CustomAudienceResponse(await this.KomfoProvider.RetrieveCustomAudienceAsync(
        this.Configuration.Token.AccessToken,
        customAudienceRequest.Configuration.CustomAudienceId));
    }

    /// <summary>
    /// Executes the <see cref="ICustomAudienceDeleteRequest"/> asynchronously.
    /// </summary>
    /// <param name="customAudienceDeleteRequest">The custom audience delete request.</param>
    /// <returns>The <see cref="Task{ICustomAudienceDeleteResponse}"/>.</returns>
    protected async Task<ICustomAudienceDeleteResponse> ExecuteRequestAsync(ICustomAudienceDeleteRequest customAudienceDeleteRequest)
    {
      return new CustomAudienceDeleteResponse(await this.KomfoProvider.DeleteCustomAudienceAsync(
        this.Configuration.Token.AccessToken,
        customAudienceDeleteRequest.Configuration.CustomAudienceId));
    }

    /// <summary>
    /// Executes the <see cref="IAddUsersToCustomAudienceRequest"/> asynchronously.
    /// </summary>
    /// <param name="addUsersRequest">The add users request.</param>
    /// <returns>The <see cref="Task{IAddUsersToCustomAudienceResponse}"/>.</returns>
    protected async Task<IAddUsersToCustomAudienceResponse> ExecuteRequestAsync(IAddUsersToCustomAudienceRequest addUsersRequest)
    {
      int entriesReceived;

      switch (addUsersRequest.Configuration.Users.DataType)
      {
        case UpdateUsersInCustomAudienceDataType.Emails:
          entriesReceived = await this.KomfoProvider.AddEmailsToCustomAudienceAsync(
            this.Configuration.Token.AccessToken,
            addUsersRequest.Configuration.CustomAudienceId,
            addUsersRequest.Configuration.Users.Data,
            (!addUsersRequest.Configuration.Users.IsHashedAlready) && addUsersRequest.Configuration.Users.WithHashing,
            addUsersRequest.Configuration.Users.IsHashedAlready);
          break;
        case UpdateUsersInCustomAudienceDataType.PhoneNumbers:
          entriesReceived = await this.KomfoProvider.AddPhoneNumbersToCustomAudienceAsync(
            this.Configuration.Token.AccessToken,
            addUsersRequest.Configuration.CustomAudienceId,
            addUsersRequest.Configuration.Users.Data,
            (!addUsersRequest.Configuration.Users.IsHashedAlready) && addUsersRequest.Configuration.Users.WithHashing,
            addUsersRequest.Configuration.Users.IsHashedAlready);
          break;
        case UpdateUsersInCustomAudienceDataType.FacebookIds:
          entriesReceived = await this.KomfoProvider.AddFacebookIdsToCustomAudienceAsync(
            this.Configuration.Token.AccessToken,
            addUsersRequest.Configuration.CustomAudienceId,
            addUsersRequest.Configuration.Users.Data,
            addUsersRequest.Configuration.Users.FacebookApplicationsIds);
          break;
        default:
          throw new ArgumentOutOfRangeException(
            "addUsersRequest", 
            string.Format(CultureInfo.InvariantCulture, "Unsupported data type: {0}", addUsersRequest.Configuration.Users.DataType));
      }

      return new AddUsersToCustomAudienceResponse(new UpdateUsersInCustomAudienceResponseData(entriesReceived));
    }

    /// <summary>
    /// Executes the <see cref="IRemoveUsersFromCustomAudienceRequest"/> asynchronously.
    /// </summary>
    /// <param name="removeUsersRequest">The remove users request.</param>
    /// <returns>The <see cref="Task{IRemoveUsersFromCustomAudienceResponse}"/>.</returns>
    protected async Task<IRemoveUsersFromCustomAudienceResponse> ExecuteRequestAsync(IRemoveUsersFromCustomAudienceRequest removeUsersRequest)
    {
      int entriesReceived;

      switch (removeUsersRequest.Configuration.Users.DataType)
      {
        case UpdateUsersInCustomAudienceDataType.Emails:
          entriesReceived = await this.KomfoProvider.RemoveEmailsFromCustomAudienceAsync(
            this.Configuration.Token.AccessToken,
            removeUsersRequest.Configuration.CustomAudienceId,
            removeUsersRequest.Configuration.Users.Data,
            (!removeUsersRequest.Configuration.Users.IsHashedAlready) && removeUsersRequest.Configuration.Users.WithHashing,
            removeUsersRequest.Configuration.Users.IsHashedAlready);
          break;
        case UpdateUsersInCustomAudienceDataType.PhoneNumbers:
          entriesReceived = await this.KomfoProvider.RemovePhoneNumbersFromCustomAudienceAsync(
            this.Configuration.Token.AccessToken,
            removeUsersRequest.Configuration.CustomAudienceId,
            removeUsersRequest.Configuration.Users.Data,
            (!removeUsersRequest.Configuration.Users.IsHashedAlready) && removeUsersRequest.Configuration.Users.WithHashing,
            removeUsersRequest.Configuration.Users.IsHashedAlready);
          break;
        case UpdateUsersInCustomAudienceDataType.FacebookIds:
          entriesReceived = await this.KomfoProvider.RemoveFacebookIdsFromCustomAudienceAsync(
            this.Configuration.Token.AccessToken,
            removeUsersRequest.Configuration.CustomAudienceId,
            removeUsersRequest.Configuration.Users.Data,
            removeUsersRequest.Configuration.Users.FacebookApplicationsIds);
          break;
        default:
          throw new ArgumentOutOfRangeException(
            "removeUsersRequest",
            string.Format(CultureInfo.InvariantCulture, "Unsupported data type: {0}", removeUsersRequest.Configuration.Users.DataType));
      }

      return new RemoveUsersFromCustomAudienceResponse(new UpdateUsersInCustomAudienceResponseData(entriesReceived));
    }

    /// <summary>
    /// Executes the <see cref="ICustomAudienceStatusRequest"/> asynchronously.
    /// </summary>
    /// <param name="customAudienceStatusRequest">The custom audience status request.</param>
    /// <returns>The <see cref="Task{ICustomAudienceStatusResponse}"/>.</returns>
    protected async Task<ICustomAudienceStatusResponse> ExecuteRequestAsync(ICustomAudienceStatusRequest customAudienceStatusRequest)
    {
      return new CustomAudienceStatusResponse(await this.KomfoProvider.RetrieveCustomAudienceStatusAsync(
        this.Configuration.Token.AccessToken,
        customAudienceStatusRequest.Configuration.CustomAudienceId));
    }

    #endregion

    #region ads/campaigns execution

    /// <summary>
    /// Executes the <see cref="ICampaignsRequest"/> asynchronously.
    /// </summary>
    /// <param name="campaignsRequest">The campaigns request.</param>
    /// <returns>The <see cref="Task{ICampaignsResponse}"/>.</returns>
    protected async Task<ICampaignsResponse> ExecuteRequestAsync(ICampaignsRequest campaignsRequest)
    {
      return new CampaignsResponse(await this.KomfoProvider.RetrieveCampaignsAsync(this.Configuration.Token.AccessToken));
    }

    /// <summary>
    /// Executes the <see cref="INewCampaignRequest"/> asynchronously.
    /// </summary>
    /// <param name="newCampaignRequest">The new campaign request.</param>
    /// <returns>The <see cref="Task{INewCampaignResponse}"/>.</returns>
    protected async Task<INewCampaignResponse> ExecuteRequestAsync(INewCampaignRequest newCampaignRequest)
    {
      return new NewCampaignResponse(new NewCampaignResponseData(await this.KomfoProvider.CreateCampaignAsync(
        this.Configuration.Token.AccessToken,
        newCampaignRequest.Configuration.Campaign.ExtCampaignId,
        newCampaignRequest.Configuration.Campaign.ExtCampaignKey,
        newCampaignRequest.Configuration.Campaign.Name,
        newCampaignRequest.Configuration.Campaign.Description)));
    }

    /// <summary>
    /// Executes the <see cref="ICampaignCustomAudiencesRequest"/> asynchronously.
    /// </summary>
    /// <param name="campaignCustomAudiencesRequest">The campaign custom audiences request.</param>
    /// <returns>The <see cref="Task{ICampaignCustomAudiencesResponse}"/>.</returns>
    protected async Task<ICampaignCustomAudiencesResponse> ExecuteRequestAsync(ICampaignCustomAudiencesRequest campaignCustomAudiencesRequest)
    {
      return new CampaignCustomAudiencesResponse(await this.KomfoProvider.RetrieveCustomAudiencesAsync(
        this.Configuration.Token.AccessToken,
        campaignCustomAudiencesRequest.Configuration.CampaignId));
    }

    /// <summary>
    /// Executes the <see cref="IAddCustomAudienceToCampaignRequest"/> asynchronously.
    /// </summary>
    /// <param name="addCustomAudienceRequest">The add custom audience request.</param>
    /// <returns>The <see cref="Task{IAddCustomAudienceToCampaignResponse}"/>.</returns>
    protected async Task<IAddCustomAudienceToCampaignResponse> ExecuteRequestAsync(IAddCustomAudienceToCampaignRequest addCustomAudienceRequest)
    {
      return new AddCustomAudienceToCampaignResponse(new UpdateCustomAudiencesInCampaignResponseData(await this.KomfoProvider.AddCustomAudienceToCampaignAsync(
        this.Configuration.Token.AccessToken,
        addCustomAudienceRequest.Configuration.CampaignId,
        addCustomAudienceRequest.Configuration.CustomAudiences.CustomAudienceId)));
    }

    /// <summary>
    /// Executes the <see cref="IRemoveCustomAudienceFromCampaignRequest"/> asynchronously.
    /// </summary>
    /// <param name="removeCustomAudienceRequest">The remove custom audience request.</param>
    /// <returns>The <see cref="Task{IRemoveCustomAudienceFromCampaignResponse}"/>.</returns>
    protected async Task<IRemoveCustomAudienceFromCampaignResponse> ExecuteRequestAsync(IRemoveCustomAudienceFromCampaignRequest removeCustomAudienceRequest)
    {
      return new RemoveCustomAudienceFromCampaignResponse(new UpdateCustomAudiencesInCampaignResponseData(await this.KomfoProvider.RemoveCustomAudienceFromCampaignAsync(
        this.Configuration.Token.AccessToken,
        removeCustomAudienceRequest.Configuration.CampaignId,
        removeCustomAudienceRequest.Configuration.CustomAudiences.CustomAudienceId)));
    }

    #endregion
  }
}