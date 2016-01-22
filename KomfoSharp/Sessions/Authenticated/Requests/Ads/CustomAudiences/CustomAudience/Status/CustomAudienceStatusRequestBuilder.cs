// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceStatusRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the custom audience status request builder.
  /// </summary>
  public class CustomAudienceStatusRequestBuilder : BaseRequestBuilder, ICustomAudienceStatusRequestBuilder, IStatusCalled, IWithPollingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceStatusRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    public CustomAudienceStatusRequestBuilder(IConfigurationProvider configurationProvider, string customAudienceId) : base(configurationProvider)
    {
      this.Request = new CustomAudienceStatusRequest(customAudienceId);
    }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected ICustomAudienceStatusRequest Request { get; private set; }

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
    /// Creates the custom audience status request.
    /// </summary>
    /// <returns>
    /// The custom audience status request.
    /// </returns>
    public ICustomAudienceStatusRequest Create()
    {
      return this.Request;
    }
  }
}