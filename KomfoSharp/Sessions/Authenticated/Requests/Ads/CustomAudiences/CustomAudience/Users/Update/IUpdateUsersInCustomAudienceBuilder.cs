// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUpdateUsersInCustomAudienceBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Fluent;

  /// <summary>
  /// Defines the update users in custom audience builder.
  /// </summary>
  public interface IUpdateUsersInCustomAudienceBuilder : IEmailsCalling, IPhoneNumbersCalling, IFacebookIdsCalling
  {
  }
}