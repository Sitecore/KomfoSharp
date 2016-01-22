// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveUsersFromCustomAudienceResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove
{
  using System;

  /// <summary>
  /// Represents the response to the remove users from custom audience request.
  /// </summary>
  [Serializable]
  public class RemoveUsersFromCustomAudienceResponse : IRemoveUsersFromCustomAudienceResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveUsersFromCustomAudienceResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public RemoveUsersFromCustomAudienceResponse(UpdateUsersInCustomAudienceResponseData data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public UpdateUsersInCustomAudienceResponseData Data { get; private set; }
  }
}