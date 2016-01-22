// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewCustomAudienceBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;

  /// <summary>
  /// Defines the new custom audience builder.
  /// </summary>
  public interface INewCustomAudienceBuilder : INameCalling<INameCalled>
  {
  }
}