// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Provider.Extensions
{
  using System;
  using System.Globalization;
  using KomfoSharp.Diagnostics;

  /// <summary>
  /// Defines <see cref="DateTime"/> extensions.
  /// </summary>
  public static class DateTimeExtensions
  {
    /// <summary>
    /// The ISO format without milliseconds.
    /// </summary>
    public const string IsoWithoutMillisecondsFormat = "yyyy-MM-ddTHH:mm:ss.0Z";

    /// <summary>
    /// Converts the <paramref name="dateTime"/> to the UTC ISO format without milliseconds.
    /// </summary>
    /// <param name="dateTime">The date.</param>
    /// <returns>
    /// The <see cref="String"/> in the "yyyy-MM-ddTHH:mm:ss.0Z" format.
    /// </returns>
    public static string ToUtcIso(this DateTime dateTime)
    {
      Assert.ArgumentCondition(dateTime.Kind == DateTimeKind.Utc, "since", "DateTime should be in the UTC format.");

      return dateTime.ToString(IsoWithoutMillisecondsFormat, CultureInfo.InvariantCulture);
    }
  }
}
