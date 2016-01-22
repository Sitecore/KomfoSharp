// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tweet.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;
  using Newtonsoft.Json;

  /// <summary>
  /// Represents the tweet.
  /// </summary>
  [Serializable]
  public class Tweet
  {
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
    /// Gets or sets the gathered time.
    /// </summary>
    /// <value>
    /// The gathered time.
    /// </value>
    /// <remarks>
    /// Date-time when Komfo gathered the tweet. Normally this field would be equal to <see cref="CreatedTime"/>. But sometimes when there is 
    /// disruption in the gathering service - they might differ.
    /// </remarks>
    [JsonProperty("gathered_time")]
    public DateTime GatheredTime { get; set; }

    /// <summary>
    /// Gets or sets from.
    /// </summary>
    /// <value>
    /// From.
    /// </value>
    /// <remarks>
    /// Twitter handle of the user that made the tweet. This field is string without the "@" character.
    /// </remarks>
    public string From { get; set; }

    /// <summary>
    /// Gets or sets the tweet identifier.
    /// </summary>
    /// <value>
    /// The tweet identifier.
    /// </value>
    /// <remarks>
    /// ID of the tweet as provided by Twitter.
    /// </remarks>
    [JsonProperty("tw_id")]
    public string TwId { get; set; }

    /// <summary>
    /// Gets or sets the created time.
    /// </summary>
    /// <value>
    /// The created time.
    /// </value>
    /// <remarks>
    /// Date-time when the post was created in the social media.
    /// </remarks>
    [JsonProperty("created_time")]
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    /// <value>
    /// The text.
    /// </value>
    /// <remarks>
    /// The text of the tweet.
    /// </remarks>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the in reply to status identifier.
    /// </summary>
    /// <value>
    /// The in reply to status identifier.
    /// </value>
    /// <remarks>
    /// The ID of the parent tweet. This field could be empty if <see cref="TwId"/> is top level tweet.
    /// </remarks>
    [JsonProperty("in_reply_to_status_id")]
    public string InReplyToStatusId { get; set; }

    /// <summary>
    /// Gets or sets the in reply to screen name.
    /// </summary>
    /// <value>
    /// The in reply to screen name.
    /// </value>
    /// <remarks>
    /// If part of conversion - the parent user screen name. This field could be empty if <see cref="TwId"/> is top level tweet.
    /// This field is string without the "@" character.
    /// </remarks>
    [JsonProperty("in_reply_to_screen_name")]
    public string InReplyToScreenName { get; set; }
  }
}
