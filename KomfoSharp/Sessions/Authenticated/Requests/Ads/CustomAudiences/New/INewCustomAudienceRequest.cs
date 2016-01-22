// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewCustomAudienceRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  /// <summary>
  /// Defines the new custom audience request builder.
  /// </summary>
  public interface INewCustomAudienceRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    NewCustomAudienceRequestConfiguration Configuration { get; }
  }
}