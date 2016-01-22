// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceStatusRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status
{
  using System;

  /// <summary>
  /// Represents the custom audience status request.
  /// </summary>
  [Serializable]
  public class CustomAudienceStatusRequest : ICustomAudienceStatusRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceStatusRequest"/> class.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    public CustomAudienceStatusRequest(string customAudienceId)
    {
      this.Configuration = new CustomAudienceStatusRequestConfiguration
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
    public CustomAudienceStatusRequestConfiguration Configuration { get; private set; }
  }
}