// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveUsersFromCustomAudienceRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the remove users from custom audience request builder.
  /// </summary>
  public class RemoveUsersFromCustomAudienceRequestBuilder : BaseRequestBuilder, IRemoveUsersFromCustomAudienceRequestBuilder, IRemoveCalled, IWithPollingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveUsersFromCustomAudienceRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="updateUsersInCustomAudienceConfiguration">The update users in custom audience configuration.</param>
    public RemoveUsersFromCustomAudienceRequestBuilder(IConfigurationProvider configurationProvider, string customAudienceId, UpdateUsersInCustomAudienceConfiguration updateUsersInCustomAudienceConfiguration)
      : base(configurationProvider)
    {
      this.Request = new RemoveUsersFromCustomAudienceRequest(customAudienceId, updateUsersInCustomAudienceConfiguration);
    }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected IRemoveUsersFromCustomAudienceRequest Request { get; private set; }

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
    /// Creates the remove users from custom audience request.
    /// </summary>
    /// <returns>
    /// The remove users from custom audience request.
    /// </returns>
    public IRemoveUsersFromCustomAudienceRequest Create()
    {
      return this.Request;
    }
  }
}