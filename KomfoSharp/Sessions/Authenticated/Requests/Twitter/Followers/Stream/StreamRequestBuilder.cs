// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream
{
  using System;
  using System.Collections.Generic;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the methods that are used to build the stream request.
  /// </summary>
  public class StreamRequestBuilder : 
    BaseRequestBuilder,
    IStreamCalled,
    IStreamRequestBuilder, 
    ITwitterHandlesCalled, 
    IFieldsCalled, 
    ISinceCalled, 
    IUntilCalled, 
    IWithPollingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamRequestBuilder" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    public StreamRequestBuilder(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
      this.StreamRequest = new StreamRequest();
    }

    /// <summary>
    /// Gets the stream request.
    /// </summary>
    /// <value>
    /// The stream request.
    /// </value>
    protected StreamRequest StreamRequest { get; private set; }

    /// <summary>
    /// Specifies the Twitter handles in the current request.
    /// </summary>
    /// <param name="twitterHandles">The Twitter handles (without <c>@</c> character).</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    public ITwitterHandlesCalled TwitterHandles(IEnumerable<string> twitterHandles)
    {
      this.StreamRequest.Configuration.TwitterHandles = twitterHandles;
      return this;
    }

    /// <summary>
    /// Specifies the fields to be returned in the current request.
    /// </summary>
    /// <param name="fields">The tweet fields.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    IFieldsCalled IFieldsCalling<IFieldsCalled, TweetFields>.Fields(TweetFields fields)
    {
      this.StreamRequest.Configuration.Fields = fields;
      return this;
    }

    /// <summary>
    /// Specifies the since date-time in the current request.
    /// </summary>
    /// <param name="since">The since date-time.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    ISinceCalled ISinceCalling.Since(DateTime since)
    {
      this.StreamRequest.Configuration.Since = since;
      return this;
    }

    /// <summary>
    /// Specifies the until date-time in the current request.
    /// </summary>
    /// <param name="until">The until date-time.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    IUntilCalled IUntilCalling.Until(DateTime until)
    {
      this.StreamRequest.Configuration.Until = until;
      return this;
    }

    /// <summary>
    /// Enables the polling in the current request.
    /// </summary>
    /// <returns>
    /// The current request builder.
    /// </returns>
    IWithPollingCalled IWithPollingCalling<IWithPollingCalled>.WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc)
    {
      this.StreamRequest.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the request.
    /// </summary>
    /// <returns>The request.</returns>
    IStreamRequest ICreateCalling<IStreamRequest>.Create()
    {
      return this.StreamRequest;
    }
  }
}