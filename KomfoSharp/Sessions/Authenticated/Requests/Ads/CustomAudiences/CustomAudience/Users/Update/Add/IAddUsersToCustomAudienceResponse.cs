// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddUsersToCustomAudienceResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add
{
  /// <summary>
  /// Defines the response to the add users to custom audience request.
  /// </summary>
  public interface IAddUsersToCustomAudienceResponse
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