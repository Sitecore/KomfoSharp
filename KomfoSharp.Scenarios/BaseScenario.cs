// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseScenario.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Scenarios
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using KomfoSharp.Model;
  using Newtonsoft.Json;

  public abstract class BaseScenario
  {
    protected void ShowTweet(Tweet tweet)
    {
      this.Show("Tweet: {0}", tweet);
    }

    protected IEnumerable<Tweet> CreateFakeTweets(IEnumerable<string> ids, string channel)
    {
      var rand = new Random();

      return ids.Select(id => new Tweet
      {
        Channel = channel,
        RequestHandle = id,
        GatheredTime = DateTime.UtcNow.AddHours(rand.Next(-24, -1)),
        CreatedTime = DateTime.UtcNow.AddHours(rand.Next(-24, -1)),
        From = id,
        InReplyToScreenName = string.Empty,
        InReplyToStatusId = string.Empty,
        Text = string.Format("Take a look at @{0}! #{1}", channel, Guid.NewGuid().ToString()),
        TwId = Guid.NewGuid().ToString()
      });
    }

    protected void ShowMetric(Metric metric)
    {
      this.Show("Metric: {0}", metric);
    }

    protected IEnumerable<Metric> CreateFakeMetrics(IEnumerable<string> ids, string channel)
    {
      var rand = new Random();

      return ids.Select(id => new Metric
      {
        Channel = channel,
        RequestHandle = id,
        UpdatedTime = DateTime.UtcNow.AddHours(rand.Next(-5, -1)),
        Engagement =
        {
          Score = rand.Next(1, 10),
          Tweets = rand.Next(1, 150),
          Replies = rand.Next(1, 150)
        },
        Sentiment =
        {
          Score = (SentimentScoreValue)rand.Next(1, 3),
          Negative = rand.Next(1, 100),
          Neutral = rand.Next(1, 100),
          Positive = rand.Next(1, 100)
        }
      });
    }

    protected Token CreateFakeToken()
    {
      return new Token
      {
        AccessToken = Guid.NewGuid().ToString(),
        ExpiresIn = TimeSpan.FromDays(60.0)
      };
    }

    protected void ShowCustomAudience(CustomAudience customAudience)
    {
      this.Show("Custom Audience: {0}", customAudience);
    }

    protected void ShowCustomAudienceStatus(CustomAudienceStatus customAudienceStatus)
    {
      this.Show("Custom Audience Status: {0}", customAudienceStatus);
    }

    protected void ShowCampaign(Campaign campaign)
    {
      this.Show("Campaign: {0}", campaign);
    }

    protected void Show(string format, object value)
    {
      Console.WriteLine(format, JsonConvert.SerializeObject(value, Formatting.Indented));
    }
  }
}