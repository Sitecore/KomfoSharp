// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserIdentificationType.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Provider
{
  using System;

  /// <summary>
  /// Defines equatable types of the custom audience user identification.
  /// </summary>
  public class CustomAudienceUserIdentificationType : IEquatable<CustomAudienceUserIdentificationType>
  {
    /// <summary>
    /// The identification by email
    /// </summary>
    private static readonly CustomAudienceUserIdentificationType emailType = new CustomAudienceUserIdentificationType("EMAIL");

    /// <summary>
    /// The SHA256 version of the identification by email
    /// </summary>
    private static readonly CustomAudienceUserIdentificationType emailSha256Type = new CustomAudienceUserIdentificationType("EMAIL_SHA256");

    /// <summary>
    /// The identification by phone
    /// </summary>
    private static readonly CustomAudienceUserIdentificationType phoneType = new CustomAudienceUserIdentificationType("PHONE");

    /// <summary>
    /// The SHA256 version of the identification by phone
    /// </summary>
    private static readonly CustomAudienceUserIdentificationType phoneSha256Type = new CustomAudienceUserIdentificationType("PHONE_SHA256");

    /// <summary>
    /// The identification by Facebook UID
    /// </summary>
    private static readonly CustomAudienceUserIdentificationType facebookUidType = new CustomAudienceUserIdentificationType("FB_UID");

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceUserIdentificationType"/> class.
    /// </summary>
    /// <param name="identificationType">Type of the identification.</param>
    public CustomAudienceUserIdentificationType(string identificationType)
    {
      this.IdentificationType = identificationType;
    }

    /// <summary>
    /// Gets the identification by email
    /// </summary>
    /// <value>
    /// The identification by email.
    /// </value>
    public static CustomAudienceUserIdentificationType Email
    {
      get
      {
        return emailType;
      }
    }

    /// <summary>
    /// Gets the SHA256 version of the identification by email
    /// </summary>
    /// <value>
    /// The SHA256 version of the identification by email.
    /// </value>
    public static CustomAudienceUserIdentificationType EmailSha256
    {
      get
      {
        return emailSha256Type;
      }
    }

    /// <summary>
    /// Gets the identification by phone
    /// </summary>
    /// <value>
    /// The identification by phone.
    /// </value>
    public static CustomAudienceUserIdentificationType Phone
    {
      get
      {
        return phoneType;
      }
    }

    /// <summary>
    /// Gets the SHA256 version of the identification by phone
    /// </summary>
    /// <value>
    /// The SHA256 version of the identification by phone
    /// </value>
    public static CustomAudienceUserIdentificationType PhoneSha256
    {
      get
      {
        return phoneSha256Type;
      }
    }

    /// <summary>
    /// Gets the identification by Facebook UID
    /// </summary>
    /// <value>
    /// The  identification by Facebook UID.
    /// </value>
    public static CustomAudienceUserIdentificationType FacebookUid
    {
      get
      {
        return facebookUidType;
      }
    }

    /// <summary>
    /// Gets the type of the identification.
    /// </summary>
    /// <value>
    /// The type of the identification.
    /// </value>
    public string IdentificationType { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="CustomAudienceUserIdentificationType"/> is SHA256 version.
    /// </summary>
    /// <value>
    ///   <c>true</c> if SHA256 version; otherwise, <c>false</c>.
    /// </value>
    public bool IsSha256Version
    {
      get
      {
        return this.IdentificationType.EndsWith("_SHA256", StringComparison.OrdinalIgnoreCase);
      }
    }

    /// <summary>
    /// The equality operator for comparing two <see cref="CustomAudienceUserIdentificationType"/> objects.
    /// </summary>
    /// 
    /// <returns>
    /// Returns <see cref="T:System.Boolean"/>.true if the specified <paramref name="left"/> and <paramref name="right"/> parameters are equal; otherwise, false.
    /// </returns>
    /// <param name="left">The left <see cref="CustomAudienceUserIdentificationType"/> to an equality operator.</param><param name="right">The right  <see cref="CustomAudienceUserIdentificationType"/> to an equality operator.</param>
    public static bool operator ==(CustomAudienceUserIdentificationType left, CustomAudienceUserIdentificationType right)
    {
      if ((left as object) == null)
      {
        return (right as object) == null;
      }

      return (right as object) != null && left.Equals(right);
    }

    /// <summary>
    /// The inequality operator for comparing two <see cref="CustomAudienceUserIdentificationType"/> objects.
    /// </summary>
    /// 
    /// <returns>
    /// Returns <see cref="T:System.Boolean"/>.true if the specified <paramref name="left"/> and <paramref name="right"/> parameters are in-equal; otherwise, false.
    /// </returns>
    /// <param name="left">The left <see cref="CustomAudienceUserIdentificationType"/> to an inequality operator.</param><param name="right">The right  <see cref="CustomAudienceUserIdentificationType"/> to an inequality operator.</param>
    public static bool operator !=(CustomAudienceUserIdentificationType left, CustomAudienceUserIdentificationType right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Determines whether the specified <see cref="CustomAudienceUserIdentificationType"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// 
    /// <returns>
    /// Returns <see cref="T:System.Boolean"/>.true if the specified object is equal to the current object; otherwise, false.
    /// </returns>
    /// <param name="other">The custom audience user identification type to compare with the current object.</param>
    public bool Equals(CustomAudienceUserIdentificationType other)
    {
      if (other == null)
        return false;
      if (object.ReferenceEquals((object)this.IdentificationType, (object)other.IdentificationType))
        return true;
      return string.Compare(this.IdentificationType, other.IdentificationType, StringComparison.OrdinalIgnoreCase) == 0;
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CustomAudienceUserIdentificationType);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode()
    {
      return this.IdentificationType.ToUpperInvariant().GetHashCode();
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return this.IdentificationType;
    }
  }
}