// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceDeleteRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using System;

  /// <summary>
  /// Represents the custom audience delete request.
  /// </summary>
  [Serializable]
  public class CustomAudienceDeleteRequest : ICustomAudienceDeleteRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceDeleteRequest"/> class.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    public CustomAudienceDeleteRequest(string customAudienceId)
    {
      this.Configuration = new CustomAudienceDeleteRequestConfiguration
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
    public CustomAudienceDeleteRequestConfiguration Configuration { get; private set; }
  }
}