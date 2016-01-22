// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUntilCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream.Fluent
{
  using System;

  /// <summary>
  /// Defines the call to specify the until date.
  /// </summary>
  public interface IUntilCalling
  {
    /// <summary>
    /// Specifies the until date.
    /// </summary>
    /// <param name="until">The until date.</param>
    /// <returns>The result of the call.</returns>
    IUntilCalled Until(DateTime until);
  }
}