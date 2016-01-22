// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCustomAudienceRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the new custom audience request builder.
  /// </summary>
  public class NewCustomAudienceRequestBuilder : BaseRequestBuilder, INewCustomAudienceRequestBuilder, INewCalled, IWithPollingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCustomAudienceRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="customAudience">The custom audience.</param>
    public NewCustomAudienceRequestBuilder(IConfigurationProvider configurationProvider, CustomAudience customAudience) : base(configurationProvider)
    {
      this.Request = new NewCustomAudienceRequest
      {
        Configuration =
        {
          CustomAudience = customAudience
        }
      };
    }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected NewCustomAudienceRequest Request { get; private set; }

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
    /// Creates the new custom audience request.
    /// </summary>
    /// <returns>
    /// The new custom audience request.
    /// </returns>
    public INewCustomAudienceRequest Create()
    {
      return this.Request;
    }
  }
}