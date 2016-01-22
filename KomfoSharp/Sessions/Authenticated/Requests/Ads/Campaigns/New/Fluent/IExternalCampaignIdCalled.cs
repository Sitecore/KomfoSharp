// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExternalCampaignIdCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New.Fluent
{
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines interfaces to build request with the external campaign ID specified.
  /// </summary>
  public interface IExternalCampaignIdCalled : INameCalling<INameCalled>, IDescriptionCalling<IDescriptionCalled>, ICreateCalling<Campaign>
  {
  }
}