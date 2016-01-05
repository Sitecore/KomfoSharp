# KomfoSharp

KomfoSharp is a .NET wrapper for the Komfo API. Find [more](https://Sitecore.github.io/KomfoSharp/) about KomfoSharp API.

## Installation

To install KomfoSharp, in Visual Studio, on the Tools tab, click Library Package Manager (or NuGet Packet Manager) and then click Package Manager Console.

Enter the Install-Package command with the name of the package at the prompt:

    PM> Install-Package KomfoSharp

KomfoSharp depends on *Newtonsoft.Json*, therefore, to prevent *Newtonsoft.Json* from being overwritten, you need to include the *IgnoreDependencies* parameter in the command.

## Introduction

KomfoSharp is a Komfo API library that simplifies adding Komfo to your .NET desktop and web applications. You can build your own integration layer with Komfo utilizing the fluent API provided by KomfoSharp.

## Open Source Support

This project is open source software.

## Learn the Komfo API

Make sure you visit [https://developers.komfo.com](https://developers.komfo.com) to learn more about the Komfo API.

## Komfo API version

KomfoSharp supports Komfo API v1.

## Supported Platforms

- .NET 4.5

## Asynchronous approach

KomfoSharp provides the asynchronous API to make calls to Komfo, based on the [async/await](https://msdn.microsoft.com/en-us/library/hh191443.aspx) capabilities of C#.

## TDD ready

KomfoSharp API is TDD ready and can be mocked in your tests.

## Data format handling

KomfoSharp handles JSON serialization and deserialization. When you send requests:

- Pass the date and time in UTC.
- Pass Twitter handles without the *@* sign.

## Configuration

You must copy the configurations presented in the *KomfoSharp.dll.config* to the configuration file of your application (for example: *web.config*).

## Authentication

To access the Komfo API, you must first receive the access token. To receive the token, you need the *client_id* and *client_secret* which you can copy from the settings page of your Komfo account.

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .NonAuthenticated
  .Create())
{
    var tokensRequest = komfoSession.Requests.OAuth20.Tokens
      .ClientId("<your_client_id>")
      .ClientSecret("<your_client_secret>")
      .Scopes(TokenScopes.TwitterFollowers | TokenScopes.Advertising )
      .Create();

    var tokensResponse = await komfoSession.ExecuteAsync(tokensRequest);

    Console.WriteLine(
      "Access Token: {0}, expires in: {1} days.",
      tokensResponse.Data.AccessToken,
      tokensResponse.Data.ExpiresIn.TotalDays);
}
```

Use the access token that you received in all subsequent API calls. When the access token expires, make a new authentication call to get a fresh copy. You can enable automatic renewal of the token using the following session setup:

```csharp
var komfoSessions = new KomfoSessions();

using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .WithTokenRenewal()
  .ClientId("<your_client_id>")
  .ClientSecret("<your_client_secret>")
  .Scopes(TokenScopes.TwitterFollowers | TokenScopes.Advertising)
  .Create())
{
  // create requests inside the session
}
```

If the token is renewed, the fresh copy is available in `komfoSession.Configuration.Token`.

The Komfo API is divided into areas that you connect to separately. To get access to an area the appropriate token scope should be used:

- `TokenScopes.TwitterFollowers` - gives access to the Twitter followers area;
- `TokenScopes.Advertising` - gives access to the Advertising area;
- `TokenScopes.All` - gives access to the all areas above.

## Twitter followers

This API area provides access to Twitter data, as well as to Sentiment and Engagement scores of your active followers on Twitter.

### Retrieve metrics

To retrieve the Engagement and Sentiment scores for a set of Twitter handles:

```csharp
var komfoSessions = new KomfoSessions();

using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
    .TwitterHandles(new[] { "<twitter_handle1>", "<twitter_handle2>", "<twitter_handle3>" })
    .Create();

  var metricsResponse = await komfoSession.ExecuteAsync(metricsRequest);

  foreach (var metric in metricsResponse.Data)
  {
    this.ShowMetric(metric);
  }
}
```

You can specify a set of fields to retrieve:

```csharp
var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
  .TwitterHandles(new[] { "<twitter_handle1>", "<twitter_handle2>", "<twitter_handle3>" })
  .Fields(MetricFields.Channel | MetricFields.RequestHandle | MetricFields.Engagement)
  .Create();
```

### Retrieve stream

You can retrieve all the Twitter stories in a certain period for a set of Twitter handles as follows:

```csharp
var komfoSessions = new KomfoSessions();

