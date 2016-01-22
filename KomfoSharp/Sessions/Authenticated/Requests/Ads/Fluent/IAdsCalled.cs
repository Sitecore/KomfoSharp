// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAdsCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.Fluent;

  /// <summary>
  /// Defines interfaces to build ads requests.
  /// </summary>
  public interface IAdsCalled : ICustomAudiencesCalling<ICustomAudiencesCalled>, ICampaignsCalling
  {
  }
}