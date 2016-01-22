// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUpdateCustomAudiencesInCampaignBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;

  /// <summary>
  /// Defines the update custom audiences in campaign builder.
  /// </summary>
  public interface IUpdateCustomAudiencesInCampaignBuilder : ICustomAudienceIdCalling<ICustomAudienceIdCalled>
  {
  }
}