// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Assert.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Diagnostics
{
  using System;

  /// <summary>
  /// Defines methods for performing assertions.
  /// </summary>
  public static class Assert
  {
    /// <summary>
    /// Asserts that argument is not null or empty.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <exception cref="System.ArgumentNullException">
    /// </exception>
    public static void ArgumentNotNullOrEmpty(string argument, string argumentName)
    {
      if (!string.IsNullOrEmpty(argument))
      {
        return;
      }

      if (argument == null)
      {
        if (argumentName != null)
        {
          throw new ArgumentNullException(argumentName, "Nulls are not allowed.");
        }

        throw new ArgumentNullException();
      }

      if (argumentName != null)
      {
        throw new ArgumentException("Empty strings are not allowed.", argumentName);
      }

      throw new ArgumentException("Empty strings are not allowed.");
    }

    /// <summary>
    /// Asserts that argument is not null.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <exception cref="System.ArgumentNullException">
    /// </exception>
    public static void ArgumentNotNull(object argument, string argumentName)
    {
      if (argument != null)
      {
        return;
      }

      if (argumentName != null)
      {
        throw new ArgumentNullException(argumentName);
      }

      throw new ArgumentNullException();
    }

    /// <summary>
    /// Arguments the condition.
    /// </summary>
    /// <param name="condition">if set to <c>true</c> [condition].</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message.</param>
    /// <exception cref="System.ArgumentException">
    /// </exception>
    public static void ArgumentCondition(bool condition, string argumentName, string message)
    {
      if (condition)
      {
        return;
      }

      if (argumentName != null)
      {
        throw new ArgumentException(message, argumentName);
      }

      throw new ArgumentException(message);
    }

    /// <summary>
    /// Asserts that <paramref name="target"/> is not null.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="message">The message.</param>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static void NotNull(object target, string message)
    {
      if (target != null)
      {
        return;
      }

      throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Asserts that <paramref name="target"/> is not null or empty string.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="message">The message.</param>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static void NotNullOrEmpty(string target, string message)
    {
      if (!string.IsNullOrEmpty(target))
      {
        return;
      }

      throw new InvalidOperationException(message);
    }
  }
}
