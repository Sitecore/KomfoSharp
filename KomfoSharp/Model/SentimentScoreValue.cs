// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SentimentScoreValue.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;

  /// <summary>
  /// Defines constants for <see cref="Sentiment.Score"/> values.
  /// </summary>
  [Serializable]
  public enum SentimentScoreValue
  {
    /// <summary>
    /// The positive value.
    /// </summary>
    Positive = 1,

    /// <summary>
    /// The negative value.
    /// </summary>
    Negative,

    /// <summary>
    /// The neutral value.
    /// </summary>
    Neutral
  }
}