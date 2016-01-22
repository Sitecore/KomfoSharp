// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceDeleteRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the custom audience delete request builder.
  /// </summary>
  public class CustomAudienceDeleteRequestBuilder : BaseRequestBuilder, IDeleteCalled
  {
    /// <summary>
    /// The request
    /// </summary>
    private ICustomAudienceDeleteRequest request;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    public CustomAudienceDeleteRequestBuilder(IConfigurationProvider configurationProvider, string customAudienceId)
      : base(configurationProvider)
    {
      this.CustomAudienceId = customAudienceId;
    }

    /// <summary>
    /// Gets the custom audience identifier.
    /// </summary>
    /// <value>
    /// The custom audience identifier.
    /// </value>
    protected string CustomAudienceId { get; private set; }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected ICustomAudienceDeleteRequest Request
    {
      get
      {
        return this.request ?? (this.request = new CustomAudienceDeleteRequest(this.CustomAudienceId));
      }
    }

    /// <summary>
    /// Enables the polling.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public ICustomAudienceDeleteRequestBuilder WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null)
    {
      this.Request.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the request.
    /// </summary>
    /// <returns>
    /// The request.
    /// </returns>
    public ICustomAudienceDeleteRequest Create()
    {
      return this.Request;
    }
  }
}