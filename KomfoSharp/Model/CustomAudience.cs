// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudience.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;

  /// <summary>
  /// Represents the custom audience.
  /// </summary>
  [Serializable]
  public class CustomAudience
  {
    /// <summary>
    /// Gets or sets the custom audience identifier.
    /// </summary>
    /// <value>
    /// The custom audience identifier.
    /// </value>
    /// <remarks>
    /// The id is a string of digits and letters and is case sensitive. 
    /// Maximum size 200 characters.
    /// </remarks>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    /// <remarks>
    /// A string up to 100 characters.
    /// </remarks>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    /// <remarks>
    /// The description is string up to 500 characters.
    /// </remarks>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the campaign ids.
    /// </summary>
    /// <value>
    /// The campaign ids.
    /// </value>
    /// <remarks>
    /// If the custom audience is part of a single of multiple campaigns – this field will specify them as a list of strings.
    /// </remarks>
    [JsonProperty("campaign_ids")]
    public IEnumerable<string> CampaignIds { get; set; }
  }
}