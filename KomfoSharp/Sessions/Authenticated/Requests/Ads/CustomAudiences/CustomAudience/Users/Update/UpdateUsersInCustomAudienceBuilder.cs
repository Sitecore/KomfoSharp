// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateUsersInCustomAudienceBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update
{
  using System.Collections.Generic;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the update users in custom audience builder.
  /// </summary>
  public class UpdateUsersInCustomAudienceBuilder : IUpdateUsersInCustomAudienceBuilder, IEmailsCalled, IPhoneNumbersCalled, IFacebookIdsCalled, IFacebookApplicationsIdsCalled, IWithHashingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUsersInCustomAudienceBuilder"/> class.
    /// </summary>
    public UpdateUsersInCustomAudienceBuilder()
    {
      this.Configuration = new UpdateUsersInCustomAudienceConfiguration();
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    protected UpdateUsersInCustomAudienceConfiguration Configuration { get; private set; }

    /// <summary>
    /// Specifies the emails.
    /// </summary>
    /// <param name="emails">The emails.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IEmailsCalled Emails(IEnumerable<string> emails)
    {
      this.Configuration.Data = emails;
      this.Configuration.DataType = UpdateUsersInCustomAudienceDataType.Emails;
      return this;
    }

    /// <summary>
    /// Specifies the phone numbers.
    /// </summary>
    /// <param name="phoneNumbers">The phone numbers.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IPhoneNumbersCalled PhoneNumbers(IEnumerable<string> phoneNumbers)
    {
      this.Configuration.Data = phoneNumbers;
      this.Configuration.DataType = UpdateUsersInCustomAudienceDataType.PhoneNumbers;
      return this;
    }

    /// <summary>
    /// Specify the Facebook IDs.
    /// </summary>
    /// <param name="facebookIds">The Facebook IDs.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IFacebookIdsCalled FacebookIds(IEnumerable<string> facebookIds)
    {
      this.Configuration.Data = facebookIds;
      this.Configuration.DataType = UpdateUsersInCustomAudienceDataType.FacebookIds;
      return this;
    }

    /// <summary>
    /// Specifies the Facebook applications IDs.
    /// </summary>
    /// <param name="facebookApplicationsIds">The Facebook applications IDs.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    IFacebookApplicationsIdsCalled IFacebookApplicationsIdsCalling.FacebookApplicationsIds(IEnumerable<string> facebookApplicationsIds)
    {
      this.Configuration.FacebookApplicationsIds = facebookApplicationsIds;
      return this;
    }

    /// <summary>
    /// Enables the hashing.
    /// </summary>
    /// <param name="isHashedAlready">if set to <c>true</c>; the data is hashed already; otherwise, the data will be hashed.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    IWithHashingCalled IWithHashingCalling.WithHashing(bool isHashedAlready)
    {
      this.Configuration.WithHashing = true;
      this.Configuration.IsHashedAlready = isHashedAlready;
      return this;
    }

    /// <summary>
    /// Creates the update users in custom audience configuration.
    /// </summary>
    /// <returns>
    /// The update users in custom audience configuration.
    /// </returns>
    UpdateUsersInCustomAudienceConfiguration ICreateCalling<UpdateUsersInCustomAudienceConfiguration>.Create()
    {
      return this.Configuration;
    }
  }
}