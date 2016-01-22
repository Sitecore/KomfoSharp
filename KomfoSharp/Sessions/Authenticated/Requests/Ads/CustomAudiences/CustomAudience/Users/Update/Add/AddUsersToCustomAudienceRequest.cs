// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUsersToCustomAudienceRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add
{
  using System;

  /// <summary>
  /// Represents the add users to custom audience request.
  /// </summary>
  [Serializable]
  public class AddUsersToCustomAudienceRequest : IAddUsersToCustomAudienceRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AddUsersToCustomAudienceRequest"/> class.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="updateUsersConfiguration">The update users configuration.</param>
    public AddUsersToCustomAudienceRequest(string customAudienceId, UpdateUsersInCustomAudienceConfiguration updateUsersConfiguration)
    {
      this.Configuration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = customAudienceId,
        Users = updateUsersConfiguration
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