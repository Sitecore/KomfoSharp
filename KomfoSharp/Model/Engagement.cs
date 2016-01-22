// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Engagement.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Model
{
  using System;

  /// <summary>
  /// Represents the engagement metric.
  /// </summary>
  [Serializable]
  public class Engagement
  {
    /// <summary>
    /// Gets or sets the score.
    /// </summary>
    /// <value>
    /// The score.
    /// </value>
    /// <remarks>
    /// The actual engagement score. Values: 1-10
    /// </remarks>
    public int Score { get; set; }

    /// <summary>
    /// Gets or sets the tweets.
    /// </summary>
    /// <value>
    /// The tweets.
    /// </value>
    /// <remarks>
    /// The number of tweets for the last 90 days.
    /// </remarks>
    public int Tweets { get; set; }

    /// <summary>
    /// Gets or sets the replies.
    /// </summary>
    /// <value>
    /// The replies.
    /// </value>
    /// <remarks>
    /// The number of replies for the last 90 days.
    /// </remarks>
    public int Replies { get; set; }
  }
}
