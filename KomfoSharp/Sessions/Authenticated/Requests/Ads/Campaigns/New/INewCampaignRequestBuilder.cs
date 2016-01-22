// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewCampaignRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines interfaces to build the new campaign request.
  /// </summary>
  public interface INewCampaignRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<INewCampaignRequest>
  {
  }
}