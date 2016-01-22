// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Fluent
{
  using System;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the call to add new entity.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  /// <typeparam name="TEntityBuilder">The type of the entity builder.</typeparam>
  /// <typeparam name="TEntity">The type of the entity.</typeparam>
  public interface INewCalling<out T, out TEntityBuilder, in TEntity>
  {
    /// <summary>
    /// Adds new entity.
    /// </summary>
    /// <param name="entitySetupFunc">The entity setup function.</param>
    /// <returns>The result of the call.</returns>
    T New(Func<TEntityBuilder, ICreateCalling<TEntity>> entitySetupFunc);

    /// <summary>
    /// Adds new entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The result of the call.</returns>
    T New(TEntity entity);
  }
}