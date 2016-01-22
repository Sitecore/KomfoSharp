// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateUsersInCustomAudienceResponseData.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update
{
  using System;

  /// <summary>
  /// Represents the data in the response to the update users in custom audience request.
  /// </summary>
  [Serializable]
  public class UpdateUsersInCustomAudienceResponseData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUsersInCustomAudienceResponseData"/> class.
    /// </summary>
    /// <param name="entriesReceived">The entries received.</param>
    public UpdateUsersInCustomAudienceResponseData(int entriesReceived)
    {
      this.EntriesReceived = entriesReceived;
    }

    /// <summary>
    /// Gets the entries received.
    /// </summary>
    /// <value>
    /// The entries received.
    /// </value>
    public int EntriesReceived { get; private set; }
  }
}