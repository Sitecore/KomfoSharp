// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCustomAudienceBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the new custom audience builder.
  /// </summary>
  public class NewCustomAudienceBuilder : INewCustomAudienceBuilder, INameCalled, IDescriptionCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCustomAudienceBuilder"/> class.
    /// </summary>
    public NewCustomAudienceBuilder()
    {
      this.CustomAudience = new CustomAudience();
    }

    /// <summary>
    /// Gets the custom audience.
    /// </summary>
    /// <value>
    /// The custom audience.
    /// </value>
    protected CustomAudience CustomAudience { get; private set; }

    /// <summary>
    /// Specifies the name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public INameCalled Name(string name)
    {
      this.CustomAudience.Name = name;
      return this;
    }

    /// <summary>
    /// Specifies the description.
    /// </summary>
    /// <param name="description">The description.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    IDescriptionCalled IDescriptionCalling<IDescriptionCalled>.Description(string description)
    {
      this.CustomAudience.Description = description;
      return this;
    }

    /// <summary>
    /// Creates the custom audience.
    /// </summary>
    /// <returns>
    /// The custom audience.
    /// </returns>
    CustomAudience ICreateCalling<CustomAudience>.Create()
    {
      return this.CustomAudience;
    }
  }
}