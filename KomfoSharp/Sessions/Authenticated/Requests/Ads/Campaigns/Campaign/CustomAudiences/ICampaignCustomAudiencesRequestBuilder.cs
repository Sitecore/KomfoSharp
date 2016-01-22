// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignCustomAudiencesRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the campaign custom audiences request builder.
  /// </summary>
  public interface ICampaignCustomAudiencesRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<ICampaignCustomAudiencesRequest>
  {
  }
}