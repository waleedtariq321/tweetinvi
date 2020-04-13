# Search

## User Search

<div class="iterator-available">

``` c#
var users = await client.Search.SearchUsers("tweetinvi");

// or

var usersIterator = client.Search.GetSearchUsersIterator("tweetinvi");
```
</div>

<div class="warning" style="padding-bottom: 1px">

User search paging of Twitter API does not behave the same as other Twitter API endpoint (including tweet search).
* First search will run on page 1 (and not 0) - do not change this value to 0.
* Pages can return the same items multiple times (tweetinvi will filter these for you).
* Tweetinvi will need to perform 1 additional request to detect the completion of the iterator.

<details>
<summary>Why?</summary>

* Twitter user search API always return results regardless of the page you request.
* Twitter user search API always return a number of items equal to count requested.
* Twitter can return multiple time the same results in different pages.

</details>

</div>


## Tweet Search

## Saved Searches