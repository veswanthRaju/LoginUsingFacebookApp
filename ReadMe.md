# Settings we Should made in Facebook Developer before we get started

> First should login into [facebook developer](https://developers.facebook.com) with our facebook account.

> And then We should create one sample app in facebook developer app and it will generate one **unique id.**

> And then we should make our app to **Live** for this go to **app review** option on the left side and Switch on **make our app public**

> And then go to **Products** on the left side and click on **add products** and then go to that product and give redirect url as [here](http://www.facebook.com/connect/login_success.html)
 this will be used in our application

# Setup we should made in our application

> Add nuget package xamarin.auth

> And give app id and above redirect Url in **OAuth2Authenticator** constructor 

> And then give the Graph Uri based on the data we need to display. For those url's just follow [this link.](https://developers.facebook.com/docs/graph-api/using-graph-api/)

> [Reference](https://visualstudiomagazine.com/articles/2014/07/01/be-more-social.aspx)

> If we want to get more fields from facebook like email, birthday and anything just made setting in [this url](https://developers.facebook.com/tools/explorer/332063047134905?method=GET&path=me%3Ffields%3Did%2Cname&version=v2.7)

### For Twitter Just follow the [Link here](https://mobileprogrammerblog.wordpress.com/2016/01/23/xamarin-android-twitter-authentication/)
