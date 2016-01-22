// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddUsersToCustomAudienceRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add
{
  /// <summary>
  /// Defines the add users to custom audience request.
  /// </summary>
  public interface IAddUsersToCustomAudienceRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    UpdateUsersInCustomAudienceRequestConfiguration Configuration { get; }
  }
}