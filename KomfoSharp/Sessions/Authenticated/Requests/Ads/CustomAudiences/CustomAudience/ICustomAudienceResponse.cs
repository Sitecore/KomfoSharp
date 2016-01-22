// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudienceResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using KomfoSharp.Model;

  /// <summary>
  /// Defines the response to the custom audience request.
  /// </summary>
  public interface ICustomAudienceResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    CustomAudience Data { get; }
  }
}