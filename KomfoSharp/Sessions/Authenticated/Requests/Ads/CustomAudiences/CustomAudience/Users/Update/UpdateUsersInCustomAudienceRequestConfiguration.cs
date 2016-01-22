// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateUsersInCustomAudienceRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update
{
  using System;

  /// <summary>
  /// Represents the update users in custom audience request configuration.
  /// </summary>
  [Serializable]
  public class UpdateUsersInCustomAudienceRequestConfiguration : BaseRequestConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUsersInCustomAudienceRequestConfiguration"/> class.
    /// </summary>
    public UpdateUsersInCustomAudienceRequestConfiguration()
    {
      this.Users = new UpdateUsersInCustomAudienceConfiguration();
    }

    /// <summary>
    /// Gets or sets the custom audience identifier.
    /// </summary>
    /// <value>
    /// The custom audience identifier.
    /// </value>
    public string CustomAudienceId { get; set; }

    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    /// <value>
    /// The users.
    /// </value>
    public UpdateUsersInCustomAudienceConfiguration Users { get; set; }
  }
}