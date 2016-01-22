// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignsCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Fluent
{
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;

  /// <summary>
  /// Defines interfaces to build campaigns requests.
  /// </summary>
  public interface ICampaignsCalled : ICampaignsRequestBuilder, INewCalling<INewCalled, INewCampaignBuilder, Campaign>, ICampaignIdCalling
  {
  }
}