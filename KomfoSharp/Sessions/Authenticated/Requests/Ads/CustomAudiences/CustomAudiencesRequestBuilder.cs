// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudiencesRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the custom audiences request builder.
  /// </summary>
  public class CustomAudiencesRequestBuilder : BaseRequestBuilder, ICustomAudiencesRequestBuilder, ICustomAudiencesCalled, Fluent.IWithPollingCalled
  {
    /// <summary>
    /// The request
    /// </summary>
    private ICustomAudiencesRequest request;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudiencesRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    public CustomAudiencesRequestBuilder(IConfigurationProvider configurationProvider)
      : base(configurationProvider)
    {
    }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected ICustomAudiencesRequest Request
    {
      get
      {
        return this.request ?? (this.request = new CustomAudiencesRequest());
      }
    }

    /// <summary>
    /// Enables the polling.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public Fluent.IWithPollingCalled WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null)
    {
      this.Request.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the custom audiences request.
    /// </summary>
    /// <returns>
    /// The custom audiences request.
    /// </returns>
    public ICustomAudiencesRequest Create()
    {
      return this.Request;
    }

    /// <summary>
    /// Builds new custom audience.
    /// </summary>
    /// <param name="entitySetupFunc">The entity setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public INewCalled New(Func<INewCustomAudienceBuilder, ICreateCalling<Model.CustomAudience>> entitySetupFunc)
    {
      return new NewCustomAudienceRequestBuilder(this.ConfigurationProvider, entitySetupFunc(new NewCustomAudienceBuilder()).Create());
    }

    /// <summary>
    /// Builds new custom audience request.
    /// </summary>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public INewCalled New(Model.CustomAudience entity)
    {
      return new NewCustomAudienceRequestBuilder(this.ConfigurationProvider, entity);
    }

    /// <summary>
    /// Specifies the custom audience ID.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public ICustomAudienceIdCalled CustomAudienceId(string customAudienceId)
    {
      return new CustomAudienceRequestBuilder(this.ConfigurationProvider, customAudienceId);
    }
  }
}