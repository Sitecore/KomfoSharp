// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsersCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Fluent
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;

  /// <summary>
  /// Defines interfaces to build custom audience users requests.
  /// </summary>
  public interface IUsersCalled : IAddCalling<IAddCalled, IUpdateUsersInCustomAudienceBuilder, UpdateUsersInCustomAudienceConfiguration>, IRemoveCalling<IRemoveCalled, IUpdateUsersInCustomAudienceBuilder, UpdateUsersInCustomAudienceConfiguration>
  {
  }
}