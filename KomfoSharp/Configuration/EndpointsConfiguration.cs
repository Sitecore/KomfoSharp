// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndpointsConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Text;
  using KomfoSharp.Configuration.Endpoints;
  using KomfoSharp.Diagnostics;

  /// <summary>
  /// The endpoints configuration
  /// </summary>
  [Serializable]
  public class EndpointsConfiguration
  {
    /// <summary>
    /// The endpoint configurations
    /// </summary>
    private readonly List<EndpointBase> endpointConfigurations;

    /// <summary>
    /// The base URL
    /// </summary>
    private readonly Uri baseUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndpointsConfiguration" /> class.
    /// </summary>
    /// <param name="endpointConfigurations">The endpoint configurations.</param>
    /// <param name="baseUrl">The base URL.</param>
    public EndpointsConfiguration(List<EndpointBase> endpointConfigurations, Uri baseUrl)
    {
      Assert.ArgumentNotNull(endpointConfigurations, "edpointConfigurations");
      this.endpointConfigurations = endpointConfigurations;
      this.baseUrl = baseUrl;
    }

    /// <summary>
    /// Gets or sets the tokens endpoint configuration.
    /// </summary>
    /// <value>
    /// The tokens.
    /// </value>
    public TokensEndpoint Tokens
    {
      get
      {
        return this.GetEndpoint<TokensEndpoint>();
      }
    }

    /// <summary>
    /// Gets or sets the metrics endpoint configuration.
    /// </summary>
    /// <value>
    /// The metrics.
    /// </value>
    public MetricsEndpoint Metrics
    {
      get
      {
        return this.GetEndpoint<MetricsEndpoint>();
      }
    }

    /// <summary>
    /// Gets or sets the stream endpoint configuration.
    /// </summary>
    /// <value>
    /// The stream.
    /// </value>
    public StreamEndpoint Stream
    {
      get
      {
        return this.GetEndpoint<StreamEndpoint>();
      }
    }

    /// <summary>
    /// Gets the custom audiences endpoint configuration.
    /// </summary>
    /// <value>
    /// The custom audiences endpoint configuration.
    /// </value>
    public CustomAudiencesEndpoint CustomAudiences
    {
      get
      {
        return this.GetEndpoint<CustomAudiencesEndpoint>();
      }
    }

    /// <summary>
    /// Gets the custom audience endpoint configuration.
    /// </summary>
    /// <value>
    /// The custom audience endpoint configuration.
    /// </value>
    public CustomAudienceEndpoint CustomAudience
    {
      get
      {
        return this.GetEndpoint<CustomAudienceEndpoint>();
      }
    }

    /// <summary>
    /// Gets the custom audience users endpoint configuration.
    /// </summary>
    /// <value>
    /// The custom audience users endpoint configuration.
    /// </value>
    public CustomAudienceUsersEndpoint CustomAudienceUsers
    {
      get
      {
        return this.GetEndpoint<CustomAudienceUsersEndpoint>();
      }
    }

    /// <summary>
    /// Gets the custom audience status endpoint configuration.
    /// </summary>
    /// <value>
    /// The custom audience status endpoint configuration.
    /// </value>
    public CustomAudienceStatusEndpoint CustomAudienceStatus
    {
      get
      {
        return this.GetEndpoint<CustomAudienceStatusEndpoint>();
      }
    }

    /// <summary>
    /// Gets the campaigns endpoint configuration.
    /// </summary>
    /// <value>
    /// The campaigns endpoint configuration.
    /// </value>
    public CampaignsEndpoint Campaigns
    {
      get
      {
        return this.GetEndpoint<CampaignsEndpoint>();
      }
    }

    /// <summary>
    /// Gets the campaigns endpoint configuration.
    /// </summary>
    /// <value>
    /// The campaigns endpoint configuration.
    /// </value>
    public CampaignCustomAudiencesEndpoint CampaignCustomAudiences
    {
      get
      {
        return this.GetEndpoint<CampaignCustomAudiencesEndpoint>();
      }
    }

    /// <summary>
    /// Gets the endpoint configuration.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>The instance of <typeparamref name="T"/>.</returns>
    public T GetEndpoint<T>() where T : EndpointBase
    {
      return this.endpointConfigurations.OfType<T>().First();
    }

    /// <summary>
    /// Gets the endpoint URL.
    /// </summary>
    /// <param name="endpoint">The endpoint.</param>
    /// <param name="parameters">The parameters. The parameter key should be without curly braces.</param>
    /// <returns>
    /// An <see cref="Uri" /> instance.
    /// </returns>
    public Uri GetEndpointUrl(EndpointBase endpoint, IDictionary<string,string> parameters = null )
    {
      if ((parameters == null) || !parameters.Any())
      {
        return new Uri(this.baseUrl, endpoint.Path);
      }
      
      var formattedPath = new StringBuilder(endpoint.Path);
      foreach (var parameter in parameters)
      {
        formattedPath.Replace(string.Format(CultureInfo.InvariantCulture, "{{{0}}}", parameter.Key), parameter.Value);
      }

      return new Uri(this.baseUrl, formattedPath.ToString());
    }
  }
}
