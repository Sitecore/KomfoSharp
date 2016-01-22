// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudiencesCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Fluent
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;

  /// <summary>
  /// Defines interfaces to build the custom audiences requests.
  /// </summary>
  public interface ICustomAudiencesCalled : 
    ICampaignCustomAudiencesRequestBuilder, 
    IAddCalling<IAddCalled, IUpdateCustomAudiencesInCampaignBuilder, UpdateCustomAudiencesInCampaignConfiguration>, 
    IRemoveCalling<IRemoveCalled, IUpdateCustomAudiencesInCampaignBuilder, UpdateCustomAudiencesInCampaignConfiguration>
  {
  }
}