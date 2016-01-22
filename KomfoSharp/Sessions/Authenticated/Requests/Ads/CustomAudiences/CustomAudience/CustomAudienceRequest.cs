// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using System;

  /// <summary>
  /// Represents the custom audience request.
  /// </summary>
  [Serializable]
  public class CustomAudienceRequest : ICustomAudienceRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceRequest"/> class.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    public CustomAudienceRequest(string customAudienceId)
    {
      this.Configuration = new CustomAudienceRequestConfiguration
      {
        CustomAudienceId = customAudienceId
      };
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public CustomAudienceRequestConfiguration Configuration { get; private set; }
  }
}