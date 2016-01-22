// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudienceRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  /// <summary>
  /// Defines the custom audience request.
  /// </summary>
  public interface ICustomAudienceRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    CustomAudienceRequestConfiguration Configuration { get; }
  }
}