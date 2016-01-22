// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticatedSession.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated
{
  using System;
  using System.Threading.Tasks;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream;

  /// <summary>
  /// Defines the methods that are used to manage authenticated requests.
  /// </summary>
  public interface IAuthenticatedSession : IDisposable
  {
    /// <summary>
    /// Gets the configuration of the current session.
    /// </summary>
    /// <value>
    /// The configuration of the current session.
    /// </value>
    AuthenticatedSessionConfiguration Configuration { get; }

    /// <summary>
    /// Gets the authenticated requests builder.
    /// </summary>
    /// <value>
    /// The authenticated requests builder.
    /// </value>
    IAuthenticatedRequestsBuilder Requests { get; }

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
    Task<IStreamResponse> ExecuteAsync(IStreamRequest streamRequest);

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
    Task<IMetricsResponse> ExecuteAsync(IMetricsRequest metricsRequest);

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
    Task<ICustomAudiencesResponse> ExecuteAsync(ICustomAudiencesRequest customAudiencesRequest);

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
    Task<INewCustomAudienceResponse> ExecuteAsync(INewCustomAudienceRequest newCustomAudienceRequest);

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
    Task<ICustomAudienceResponse> ExecuteAsync(ICustomAudienceRequest customAudienceRequest);

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
    Task<ICustomAudienceDeleteResponse> ExecuteAsync(ICustomAudienceDeleteRequest customAudienceDeleteRequest);

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
    ///   var addUsersResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);
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
    Task<IAddUsersToCustomAudienceResponse> ExecuteAsync(IAddUsersToCustomAudienceRequest addUsersToCustomAudienceRequest);

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
    Task<IRemoveUsersFromCustomAudienceResponse> ExecuteAsync(IRemoveUsersFromCustomAudienceRequest removeUsersFromCustomAudienceRequest);

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
    Task<ICustomAudienceStatusResponse> ExecuteAsync(ICustomAudienceStatusRequest customAudienceStatusRequest);

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
    Task<ICampaignsResponse> ExecuteAsync(ICampaignsRequest campaignsRequest);

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
    Task<INewCampaignResponse> ExecuteAsync(INewCampaignRequest newCampaignRequest);

    /// <summary>
    /// Executes the <see cref="ICampaignCustomAudiencesRequest"/> asynchronously.
    /// </summary>
    /// <param name="campaignCustomAudiencesRequest">The campaign custom audiences request.</param>
    /// <returns>The <see cref="ICampaignCustomAudiencesResponse"/>.</returns>
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
    Task<ICampaignCustomAudiencesResponse> ExecuteAsync(ICampaignCustomAudiencesRequest campaignCustomAudiencesRequest);

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
    Task<IAddCustomAudienceToCampaignResponse> ExecuteAsync(IAddCustomAudienceToCampaignRequest addCustomAudienceToCampaignRequest);

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
    Task<IRemoveCustomAudienceFromCampaignResponse> ExecuteAsync(IRemoveCustomAudienceFromCampaignRequest removeCustomAudienceFromCampaignRequest);

    #endregion
  }
}