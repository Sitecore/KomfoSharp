// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRemoveUsersFromCustomAudienceRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the remove users from custom audience request builder.
  /// </summary>
  public interface IRemoveUsersFromCustomAudienceRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<IRemoveUsersFromCustomAudienceRequest>
  {
  }
}