// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRemoveUsersFromCustomAudienceRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove
{
  /// <summary>
  /// Defines the remove users from custom audience request.
  /// </summary>
  public interface IRemoveUsersFromCustomAudienceRequest
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