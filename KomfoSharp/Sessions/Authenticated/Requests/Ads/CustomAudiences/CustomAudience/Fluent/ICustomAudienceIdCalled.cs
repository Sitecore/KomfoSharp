// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudienceIdCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Fluent
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;

  /// <summary>
  /// Defines interfaces to build request with the custom audience ID specified.
  /// </summary>
  public interface ICustomAudienceIdCalled : ICustomAudienceRequestBuilder, IStatusCalling, IUsersCalling, IDeleteCalling<IDeleteCalled>
  {
  }
}