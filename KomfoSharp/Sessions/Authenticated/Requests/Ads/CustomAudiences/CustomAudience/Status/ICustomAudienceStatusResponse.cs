// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudienceStatusResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status
{
  using KomfoSharp.Model;

  /// <summary>
  /// Defines the response to the custom audience status request.
  /// </summary>
  public interface ICustomAudienceStatusResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    CustomAudienceStatus Data { get; }
  }
}