using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var streamRequest = komfoSession.Requests.Twitter.Followers.Stream
    .TwitterHandles(new[] { "<twitter_handle1>", "<twitter_handle2>", "<twitter_handle3>" })
    .Create();

  var tweets = new List<Tweet>();
  do
  {
    var streamResponse = await komfoSession.ExecuteAsync(streamRequest);
    tweets.AddRange(streamResponse.Data);
    streamRequest = streamResponse.Next;
  }
  while (streamRequest!= null);

  foreach (var tweet in streamResponse.Data)
  {
    this.ShowTweet(tweet);
  }
}
```

The Komfo API has a limitation on the number of results per call. The KomfoSharp automatically prepares subsequent requests if necessary (`streamResponse.Next`). To achieve this, the `TweetFields.GatheredTime` field must be included in the results.

You can specify the fields to retrieve and the time period of the data to include in the response:

```csharp
var streamRequest = komfoSession.Requests.Twitter.Followers.Stream
  .TwitterHandles(new[] { "<twitter_handle1>", "<twitter_handle2>", "<twitter_handle3>" })
  .Fields(TweetFields.Channel | TweetFields.RequestHandle | TweetFields.Text )
  .Since(DateTime.UtcNow.AddMonths(-1))
  .Until(DateTime.UtcNow.AddHours(-1))
  .Create();
```

Please refer to the Komfo API documentation for the default values of parameters and limitations.

## Advertising

This API area provides access to Facebook advertising data, as well as Ad Accounts and Custom Audiences.

To use the advertising API the one must have Facebook Ad account.

### Custom Audiences

You can retrieve the custom audiences as follows:

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var customAudiencesRequest = komfoSession.Requests.Ads.CustomAudiences.Create();

  var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);

  foreach (var customAudience in customAudiencesResponse.Data)
  {
    this.ShowCustomAudience(customAudience);
  }
}
```

It's possible to create new empty custom audience:

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var newCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    .New(customAudience => customAudience
      .Name("Sportsmen")
      .Description("The people who like sport"))
    .Create();

  var newCustomAudienceResponse = await komfoSession.ExecuteAsync(newCustomAudienceRequest);

  this.Show("New custom audience ID: {0}", newCustomAudienceResponse.Data.CustomAudienceId);
}
```

And add users to it, providing either:

- Emails

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    .CustomAudienceId("<ID>")
    .Users
    .Add(users => users.Emails(new[] { "<email1>", "<email2>" }))
    .Create();

  var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);

  this.Show("Entries received: {0}", addUsersToCustomAudienceResponse.Data.EntriesReceived);
}
```

- Phone numbers

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    .CustomAudienceId("<ID>")
    .Users
    .Add(users => users.PhoneNumbers(new[] { "<phone1>", "<phone2>" }))
    .Create();

  var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);

  this.Show("Entries received: {0}", addUsersToCustomAudienceResponse.Data.EntriesReceived);
}
```

- Facebook IDs

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    .CustomAudienceId("<ID>")
    .Users
    .Add(users => users
      .FacebookIds(new[] { "<fb_ID1>", "<fb_ID1>" })
      .FacebookApplicationsIds(new[] { "<fb_App_ID1>" }))
    .Create();

  var addUsersToCustomAudienceResponse = await komfoSession.ExecuteAsync(addUsersToCustomAudienceRequest);

  this.Show("Entries received: {0}", addUsersToCustomAudienceResponse.Data.EntriesReceived);
}
```

The users can be removed from the custom audience in the same manner as they were added (using `komfoSession.Requests.Ads.CustomAudiences.CustomAudienceId("<ID>").Users.Remove`).

Please refer to the Komfo API documentation for the limitations on amount of users per request.

For better security, emails and phone numbers can be hashed with *SHA256* method:

```csharp
var addUsersToCustomAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
  .CustomAudienceId("55544")
  .Users
  .Add(users => users
    .Emails(new[] { "<email1>", "<email2>" })
    .WithHashing())
  .Create();
```

If the hashing is enabled, apply the following rules:

- Normalize email addresses by trimming leading and trailing whitespace and converting all characters to lowercase.
- Normalize phone numbers by removing any symbols, letters, and any leading zeroes.

To get the status, some statistics and error conditions for the custom audience:

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var customAudienceStatusRequest = komfoSession.Requests.Ads.CustomAudiences
    .CustomAudienceId("<ID>")
    .Status
    .Create();

  var customAudienceStatusResponse = await komfoSession.ExecuteAsync(customAudienceStatusRequest);

  this.ShowCustomAudienceStatus(customAudienceStatusResponse.Data);
}
```

The most important number that you can get from the custom audience status is approximate size - the correct number of Facebook recognized users in the audience. This number is provided by Facebook after it finishes processing and matching the list.

To retrieve the particular custom audience:

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var customAudienceRequest = komfoSession.Requests.Ads.CustomAudiences
    .CustomAudienceId("<ID>")
    .Create();

  var customAudienceResponse = await komfoSession.ExecuteAsync(customAudienceRequest);

  this.ShowCustomAudience(customAudienceResponse.Data);
}
```

If the custom audience is a part of a single or multiple campaigns – the campaign IDs will be presented in the result.

