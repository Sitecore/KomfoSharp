// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRemoveUsersFromCustomAudienceResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove
{
  /// <summary>
  /// Defines the remove users from custom audience request builder.
  /// </summary>
  public interface IRemoveUsersFromCustomAudienceResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    UpdateUsersInCustomAudienceResponseData Data { get; }
  }
}