// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Metric.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;
  using Newtonsoft.Json;

  /// <summary>
  /// Represents the metric.
  /// </summary>
  [Serializable]
  public class Metric
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Metric"/> class.
    /// </summary>
    public Metric()
    {
      this.Engagement = new Engagement();
      this.Sentiment = new Sentiment();
    }

    /// <summary>
    /// Gets or sets the channel.
    /// </summary>
    /// <value>
    /// The channel.
    /// </value>
    /// <remarks>
    /// The Twitter handle of the channel in Komfo. This field is string without the "@" character.
    /// </remarks>
    public string Channel { get; set; }

    /// <summary>
    /// Gets or sets the request handle.
    /// </summary>
    /// <value>
    /// The request handle.
    /// </value>
    /// <remarks>
    /// Twitter handle of the fan. This field is string without the "@" character.
    /// </remarks>
    [JsonProperty("request_handle")]
    public string RequestHandle { get; set; }

    /// <summary>
    /// Gets or sets the updated time.
    /// </summary>
    /// <value>
    /// The updated time.
    /// </value>
    /// <remarks>
    /// The server time when Komfo build these stats.
    /// </remarks>
    [JsonProperty("updated_time")]
    public DateTime UpdatedTime { get; set; }

    /// <summary>
    /// Gets or sets the engagement.
    /// </summary>
    /// <value>
    /// The engagement.
    /// </value>
    public Engagement Engagement { get; set; }

    /// <summary>
    /// Gets or sets the sentiment.
    /// </summary>
    /// <value>
    /// The sentiment.
    /// </value>
    public Sentiment Sentiment { get; set; }
  }
}
