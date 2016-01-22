// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEmailsCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Fluent
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines the call to specify the emails.
  /// </summary>
  public interface IEmailsCalling
  {
    /// <summary>
    /// Specifies the emails.
    /// </summary>
    /// <param name="emails">The emails.</param>
    /// <returns>The result of the call.</returns>
    IEmailsCalled Emails(IEnumerable<string> emails);
  }
}