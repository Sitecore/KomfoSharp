// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the custom audience request builder.
  /// </summary>
  public class CustomAudienceRequestBuilder : BaseRequestBuilder, ICustomAudienceRequestBuilder, ICustomAudienceIdCalled, Fluent.IWithPollingCalled, IUsersCalled
  {
    /// <summary>
    /// The request
    /// </summary>
    private ICustomAudienceRequest request;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    public CustomAudienceRequestBuilder(IConfigurationProvider configurationProvider, string customAudienceId) : base(configurationProvider)
    {
      this.CustomAudienceId = customAudienceId;
    }

    /// <summary>
    /// Gets the custom audience identifier.
    /// </summary>
    /// <value>
    /// The custom audience identifier.
    /// </value>
    protected string CustomAudienceId { get; private set; }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected ICustomAudienceRequest Request
    {
      get
      {
        return this.request ?? (this.request = new CustomAudienceRequest(this.CustomAudienceId));
      }
    }

    /// <summary>
    /// Gets the custom audience status request builder.
    /// </summary>
    /// <value>
    /// The custom audience status request builder.
    /// </value>
    public IStatusCalled Status
    {
      get
      {
        return new CustomAudienceStatusRequestBuilder(this.ConfigurationProvider, this.CustomAudienceId);
      }
    }

    /// <summary>
    /// Gets the custom audience users request builder.
    /// </summary>
    /// <value>
    /// The custom audience users request builder.
    /// </value>
    public IUsersCalled Users
    {
      get
      {
        return this;
      }
    }

    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IDeleteCalled Delete()
    {
      return new CustomAudienceDeleteRequestBuilder(this.ConfigurationProvider, this.CustomAudienceId);
    }

    /// <summary>
    /// Enables the polling.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public Fluent.IWithPollingCalled WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null)
    {
      this.Request.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the custom audience request.
    /// </summary>
    /// <returns>
    /// The custom audience request.
    /// </returns>
    public ICustomAudienceRequest Create()
    {
      return this.Request;
    }

    /// <summary>
    /// Adds the users configuration to add to the request.
    /// </summary>
    /// <param name="entitySetupFunc">The entity setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    IAddCalled IAddCalling<IAddCalled, IUpdateUsersInCustomAudienceBuilder, UpdateUsersInCustomAudienceConfiguration>.Add(Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>> entitySetupFunc)
    {
      return new AddUsersToCustomAudienceRequestBuilder(this.ConfigurationProvider, this.CustomAudienceId, entitySetupFunc(new UpdateUsersInCustomAudienceBuilder()).Create());
    }

    /// <summary>
    /// Adds the users configuration to remove to the request.
    /// </summary>
    /// <param name="entitySetupFunc">The entity setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    IRemoveCalled IRemoveCalling<IRemoveCalled, IUpdateUsersInCustomAudienceBuilder, UpdateUsersInCustomAudienceConfiguration>.Remove(Func<IUpdateUsersInCustomAudienceBuilder, ICreateCalling<UpdateUsersInCustomAudienceConfiguration>> entitySetupFunc)
    {
      return new RemoveUsersFromCustomAudienceRequestBuilder(this.ConfigurationProvider, this.CustomAudienceId, entitySetupFunc(new UpdateUsersInCustomAudienceBuilder()).Create());
    }

    
  }
}