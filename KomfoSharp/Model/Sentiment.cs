// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sentiment.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;

  /// <summary>
  /// Represents the sentiment metric.
  /// </summary>
  [Serializable]
  public class Sentiment
  {
    /// <summary>
    /// Gets or sets the score.
    /// </summary>
    /// <value>
    /// The score.
    /// </value>
    /// <remarks>
    /// The actual sentiment score. Values: <see cref="SentimentScoreValue"/>.
    /// </remarks>
    public SentimentScoreValue Score { get; set; }

    /// <summary>
    /// Gets or sets the positive.
    /// </summary>
    /// <value>
    /// The positive.
    /// </value>
    /// <remarks>
    /// The number of positive tweets/direct messages.
    /// </remarks>
    public int Positive { get; set; }

    /// <summary>
    /// Gets or sets the negative.
    /// </summary>
    /// <value>
    /// The negative.
    /// </value>
    /// <remarks>
    /// The number of negative tweets/direct messages.
    /// </remarks>
    public int Negative { get; set; }

    /// <summary>
    /// Gets or sets the neutral.
    /// </summary>
    /// <value>
    /// The neutral.
    /// </value>
    /// <remarks>
    /// The number of neutral tweets/direct messages.
    /// </remarks>
    public int Neutral { get; set; }
  }
}