To delete the particular custom audiences:

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var customAudienceDeleteRequest = komfoSession.Requests.Ads.CustomAudiences
    .CustomAudienceId("<ID>")
    .Delete()
    .Create();

  var customAudienceDeleteResponse = await komfoSession.ExecuteAsync(customAudienceDeleteRequest);

  this.ShowCustomAudience(customAudienceDeleteResponse.Data);
}
```

**It's possible to delete only those custom audiences that are not in an active ad set.**

### Campaigns

To retrieve the campaigns:

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var campaignsRequest = komfoSession.Requests.Ads.Campaigns.Create();

  var campaignsResponse = await komfoSession.ExecuteAsync(campaignsRequest);

  foreach (var campaign in campaignsResponse.Data)
  {
    this.ShowCampaign(campaign);
  }
}
```

To create an empty campaign:

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var newCampaignRequest = komfoSession.Requests.Ads.Campaigns
    .New(campaign => campaign
      .ExternalCampaignKey("<key>")
      .ExternalCampaignId("<ID>")
      .Name("Summer 2015")
      .Description("The campaign to be started at summer, 2015"))
    .Create();

  var newCampaignResponse = await komfoSession.ExecuteAsync(newCampaignRequest);

  this.Show("New campaign ID: {0}", newCampaignResponse.Data.CampaignId);
}
```

To add a custom audience to an empty campaign (one custom audience per call):

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var addCustomAudienceToCampaignRequest = komfoSession.Requests.Ads.Campaigns
    .CampaignId("<ID>")
    .CustomAudiences
    .Add(customAudience => customAudience.CustomAudienceId("<ID>"))
    .Create();

  var addCustomAudienceToCampaignResponse = await komfoSession.ExecuteAsync(addCustomAudienceToCampaignRequest);

  this.Show("Added custom audience ID: {0}", addCustomAudienceToCampaignResponse.Data.CustomAudienceId);
}
```

Custom audiences can be removed from the campaign in the same way as they were added (using `komfoSession.Requests.Ads.Campaigns.CampaignId("<ID>").CustomAudiences.Remove`).

To retrieve all custom audiences that included in the specific campaign:

```csharp
var komfoSessions = new KomfoSessions();
using (var komfoSession = komfoSessions
  .Authenticated
  .Token(token)
  .Create())
{
  var customAudiencesRequest = komfoSession.Requests.Ads.Campaigns
    .CampaignId("<ID>")
    .CustomAudiences
    .Create();

  var customAudiencesResponse = await komfoSession.ExecuteAsync(customAudiencesRequest);

  foreach (var customAudience in customAudiencesResponse.Data)
  {
    this.ShowCustomAudience(customAudience);
  }
}
```

## Dealing with Komfo API Rate Limiting

Komfo limits the frequency of API calls per access token. After the limit is exceeded, the *HTTP 429* error is returned. KomfoSharp provides the way to poll Komfo in such cases:

```csharp
var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
  .TwitterHandles(new[] { "<twitter_handle1>", "<twitter_handle2>", "<twitter_handle3>" })
  .WithPolling()
  .Create();
```

KomfoSharp tries to resend the request several times at a certain interval. The default polling configuration is specified in the KomfoSharp configuration. You can override the default polling configuration:

```csharp
var metricsRequest = komfoSession.Requests.Twitter.Followers.Metrics
  .TwitterHandles(new[] { "<twitter_handle1>", "<twitter_handle2>", "<twitter_handle3>" })
  .WithPolling(polling => polling.Interval(TimeSpan.FromSeconds(15)).Attempts(4))
  .Create();
```

## Handling Komfo API Errors

When Komfo is not able to handle a request and returns an error, KomfoSharp captures it and raises an exception. To handle exceptions:

```csharp
try
{
  var metricsResponse = await komfoSession.ExecuteAsync(metricsRequest);
}
catch (KomfoProviderException ex)
{
  // analyze ex.KomfoStatusCode
}
```

The Komfo error code is available in the `KomfoStatusCode` property of the `KomfoProviderException` class. The possible codes are:

```csharp
public enum KomfoStatusCode
{
  SecureConnection = 1,
  InvalidApiCall = 2,
  InvalidHttpMethod = 3,
  MissingOrInvalidRequiredParameter = 4,
  InvalidAccessToken = 5,
  RateLimit = 6,
  InternalServerError = 7,
  InvalidClientCredentials = 50,
  MoreThan100TwitterHandles = 100,
  SinceGreaterThanUntil = 101,
  SinceAndUntilMustBeWithinTheLast30Days = 102,
  AtLeastOneInvalidValueIsPassedInIds = 103,
  ThereIsNoActiveAdAccountInClient = 200,
  FacebookGeneralError = 201,
  AdAccountAccessTokenNotValid = 202,
  MoreThan5000EmailsInGroup = 231,
  AudienceIdIsNotValidCustomAudience = 232,
  MissingFacebookApp = 233,
  CustomAudienceParticipateInActiveAdvertisements = 234,
  CampaignIdIsNotValidCampaign = 250,
  CustomAudienceIsAddedMultipleTimesToCampaign = 251,
  CustomAudienceDoesNotExistInCampaignButIsRequestedToBeRemoved = 252
}
```
