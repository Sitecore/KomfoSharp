// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TweetFields.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  /// Defines constants for <see cref="Tweet"/> fields filter.
  /// </summary>
  [Flags]
  public enum TweetFields
  {
    /// <summary>
    /// The none of <see cref="Tweet"/> fields.
    /// </summary>
    None = 0,

    /// <summary>
    /// The <see cref="Tweet.Channel"/> field.
    /// </summary>
    [EnumMember(Value = "channel")]
    Channel = 1,

    /// <summary>
    /// The <see cref="Tweet.RequestHandle"/> field.
    /// </summary>
    [EnumMember(Value = "request_handle")]
    RequestHandle = 2,

    /// <summary>
    /// The <see cref="Tweet.GatheredTime"/> field.
    /// </summary>
    [EnumMember(Value = "gathered_time")]
    GatheredTime = 4,

    /// <summary>
    /// The <see cref="Tweet.From"/> field.
    /// </summary>
    [EnumMember(Value = "from")]
    From = 8,

    /// <summary>
    /// The <see cref="Tweet.TwId"/> field.
    /// </summary>
    [EnumMember(Value = "tw_id")]
    TwId = 16,

    /// <summary>
    /// The <see cref="Tweet.CreatedTime"/> field.
    /// </summary>
    [EnumMember(Value = "created_time")]
    CreatedTime = 32,

    /// <summary>
    /// The <see cref="Tweet.Text"/> field.
    /// </summary>
    [EnumMember(Value = "text")]
    Text = 64,

    /// <summary>
    /// The <see cref="Tweet.InReplyToStatusId"/> field.
    /// </summary>
    [EnumMember(Value = "in_reply_to_status_id")]
    InReplyToStatusId = 128,

    /// <summary>
    /// The <see cref="Tweet.InReplyToScreenName"/> field.
    /// </summary>
    [EnumMember(Value = "in_reply_to_screen_name")]
    InReplyToScreenName = 256,

    /// <summary>
    /// The all <see cref="Tweet"/> fields.
    /// </summary>
    [EnumMember(Value = "channel, request_handle, gathered_time, from, tw_id, created_time, text, in_reply_to_status_id, in_reply_to_screen_name")]
    All = Channel + RequestHandle + GatheredTime + From + TwId + CreatedTime + Text + InReplyToStatusId + InReplyToScreenName
  }
}