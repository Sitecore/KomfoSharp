// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudiencesCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.Fluent
{
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;

  /// <summary>
  /// Defines interfaces to build custom audiences requests.
  /// </summary>
  public interface ICustomAudiencesCalled :
    ICustomAudiencesRequestBuilder,
    INewCalling<INewCalled, INewCustomAudienceBuilder, CustomAudience>,
    ICustomAudienceIdCalling<ICustomAudienceIdCalled>
  {
  }
}