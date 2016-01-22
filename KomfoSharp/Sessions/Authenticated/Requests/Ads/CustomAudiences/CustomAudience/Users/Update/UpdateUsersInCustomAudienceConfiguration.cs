// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateUsersInCustomAudienceConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Represents the update users in custom audience configuration
  /// </summary>
  [Serializable]
  public class UpdateUsersInCustomAudienceConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUsersInCustomAudienceConfiguration"/> class.
    /// </summary>
    public UpdateUsersInCustomAudienceConfiguration()
    {
      this.DataType = UpdateUsersInCustomAudienceDataType.Unknown;
      this.WithHashing = false;
      this.IsHashedAlready = false;
    }

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public IEnumerable<string> Data { get; set; }

    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>
    /// The type of the data.
    /// </value>
    public UpdateUsersInCustomAudienceDataType DataType { get; set; }

    /// <summary>
    /// Gets or sets the Facebook applications IDs.
    /// </summary>
    /// <value>
    /// The Facebook applications IDs.
    /// </value>
    public IEnumerable<string> FacebookApplicationsIds { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether data should be hashed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the data should be hashed; otherwise, <c>false</c>.
    /// </value>
    public bool WithHashing { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether data is hashed already.
    /// </summary>
    /// <value>
    /// <c>true</c> if the data is hashed already; otherwise, <c>false</c>.
    /// </value>
    public bool IsHashedAlready { get; set; }
  }
}