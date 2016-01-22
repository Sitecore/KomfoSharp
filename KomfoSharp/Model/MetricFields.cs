// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricFields.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  /// Defines constants for <see cref="Metric"/> fields filter.
  /// </summary>
  [Flags]
  public enum MetricFields
  {
    /// <summary>
    /// The none of <see cref="Metric"/> fields.
    /// </summary>
    None = 0,

    /// <summary>
    /// The <see cref="Metric.Channel"/> field.
    /// </summary>
    [EnumMember(Value = "channel")]
    Channel = 1,

    /// <summary>
    /// The <see cref="Metric.RequestHandle"/> field.
    /// </summary>
    [EnumMember(Value = "request_handle")]
    RequestHandle = 2,

    /// <summary>
    /// The <see cref="Metric.UpdatedTime"/> field.
    /// </summary>
    [EnumMember(Value = "updated_time")]
    UpdatedTime = 4,

    /// <summary>
    /// The <see cref="Metric.Engagement"/> field.
    /// </summary>
    [EnumMember(Value = "engagement")]
    Engagement = 8,

    /// <summary>
    /// The <see cref="Metric.Sentiment"/> field.
    /// </summary>
    [EnumMember(Value = "sentiment")]
    Sentiment = 16,

    /// <summary>
    /// The all <see cref="Metric"/> fields.
    /// </summary>
    [EnumMember(Value = "channel, request_handle, updated_time, engagement, sentiment")]
    All = Channel + RequestHandle + UpdatedTime + Engagement + Sentiment
  }
}