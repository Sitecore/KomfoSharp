// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignsRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines interfaces to build the campaigns requests.
  /// </summary>
  public interface ICampaignsRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<ICampaignsRequest>
  {
     
  }
}