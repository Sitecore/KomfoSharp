// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Campaign.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;
  using Newtonsoft.Json;

  /// <summary>
  /// Represents the ad campaign.
  /// </summary>
  [Serializable]
  public class Campaign
  {
    /// <summary>
    /// Gets or sets the campaign identifier.
    /// </summary>
    /// <value>
    /// The campaign identifier.
    /// </value>
    /// <remarks>
    /// The ID is a string with maximum size 200 characters and is case sensitive.
    /// </remarks>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    /// <remarks>
    /// Maximum 100 characters.
    /// </remarks>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    /// <remarks>
    /// Maximum 500 characters.
    /// </remarks>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the ID of the campaign in the external system.
    /// </summary>
    /// <value>
    /// The ID of the campaign in the external system.
    /// </value>
    /// <remarks>
    /// Maximum 100 characters.
    /// </remarks>
    [JsonProperty("ext_campaign_id")]
    public string ExtCampaignId { get; set; }

    /// <summary>
    /// Gets or sets the URL parameter name used to pass <see cref="ExtCampaignId"/>.
    /// </summary>
    /// <value>
    /// The URL parameter name used to pass <see cref="ExtCampaignId"/>.
    /// </value>
    [JsonProperty("ext_campaign_key")]
    public string ExtCampaignKey { get; set; }
  }
}