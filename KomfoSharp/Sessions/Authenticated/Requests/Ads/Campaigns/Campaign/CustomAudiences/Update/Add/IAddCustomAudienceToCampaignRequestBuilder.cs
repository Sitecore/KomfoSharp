// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddCustomAudienceToCampaignRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the add custom audiences to campaign request builder.
  /// </summary>
  public interface IAddCustomAudienceToCampaignRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<IAddCustomAudienceToCampaignRequest>
  {
     
  }
}