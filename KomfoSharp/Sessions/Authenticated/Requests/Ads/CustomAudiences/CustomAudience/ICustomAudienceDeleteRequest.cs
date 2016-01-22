// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudienceDeleteRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  /// <summary>
  /// Defines the custom audience delete request.
  /// </summary>
  public interface ICustomAudienceDeleteRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    CustomAudienceDeleteRequestConfiguration Configuration { get; }
  }
}