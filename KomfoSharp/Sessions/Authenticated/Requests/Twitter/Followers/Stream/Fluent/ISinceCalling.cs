// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISinceCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream.Fluent
{
  using System;

  /// <summary>
  /// Defines the call to specify the since date.
  /// </summary>
  public interface ISinceCalling
  {
    /// <summary>
    /// Specifies the since date.
    /// </summary>
    /// <param name="since">The since date.</param>
    /// <returns>The result of the call.</returns>
    ISinceCalled Since(DateTime since);
  }
}