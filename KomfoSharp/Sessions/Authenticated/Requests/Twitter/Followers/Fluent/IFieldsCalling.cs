// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFieldsCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent
{
  /// <summary>
  /// Defines the call to specify fields.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  /// <typeparam name="TFields">The type of the fields.</typeparam>
  public interface IFieldsCalling<out T, in TFields>
  {
    /// <summary>
    /// Specifies the fields.
    /// </summary>
    /// <param name="fields">The fields.</param>
    /// <returns>The result of the call.</returns>
    T Fields(TFields fields);
  }
}