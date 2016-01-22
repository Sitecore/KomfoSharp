// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignsCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Fluent
{
  /// <summary>
  /// Specifies the call to campaigns requests.
  /// </summary>
  public interface ICampaignsCalling
  {
    /// <summary>
    /// Gets the campaigns requests builder.
    /// </summary>
    /// <value>
    /// The campaigns requests builder.
    /// </value>
    ICampaignsCalled Campaigns { get; }
  }
}