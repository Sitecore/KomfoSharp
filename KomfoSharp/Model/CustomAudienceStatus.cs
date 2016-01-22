// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceStatus.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;
  using Newtonsoft.Json;

  /// <summary>
  /// Represents the status of the custom audience.
  /// </summary>
  /// <remarks>
  /// The status, some statistics and error conditions for the custom audience.
  /// </remarks>
  [Serializable]
  public class CustomAudienceStatus
  {
    /// <summary>
    /// Gets or sets the custom audience identifier.
    /// </summary>
    /// <value>
    /// The custom audience identifier.
    /// </value>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the number of users requested to be added to the custom audience.
    /// </summary>
    /// <value>
    /// The number of users requested to be added to the custom audience.
    /// </value>
    [JsonProperty("users_inserted")]
    public int UsersInserted { get; set; }

    /// <summary>
    /// Gets or sets the number of users requested to be removed to the custom audience.
    /// </summary>
    /// <value>
    /// The number of users requested to be removed from the custom audience.
    /// </value>
    [JsonProperty("users_deleted")]
    public int UsersDeleted { get; set; }

    /// <summary>
    /// Gets or sets the approximate size of the users recognized by Facebook.
    /// </summary>
    /// <value>
    /// The approximate size of the users recognized by Facebook.
    /// </value>
    [JsonProperty("approximate_size")]
    public int ApproximateSize { get; set; }

    /// <summary>
    /// Gets or sets the total number of user entries that were not valid as determined by Facebook.
    /// </summary>
    /// <value>
    /// The total number of user entries that were not valid as determined by Facebook.
    /// </value>
    [JsonProperty("invalid_entries")]
    public int InvalidEntries { get; set; }
  }
}