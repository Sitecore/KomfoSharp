// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveUsersFromCustomAudienceRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove
{
  using System;

  /// <summary>
  /// Represents the remove users from custom audience request.
  /// </summary>
  [Serializable]
  public class RemoveUsersFromCustomAudienceRequest : IRemoveUsersFromCustomAudienceRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveUsersFromCustomAudienceRequest"/> class.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="updateUsersInCustomAudienceConfiguration">The update users in custom audience configuration.</param>
    public RemoveUsersFromCustomAudienceRequest(string customAudienceId, UpdateUsersInCustomAudienceConfiguration updateUsersInCustomAudienceConfiguration)
    {
      this.Configuration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = customAudienceId,
        Users = updateUsersInCustomAudienceConfiguration
      };
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public UpdateUsersInCustomAudienceRequestConfiguration Configuration { get; private set; }
  }
}