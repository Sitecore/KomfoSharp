// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticatedRequestsBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests
{
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream.Fluent;

  /// <summary>
  /// Defines the methods to build authenticated requests.
  /// </summary>
  public class AuthenticatedRequestsBuilder : IAuthenticatedRequestsBuilder, ITwitterCalled, IFollowersCalled, IAdsCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticatedRequestsBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    public AuthenticatedRequestsBuilder(IConfigurationProvider configurationProvider)
    {
      this.ConfigurationProvider = configurationProvider;
    }

    /// <summary>
    /// Gets the configuration provider.
    /// </summary>
    /// <value>
    /// The configuration provider.
    /// </value>
    protected IConfigurationProvider ConfigurationProvider { get; private set; }

    /// <summary>
    /// Gets the Twitter requests builder.
    /// </summary>
    /// <value>
    /// The Twitter requests builder.
    /// </value>
    public ITwitterCalled Twitter
    {
      get
      {
        return this;
      }
    }

    /// <summary>
    /// Gets the followers requests builder.
    /// </summary>
    /// <value>
    /// The followers requests builder.
    /// </value>
    IFollowersCalled IFollowersCalling.Followers
    {
      get
      {
        return this;
      }
    }

    /// <summary>
    /// Gets the metrics requests builder.
    /// </summary>
    /// <value>
    /// The metrics requests builder.
    /// </value>
    IMetricsCalled IMetricsCalling.Metrics
    {
      get
      {
        return new MetricsRequestBuilder(this.ConfigurationProvider);
      }
    }

    /// <summary>
    /// Gets the stream requests builder.
    /// </summary>
    /// <value>
    /// The stream requests builder.
    /// </value>
    IStreamCalled IStreamCalling.Stream
    {
      get
      {
        return new StreamRequestBuilder(this.ConfigurationProvider);
      }
    }


    /// <summary>
    /// Gets the ads requests builder.
    /// </summary>
    /// <value>
    /// The ads requests builder.
    /// </value>
    public IAdsCalled Ads 
    {
      get
      {
        return this;
      }
    }

    /// <summary>
    /// Gets the custom audiences requests builder.
    /// </summary>
    /// <value>
    /// The custom audiences requests builder.
    /// </value>
    ICustomAudiencesCalled ICustomAudiencesCalling<ICustomAudiencesCalled>.CustomAudiences
    {
      get
      {
        return new CustomAudiencesRequestBuilder(this.ConfigurationProvider);
      }
    }

    /// <summary>
    /// Gets the campaigns requests builder.
    /// </summary>
    /// <value>
    /// The campaigns requests builder.
    /// </value>
    ICampaignsCalled ICampaignsCalling.Campaigns
    {
      get
      {
        return new CampaignsRequestBuilder(this.ConfigurationProvider);
      }
    }
  }
}