// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateUsersInCustomAudienceDataType.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update
{
  /// <summary>
  /// Defines the data types for the update users requests.
  /// </summary>
  public enum UpdateUsersInCustomAudienceDataType
  {
    /// <summary>
    /// The unknown data type
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The emails data type
    /// </summary>
    Emails,

    /// <summary>
    /// The phone numbers data type
    /// </summary>
    PhoneNumbers,

    /// <summary>
    /// The Facebook IDs data type
    /// </summary>
    FacebookIds
  }
}