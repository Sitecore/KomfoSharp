// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRemoveCustomAudienceFromCampaignRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the remove custom audience from campaign request builder.
  /// </summary>
  public interface IRemoveCustomAudienceFromCampaignRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<IRemoveCustomAudienceFromCampaignRequest>
  {
  }
}