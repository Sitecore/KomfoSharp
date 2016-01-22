// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPhoneNumbersCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Fluent
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines the call to specify the phone numbers.
  /// </summary>
  public interface IPhoneNumbersCalling
  {
    /// <summary>
    /// Specifies the phone numbers.
    /// </summary>
    /// <param name="phoneNumbers">The phone numbers.</param>
    /// <returns>The result of the call.</returns>
    IPhoneNumbersCalled PhoneNumbers(IEnumerable<string> phoneNumbers);
  }
}