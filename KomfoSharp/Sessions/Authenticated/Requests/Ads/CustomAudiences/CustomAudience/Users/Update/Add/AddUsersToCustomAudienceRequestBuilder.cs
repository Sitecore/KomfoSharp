// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUsersToCustomAudienceRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the add users to custom audience request builder.
  /// </summary>
  public class AddUsersToCustomAudienceRequestBuilder : BaseRequestBuilder, IAddUsersToCustomAudienceRequestBuilder, IAddCalled, IWithPollingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AddUsersToCustomAudienceRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="updateUsersInCustomAudienceConfiguration">The update users in custom audience configuration.</param>
    public AddUsersToCustomAudienceRequestBuilder(IConfigurationProvider configurationProvider, string customAudienceId, UpdateUsersInCustomAudienceConfiguration updateUsersInCustomAudienceConfiguration)
      : base(configurationProvider)
    {
      this.Request = new AddUsersToCustomAudienceRequest(customAudienceId, updateUsersInCustomAudienceConfiguration);
    }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected IAddUsersToCustomAudienceRequest Request { get; private set; }

    /// <summary>
    /// Enables the polling.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IWithPollingCalled WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null)
    {
      this.Request.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the add users to custom audience request.
    /// </summary>
    /// <returns>
    /// The add users to custom audience request.
    /// </returns>
    public IAddUsersToCustomAudienceRequest Create()
    {
      return this.Request;
    }
  }
}