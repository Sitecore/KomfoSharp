// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeleteCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Fluent
{
  /// <summary>
  /// Defines the call to remove an entity.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  public interface IDeleteCalling<out T>
  {
    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <returns>The result of the call.</returns>
    T Delete();
  }
}