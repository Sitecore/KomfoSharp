// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudienceStatusRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status
{
  /// <summary>
  /// Defines the custom audience status request.
  /// </summary>
  public interface ICustomAudienceStatusRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    CustomAudienceStatusRequestConfiguration Configuration { get; }
  }
}