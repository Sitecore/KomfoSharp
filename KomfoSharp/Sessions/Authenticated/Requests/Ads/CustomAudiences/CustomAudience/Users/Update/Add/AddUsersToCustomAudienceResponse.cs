// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUsersToCustomAudienceResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add
{
  using System;

  /// <summary>
  /// Represents the response to the add users to custom audience request.
  /// </summary>
  [Serializable]
  public class AddUsersToCustomAudienceResponse : IAddUsersToCustomAudienceResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AddUsersToCustomAudienceResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public AddUsersToCustomAudienceResponse(UpdateUsersInCustomAudienceResponseData data)
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