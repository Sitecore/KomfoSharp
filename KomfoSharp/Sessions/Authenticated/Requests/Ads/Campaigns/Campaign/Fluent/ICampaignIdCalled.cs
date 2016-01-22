// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignIdCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.Fluent
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;

  /// <summary>
  /// Defines interfaces to build request with the campaign ID specified.
  /// </summary>
  public interface ICampaignIdCalled : ICustomAudiencesCalling<ICustomAudiencesCalled>
  {
  }
}